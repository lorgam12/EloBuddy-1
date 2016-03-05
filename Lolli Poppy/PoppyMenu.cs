using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Reflection;

namespace Lolli_Poppy
{
    class PoppyMenu
    {
        public static Menu Principal, Combo, Laneclear, Jungleclear, Flee, Misc, Spells, Draw, Killsteal, Beta;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Lolli Poppy", "Poppy");
            Principal.AddLabel("Lolli Poppy v" + Assembly.GetExecutingAssembly().GetName().Version);
            Principal.AddSeparator(2);
            Principal.AddLabel("Good game !");

            Beta = Principal.AddSubMenu("Beta", "Beta");
            Beta.Add("Beta", new CheckBox("Beta?", false));
            Beta.Add("AutoRCombo", new CheckBox("Auto R"));
            Beta.AddLabel("Auto R = If you are in danger");
            Beta.Add("AutoRComboHealth", new Slider("Auto R if your HP is below {0} %", 25, 10, 40));
            Beta.Add("AutoRComboEnemy", new Slider("Auto R if you {0} enemies in the range of R", 2, 1, 5));
            Beta.AddSeparator(2);
            Beta.Add("BetaECombo", new CheckBox("New logic to the skill E"));
            Beta.Add("BetaRCombo", new CheckBox("Use R"));
            Beta.Add("BetaRComboKey", new KeyBind("Press Key To use R", false, KeyBind.BindTypes.HoldActive, 'T'));
            Beta.AddSeparator(2);
            Beta.Add("BetaInterrupter", new CheckBox("Interrupter using R and E"));

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("UseQCombo", new CheckBox("Use Q"));
            Combo.Add("UseWCombo", new CheckBox("Use W"));
            Combo.Add("UseECombo", new CheckBox("Use E"));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("UseQLane", new CheckBox("Use Q"));
            Laneclear.Add("UseQLaneMin", new Slider("Use Q if hit {0} minions", 3, 1, 5));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("UseQJG", new CheckBox("Use Q"));
            Jungleclear.Add("UseEJG", new CheckBox("Use E"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("UseEFlee", new CheckBox("Use E in Minion"));
            Flee.Add("UseWFlee", new CheckBox("Use W"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("Skin", new CheckBox("Skinhack ?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 2, 0, 6));
            Misc.Add("Interrupter", new CheckBox("Interrupter ?"));
            Misc.Add("Gapcloser", new CheckBox("Gapcloser ?"));

            Spells = Principal.AddSubMenu("Spells", "Spells");
            foreach (AIHeroClient Enemy in EntityManager.Heroes.Enemies)
            {
                foreach (var Check in SpellsDatabase.Spells)
                {
                    if (Enemy.Hero == Check.Champion)
                    {
                        Spells.AddLabel(Check.Name);
                        Spells.Add(Check.Name + "/" + Check.SpellSlots, new CheckBox(Check.SpellSlots.ToString()));
                        Spells.AddSeparator(2);
                    }
                }
            }

            Draw = Principal.AddSubMenu("Drawing", "Drawing");
            Draw.Add("DrawQ", new CheckBox("Draw Q"));
            Draw.Add("DrawW", new CheckBox("Draw W"));
            Draw.Add("DrawE", new CheckBox("Draw E"));
            Draw.Add("DrawR", new CheckBox("Draw R min and max range"));
        }

        public static bool CheckBox(Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }

        public static bool Keybind(Menu m, string s)
        {
            return m[s].Cast<KeyBind>().CurrentValue;
        }
    }
}
