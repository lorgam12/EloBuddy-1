using System.Reflection;
using EloBuddy;
using EloBuddy.SDK;
using i = EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;

namespace Championship_Riven
{
    class Menu
    {
        public static i.Menu Principal, Q, W, E, R, R2, Misc, Humanizer, Item, Draw;
        private static bool IsSpell(SpellSlot Slot) { if (Slot == SpellSlot.Q || Slot == SpellSlot.W || Slot == SpellSlot.E || Slot == SpellSlot.R) { return true; } return false; }

        public static void Load()
        {
            try
            {
                Principal = i.MainMenu.AddMenu("Championship Riven", "RivenAkr");
                Principal.Add("Skin", new CheckBox("Enable Skin Hack?", false));
                Principal.Add("SkinID", new Slider("Skin ID: {0}", 4, 0, 5));

                Q = Principal.AddSubMenu("Q Configs");
                Q.Add("Any", new CheckBox("Use on Laneclear/Jungleclear?"));
                Q.AddSeparator(2);
                Q.Add("Keep", new CheckBox("Keep Q Alive?"));

                W = Principal.AddSubMenu("W Configs");
                W.Add("Lane", new CheckBox("Use on Laneclear?"));
                W.Add("Jungle", new CheckBox("Use on Jungleclear?"));
                W.AddSeparator(2);
                W.Add("LaneMin", new Slider("Min {0} minions to use W on Laneclear", 3, 1, 6));

                E = Principal.AddSubMenu("E Configs");
                E.Add("Jungle", new CheckBox("Use on Jungleclear?"));
                E.Add("Shield", new CheckBox("Use on Spells?"));
                E.AddSeparator(2);
                E.AddLabel(".: Spells :.");
                foreach (var x in EntityManager.Heroes.Enemies)
                {
                    foreach(var y in x.Spellbook.Spells)
                    {
                        if (y.SData.TargettingType == SpellDataTargetType.Unit && IsSpell(y.Slot))
                        {
                            E.Add(x.ChampionName + "/" + y.Slot, new CheckBox(x.ChampionName + " " + y.Slot.ToString()));
                        }
                    }
                }

                R = Principal.AddSubMenu("R1 Configs");
                R.Add("Force", new KeyBind("Force R1?", false, KeyBind.BindTypes.PressToggle, 'M'));

                R2 = Principal.AddSubMenu("R2 Configs");
                R2.Add("Mode", new ComboBox("R2 Mode:", 0, "Kill Only", "Max Damage"));
                R2.Add("Save", new CheckBox("Save R2 (When in AA Range)", true));

                Misc = Principal.AddSubMenu("Misc");
                Misc.Add("Burst", new KeyBind("Burst", false, KeyBind.BindTypes.HoldActive, 'T'));
                Misc.AddSeparator(2);
                Misc.Add("QGap", new CheckBox("Use 3Q on Gapcloser?"));
                Misc.Add("QInt", new CheckBox("Use 3Q to Interrupt?"));
                Misc.Add("WGap", new CheckBox("Use W on Gapcloser?"));
                Misc.Add("WInt", new CheckBox("Use W to Interrupt?"));

                Humanizer = Principal.AddSubMenu("Humanizer");
                Humanizer.Add("Emotes", new CheckBox("Use Emotes?"));
                Humanizer.Add("Ping", new CheckBox("Calc Ping?", false));
                Humanizer.Add("Q2", new Slider("Delay Q2: {0}", 30, 0, 40));
                Humanizer.Add("Q3", new Slider("Delay Q3: {0}", 40, 0, 50));

                Item = Principal.AddSubMenu("Items");
                Item.Add("Hydra", new CheckBox("Use Hydra?"));
                Item.Add("Tiamat", new CheckBox("Use Tiamat?"));
                Item.Add("Youmuu", new CheckBox("Use Youmuu?"));
                Item.AddSeparator(2);
                Item.Add("Ignite", new CheckBox("Auto use Ignite?")); 

                Draw = Principal.AddSubMenu("Draws");
                Draw.Add("Burst", new CheckBox("Draw Burst Range?"));
                Draw.AddLabel(".: Spells :.");
                Draw.Add("Disable", new CheckBox("Disable All?", false));
                Draw.AddSeparator(1);
                Draw.Add("Q", new CheckBox("Draw Q?"));
                Draw.Add("W", new CheckBox("Draw W?"));
                Draw.Add("E", new CheckBox("Draw E?"));
                Draw.Add("R", new CheckBox("Draw R2?"));
                Draw.AddSeparator(2);
                Draw.Add("Status", new CheckBox("Draw Status About Settings?"));
               
            }
            catch (Exception e)
            {
                Extensions.Debug(e.Message);
            }
        }

        public static bool CheckBox(i.Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(i.Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }

        public static bool Keybind(i.Menu m, string s)
        {
            return m[s].Cast<KeyBind>().CurrentValue;
        }

        public static int ComboBox(i.Menu m, string s)
        {
            return m[s].Cast<ComboBox>().SelectedIndex;
        }
    }
}
