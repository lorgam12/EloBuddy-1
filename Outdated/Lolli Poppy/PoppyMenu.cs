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
        public static Menu Principal, Combo, Laneclear, Jungleclear, Flee, Misc, Spells, Draw, Killsteal;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Lolli Poppy", "Poppy");
            Principal.AddLabel("Lolli Poppy v" + Assembly.GetExecutingAssembly().GetName().Version);
            Principal.AddSeparator(2);
            Principal.AddLabel("Good game !");

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("UseQCombo", new CheckBox("Use Q"));
            Combo.Add("UseWCombo", new CheckBox("Use W"));
            Combo.Add("UseECombo", new CheckBox("Use E"));
            Combo.Add("UseRCombo", new CheckBox("Use R"));
            Combo.Add("UseRComboKey", new KeyBind("Press Key To use R", false, KeyBind.BindTypes.HoldActive, 'T'));

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
            Draw.Add("DrawP", new CheckBox("Draw Passive"));
            Draw.Add("DrawDMG", new CheckBox("Draw Combo Damage"));
            Draw.Add("DrawTarget", new CheckBox("Draw Target ult"));
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
