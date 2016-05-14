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
    class Laneclear
    {
        public static void Init()
        {
            var Minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Elise.Q.Range);

            foreach(var Minion in Minions)
            {
                if(Elise.CheckForm())
                {
                    if(Elise.Q.IsReady() && EliseMenu.CheckBox(EliseMenu.Laneclear, "Q"))
                    {
                        if(Minion.IsValidTarget(Elise.Q.Range))
                        {
                            Elise.Q.Cast(Minion);
                        }
                    }

                    if (Elise.W.IsReady() && EliseMenu.CheckBox(EliseMenu.Laneclear, "W"))
                    {
                        if (Minion.IsValidTarget(Elise.W.Range))
                        {
                            Elise.W.Cast(Minion);
                        }
                    }
                }
                else
                {
                    if(Elise.Q2.IsReady() && EliseMenu.CheckBox(EliseMenu.Laneclear, "Q2"))
                    {
                        if(Minion.IsValidTarget(Elise.Q2.Range))
                        {
                            Elise.Q2.Cast(Minion);
                        }
                    }

                    if(Elise.W2.IsReady() && EliseMenu.CheckBox(EliseMenu.Laneclear, "W2"))
                    {
                        if(Minion.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                        {
                            Elise.W2.Cast();
                        }
                    }
                }

                if (!Elise.Q.IsReady() && !Elise.W.IsReady())
                {
                    if (Elise.R.IsReady())
                    {
                        Elise.R.Cast();
                    }
                }
                else
                {
                    if(!Elise.Q2.IsReady() && !Elise.W2.IsReady())
                    {
                        if(Elise.R.IsReady())
                        {
                            Elise.R.Cast();
                        }
                    }
                }
            }
        }
    }
}
