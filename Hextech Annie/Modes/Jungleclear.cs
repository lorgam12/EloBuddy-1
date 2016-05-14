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
    class Jungleclear
    {
        public static void Init()
        {
			var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.IsValid && !x.IsDead).OrderByDescending(x => x.MaxHealth);

            foreach (var Monster in Monsters)
            {
                if(Annie.Q.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Jungleclear, "Q"))
                {
                    if(Monster.IsValidTarget(Annie.Q.Range))
                    {
                        Annie.Q.Cast(Monster);
                    }
                }

                if (Annie.W.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Jungleclear, "W"))
                {
                    if (Monster.IsValidTarget(Annie.W.Range))
                    {
                        Annie.W.Cast(Monster);
                    }
                }
            }
        }
    }
}
