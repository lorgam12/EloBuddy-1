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

namespace Koi_Nami
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Nami)
                return;

            NamiMenu.Init();
            Nami.Init();
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if(Nami.Q.IsReady() || NamiMenu.CheckBox(NamiMenu.Draw, "Q"))
            {
                Circle.Draw(Color.Pink, Nami.Q.Range, Player.Instance.Position);
            }

            if (Nami.W.IsReady() || NamiMenu.CheckBox(NamiMenu.Draw, "W"))
            {
                Circle.Draw(Color.Pink, Nami.W.Range, Player.Instance.Position);
            }

            if (Nami.E.IsReady() || NamiMenu.CheckBox(NamiMenu.Draw, "E"))
            {
                Circle.Draw(Color.Pink, Nami.E.Range, Player.Instance.Position);
            }

            if (Nami.R.IsReady() || NamiMenu.CheckBox(NamiMenu.Draw, "R"))
            {
                Circle.Draw(Color.Pink, Nami.R.Range, Player.Instance.Position);
            }
        }
    }
}
