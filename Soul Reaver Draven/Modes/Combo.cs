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
    class Combo
    {
        public static void Init()
        {
            if (Target() != null)
            {
                if (Draven.Q.IsReady() && DravenMenu.CheckBox(DravenMenu.Combo, "Q"))
                {
                    if (Target().IsValidTarget(Player.Instance.GetAutoAttackRange() + 70))
                    {
                        Draven.Q.Cast();
                    }
                }

                if (Draven.E.IsReady() && DravenMenu.CheckBox(DravenMenu.Combo, "E"))
                {
                    if (Target().IsValidTarget(Draven.E.Range))
                    {
                        var EPred = Draven.E.GetPrediction(Target());

                        if (EPred.HitChancePercent >= DravenMenu.Slider(DravenMenu.Principal, "EPred"))
                        {
                            Draven.E.Cast(EPred.UnitPosition);
                        }
                    }
                }

                if (Draven.R.IsReady() && DravenMenu.CheckBox(DravenMenu.Combo, "R"))
                {
                    if (Target().IsValidTarget(Draven.R.Range))
                    {
                        var RPred = Draven.R.GetPrediction(Target());

                        if (RPred.HitChancePercent >= DravenMenu.Slider(DravenMenu.Principal, "RPred"))
                        {
                            if (Target().Health <= Damages.RDamage(Target()))
                            {
                                Draven.R.Cast(RPred.UnitPosition);
                            }
                        }
                    }
                }
            }
        }

        public static AIHeroClient Target()
        {
            return TargetSelector.GetTarget(Draven.R.Range, DamageType.Physical);
        }
    }
}
