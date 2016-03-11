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

namespace Activator_chan
{
    class ActivatorMenu
    {
        public static Menu Principal, Offensive, Defensive, Priority, Misc;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Activator-chan", "Activator");

            Offensive = Principal.AddSubMenu("Offensive", "Offensive");

            Offensive.Add("Youmu", new CheckBox("Use Youmu?"));
            if (ActivatorItems.Check(ActivatorItems.Youmu))
            {
                Offensive.Add("Youmu0x01", new Slider("Use Youmu case the enemy's life is below {0}%", 40, 10, 60));
            }

            Offensive.Add("Tiamat", new CheckBox("Use Tiamat?"));
            if (ActivatorItems.Check(ActivatorItems.Tiamat))
            {
                Offensive.Add("Tiamat0x01", new Slider("Use Tiamat case the enemy's life is below {0}%", 70, 10, 80));
                Offensive.Add("Tiamat0x02", new Slider("Use Tiamat case hit {0} minions", 3, 0, 5));
            }

            Offensive.Add("Bilgewater", new CheckBox("Use Bilgewater?"));
            if (ActivatorItems.Check(ActivatorItems.Bilgewater))
            {
                Offensive.Add("Bilgewater0x01", new Slider("Use Bilgewater case the enemy's life is below {0}%", 60, 10, 80));
            }

            Offensive.Add("Muramana", new CheckBox("Use Muramana?"));
            if (ActivatorItems.Check(ActivatorItems.Muramana))
            {
                Offensive.Add("Muramana0x01", new Slider("Use Muramana case the enemy's life is below {0}%", 60, 10, 80));
                Offensive.Add("Muramana0x03", new Slider("Use Muramana if you have more than {0} enemies", 1, 1, 4));
            }

            Offensive.Add("HydraRavenous", new CheckBox("Use Hydra Ravenous?"));
            if (ActivatorItems.Check(ActivatorItems.HydraRavenous))
            {
                Offensive.Add("HydraRavenous0x01", new Slider("Use Hydra Ravenous case the enemy's life is below {0}%", 70, 10, 80));
                Offensive.Add("HydraRavenous0x02", new Slider("Use Hydra Ravenous case hit {0} minions", 4, 0, 5));
            }

            Offensive.Add("HydraTitanic", new CheckBox("Use Hydra Titanic?"));
            if (ActivatorItems.Check(ActivatorItems.HydraTitanic))
            {
                Offensive.Add("HydraTitanic0x01", new Slider("Use Hydra Titanic case the enemy's life is below {0}%", 70, 10, 80));
                Offensive.Add("HydraTitanic0x02", new Slider("Use Hydra Titanic case hit {0} minions", 4, 0, 5));
            }

            Offensive.Add("BladeKing", new CheckBox("Use Blade of the Ruined King?"));
            if (ActivatorItems.Check(ActivatorItems.BladeKing))
            {
                Offensive.Add("BladeKing0x01", new Slider("Use lade of the Ruined King case the enemy's life is below {0}%", 60, 10, 80));
            }

            Defensive = Principal.AddSubMenu("Defensive", "Defensive");

            Defensive.Add("Zhonya", new CheckBox("Use Zhonya?"));
            if (ActivatorItems.Check(ActivatorItems.Zhonya))
            {
                foreach(AIHeroClient x in EntityManager.Heroes.AllHeroes)
                {
                    foreach(var z in SpellsDatabase.CheckSpells)
                    {
                        if(x.Hero == z.Champ)
                        {
                            Defensive.Add("Zhonya/" + z.Champ.ToString(), new CheckBox(z.Champ + " / " + z.Slot));
                        }
                    }
                }
            }

            Defensive.Add("Seraph", new CheckBox("Use Seraph?"));
            if (ActivatorItems.Check(ActivatorItems.Seraph))
            {
                foreach (AIHeroClient x in EntityManager.Heroes.Enemies)
                {
                    foreach (var z in SpellsDatabase.CheckSpells)
                    {
                        if (x.Hero == z.Champ)
                        {
                            Defensive.Add("Seraph/" + z.Champ.ToString(), new CheckBox(z.Champ + " / " + z.Slot));
                        }
                    }
                }
            }

