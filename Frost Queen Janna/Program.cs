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
using System.Drawing;
using EloBuddy.SDK.Rendering;
using Color = System.Drawing.Color;

namespace Frost_Queen_Janna
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Janna")
                return;

            JannaMenu.Load();
            Janna.Load();
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if(Janna.Q.IsReady() || JannaMenu.CheckBox(JannaMenu.Draw, "DrawQ"))
            {
                Drawing.DrawCircle(Player.Instance.Position, Janna.Q.MinimumRange, Color.DarkCyan);
            }

            if(Janna.W.IsReady() || JannaMenu.CheckBox(JannaMenu.Draw, "DrawW"))
            {
                Drawing.DrawCircle(Player.Instance.Position, Janna.W.Range, Color.DarkCyan);
            }

            if(Janna.E.IsReady() || JannaMenu.CheckBox(JannaMenu.Draw, "DrawE"))
            {
                Drawing.DrawCircle(Player.Instance.Position, Janna.E.Range, Color.DarkCyan);
            }

            if(Janna.R.IsReady() || JannaMenu.CheckBox(JannaMenu.Draw, "DrawR"))
            {
                Drawing.DrawCircle(Player.Instance.Position, Janna.R.Range, Color.DarkCyan);
            }
        }
    }
}
