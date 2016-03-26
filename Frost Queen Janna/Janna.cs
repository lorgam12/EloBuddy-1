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
using System.Drawing;
using EloBuddy.SDK.Rendering;


namespace Frost_Queen_Janna
{
    class Janna
    {
        public static Spell.Chargeable Q;
        public static Spell.Targeted W;
        public static Spell.Targeted E;
        public static Spell.Active R;

        public static void Load()
        {
            Q = new Spell.Chargeable(SpellSlot.Q, 850, 1700, 3, 250, 900, 120);
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Active(SpellSlot.R, 725);

            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsAlly || sender.IsMe || sender == null)
                return;

            if (!Q.IsInRange(args.End) && !Q.IsInRange(args.Start))
                return;

            if(E.IsReady() || JannaMenu.CheckBox(JannaMenu.Shield, "UseShield"))
            {
                var Allies = EntityManager.Heroes.Allies.Where(x => x.IsValid || x.IsValidTarget(E.Range)).OrderByDescending(x => JannaMenu.Slider(JannaMenu.Shield, "E/" + x.BaseSkinName));

                foreach (var Ally in Allies)
                {
                    if (SpellSlot.Q == args.Slot || JannaMenu.CheckBox(JannaMenu.Shield, "E/" + sender.BaseSkinName + "/Q"))
                    {
                        E.Cast(Ally);
                    }

                    if (SpellSlot.W == args.Slot || JannaMenu.CheckBox(JannaMenu.Shield, "E/" + sender.BaseSkinName + "/W"))
                    {
                        E.Cast(Ally);
                    }

                    if (SpellSlot.E == args.Slot || JannaMenu.CheckBox(JannaMenu.Shield, "E/" + sender.BaseSkinName + "/E"))
                    {
                        E.Cast(Ally);
                    }

                    if (SpellSlot.R == args.Slot || JannaMenu.CheckBox(JannaMenu.Shield, "E/" + sender.BaseSkinName + "/R"))
                    {
                        E.Cast(Ally);
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsMe || sender.IsAlly || sender == null)
                return;

            if (!JannaMenu.CheckBox(JannaMenu.Misc, "Gapcloser"))
                return;

            if (Q.IsReady() || JannaMenu.CheckBox(JannaMenu.Misc, "GapcloserQ"))
            {
                if(e.End == Player.Instance.Position)
                {
                    if(sender.IsValidTarget(Q.Range))
                    {
                        if(!Q.IsCharging)
                        {
                            Q.StartCharging();
                        }else if(Q.Range == Q.MaximumRange)
                        {
                            Q.Cast(sender.Position);
                        }
                        else
                        {
                            if (Q.IsCharging)
                                return;

                            var QPred = Q.GetPrediction(sender);

                            if(QPred.HitChance >= HitChance.Medium)
                            {
                                Q.Cast(QPred.CastPosition);
                            }
                        }
                    }
                }
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsAlly || sender.IsMe || sender == null)
                return;

            if (!JannaMenu.CheckBox(JannaMenu.Misc, "Interrupter"))
                return;

            if(Q.IsReady() || JannaMenu.CheckBox(JannaMenu.Misc, "InterrupterQ"))
            {
                if(sender.IsValidTarget(Q.Range))
                {
                    if (!Q.IsCharging)
                    {
                        Q.StartCharging();
                    }
                    else if (Q.Range == Q.MaximumRange)
                    {
                        Q.Cast(sender.Position);
                    }
                    else
                    {
                        if (Q.IsCharging)
                            return;

                        var QPred = Q.GetPrediction(sender);

                        if (QPred.HitChance >= HitChance.Medium)
                        {
                            Q.Cast(QPred.CastPosition);
                        }
                    }
                }
            }

            if(R.IsReady() || JannaMenu.CheckBox(JannaMenu.Misc, "InterrupterR"))
            {
                if(sender.IsValidTarget(R.Range))
                {
                    if(e.DangerLevel == DangerLevel.High)
                    {
                        R.Cast();
                    }
                }
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if(JannaMenu.CheckBox(JannaMenu.Misc, "SkinHack"))
            {
                Player.Instance.SetSkinId(JannaMenu.Slider(JannaMenu.Misc, "SkinID"));
            }

            if(R.IsReady() || JannaMenu.CheckBox(JannaMenu.Ultimate, "AutoR"))
            {
                foreach (var Ally in EntityManager.Heroes.Allies.Where(x => x.IsValidTarget(R.Range)).OrderByDescending(x => JannaMenu.Slider(JannaMenu.Shield, "E/" + x.BaseSkinName)))
                {
                    if(Ally.HealthPercent <= JannaMenu.Slider(JannaMenu.Ultimate, "AutoRHP"))
                    {
                        R.Cast();
                        Orbwalker.DisableMovement = true;
                    }
                    else
                    {
                        Orbwalker.DisableMovement = false;
                    }
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (Q.IsReady() || JannaMenu.CheckBox(JannaMenu.Combo, "UseQCombo"))
                {
                    var Target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

                    if (Target == null)
                        return;

                    if(Target.IsValidTarget(Q.Range))
                    {
                        if (!Q.IsCharging)
                        {
                            Q.StartCharging();
                        }
                        else if (Q.Range == Q.MaximumRange)
                        {
                            Q.Cast(Target.Position);
                        }
                        else
                        {
                            if (Q.IsCharging)
                                return;

                            var QPred = Q.GetPrediction(Target);

                            if (QPred.HitChance >= HitChance.High)
                            {
                                Q.Cast(QPred.CastPosition);
                            }
                        }
                    }
                }

                if (W.IsReady() || JannaMenu.CheckBox(JannaMenu.Combo, "UseWCombo"))
                {
                    var Target = TargetSelector.GetTarget(W.Range, DamageType.Magical);

                    if (Target == null)
                        return;

                    if(Target.IsValidTarget(W.Range))
                    {
                        W.Cast(Target);
                    }
                }
            }
        }
    }
}
