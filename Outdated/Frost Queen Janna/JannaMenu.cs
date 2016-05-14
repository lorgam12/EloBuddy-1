using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Drawing;
using EloBuddy.SDK.Rendering;

namespace Frost_Queen_Janna
{
    class JannaMenu
    {
        public static Menu Principal, Combo, Flee, Misc, Shield, Ultimate, Draw;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Frost Queen Janna", "Janna");
            Principal.AddLabel("Frost Queen Janna v" + Assembly.GetExecutingAssembly().GetName().Version);
            Principal.AddSeparator(2);
            Principal.AddLabel("Good game !");

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.Add("UseQCombo", new CheckBox("Use Q"));
            Combo.Add("UseWCombo", new CheckBox("Use W"));

            Shield = Principal.AddSubMenu("Shield", "Shield");
            Shield.Add("UseShield", new CheckBox("Use Shield"));
            Shield.AddLabel("Order of priority to use the shield");
            Shield.AddSeparator(1);
            foreach (var Allies in EntityManager.Heroes.Allies)
            {
                Shield.Add("E/" + Allies.BaseSkinName, new Slider(Allies.ChampionName, 3, 1, 5));
            }
            Shield.AddSeparator(3);
            foreach (var Enemies in EntityManager.Heroes.Enemies)
            {
                Shield.AddLabel(Enemies.ChampionName + "Use the shield in following spell");
                Shield.AddSeparator(1);
                Shield.Add("E/" + Enemies.BaseSkinName + "/Q", new CheckBox(Enemies.ChampionName + " (Q)", false));
                Shield.Add("E/" + Enemies.BaseSkinName + "/W", new CheckBox(Enemies.ChampionName + " (W)", false));
                Shield.Add("E/" + Enemies.BaseSkinName + "/E", new CheckBox(Enemies.ChampionName + " (E)", false));
                Shield.Add("E/" + Enemies.BaseSkinName + "/R", new CheckBox(Enemies.ChampionName + " (R)", false));
                Shield.AddSeparator(2);
            }

            Ultimate = Principal.AddSubMenu("Ultimate", "Ultimate");
            Ultimate.Add("AutoR", new CheckBox("Auto R"));
            Ultimate.Add("AutoRHP", new Slider("If the HP of an ally is below {0}%", 15, 5, 40));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("SkinHack", new CheckBox("Skinhack ?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 3, 0, 6));
            Misc.Add("Interrupter", new CheckBox("Interrupter ?"));
            Misc.Add("InterrupterQ", new CheckBox("Interrupter using Q ?"));
            Misc.Add("InterrupterR", new CheckBox("Interrupter using R ?"));
            Misc.Add("Gapcloser", new CheckBox("Gapcloser ?"));
            Misc.Add("GapcloserQ", new CheckBox("Gapcloser using Q ?"));

            Draw = Principal.AddSubMenu("Drawing", "Drawing");
            Draw.Add("DrawQ", new CheckBox("Draw Q min and max range"));
            Draw.Add("DrawW", new CheckBox("Draw W"));
            Draw.Add("DrawE", new CheckBox("Draw E"));
            Draw.Add("DrawR", new CheckBox("Draw R"));
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