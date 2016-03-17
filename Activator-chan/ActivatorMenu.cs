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
        public static Menu Principal, Offensive, Defensive, Consumable, Summoner, Priority;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Activator", "Activator");
            Principal.Add("RangeCheck", new Slider("{0} Minimum range to check for enemies", 800, 500, 1400));
            Principal.Add("DrawRange", new CheckBox("Draw Range ?"));
            Principal.Add("ChatDebug", new CheckBox("Show erros ?", false));

            Consumable = Principal.AddSubMenu("Consumable", "Consumable");

            Consumable.Add("HealthPotion", new CheckBox("Use Health Potion ?"));
            Consumable.Add("HealthPotion/01", new Slider("Use Health Potion case life is below {0}%", 15, 10, 60));
            Consumable.AddSeparator(2);

            Consumable.Add("Biscuit", new CheckBox("Use Biscuit ?"));
            Consumable.Add("Biscuit/01", new Slider("Use Biscuit case life is below {0}%", 15, 10, 60));
            Consumable.AddSeparator(2);

            Consumable.Add("RefillablePotion", new CheckBox("Use Refillable Potion ?"));
            Consumable.Add("RefillablePotion/01", new Slider("Use Refillable Potion case life is below {0}%", 15, 10, 60));
            Consumable.AddSeparator(2);

            Consumable.Add("CorruptingPotion", new CheckBox("Use Corrupting Potion ?"));
            Consumable.Add("CorruptingPotion/01", new Slider("Use Corrupting Potion case life is below {0}%", 15, 10, 60));
            Consumable.AddSeparator(2);

            Consumable.Add("HuntersPotion", new CheckBox("Use Hunters Potion ?"));
            Consumable.Add("HuntersPotion/01", new Slider("Use Hunters Potion case life is below {0}%", 15, 10, 60));
            Consumable.AddSeparator(2);

            Offensive = Principal.AddSubMenu("Offensive", "Offensive");

            Offensive.AddLabel("Youmu");
            Offensive.Add("Youmu", new CheckBox("Use Youmu ?"));
            Offensive.Add("Youmu/01", new Slider("Use Youmu case the enemy's life is below {0}%", 40, 10, 60));
            Offensive.AddSeparator(2);

            Offensive.AddLabel("Tiamat");
            Offensive.Add("Tiamat", new CheckBox("Use Tiamat ?"));
            Offensive.Add("Tiamat/01", new Slider("Use Tiamat case the enemy's life is below {0}%", 70, 10, 80));
            Offensive.Add("Tiamat/02", new Slider("Use Tiamat case hit {0} minions", 3, 0, 5));
            Offensive.AddSeparator(2);

            Offensive.AddLabel("Muramana");
            Offensive.Add("Muramana", new CheckBox("Use Muramana ?"));
            Offensive.Add("Muramana/01", new Slider("Use Muramana case the enemy's life is below {0}%", 60, 10, 80));
            Offensive.Add("Muramana/02", new Slider("Use Muramana if you have more than {0} enemies", 1, 1, 4));
            Offensive.AddSeparator(2);

            Offensive.AddLabel("Hydra Ravenous");
            Offensive.Add("HydraRavenous", new CheckBox("Use Hydra Ravenous ?"));
            Offensive.Add("HydraRavenous/01", new Slider("Use Hydra Ravenous case the enemy's life is below {0}%", 70, 10, 80));
            Offensive.Add("HydraRavenou02", new Slider("Use Hydra Ravenous case hit {0} minions", 4, 0, 5));
            Offensive.AddSeparator(2);

            Offensive.AddLabel("Hydra Titanic");
            Offensive.Add("HydraTitanic", new CheckBox("Use Hydra Titanic ?"));
            Offensive.Add("HydraTitanic/01", new Slider("Use Hydra Titanic case the enemy's life is below {0}%", 70, 10, 80));
            Offensive.Add("HydraTitanic/02", new Slider("Use Hydra Titanic case hit {0} minions", 4, 0, 5));
            Offensive.AddSeparator(2);

            Offensive.AddLabel("Blade of the Ruined King");
            Offensive.Add("BladeKing", new CheckBox("Use Blade of the Ruined King ?"));
            Offensive.Add("BladeKing/01", new Slider("Use lade of the Ruined King case the enemy's life is below {0}%", 60, 10, 80));
            Offensive.AddSeparator(2);

            Defensive = Principal.AddSubMenu("Defensive", "Defensive");

            Defensive.AddLabel("Zhonya");
            Defensive.Add("Zhonya", new CheckBox("Use Zhonya ?"));
            Defensive.AddSeparator();
            foreach (AIHeroClient x in EntityManager.Heroes.AllHeroes)
            {
                foreach (var z in SpellsDatabase.CheckSpells)
                {
                    if (x.Hero == z.Champ)
                    {
                        Defensive.Add("Zhonya/" + z.Champ.ToString(), new CheckBox(z.Champ + " / " + z.Slot));
                    }
                }
            }
            Defensive.AddSeparator(2);

            Defensive.AddLabel("Seraph");
            Defensive.Add("Seraph", new CheckBox("Use Seraph ?"));
            Defensive.AddSeparator();
            foreach (AIHeroClient x in EntityManager.Heroes.AllHeroes)
            {
                foreach (var z in SpellsDatabase.CheckSpells)
                {
                    if (x.Hero == z.Champ)
                    {
                        Defensive.Add("Seraph/" + z.Champ.ToString(), new CheckBox(z.Champ + " / " + z.Slot));
                    }
                }
            }
            Defensive.AddSeparator(2);

            Defensive.AddLabel("Solari");
            Defensive.Add("Solari", new CheckBox("Use Solari ?"));
            Defensive.Add("Solari/01", new Slider("Use Solari case the allies life is below {0}%", 50, 10, 60));
            Defensive.Add("Solari/02", new Slider("Use Solari if you have more than {0} enemies", 2, 0, 4));
            Defensive.AddSeparator(2);

            Defensive.AddLabel("Face Mountain");
            Defensive.Add("FaceMountain", new CheckBox("Use FaceMountain ?"));
            Defensive.Add("FaceMountain/01", new Slider("Use Face Mountain case the allies life is below {0}%", 50, 10, 60));
            Defensive.Add("FaceMountain/02", new Slider("Use Face Mountain if you have more than {0} enemies", 1, 0, 4));
            Defensive.AddSeparator(2);

            Defensive.AddLabel("Crucible Mikael");
            Defensive.Add("Mikael", new CheckBox("Use Crucible Mikael ?"));
            Defensive.AddSeparator();
            Defensive.Add("Mikael-Fear", new CheckBox("Use Crucible Mikael in the buff Fear"));
            Defensive.Add("Mikael-Charm", new CheckBox("Use Crucible Mikael in the buff Charm"));
            Defensive.Add("Mikael-Silence", new CheckBox("Use Crucible Mikael in the buff Silence"));
            Defensive.Add("Mikael-Snare", new CheckBox("Use Crucible Mikael in the buff Snare"));
            Defensive.Add("Mikael-Taunt", new CheckBox("Use Crucible Mikael in the buff Taunt"));
            Defensive.Add("Mikael-Suppression", new CheckBox("Use Crucible Mikael in the buff Suppression"));
            Defensive.Add("Mikael-Polymorph", new CheckBox("Use Crucible Mikael in the buff Polymorph"));
            Defensive.Add("Mikael-Blind", new CheckBox("Use Crucible Mikael in the buff Blind"));
            Defensive.AddSeparator(2);

            Defensive.AddLabel("Qss");
            Defensive.Add("Qss", new CheckBox("Use Qss?"));
            Defensive.AddSeparator();
            Defensive.Add("Qss-Fear", new CheckBox("Use Qss in the buff Fear"));
            Defensive.Add("Qss-Charm", new CheckBox("Use Qss in the buff Charm"));
            Defensive.Add("Qss-Silence", new CheckBox("Use Qss in the buff Silence"));
            Defensive.Add("Qss-Snare", new CheckBox("Use Qss in the buff Snare"));
            Defensive.Add("Qss-Taunt", new CheckBox("Use Qss in the buff Taunt"));
            Defensive.Add("Qss-Suppression", new CheckBox("Use Qss in the buff Suppression"));
            Defensive.Add("Qss-Polymorph", new CheckBox("Use Qss in the buff Polymorph"));
            Defensive.Add("Qss-Blind", new CheckBox("Use Qss in the buff Blind"));
            Defensive.AddSeparator(2);

            Summoner = Principal.AddSubMenu("Summoner", "Summoner");

            if(Spells.CheckBarrier())
            {
                Summoner.AddLabel("Barrier");
                Summoner.Add("Barrier", new CheckBox("Use Barrier ?"));
                Summoner.Add("Barrier/01", new CheckBox("Use Barrier If you are in danger"));
                Summoner.AddSeparator(2);
            }

            if(Spells.CheckCleanse())
            {
                Summoner.AddLabel("Cleanse");
                Summoner.Add("Cleanse", new CheckBox("Use Cleanse ?"));
                Summoner.AddSeparator();
                Summoner.Add("Cleanse-Fear", new CheckBox("Use Cleanse in the buff Fear"));
                Summoner.Add("Cleanse-Charm", new CheckBox("Use Cleanse in the buff Charm"));
                Summoner.Add("Cleanse-Silence", new CheckBox("Use Cleanse in the buff Silence"));
                Summoner.Add("Cleanse-Snare", new CheckBox("Use Cleanse in the buff Snare"));
                Summoner.Add("Cleanse-Taunt", new CheckBox("Use Cleanse in the buff Taunt"));
                Summoner.Add("Cleanse-Suppression", new CheckBox("Use Cleanse in the buff Suppression"));
                Summoner.Add("Cleanse-Polymorph", new CheckBox("Use Cleanse in the buff Polymorph"));
                Summoner.Add("Cleanse-Blind", new CheckBox("Use Cleanse in the buff Blind"));
                Summoner.AddSeparator(2);
            }

            if(Spells.CheckHeal())
            {
                Summoner.AddLabel("Heal");
                Summoner.Add("Heal", new CheckBox("Use Heal ?"));
                Summoner.Add("Heal/01", new Slider("Use Heal case life is below {0}%", 15, 5, 40));
                Summoner.AddSeparator(2);
            }

            if(Spells.CheckSmite())
            {
                Summoner.AddLabel("Smite");
                Summoner.Add("Smite", new CheckBox("Use Smite ?"));
                Summoner.AddSeparator();
                Summoner.Add("SRU_Baron", new CheckBox("Baron"));
                Summoner.Add("SRU_Dragon", new CheckBox("Dragon"));
                Summoner.AddSeparator();
                Summoner.Add("SRU_Blue", new CheckBox("Blue"));
                Summoner.Add("SRU_Red", new CheckBox("Red"));
                Summoner.Add("Sru_Crab", new CheckBox("Skuttles(Crab)"));
                Summoner.AddSeparator();
                Summoner.Add("SRU_Gromp", new CheckBox("Gromp", false));
                Summoner.Add("SRU_Murkwolf", new CheckBox("Murkwolf", false));
                Summoner.Add("SRU_Krug", new CheckBox("Krug", false));
                Summoner.Add("SRU_Razorbeak", new CheckBox("Razerbeak", false));
                Summoner.AddSeparator(2);
            }

            if(Spells.CheckIgnite())
            {
                Summoner.AddLabel("Ignite");
                Summoner.Add("Ignite", new CheckBox("Use Ignite ?"));
                Summoner.AddSeparator();
                foreach (var Enemy in EntityManager.Heroes.Enemies)
                {
                    Summoner.Add("Ignite/" + Enemy.ChampionName, new CheckBox(Enemy.ChampionName));
                }
                Summoner.AddSeparator(2);
            }
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
