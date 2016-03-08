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
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 2000, 100);
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 250, 1650, 150);
            E = new Spell.Skillshot(SpellSlot.E, 450, SkillShotType.Linear);
            R = new Spell.Skillshot(SpellSlot.R, 1000, SkillShotType.Linear, 250, 2100, 100);
            R2 = new Spell.Skillshot(SpellSlot.R, 1700, SkillShotType.Cone, 250, 2100, 120);

            Youmu = new Item((int)ItemId.Youmuus_Ghostblade);

            DamageIndicator.Initialize(GetTotalDamage);
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        public static float GetTotalDamage(Obj_AI_Base T)
        {
            float Damage = 0f;

            if (Q.IsReady())
            {
                Damage += QDMG(T);
            }

            if (W.IsReady())
            {
                Damage += WDMG(T);
            }

            if (R.IsReady())
            {
                Damage += RDMG(T);
            }

            return Damage;
        }

        public static float QDMG(Obj_AI_Base T)
        {
            return Player.Instance.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 55, 70, 85, 100, 115 }[Q.Level] + 0.75f * Player.Instance.TotalAttackDamage);
        }

        public static float WDMG(Obj_AI_Base T)
        {
            return Player.Instance.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 60, 110, 160, 210, 260 }[W.Level] + 0.6f * Player.Instance.TotalMagicalDamage);
        }

        public static float RDMG(Obj_AI_Base T)
        {
            return Player.Instance.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 250, 400, 550 }[R.Level] + 1.5f * Player.Instance.TotalAttackDamage);
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsMe)
                return;

            if (sender.IsDead && sender.IsZombie)
                return;

            if(e.Start == e.End)
            {
                var WPred = W.GetPrediction(sender);

                if(WPred.HitChance >= HitChance.High)
                {
                    W.Cast(WPred.UnitPosition);
                }
            }

            if(e.End == Player.Instance.Position)
            {
                var PosX = Player.Instance.Position.X / 2;
                var PosY = Player.Instance.Position.Y;
                var PosZ = Player.Instance.Position.Z;

                Vector3 Loc = new Vector3(PosX, PosY, PosZ);

                E.Cast((Game.CursorPos.Distance(Player.Instance) > E.Range ? Player.Instance.Position.Extend(Loc, E.Range - 1).To3D() : Loc));
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (W.IsReady())
            {
                var WPred = W.GetPrediction(sender);

                if (WPred.HitChance >= HitChance.Medium)
                {
                    W.Cast(WPred.UnitPosition);
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
                var TargetR = TargetSelector.GetTarget(R.Range, DamageType.Physical);

                if (TargetR == null)
                    return;

                if (TargetR.IsValidTarget(R.Range))
                {
                    if(TargetR.Health <= RDMG(TargetR))
                    {
                        var RPred = R.GetPrediction(TargetR);

                        if(RPred.HitChance >= HitChance.High)
                        {
                            R.Cast(RPred.UnitPosition);
                        }
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var TargetQ = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                var TargetW = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                var TargetE = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                var TargetR = TargetSelector.GetTarget(R.Range, DamageType.Physical);
                var TargetR2 = TargetSelector.GetTarget(R2.Range, DamageType.Physical);

                if(GravesMenu.CheckBox(GravesMenu.Combo, "UseYoumu") && GravesMenu.Slider(GravesMenu.Combo, "UseYoumuX") >= TargetQ.HealthPercent)
                {
                    if (TargetQ == null)
                        return;

                    if(Youmu.IsOwned() && Youmu.IsReady())
                    {
                        Youmu.Cast();
                    }
                }

                if (Q.IsReady())
                {
                    if (TargetQ == null)
                        return;

                    if (TargetQ.IsValidTarget(Q.Range))
                    {
                        var Check = TargetQ.BoundingRadius + TargetQ.Position.Extend(Player.Instance.Position, Q.Range);
                        var QPred = Q.GetPrediction(TargetQ);

                        if (Check.IsWall())
                        {
                            if (QPred.HitChance >= HitChance.High)
                            {
                                Q.Cast(QPred.UnitPosition);
                            }
                        }
                        else
                        {
                            if (QPred.HitChance >= HitChance.Medium)
                            {
                                Q.Cast(QPred.UnitPosition);
                            }
                        }
                    }
                }

                if (W.IsReady())
                {
                    if (TargetW == null)
                        return;

                    if (TargetW.IsValidTarget(W.Range))
                    {
                        var WPred = W.GetPrediction(TargetW);

                        if (WPred.HitChance >= HitChance.High)
                        {
                            W.Cast(WPred.CastPosition);
                        }
                    }
                }

                if (E.IsReady())
                {
                    if (TargetE == null)
                        return;

                    if (TargetE.IsValidTarget(E.Range + Player.Instance.GetAutoAttackRange(TargetE)))
                    {
                        if(GravesMenu.CheckBox(GravesMenu.Combo, "UseEComboMouse"))
                        {
                            E.Cast((Game.CursorPos.Distance(Player.Instance) > E.Range ? Player.Instance.Position.Extend(Game.CursorPos, E.Range - 1).To3D() : Game.CursorPos));
                        }
                        else if (GravesMenu.CheckBox(GravesMenu.Combo, "UseECombo"))
                        {
                            E.Cast(TargetE.Position);
                        }
                        else
                        {
                            E.Cast((Game.CursorPos.Distance(Player.Instance) > E.Range ? Player.Instance.Position.Extend(Game.CursorPos, E.Range - 1).To3D() : Game.CursorPos));
                        }
                    }
                }

                if (R.IsReady())
                {
                    if (TargetR == null)
                        return;

                    if (TargetR.IsValidTarget(R.Range))
                    {
                        if (TargetR.Health < RDMG(TargetR))
                        {
                            var RPred = R.GetPrediction(TargetR);

                            if (RPred.HitChance >= HitChance.High)
                            {
                                R.Cast(RPred.UnitPosition);
                            }
                        }
                    }
                    else
                    {
                        if (TargetR2.IsValidTarget(R.Range))
                        {
                            if (TargetR2.Health >= Player.Instance.GetSpellDamage(TargetR2, SpellSlot.R))
                            {
                                var RPred = R2.GetPrediction(TargetR2);

                                if (RPred.HitChance >= HitChance.High)
                                {
                                    R2.Cast(TargetR2);
                                }
                            }
                        }
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.Minions.Where(x => x.IsValidTarget(Q.Range) && !x.IsDead && x.IsEnemy);
                var Minions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(Minion, Q.Width, (int) Q.Range);

                if (Minion == null)
                    return;

                if(Q.IsReady() && GravesMenu.CheckBox(GravesMenu.Laneclear, "UseQLane"))
                {
                    if (Minions.HitNumber >= GravesMenu.Slider(GravesMenu.Laneclear, "UseQLaneMin"))
                    {
                        Q.Cast(Minions.CastPosition);
                    }
                }

                if(E.IsReady() && GravesMenu.CheckBox(GravesMenu.Laneclear, "UseELane"))
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

