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

namespace Slayer_Pantheon.Modes
{
    class Jungleclear
    {
        public static void Init()
        {
            var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.IsValid && !x.IsDead).OrderByDescending(x => x.MaxHealth);

            foreach (var Monster in Monsters)
            {
                if (Pantheon.Q.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Jungleclear, "Q"))
                {
                    if (Monster.IsValidTarget(Pantheon.Q.Range))
                    {
                        Pantheon.Q.Cast(Monster);
                    }
                }

                if (Pantheon.W.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Jungleclear, "W"))
                {
                    if (Monster.IsValidTarget(Pantheon.W.Range))
                    {
                        Pantheon.W.Cast(Monster);
                    }
                }

                if (Pantheon.E.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Jungleclear, "E"))
                {
                    if (Monster.IsValidTarget(Pantheon.E.Range))
                    {
                        Pantheon.E.Cast(Monster);
                    }
                }
            }
        }
    }
}
