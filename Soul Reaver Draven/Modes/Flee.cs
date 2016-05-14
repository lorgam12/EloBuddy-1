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

namespace Soul_Reaver_Draven.Modes
{
    class Flee
    {
        public static void Init()
        {
            if(Draven.W.IsReady() && DravenMenu.CheckBox(DravenMenu.Flee, "W"))
            {
                Draven.W.Cast();
            }

            if(Draven.E.IsReady() && DravenMenu.CheckBox(DravenMenu.Flee, "E"))
            {
                var Target = TargetSelector.GetTarget(Draven.E.Range, DamageType.Physical);

                if(Target != null)
                {
                    var EPred = Draven.E.GetPrediction(Target);

                    if(EPred.HitChancePercent >= DravenMenu.Slider(DravenMenu.Principal, "EPred"))
                    {
                        Draven.E.Cast(EPred.UnitPosition);
                    }
                }
            }
        }
    }
}
