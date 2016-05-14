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

namespace Hextech_Annie
{
    class SkinHack
    {
        public static void Init()
        {
            if (AnnieMenu.CheckBox(AnnieMenu.Misc, "SkinHack"))
            {
                Player.Instance.SetSkinId(AnnieMenu.Slider(AnnieMenu.Misc, "SkinID"));
            }

            if (AnnieMenu.Keybind(AnnieMenu.Misc, "Reset"))
            {
                Player.Instance.SetModel(Player.Instance.ChampionName);
            }

            foreach (var x in ObjectManager.Get<Obj_AI_Minion>().Where(x => x.Name.ToLower().Equals("tibbers") && x.IsValid && !x.IsDead))
            {
                x.SetSkinId(AnnieMenu.Slider(AnnieMenu.Misc, "SkinID"));
            }
        }
    }
}
