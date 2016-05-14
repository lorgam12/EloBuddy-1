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

namespace Hextech_Annie.Modes
{
    class Combo
    {
        public static void Init()
        {
            if(Annie.Q.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Combo, "Q"))
            {
                if(Target().IsValidTarget(Annie.Q.Range))
                {
                    Annie.Q.Cast(Target());
                }
            }

            if (Annie.W.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Combo, "W"))
            {
                if (Target().IsValidTarget(Annie.W.Range))
                {
                    var WPred = Annie.W.GetPrediction(Target());
                    
                    if(WPred.HitChancePercent >= AnnieMenu.Slider(AnnieMenu.Principal, "WPred"))
                    {
                        Annie.W.Cast(Target());
                    }
                }
            }

            if (Annie.R.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Combo, "R"))
            {
                if (Target().IsValidTarget(Annie.R.Range))
                {
                    var RPred = Annie.R.GetPrediction(Target());

                    if (RPred.HitChancePercent >= AnnieMenu.Slider(AnnieMenu.Principal, "RPred"))
                    {
                        if(AnnieMenu.CheckBox(AnnieMenu.Combo, "Only"))
                        {
                            if(Annie.HasStun())
                            {
                                Annie.R.Cast(Target());
                            }
                        }
                        else
                        {
                            Annie.R.Cast(Target());
                        }
                    }
                }
            }
        }

        public static AIHeroClient Target()
        {
            return TargetSelector.GetTarget(Annie.Q.Range, DamageType.Magical);
        }
    }
}
