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
    class Laneclear
    {
        public static void Init()
        {
            var Minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Annie.Q.Range);

            foreach (var Minion in Minions)
            {
                if(AnnieMenu.CheckBox(AnnieMenu.Laneclear, "Q"))
                {
                    if(Minion.IsValidTarget(Annie.Q.Range))
                    {
                        if(Annie.Q.IsReady())
                        {
                            if (AnnieMenu.CheckBox(AnnieMenu.Laneclear, "Exception"))
                            {
                                if (!Annie.HasStun())
                                {
                                    Annie.Q.Cast(Minion);
                                }
                            }
                            else
                            {
                                Annie.Q.Cast(Minion);
                            }
                        }
                    }
                }

                if (AnnieMenu.CheckBox(AnnieMenu.Laneclear, "W"))
                {
                    if (Minion.IsValidTarget(Annie.W.Range))
                    {
                        if(Annie.W.IsReady())
                        {
                            if (AnnieMenu.CheckBox(AnnieMenu.Laneclear, "Exception"))
                            {
                                if (!Annie.HasStun())
                                {
                                    Annie.W.Cast(Minion);
                                }
                            }
                            else
                            {
                                Annie.W.Cast(Minion);
                            }
                        }
                    }
                }
            }
        }
    }
}
