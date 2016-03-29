using System.Reflection;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Championship_Riven
{
    class RivenMenu
    {
        public static Menu Principal, Combo, Burst, Shield, Laneclear, Jungleclear, Flee, Misc, Draw, Killsteal;

        public static void Load()
        {
            Principal = MainMenu.AddMenu("Championship Riven", "Riven");
            Principal.AddLabel("Championship Riven v" + Assembly.GetExecutingAssembly().GetName().Version);
            Principal.AddSeparator(2);
            Principal.AddLabel("Good game !");

            Combo = Principal.AddSubMenu("Combo", "Combo");
            Combo.AddSeparator(3);
            Combo.AddLabel("Spells Combo: Settings");
            Combo.Add("UseQCombo", new CheckBox("Use Q?"));
            Combo.Add("UseWCombo", new CheckBox("Use W?"));
            Combo.Add("UseECombo", new CheckBox("Use E"));
            Combo.Add("UseRCombo", new CheckBox("Use R?"));
            Combo.Add("UseR2Combo", new CheckBox("Use R2?"));
            Combo.AddSeparator(3);
            Combo.AddLabel("Items Combo: Setttings");
            Combo.Add("UseYoumuu", new CheckBox("Use Youmu?"));
            Combo.Add("UseTiamat", new CheckBox("Use Tiamat?"));
            Combo.Add("UseHydra", new CheckBox("Use Hydra?"));
            Combo.Add("UseIgnite", new CheckBox("Use Ignite?"));

            Combo.AddSeparator(3);
            Combo.AddLabel("Spell W: Settings");
            foreach(var Enemy in EntityManager.Heroes.Enemies)
            {
                Combo.Add("W/" + Enemy.BaseSkinName, new CheckBox(Enemy.ChampionName));
            }
            Combo.Add("W/Auto", new Slider("Auto W if {0} Enemies <=", 3, 1, 5));
            Combo.AddSeparator(3);
            Combo.AddLabel("Spell R: Settings");
            Combo.Add("UseRType", new ComboBox("Use R when", 1, "Normal Kill", "Hard Kill", "Always"));
            Combo.Add("DontR1", new Slider("Dont R if Target HP {0}% <=", 25, 10, 50));
            Combo.AddSeparator(3);
            Combo.AddLabel("Spell R2: Settings");
            Combo.Add("UseR2Type", new ComboBox("Use R2 when", 0, "Kill only", "Max damage"));
            foreach (var Enemy in EntityManager.Heroes.Enemies)
            {
                Combo.Add("R2/" + Enemy.BaseSkinName, new CheckBox(Enemy.ChampionName));
            }

            Shield = Principal.AddSubMenu("Shield", "Shield");
            Shield.AddLabel("Spell E: Settings");
            foreach(var Enemy in EntityManager.Heroes.Enemies)
            {
                Shield.AddLabel(Enemy.ChampionName);
                Shield.Add("E/" + Enemy.BaseSkinName + "/Q", new CheckBox(Enemy.ChampionName + " (Q)", false));
                Shield.Add("E/" + Enemy.BaseSkinName + "/W", new CheckBox(Enemy.ChampionName + " (W)", false));
                Shield.Add("E/" + Enemy.BaseSkinName + "/E", new CheckBox(Enemy.ChampionName + " (E)", false));
                Shield.Add("E/" + Enemy.BaseSkinName + "/R", new CheckBox(Enemy.ChampionName + " (R)", false));
                Shield.AddSeparator(1);
            }

            Burst = Principal.AddSubMenu("Burst", "Burst");
            Burst.AddLabel("Burst: Settings");
            Burst.AddLabel("The combo burst key is the Combo !");
            Burst.AddLabel("This 'Burst allowed' option is just to confirm that you want to use the Burst");
            Burst.AddSeparator(2);
            Burst.Add("BurstAllowed", new KeyBind("Burst Allowed ?", false, KeyBind.BindTypes.PressToggle, 'T'));
            Burst.Add("BurstType", new ComboBox("Burst:", 0, "Damage Check", "Always"));

            Laneclear = Principal.AddSubMenu("Laneclear", "Laneclear");
            Laneclear.Add("UseQLane", new CheckBox("Use Q"));
            Laneclear.Add("UseWLane", new CheckBox("Use W"));
            Laneclear.Add("UseWLaneMin", new Slider("Use W if hit {0} minions", 3, 1, 5));

            Jungleclear = Principal.AddSubMenu("Jungleclear", "Jungleclear");
            Jungleclear.Add("UseQJG", new CheckBox("Use Q"));
            Jungleclear.Add("UseWJG", new CheckBox("Use W"));
            Jungleclear.Add("UseEJG", new CheckBox("Use E"));

            Flee = Principal.AddSubMenu("Flee", "Flee");
            Flee.Add("UseQFlee", new CheckBox("Use Q"));
            Flee.Add("UseEFlee", new CheckBox("Use E"));

            Misc = Principal.AddSubMenu("Misc", "Misc");
            Misc.Add("Skin", new CheckBox("Skinhack ?", false));
            Misc.Add("SkinID", new Slider("Skin ID: {0}", 4, 0, 6));
            Misc.Add("Interrupter", new CheckBox("Interrupter ?"));
            Misc.Add("InterrupterW", new CheckBox("Interrupter with W ?"));
            Misc.Add("Gapcloser", new CheckBox("Gapcloser ?"));
            Misc.Add("GapcloserW", new CheckBox("Use W on Gapcloser ?"));
            Misc.Add("BrokenAnimations", new CheckBox("Broken Animations ?"));

            Draw = Principal.AddSubMenu("Drawing", "Drawing");
            Draw.Add("DrawQ", new CheckBox("Draw Q"));
            Draw.Add("DrawW", new CheckBox("Draw W"));
            Draw.Add("DrawE", new CheckBox("Draw E"));
            Draw.Add("DrawR", new CheckBox("Draw R2"));
            Draw.Add("DrawDamage", new CheckBox("Draw Damage"));
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

        public static int ComboBox(Menu m, string s)
        {
            return m[s].Cast<ComboBox>().SelectedIndex;
        }
    }
}
