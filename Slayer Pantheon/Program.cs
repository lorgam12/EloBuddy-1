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

namespace Slayer_Pantheon
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Pantheon)
                return;
			
            PantheonMenu.Init();
            Pantheon.Init();
            Drawing.OnDraw += Drawing_OnDraw;
        }
		
		private static void Drawing_OnDraw(EventArgs args)
        {
            if (Pantheon.Q.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Draw, "Q"))
            {
                Circle.Draw(Color.LimeGreen, Pantheon.Q.Range, Player.Instance.Position);
            }

            if (Pantheon.W.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Draw, "W"))
            {
                Circle.Draw(Color.LimeGreen, Pantheon.W.Range, Player.Instance.Position);
            }

            if (Pantheon.E.IsReady() && PantheonMenu.CheckBox(PantheonMenu.Draw, "E"))
            {
                Circle.Draw(Color.LimeGreen, Pantheon.E.Range, Player.Instance.Position);
            }
        }
    }
}
