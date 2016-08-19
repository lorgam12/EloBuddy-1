using EloBuddy.SDK.Menu.Values;

namespace JokerLib.Helper
{
    public static class Menu
    {
        public static bool CheckBox(EloBuddy.SDK.Menu.Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        public static int Slider(EloBuddy.SDK.Menu.Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }

        public static bool Keybind(EloBuddy.SDK.Menu.Menu m, string s)
        {
            return m[s].Cast<KeyBind>().CurrentValue;
        }

        public static int ComboBox(EloBuddy.SDK.Menu.Menu m, string s)
        {
            return m[s].Cast<ComboBox>().SelectedIndex;
        }
    }
}
