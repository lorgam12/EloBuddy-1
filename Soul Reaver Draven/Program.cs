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

namespace Soul_Reaver_Draven
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Draven)
                return;

            DravenMenu.Init();
            Draven.Init();
			Drawing.OnDraw += Drawing_OnDraw;
        }
		
		private static void Drawing_OnDraw(EventArgs args)
        {
            if (DravenMenu.CheckBox(DravenMenu.Draw, "Axes"))
            {
                var Axe = AxesManager.Axes.Where(x => x.Axe.IsValid).FirstOrDefault();

                if (Axe != null)
                {
                    Circle.Draw(Color.Lime, 120, Axe.Axe.Position);
                }
            }

            if (Draven.E.IsReady() && DravenMenu.CheckBox(DravenMenu.Draw, "E"))
            {
                Circle.Draw(Color.DarkBlue, Draven.E.Range, Player.Instance.Position);
            }

            if (DravenMenu.CheckBox(DravenMenu.Draw, "Catch"))
            {
                if (DravenMenu.ComboBox(DravenMenu.Axes, "Mode") == 0)
                {
                    Circle.Draw(Color.LimeGreen, DravenMenu.Slider(DravenMenu.Axes, "Range"), Game.CursorPos);
                }
                else if (DravenMenu.ComboBox(DravenMenu.Axes, "Mode") == 1)
                {
                    Circle.Draw(Color.LimeGreen, DravenMenu.Slider(DravenMenu.Axes, "Range"), Player.Instance.Position);
                }
            }
        }
    }
}
