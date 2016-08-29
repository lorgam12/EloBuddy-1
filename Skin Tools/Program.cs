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

namespace Skin_Tools
{
    class Program
    {
        private static Menu Principal;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += delegate
            {
                Principal = MainMenu.AddMenu("Skin Tools", "Skin");
                Principal.Add("Enable", new CheckBox("Enable?"));
                Principal.Add("ID", new Slider("Skin ID: {0}", 0, 0, 20));

                Game.OnTick += delegate
                {
                    if (CheckBox(Principal, "Enable"))
                    {
                        Player.Instance.SetSkinId(Slider(Principal, "ID"));
                    }

                };
            };
        }

        private static bool CheckBox(Menu m, string s)
        {
            return m[s].Cast<CheckBox>().CurrentValue;
        }

        private static int Slider(Menu m, string s)
        {
            return m[s].Cast<Slider>().CurrentValue;
        }
    }
}
