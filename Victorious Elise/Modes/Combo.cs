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

namespace Victorious_Elise.Modes
{
    class Combo
    {
        public static void Init()
        {
            // Human Form

            if(Elise.CheckForm())
            {
                if (Elise.E.IsReady() && EliseMenu.CheckBox(EliseMenu.Combo, "E"))
                {
                    if (Target().IsValidTarget(Elise.E.Range))
                    {
                        var EPred = Elise.E.GetPrediction(Target());

                        if (EPred.HitChancePercent >= EliseMenu.Slider(EliseMenu.Principal, "EPred"))
                        {
                            Core.DelayAction(() => Elise.E.Cast(Target()), EliseMenu.Slider(EliseMenu.Combo, "Delay"));
                        }
                    }
                }

                if (Elise.Q.IsReady() && EliseMenu.CheckBox(EliseMenu.Combo, "Q"))
                {
                    if (Target().IsValidTarget(Elise.Q.Range))
                    {
                        Core.DelayAction(() => Elise.Q.Cast(Target()), EliseMenu.Slider(EliseMenu.Combo, "Delay"));
                    }
                }

                if (Elise.W.IsReady() && EliseMenu.CheckBox(EliseMenu.Combo, "W"))
                {
                    if (Target().IsValidTarget(Elise.W.Range))
                    {
                        var WPred = Elise.W.GetPrediction(Target());

                        if (WPred.HitChancePercent >= EliseMenu.Slider(EliseMenu.Principal, "WPred"))
                        {
                            Core.DelayAction(() => Elise.W.Cast(Target()), EliseMenu.Slider(EliseMenu.Combo, "Delay"));
                        }
                    }
                }
            }else
            {
                // Spider Form

                if (Elise.Q2.IsReady() && EliseMenu.CheckBox(EliseMenu.Combo, "Q2"))
                {
                    if (Target().IsValidTarget(Elise.Q2.Range))
                    {
                        Core.DelayAction(() => Elise.Q2.Cast(Target()), EliseMenu.Slider(EliseMenu.Combo, "Delay"));
                    }
                }

                if (Elise.W2.IsReady() && EliseMenu.CheckBox(EliseMenu.Combo, "W2"))
                {
                    if (Target().IsValidTarget(Player.Instance.GetAutoAttackRange()))
                    {
                        Core.DelayAction(() => Elise.W2.Cast(), EliseMenu.Slider(EliseMenu.Combo, "Delay"));
                    }
                }

                if (Elise.E2.IsReady() && EliseMenu.CheckBox(EliseMenu.Combo, "E2"))
                {
                    if (Target().IsValidTarget(Elise.E2.Range))
                    {
                        if (Player.Instance.Distance(Target()) >= EliseMenu.Slider(EliseMenu.Combo, "E2Ex"))
                        {
                            Core.DelayAction(() => Elise.E2.Cast(Target()), EliseMenu.Slider(EliseMenu.Combo, "Delay"));
                        }
                    }
                }
            }
            // Auto Switch Form

            if (!Elise.Q.IsReady() && !Elise.W.IsReady() && !Elise.E.IsReady())
            {
                if (EliseMenu.CheckBox(EliseMenu.Combo, "Switch"))
                {
                    if (Elise.R.IsReady())
                    {
                        if (Player.Instance.CountEnemiesInRange(Elise.E.Range) > 0)
                        {
                            Elise.R.Cast();
                        }
                    }
                }
            }
            else
            {
                if(!Elise.Q2.IsReady() && !Elise.W2.IsReady() && !Elise.E2.IsReady())
                {
                    if(Elise.R.IsReady())
                    {
                        if(Player.Instance.CountEnemiesInRange(Elise.E.Range) > 0)
                        {
                            Elise.R.Cast();
                        }
                    }
                }
            }
        }

        public static AIHeroClient Target()
        {
            return TargetSelector.GetTarget(Elise.E.Range, DamageType.Magical);
        }
    }
}
