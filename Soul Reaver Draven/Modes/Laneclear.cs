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
    class Laneclear
    {
        public static void Init()
        {
            var Minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Draven.E.Range);

            foreach(var Minion in Minions)
            {
                if(Draven.Q.IsReady() && DravenMenu.CheckBox(DravenMenu.Laneclear, "Q"))
                {
                    if(Draven.AxesCount() < 2)
                    {
                        Draven.Q.Cast();
                    }
                }
            }
        }
    }
}
