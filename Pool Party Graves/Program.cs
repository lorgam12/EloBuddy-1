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

namespace Pool_Party_Graves
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Graves")
                return;

            GravesMenu.Load();
            Graves.Load();
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {

            if(Graves.Q.IsReady() && GravesMenu.CheckBox(GravesMenu.Draw, "DrawQ"))
            {
                Circle.Draw(Color.AliceBlue, Graves.Q.Range, Player.Instance.Position);
            }

            if (Graves.W.IsReady() && GravesMenu.CheckBox(GravesMenu.Draw, "DrawW"))
            {
                Circle.Draw(Color.Purple, Graves.W.Range, Player.Instance.Position);
            }

            if (Graves.E.IsReady() && GravesMenu.CheckBox(GravesMenu.Draw, "DrawE"))
            {
                Circle.Draw(Color.AliceBlue, Graves.E.Range, Player.Instance.Position);
            }

            if (Graves.R.IsReady() && GravesMenu.CheckBox(GravesMenu.Draw, "DrawR"))
            {
                Circle.Draw(Color.Purple, Graves.R.Range, Player.Instance.Position);
            }

            if (Graves.R2.IsReady() && GravesMenu.CheckBox(GravesMenu.Draw, "DrawDMG"))
            {
                Circle.Draw(Color.AliceBlue, Graves.R2.Range, Player.Instance.Position);
            }
        }
    }
}
