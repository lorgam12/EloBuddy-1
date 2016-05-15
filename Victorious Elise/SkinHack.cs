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

namespace Victorious_Elise
{
    class SkinHack
    {
        public static void Init()
        {
            if (EliseMenu.CheckBox(EliseMenu.Misc, "SkinHack"))
            {
                foreach (var x in ObjectManager.Get<Obj_AI_Minion>().Where(x => x.Name.Equals("Spiderling") && x.IsValid && !x.IsDead))
                {
                    x.SetSkinId(EliseMenu.Slider(EliseMenu.Misc, "SkinID"));
                }

                Player.Instance.SetSkinId(EliseMenu.Slider(EliseMenu.Misc, "SkinID"));

                if (Player.Instance.GetAutoAttackRange() > 225)
                {
                    if (Player.Instance.Model == "EliseSpider")
                    {
                        Player.Instance.SetModel("Elise");
                    }
                }
                else if (Player.Instance.GetAutoAttackRange() < 230)
                {
                    if (Player.Instance.Model == "Elise")
                    {
                        Player.Instance.SetModel("EliseSpider");
                    }
                }
            }

            if (EliseMenu.Keybind(EliseMenu.Misc, "Reset"))
            {
                Player.Instance.SetModel(Player.Instance.ChampionName);
            }
        }
    }
}
