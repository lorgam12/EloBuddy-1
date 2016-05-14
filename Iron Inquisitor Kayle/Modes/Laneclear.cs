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
    class Laneclear
    {
        public static void Init()
        {
            var Minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Kayle.W.Range);

            foreach (var Minion in Minions)
            {
                if (Kayle.Q.IsReady() && KayleMenu.CheckBox(KayleMenu.Laneclear, "Q"))
                {
                    if (Minion.IsValidTarget(Kayle.Q.Range))
                    {
                        Kayle.Q.Cast(Minion);
                    }
                }

                if (Kayle.E.IsReady() && KayleMenu.CheckBox(KayleMenu.Laneclear, "E"))
                {
                    if (Minion.IsValidTarget(Kayle.E.Range))
                    {
                        Kayle.E.Cast();
                    }
                }
            }
        }
    }
}
