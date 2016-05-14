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
    class Jungleclear
    {
        public static void Init()
        {
            var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.IsValid && !x.IsDead).OrderByDescending(x => x.MaxHealth);

            foreach (var Monster in Monsters)
            {
                if (Kayle.Q.IsReady() && KayleMenu.CheckBox(KayleMenu.Jungleclear, "Q"))
                {
                    if (Monster.IsValidTarget(Kayle.Q.Range))
                    {
                        Kayle.Q.Cast(Monster);
                    }
                }

                if (Kayle.E.IsReady() && KayleMenu.CheckBox(KayleMenu.Jungleclear, "E"))
                {
                    if (Monster.IsValidTarget(Kayle.E.Range))
                    {
                        Kayle.E.Cast();
                    }
                }
            }
        }
    }
}
