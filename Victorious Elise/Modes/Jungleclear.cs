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
    class Jungleclear
    {
        public static void Init()
        {
            var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.IsValid).OrderBy(x => x.MaxHealth);

            foreach(var Monster in Monsters)
            {
                if(Elise.CheckForm())
                {
                    if(Elise.Q.IsReady() && EliseMenu.CheckBox(EliseMenu.Jungleclear, "Q"))
                    {
                        if(Monster.IsValidTarget(Elise.Q.Range))
                        {
                            Elise.Q.Cast(Monster);
                        }
                    }

                    if (Elise.W.IsReady() && EliseMenu.CheckBox(EliseMenu.Jungleclear, "W"))
                    {
                        if (Monster.IsValidTarget(Elise.W.Range))
                        {
                            Elise.W.Cast(Monster);
                        }
                    }

                    if (Elise.E.IsReady() && EliseMenu.CheckBox(EliseMenu.Jungleclear, "E"))
                    {
                        if (Monster.IsValidTarget(Elise.E.Range))
                        {
                            Elise.E.Cast(Monster);
                        }
                    }
                }
                else
                {
                    if(Elise.Q2.IsReady() && EliseMenu.CheckBox(EliseMenu.Jungleclear, "Q2"))
                    {
                        if(Monster.IsValidTarget(Elise.Q2.Range))
                        {
                            Elise.Q2.Cast(Monster);
                        }
                    }

                    if (Elise.W2.IsReady() && EliseMenu.CheckBox(EliseMenu.Jungleclear, "W2"))
                    {
                        if (Monster.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                        {
                            Elise.W2.Cast();
                        }
                    }
                }

                if (!Elise.Q.IsReady() && !Elise.W.IsReady() && !Elise.E.IsReady())
                {
                    if(Elise.R.IsReady())
                    {
                        Elise.R.Cast();
                    }
                }else
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
