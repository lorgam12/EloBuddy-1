using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;

namespace Lolli_Poppy
{
    class Poppy
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Targeted E;
        public static Spell.Chargeable R;

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 430, SkillShotType.Linear, 250, null, 100);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Active(SpellSlot.W, 400);
            E = new Spell.Targeted(SpellSlot.E, 525);
            R = new Spell.Chargeable(SpellSlot.R, 500, 1200, 4000, 250, int.MaxValue, 90);

            Game.OnTick += Game_OnTick;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsEnemy && !sender.IsDead && !sender.IsZombie && sender == null)
                return;

            if(W.IsReady() && (W.IsInRange(args.End) || W.IsInRange(args.Start)))
            {
                var Enemy = (AIHeroClient)sender;

                foreach (var Check in SpellsDatabase.Spells)
                {
                    if(Enemy.Hero == Check.Champion)
                    {
                        if(args.SData.Name == Check.SpellName)
                        {
                            if (PoppyMenu.CheckBox(PoppyMenu.Spells, Check.Name + "/" + Check.SpellSlots.ToString()))
                            {
                                W.Cast();
                            }
                        }else
                        {
                            if(args.Slot == Check.SpellSlots)
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

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!sender.IsEnemy && !sender.IsDead && !sender.IsZombie && sender == null)
                return;

            if (PoppyMenu.CheckBox(PoppyMenu.Misc, "Interrupter"))
            {
                if (E.IsReady() && sender.IsValidTarget(E.Range))
                {
                    E.Cast(sender);
                }
            }

            if (PoppyMenu.CheckBox(PoppyMenu.Beta, "Beta") && PoppyMenu.CheckBox(PoppyMenu.Beta, "BetaInterrupter"))
            {
                if (e.DangerLevel == DangerLevel.High)
                {
                    if (sender.IsValidTarget(R.MaximumRange))
                    {
                        if (R.StartCharging())
                        {
                            var RPred = R.GetPrediction(sender);

                            if (RPred.HitChance >= HitChance.High)
                            {
                                Player.Instance.Spellbook.UpdateChargeableSpell(SpellSlot.R, RPred.UnitPosition, false);
                            }
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy && !sender.IsDead && !sender.IsZombie && sender == null)
                return;

            var Position = sender.BoundingRadius + sender.ServerPosition.Extend(ObjectManager.Player.ServerPosition, -360);

            if (Position.IsWall())
            {
                E.Cast(sender);
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

            if (PoppyMenu.CheckBox(PoppyMenu.Beta, "Beta"))
            {
                if(PoppyMenu.Keybind(PoppyMenu.Beta, "BetaRComboKey"))
                {
                    var T = TargetSelector.GetTarget(R.MinimumRange, DamageType.Mixed);

                    if (T == null && T.IsDead && T.IsZombie)
                        return;

                    if(T.IsValidTarget(R.MinimumRange))
                    {
                        if(R.StartCharging())
                        {   
                            var RPred = R.GetPrediction(T);

                            if(RPred.HitChance >= HitChance.High)
                            {
                                Player.Instance.Spellbook.UpdateChargeableSpell(SpellSlot.R, T.ServerPosition, true);
                            }
                        }
                    }
                }

                if(PoppyMenu.CheckBox(PoppyMenu.Beta, "AutoRCombo") && R.IsReady() && Player.Instance.CountEnemiesInRange(R.Range) >= PoppyMenu.Slider(PoppyMenu.Beta, "AutoRComboEnemy"))
                {
                    if (Player.Instance.HealthPercent >= PoppyMenu.Slider(PoppyMenu.Beta, "AutoRComboHealth"))
                    {
                        var T = EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget() && !x.IsDead && !x.IsZombie).FirstOrDefault();

                        if (T == null)
                            return;

                        if (T.IsValidTarget(R.MinimumRange))
                        {
                            var RPred = R.GetPrediction(T);

                            if (RPred.HitChance >= HitChance.High)
                            {
                                if (R.StartCharging())
                                {
                                    Player.Instance.Spellbook.UpdateChargeableSpell(SpellSlot.R, RPred.UnitPosition, true);
                                }
                            }
                        }else
                        if (T.IsValidTarget(R.MaximumRange))
                        {
                            var RPred = R.GetPrediction(T);

                            if (RPred.HitChance >= HitChance.High)
                            {
                                if (R.StartCharging())
                                {
                                    if (R.IsCharging)
                                    {
                                        R.Cast(RPred.UnitPosition);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var Check = TargetSelector.GetTarget(R.Range, DamageType.Mixed);

                if (Check == null)
                    return;

                var QTarget = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
                var ETarget = TargetSelector.GetTarget(E.Range, DamageType.Mixed);

                if (Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Combo, "UseQCombo"))
                {
                    try
                    {
                        var QPred = Q.GetPrediction(QTarget);

                        if (QPred.HitChance >= HitChance.High)
                        {
                            Q.Cast(QPred.UnitPosition);
                        }
                    }catch(Exception)
                    {
                        //Chat.Print("Error Combo: Skill Q");
                    }
                }

                if(E.IsReady())
                {
                    try
                    {
                        if(PoppyMenu.CheckBox(PoppyMenu.Beta, "Beta") && PoppyMenu.CheckBox(PoppyMenu.Beta, "BetaECombo"))
                        {
                            if (CheckWall(ETarget))
                            {
                                E.Cast(ETarget);
                            }
                        }else
                        {
                            if (PoppyMenu.CheckBox(PoppyMenu.Combo, "UseECombo"))
                            {
                                var Position = ETarget.BoundingRadius + ETarget.ServerPosition.Extend(ObjectManager.Player.ServerPosition, -360);

                                if (Position.IsWall())
                                {
                                    E.Cast(ETarget);
                                }
                            }
                        }
                    }
                    catch(Exception)
                    {
                        //Chat.Print("Error Combo: Skill E");
                    }
                }

            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                var Minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => !x.IsDead && x.IsValidTarget(Q.Range));
                var Minions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(Minion, Q.Width, (int) Q.Range);

                if (Minion == null)
                    return;

                if(Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Laneclear, "UseQLane"))
                {
                    try
                    {
                        if (Minions.HitNumber >= PoppyMenu.Slider(PoppyMenu.Laneclear, "UseQLaneMin"))
                        {
                            Q.Cast(Minions.CastPosition);
                        }
                    }catch(Exception)
                    {
                        //Chat.Print("Error Laneclear: Skill Q");
                    }

                }
            }

            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                //Copyright https://github.com/Phandaros/EloBuddy/blob/master/KaPoppy/Modes/Jungleclear.cs
                var Minion = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(E.Range + 300));

                if (Minion == null)
                    return;

                if(Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Jungleclear, "UseQJG"))
                {
                    try
                    {
                        Q.Cast(Minion);
                    }catch(Exception)
                    {
                        //Chat.Print("Error Jungle: Skill Q");
                    }
                }

                if(E.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Jungleclear, "UseEJG"))
                {
                    try
                    {
                        E.Cast(Minion);
                    }
                    catch(Exception)
                    {
                        //Chat.Print("Error Jungle: Skill E");
                    }
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
                    //Copyright https://github.com/Phandaros/EloBuddy/blob/master/KaPoppy/Modes/Flee.cs

                    var Minion = ObjectManager.Get<Obj_AI_Minion>().Where(x => Player.Instance.ServerPosition.Extend(x, Player.Instance.Distance(x) + 300).Distance(Game.CursorPos) < Player.Instance.Distance(Game.CursorPos) && x.IsValidTarget(E.Range)).OrderBy(x => x.Distance(Game.CursorPos));

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
