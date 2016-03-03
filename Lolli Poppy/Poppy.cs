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
            if (!sender.IsEnemy && sender.IsDead && sender.IsZombie && sender == null)
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
            if (!sender.IsEnemy && sender.IsDead && sender.IsZombie && sender == null)
                return;

            if (PoppyMenu.CheckBox(PoppyMenu.Misc, "Interrupter"))
            {
                if(E.IsReady() && sender.IsValidTarget(E.Range))
                {
                    E.Cast(sender);
                }else
                {
                    if(R.IsReady())
                    {
                        if (R.IsFullyCharged && sender.IsValidTarget(R.Range))
                        {
                            var RPred = R.GetPrediction(sender);

                            if(RPred.HitChance >= HitChance.Medium)
                            {
                                R.Cast(RPred.UnitPosition);
                            }
                        }else
                        {
                            R.StartCharging();
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy && !sender.IsDead && !sender.IsZombie && sender == null)
                return;

            var Position = sender.BoundingRadius + sender.Position.Extend(ObjectManager.Player.Position, -360);

            if(Position.IsWall() && sender.IsValidTarget(E.Range))
            {
                E.Cast(sender.Position);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

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

                if(E.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Combo, "UseECombo"))
                {
                    try
                    {
                        var Position = ETarget.BoundingRadius + ETarget.ServerPosition.Extend(ObjectManager.Player.ServerPosition, -360);

                        if (Position.IsWall())
                        {
                            E.Cast(ETarget);
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
