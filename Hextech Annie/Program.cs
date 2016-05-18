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
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Annie)
                return;
	
            AnnieMenu.Init();
            Annie.Init();
			Drawing.OnDraw += Drawing_OnDraw;
		}
		
		private static void Drawing_OnDraw(EventArgs args)
        {
            if (Annie.Q.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Draw, "Q"))
            {
                Circle.Draw(Color.DarkBlue, Annie.Q.Range, Player.Instance.Position);
            }

            if (Annie.W.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Draw, "W"))
            {
                Circle.Draw(Color.DarkBlue, Annie.W.Range, Player.Instance.Position);
            }

            if (Annie.R.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Draw, "R"))
            {
                Circle.Draw(Color.DarkBlue, Annie.R.Range, Player.Instance.Position);
            }

            if (Annie.R.IsReady() && Annie.Flash.IsReady() && AnnieMenu.CheckBox(AnnieMenu.Draw, "Flash"))
            {
                Circle.Draw(Color.LimeGreen, Annie.Flash.Range + Annie.R.Range, Player.Instance.Position);
            }
        }
    }
}
