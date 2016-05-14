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
    class Lasthit
    {
        public static void Init()
        {
            var Minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Elise.Q.Range);

            foreach(var Minion in Minions)
            {
                if(Elise.CheckForm())
                {
                    if(Elise.Q.IsReady() && EliseMenu.CheckBox(EliseMenu.Lasthit, "Q"))
                    {
                        if(Minion.Health - Damages.QDamage(Minion) <= 0)
                        {
                            Elise.Q.Cast(Minion);
                        }
                    }
                }
                else
                {
                    if (Elise.Q2.IsReady() && EliseMenu.CheckBox(EliseMenu.Lasthit, "Q2"))
                    {
                        if (Minion.Health - Damages.Q2Damage(Minion) <= 0)
                        {
                            Elise.Q2.Cast(Minion);
                        }
                    }
                }
            }
        }
    }
}
