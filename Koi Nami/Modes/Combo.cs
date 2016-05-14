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

namespace Koi_Nami.Modes
{
    class Combo
    {
        public static void Init()
        {
            if(Nami.Q.IsReady() && NamiMenu.CheckBox(NamiMenu.Combo, "Q"))
            {
                if(Target().IsValidTarget(Nami.Q.Range))
                {
                    var QPred = Nami.Q.GetPrediction(Target());

                    if(QPred.HitChancePercent >= NamiMenu.Slider(NamiMenu.Principal, "QPred"))
                    {
                        Nami.Q.Cast(QPred.UnitPosition);
                    }
                }
            }

            if(Nami.R.IsReady() && NamiMenu.CheckBox(NamiMenu.Combo, "R"))
            {
                foreach(var x in EntityManager.Heroes.Enemies)
                {
                    if(x.IsFacing(Player.Instance) && Player.Instance.CountEnemiesInRange(Nami.R.Range) >= NamiMenu.Slider(NamiMenu.Combo, "AutoR"))
                    {
                        Nami.R.Cast(x);
                    }
                }
            }
        }

        public static AIHeroClient Target()
        {
            return TargetSelector.GetTarget(Nami.R.Range, DamageType.Magical);
        }
    }
}