            Defensive.Add("Solari", new CheckBox("Use Solari?"));
            if (ActivatorItems.Check(ActivatorItems.Solari))
            {
                Defensive.Add("Solari0x01", new Slider("Use Solari case the allies life is below {0}%", 50, 10, 60));
                Defensive.Add("Solari0x03", new Slider("Use Solari if you have more than {0} enemies", 2, 0, 4));
            }

            Defensive.Add("FaceMountain", new CheckBox("Use FaceMountain?"));
            if (ActivatorItems.Check(ActivatorItems.FaceMountain))
            {
                Defensive.Add("FaceMountain0x01", new Slider("Use Face Mountain case the allies life is below {0}%", 50, 10, 60));
                Defensive.Add("FaceMountain0x03", new Slider("Use Face Mountain if you have more than {0} enemies", 1, 0, 4));
            }

            Defensive.Add("Mikael", new CheckBox("Use Crucible Mikael?"));
            if (ActivatorItems.Check(ActivatorItems.Mikael))
            {
                Defensive.Add("Mikael-Fear", new CheckBox("Use Crucible Mikael in the buff Fear"));
                Defensive.Add("Mikael-Charm", new CheckBox("Use Crucible Mikael in the buff Charm"));
                Defensive.Add("Mikael-Silence", new CheckBox("Use Crucible Mikael in the buff Silence"));
                Defensive.Add("Mikael-Snare", new CheckBox("Use Crucible Mikael in the buff Snare"));
                Defensive.Add("Mikael-Taunt", new CheckBox("Use Crucible Mikael in the buff Taunt"));
                Defensive.Add("Mikael-Suppression", new CheckBox("Use Crucible Mikael in the buff Suppression"));
                Defensive.Add("Mikael-Polymorph", new CheckBox("Use Crucible Mikael in the buff Polymorph"));
                Defensive.Add("Mikael-Disarm", new CheckBox("Use Crucible Mikael in the buff Disarm"));
                Defensive.Add("Mikael-NearSight", new CheckBox("Use Crucible Mikael in the buff NearSight"));
                Defensive.Add("Mikael-Blind", new CheckBox("Use Crucible Mikael in the buff Blind"));
                Defensive.Add("Mikael-Sleep", new CheckBox("Use Crucible Mikael in the buff Sleep"));
            }

            Defensive.Add("Qss", new CheckBox("Use Qss?"));
            if (ActivatorItems.Check(ActivatorItems.Qss))
            {
                Defensive.Add("Qss-Fear", new CheckBox("Use Crucible Qss in the buff Fear"));
                Defensive.Add("Qss-Charm", new CheckBox("Use Crucible Qss in the buff Charm"));
                Defensive.Add("Qss-Silence", new CheckBox("Use Crucible Qss in the buff Silence"));
                Defensive.Add("Qss-Snare", new CheckBox("Use Crucible Qss in the buff Snare"));
                Defensive.Add("Qss-Taunt", new CheckBox("Use Crucible Qss in the buff Taunt"));
                Defensive.Add("Qss-Suppression", new CheckBox("Use Crucible Qss in the buff Suppression"));
                Defensive.Add("Qss-Polymorph", new CheckBox("Use Crucible Qss in the buff Polymorph"));
                Defensive.Add("Qss-Disarm", new CheckBox("Use Crucible Qss in the buff Disarm"));
                Defensive.Add("Qss-NearSight", new CheckBox("Use Crucible Qss in the buff NearSight"));
                Defensive.Add("Qss-Blind", new CheckBox("Use Crucible Qss in the buff Blind"));
                Defensive.Add("Qss-Sleep", new CheckBox("Use Crucible Qss in the buff Sleep"));
            }

            Priority = Principal.AddSubMenu("Priority", "Priority");
            foreach (var x in EntityManager.Heroes.Allies)
            {
                Priority.Add(x.ChampionName, new Slider(x.ChampionName, 1, 1, 5));
            }

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("Skin", new CheckBox("Skin Hack?"));
            Misc.Add("SkinID", new Slider("Skin ID:", 0, 0, 15));
            Misc.Add("RangeCheck", new Slider("{0} Minimum range to check for enemies", 700, 500, 1200));
            Misc.Add("DrawRange", new CheckBox("Draw Range"));
        }

        public static bool CheckBox(Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }
    }
}
