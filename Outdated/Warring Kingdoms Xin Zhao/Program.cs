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

namespace Warring_Kingdoms_Xin_Zhao
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "XinZhao")
                return;

            XinZhaoMenu.Load();
            XinZhao.Load();
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if(XinZhao.E.IsReady())
            {
                Circle.Draw(Color.Yellow, XinZhao.E.Range, Player.Instance.Position);
            }

            if (XinZhao.E.IsReady())
            {
                Circle.Draw(Color.DarkBlue, XinZhaoMenu.Slider(XinZhaoMenu.Combo, "UseEComboMin"), Player.Instance.Position);
            }

            if (XinZhao.R.IsReady())
            {
                Circle.Draw(Color.DarkBlue, XinZhao.R.Range, Player.Instance.Position);
            }
        }
    }
}
