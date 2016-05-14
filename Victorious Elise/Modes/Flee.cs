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
    class Flee
    {
        public static void Init()
        {
            var Target = TargetSelector.GetTarget(Elise.E.Range, DamageType.Magical);

            if(Elise.CheckForm())
            {
                if(Target != null)
                {
                    if (Elise.E.IsReady() || EliseMenu.CheckBox(EliseMenu.Flee, "E"))
                    {
                        var EPred = Elise.E.GetPrediction(Target);

                        if (EPred.HitChancePercent >= EliseMenu.Slider(EliseMenu.Principal, "E"))
                        {
                            Elise.E.Cast(EPred.UnitPosition);
                        }
                    }
                }
                else
                {
                    if(Elise.R.IsReady())
                    {
                        Elise.R.Cast();
                    }
                }
            }
            else
            {
                if(Elise.E2.IsReady() || EliseMenu.CheckBox(EliseMenu.Flee, "E2"))
                {
                    Elise.E2.Cast(Game.CursorPos);
                }
            }
        }
    }
}
