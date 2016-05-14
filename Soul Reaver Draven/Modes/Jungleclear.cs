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
    class Jungleclear
    {
        public static void Init()
        {
            var Monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(x => x.IsValid).OrderBy(x => x.MaxHealth);

            foreach(var Monster in Monsters)
            {
                if(Monster.IsValidTarget(Draven.E.Range))
                {
                    if (Draven.Q.IsReady() && DravenMenu.CheckBox(DravenMenu.Jungleclear, "Q"))
                    {
                        if (Draven.AxesCount() < 2)
                        {
                            Draven.Q.Cast();
                        }
                    }

                    if(Draven.E.IsReady() && DravenMenu.CheckBox(DravenMenu.Jungleclear, "E"))
                    {
                        Draven.E.Cast(Monster);
                    }
                }
            }
        }
    }
}
