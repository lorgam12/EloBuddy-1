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

namespace Iron_Inquisitor_Kayle
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Kayle)
                return;

            KayleMenu.Init();
            Kayle.Init();
            Drawing.OnDraw += Drawing_OnDraw;			
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Kayle.Q.IsReady() && KayleMenu.CheckBox(KayleMenu.Draw, "Q"))
            {
                Circle.Draw(Color.LightGoldenrodYellow, Kayle.Q.Range, Player.Instance.Position);
            }

            if (Kayle.W.IsReady() && KayleMenu.CheckBox(KayleMenu.Draw, "W"))
            {
                Circle.Draw(Color.LightGoldenrodYellow, Kayle.W.Range, Player.Instance.Position);
            }

            if (Kayle.E.IsReady() && KayleMenu.CheckBox(KayleMenu.Draw, "E"))
            {
                Circle.Draw(Color.LightGoldenrodYellow, Kayle.E.Range, Player.Instance.Position);
            }

            if (Kayle.R.IsReady() && KayleMenu.CheckBox(KayleMenu.Draw, "R"))
            {
                Circle.Draw(Color.LightGoldenrodYellow, Kayle.R.Range, Player.Instance.Position);
            }
        }
    }
}
