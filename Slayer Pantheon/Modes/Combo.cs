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

namespace Slayer_Pantheon.Modes
{
    class Combo
    {
        public static void Init()
        {
            if (Target() != null)
            {
                if (Pantheon.Q.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Combo, "Q"))
                {
                    if (Target().IsValidTarget(Pantheon.Q.Range))
                    {
                        Pantheon.Q.Cast(Target());
                    }
                }

                if (Pantheon.W.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Combo, "W"))
                {
                    if (Target().IsValidTarget(Pantheon.Q.Range))
                    {
                        Pantheon.W.Cast(Target());
                    }
                }

                if (Pantheon.E.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Combo, "E"))
                {
                    if (Target().IsValidTarget(Pantheon.E.Range))
                    {
                        var EPred = Pantheon.E.GetPrediction(Target());

                        if (EPred.HitChancePercent >= PantheonMenu.Slider(PantheonMenu.Principal, "EPred"))
                        {
                            Pantheon.E.Cast(EPred.UnitPosition);
                        }
                    }
                }
            }
        }

        public static AIHeroClient Target()
        {
            return TargetSelector.GetTarget(Pantheon.Q.Range, DamageType.Physical);
        }
    }
}
