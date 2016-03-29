using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Pool_Party_Graves
{
    class Graves
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R, R2;
        public static Item Youmu;

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 2100, 100);
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 250, 1500, 120);
            E = new Spell.Skillshot(SpellSlot.E, 450, SkillShotType.Linear);
            R = new Spell.Skillshot(SpellSlot.R, 1000, SkillShotType.Linear, 250, 2100, 100);
            R2 = new Spell.Skillshot(SpellSlot.R, 1700, SkillShotType.Cone, 250, 2100, 100);

            Youmu = new Item((int)ItemId.Youmuus_Ghostblade);

            DamageIndicator.Initialize(DamageCombo);
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static float QDMG(AIHeroClient Target, bool Wall)
        {
            if (Target == null)
                return 0f;

            if(!Wall)
            {
                return Player.Instance.CalculateDamageOnUnit(Target, DamageType.Physical, new float[] { 55, 70, 85, 100, 115 }[Q.Level - 1] + 0.75f * Player.Instance.FlatPhysicalDamageMod);
            }

            return Player.Instance.CalculateDamageOnUnit(Target, DamageType.Physical, new float[] { 80, 125, 170, 215, 260 }[Q.Level - 1] + new float[] { 0.4f, 0.6f, 0.8f, 1f, 1.2f }[Q.Level - 1] * Player.Instance.FlatPhysicalDamageMod);
        }

        private static float RDMG(AIHeroClient Target)
        {
            if (Target == null)
                return 0f;

            return Player.Instance.CalculateDamageOnUnit(Target, DamageType.Physical, new float[] { 250, 400, 550 }[R.Level - 1] + 1.5f * Player.Instance.FlatPhysicalDamageMod); 
        }        

        private static float DamageCombo(AIHeroClient Target)
        {
            if (Target == null)
                return 0f;

            float Damage = 0f;

            if(Q.IsReady())
            {
                Damage += QDMG(Target, false);
            }

            if(R.IsReady())
            {
                Damage += RDMG(Target);
            }

            return Damage;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            if (!GravesMenu.CheckBox(GravesMenu.Misc, "Gapcloser"))
                return;

            var EPos = Player.Instance.ServerPosition + (Player.Instance.ServerPosition - sender.ServerPosition).Normalized() * 300;

            if (W.IsReady())
            {
                if(Player.Instance.Position.Distance(e.End) <= W.Range)
                {
                    W.Cast(e.End);
                }
            }

            if (e.End == Player.Instance.Position)
            {
                if(E.IsReady())
                {
                    E.Cast(EPos);
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;
            
            if (W.IsReady())
            {
                var WPred = W.GetPrediction(sender);

                if (WPred.HitChance >= HitChance.High)
                {
                    if(sender.IsValidTarget(W.Range))
                    {
                        W.Cast(WPred.UnitPosition);
                    }
                }
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (GravesMenu.CheckBox(GravesMenu.Misc, "Skin"))
            {
                Player.SetSkinId(GravesMenu.Slider(GravesMenu.Misc, "SkinID"));
            }

            if (Player.Instance.IsDead)
                return;

            if(GravesMenu.CheckBox(GravesMenu.Misc, "UseRKs"))
            {
                var Target = TargetSelector.GetTarget(R.Range, DamageType.Physical);

                if (Target == null)
                    return;

                if (Target.IsValidTarget(R.Range))
                {
                    if(Target.Health <= RDMG(Target))
                    {
                        var RPred = R.GetPrediction(Target);

                        if(RPred.HitChance >= HitChance.High)
                        {
                            R.Cast(RPred.UnitPosition);
                        }
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var Target = TargetSelector.GetTarget(R2.Range, DamageType.Physical);

                if (Target == null)
                    return;

                if(GravesMenu.CheckBox(GravesMenu.Combo, "UseYoumu") && GravesMenu.Slider(GravesMenu.Combo, "UseYoumuX") >= Target.HealthPercent)
                {
                    if(Youmu.IsOwned() || Youmu.IsReady())
                    {
                        Youmu.Cast();
                    }
                }


                if (W.IsReady() || GravesMenu.CheckBox(GravesMenu.Combo, "UseWCombo"))
                {
                    if (Target.IsValidTarget(W.Range))
                    {
                        var WPred = W.GetPrediction(Target);

                        if (WPred.HitChance >= HitChance.High)
                        {
                            W.Cast(WPred.CastPosition);
                        }
                    }
                }

                if (Q.IsReady() || GravesMenu.CheckBox(GravesMenu.Combo, "UseQCombo"))
                {
                    if (Target.IsValidTarget(Q.Range))
                    {
                        var QPred = Q.GetPrediction(Target);

                        for (int i = 0; i < 20; i++)
                        {
                            var Flags = NavMesh.GetCollisionFlags(QPred.UnitPosition);

                            if(Flags.HasFlag(CollisionFlags.Wall))
                            {
                                Q.Cast(QPred.UnitPosition);
                            }
                            else
                            {
                                if(QPred.HitChance >= HitChance.Medium)
                                {
                                    Q.Cast(QPred.UnitPosition);
                                }
                            }
                        }
                    }
                }

                if (E.IsReady() || GravesMenu.CheckBox(GravesMenu.Combo, "UseECombo"))
                {
                    if (Target.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange(Target)))
                    {
                        if(GravesMenu.CheckBox(GravesMenu.Combo, "UseEComboMouse"))
                        {
                            E.Cast((Game.CursorPos.Distance(Player.Instance) > E.Range ? Player.Instance.Position.Extend(Game.CursorPos, E.Range - 1).To3D() : Game.CursorPos));
                        }
                        else if (GravesMenu.CheckBox(GravesMenu.Combo, "UseECombo"))
                        {
                            E.Cast(Target.Position);
                        }
                    }
                }

                if (R.IsReady() || GravesMenu.CheckBox(GravesMenu.Combo, "UseRCombo"))
                {
                    if (Target.IsValidTarget(R.Range))
                    {
                        if (Target.Health < RDMG(Target))
                        {
                            var RPred = R.GetPrediction(Target);

                            if (RPred.HitChance >= HitChance.High)
                            {
                                R.Cast(RPred.UnitPosition);
                            }
                        }
                    }
                    else
                    {
                        if (Target.IsValidTarget(R2.Range))
                        {
                            if (Target.Health >= RDMG(Target))
                            {
                                var RPred = R2.GetPrediction(Target);

                                if (RPred.HitChance >= HitChance.High)
                                {
                                    R2.Cast(RPred.UnitPosition);
                                }
                            }
                        }
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.IsValid|| !x.IsDead);
                var Minions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(Minion, Q.Width, (int) Q.Range);

                if (Minion == null)
                    return;

                if(Q.IsReady() || GravesMenu.CheckBox(GravesMenu.Laneclear, "UseQLane"))
                {
                    if (Minions.HitNumber >= GravesMenu.Slider(GravesMenu.Laneclear, "UseQLaneMin"))
                    {
                        Q.Cast(Minions.CastPosition);
                    }
                }

                if(E.IsReady() || GravesMenu.CheckBox(GravesMenu.Laneclear, "UseELane"))
                {
                    if(!Player.Instance.HasBuff("gravesbasicattackammo2"))
                    {
                        E.Cast(Minions.CastPosition);
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(Q.Range));

                if (Minion == null)
                    return;

                if (Q.IsReady())
                {
                    Q.Cast(Minion);
                }

                if (E.IsReady())
                {
                    if (!Player.Instance.HasBuff("gravesbasicattackammo2"))
                    {
                        E.Cast(Minion);
                    }
                }
            }


            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                var T = TargetSelector.GetTarget(W.Range, DamageType.Physical);

                if (W.IsReady())
                {
                    var WPred = W.GetPrediction(T);

                    if (WPred.HitChance >= HitChance.High)
                    {
                        W.Cast(WPred.UnitPosition);
                    }
                }

                if (E.IsReady())
                {
                    E.Cast((Game.CursorPos.Distance(Player.Instance) > E.Range ? Player.Instance.Position.Extend(Game.CursorPos, E.Range - 1).To3D() : Game.CursorPos));
                }
            }
        }
    }
}

