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

namespace Warring_Kingdoms_Jarvan_IV
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "JarvanIV")
                return;

            JarvanMenu.Load();
            Jarvan.Load();
            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if(sender.IsAlly)
            {
                if(sender.Name == "Beacon")
                {
                    Jarvan.Estandarte = null;
                }
            }
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if(sender.IsAlly)
            {
                if(sender.Name == "Beacon")
                {
                    Jarvan.Estandarte = sender;
                }
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if(Jarvan.Estandarte != null)
            {
                Circle.Draw(Color.Red, 120, Jarvan.Estandarte.Position);
            }

            if(Jarvan.Q.IsReady() && JarvanMenu.CheckBox(JarvanMenu.Draw, "DrawQ"))
            {
                Circle.Draw(Color.Black, Jarvan.Q.Range, Player.Instance.Position);
            }

            if (Jarvan.W.IsReady() && JarvanMenu.CheckBox(JarvanMenu.Draw, "DrawW"))
            {
                Circle.Draw(Color.Black, Jarvan.W.Range, Player.Instance.Position);
            }

            if (Jarvan.E.IsReady() && JarvanMenu.CheckBox(JarvanMenu.Draw, "DrawE"))
            {
                Circle.Draw(Color.Red, Jarvan.E.Range, Player.Instance.Position);
            }

            if (Jarvan.R.IsReady() && JarvanMenu.CheckBox(JarvanMenu.Draw, "DrawR"))
            {
                Circle.Draw(Color.Red, Jarvan.R.Range, Player.Instance.Position);
            }
        }
    }
}
