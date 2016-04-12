using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Lolli_Poppy
{
    class Poppy
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Targeted E;
        public static Spell.Chargeable R;
        public static AIHeroClient TargetUlt;

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 430, SkillShotType.Linear, 250, null, 100);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Active(SpellSlot.W, 400);
            E = new Spell.Targeted(SpellSlot.E, 425);
            R = new Spell.Chargeable(SpellSlot.R, 500, 1200, 4000, 250, int.MaxValue, 90);

            Game.OnTick += Game_OnTick;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            DamageIndicator.Initialize(GetTotalDamage);
        }

        public static float GetTotalDamage(AIHeroClient T)
        {
            float Damage = 0f;

            if (Q.IsReady())
            {
                Damage += QDMG(T);
            }

            if (E.IsReady())
            {
                Damage += EDMG(T);
            }

            if (R.IsReady())
            {
                Damage += RDMG(T);
            }

            return Damage;
        }

        public static float QDMG(AIHeroClient T)
        {
            return Player.Instance.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 35, 55, 75, 95, 115 }[Q.Level] + 0.80f * Player.Instance.TotalAttackDamage + 0.07f * T.MaxHealth);
        }

        public static float EDMG(AIHeroClient T)
        {
            return Player.Instance.CalculateDamageOnUnit(T, DamageType.Magical, new float[] { 50, 70, 90, 110, 130 }[E.Level] + 0.5f * Player.Instance.TotalMagicalDamage);
        }

        public static float RDMG(AIHeroClient T)
        {
            return Player.Instance.CalculateDamageOnUnit(T, DamageType.Physical, new float[] { 200, 300, 400 }[R.Level] + 0.9f * Player.Instance.TotalAttackDamage);
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe && sender.IsDead && sender.IsAlly && sender == null)
                return;

            if (Player.Instance.IsInRange(Player.Instance, args.SData.CastRange))
            {
                if (W.IsReady())
                {
                    var Enemy = (AIHeroClient)sender;

                    foreach (var Check in SpellsDatabase.Spells)
                    {
                        if (Enemy.Hero == Check.Champion)
                        {
                            if (args.SData.Name == Check.SpellName)
                            {
                                if (PoppyMenu.CheckBox(PoppyMenu.Spells, Check.Name + "/" + Check.SpellSlots.ToString()))
                                {
                                    W.Cast();
                                }
                            }
                            else
                            {
                                if (args.Slot == Check.SpellSlots)
                                {
                                    if (PoppyMenu.CheckBox(PoppyMenu.Spells, Check.Name + "/" + Check.SpellSlots.ToString()))
                                    {
                                        W.Cast();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsMe && sender.IsDead && sender.IsAlly && sender == null)
                return;

            if (PoppyMenu.CheckBox(PoppyMenu.Misc, "Interrupter"))
            {
                if (e.DangerLevel == DangerLevel.High)
                {
                    if (E.IsReady())
                    {
                        if(sender.IsValidTarget(E.Range))
                        {
                            if(CheckWall(sender))
                            {
                                E.Cast(sender);
                            }
                        }
                    }
 
                    if (R.IsReady())
                    {
                        var RPred = R.GetPrediction(sender);

                        if (sender.IsValidTarget(R.MinimumRange) && RPred.HitChance >= HitChance.High)
                        {
                            if (R.StartCharging())
                            {
                                Player.Instance.Spellbook.UpdateChargeableSpell(SpellSlot.R, RPred.UnitPosition, true);
                            }
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsMe && sender.IsDead && sender.IsAlly && sender == null)
                return;

            if (!PoppyMenu.CheckBox(PoppyMenu.Misc, "Gapcloser"))
                return;

            if(sender.IsValidTarget(E.Range))
            {
                if (CheckWall(sender))
                {
                    E.Cast(sender);
                }
            }
        }

        private static bool CheckWall(Obj_AI_Base t)
        {
            var Distance = t.BoundingRadius + t.ServerPosition.Extend(Player.Instance.ServerPosition, -355);

            if(Distance.IsWall())
            {
                return true;
            }

            return false;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if(PoppyMenu.Keybind(PoppyMenu.Combo, "UseRComboKey"))
            {
                if (TargetUlt == null)
                    return;

                if (!R.IsCharging && R.IsReady() && TargetUlt.IsValidTarget(R.MaximumRange))
                {
                    R.StartCharging();
                }
                else if(R.IsReady() && R.Range == R.MaximumRange)
                {
                    var RPred = R.GetPrediction(TargetUlt);

                    if(RPred.HitChance >= HitChance.High)
                    {
                        R.Cast(RPred.UnitPosition);
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var Target = TargetSelector.GetTarget(R.Range, DamageType.Physical);

                if(Target != null)
                {
                    if (R.IsReady() &&  PoppyMenu.CheckBox(PoppyMenu.Combo, "UseRCombo"))
                    {
                        var RPred = R.GetPrediction(Target);

                        if (RPred.HitChance >= HitChance.High)
                        {
                            if (R.StartCharging())
                            {
                                Player.Instance.Spellbook.UpdateChargeableSpell(SpellSlot.R, RPred.UnitPosition, true);
                            }
                        }
                    }


                    if (Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Combo, "UseQCombo"))
                    {
                        var QPred = Q.GetPrediction(Target);

                        if(Target.IsValidTarget(Q.Range))
                        {
                            if (QPred.HitChance >= HitChance.High)
                            {
                                Q.Cast(QPred.UnitPosition);
                            }
                        }
                    }

                    if (E.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Combo, "UseECombo"))
                    {
                        if (Target.IsValidTarget(E.Range))
                        {
                            if (CheckWall(Target))
                            {
                                E.Cast(Target);
                            }
                        }
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => !x.IsDead || x.IsValidTarget(Q.Range) || x.Health <= 0);
                var Minions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(Minion, Q.Width, (int) Q.Range);

                if (Minion != null)
                {
                    if (Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Laneclear, "UseQLane"))
                    {
                        if (Minions.HitNumber >= PoppyMenu.Slider(PoppyMenu.Laneclear, "UseQLaneMin"))
                        {
                            Q.Cast(Minions.CastPosition);
                        }
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                var Monster = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(E.Range + 300));

                if (Monster == null)
                    return;

                if(Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Jungleclear, "UseQJG"))
                {
                    if(Monster.IsValidTarget(Q.Range))
                    {
                        Q.Cast(Monster);
                    }
                }

                if(E.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Jungleclear, "UseEJG"))
                {
                    E.Cast(Monster);
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                if(W.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Flee, "UseWFlee"))
                {
                    W.Cast();
                }

                if(E.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Flee, "UseEFlee"))
                {
                    var Minion = ObjectManager.Get<Obj_AI_Minion>().Where(x => Player.Instance.ServerPosition.Extend(x, Player.Instance.Distance(x) + 300).Distance(Game.CursorPos) < Player.Instance.Distance(Game.CursorPos) || x.IsValidTarget(E.Range)).OrderBy(x => x.Distance(Game.CursorPos));

                    if(E.IsInRange(Minion.First()))
                    {
                        E.Cast(Minion.First());
                    }
                }
            }

            if(PoppyMenu.CheckBox(PoppyMenu.Misc, "Skin"))
            {
                Player.SetSkinId(PoppyMenu.Slider(PoppyMenu.Misc, "SkinID"));
            }
        }
    }
}
