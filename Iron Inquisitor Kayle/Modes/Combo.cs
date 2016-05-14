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

namespace Iron_Inquisitor_Kayle.Modes
{
    class Combo
    {
        public static void Init()
        {
            if (Target() != null)
            {
                if (Kayle.Q.IsReady() && KayleMenu.CheckBox(KayleMenu.Combo, "Q"))
                {
                    if (Target().IsValidTarget(Kayle.Q.Range))
                    {
                        Kayle.Q.Cast(Target());
                    }
                }

                if (Kayle.E.IsReady() && KayleMenu.CheckBox(KayleMenu.Combo, "E"))
                {
                    if (Target().IsValidTarget(Kayle.E.Range))
                    {
                        Kayle.E.Cast();
                    }
                }
            }
        }

        public static AIHeroClient Target()
        {
            return TargetSelector.GetTarget(Kayle.W.Range, DamageType.Magical);
        }
    }
}
