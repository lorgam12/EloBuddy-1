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
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Elise)
                return;
			
            EliseMenu.Init();
            Elise.Init();
            Drawing.OnDraw += Drawing_OnDraw;        
		}
    
		private static void Drawing_OnDraw(EventArgs args)
        {
            // Human Form

            if (Elise.Q.IsReady() && EliseMenu.CheckBox(EliseMenu.Draw, "Q"))
            {
                Circle.Draw(Color.DeepSkyBlue, Elise.Q.Range, Player.Instance.Position);
            }

            if (Elise.W.IsReady() && EliseMenu.CheckBox(EliseMenu.Draw, "W"))
            {
                Circle.Draw(Color.DeepSkyBlue, Elise.W.Range, Player.Instance.Position);
            }

            if (Elise.E.IsReady() && EliseMenu.CheckBox(EliseMenu.Draw, "E"))
            {
                Circle.Draw(Color.DeepSkyBlue, Elise.E.Range, Player.Instance.Position);
            }

            // Spider Form

            if (Elise.Q2.IsReady() && EliseMenu.CheckBox(EliseMenu.Draw, "Q2"))
            {
                Circle.Draw(Color.DeepSkyBlue, Elise.Q2.Range, Player.Instance.Position);
            }

            if (Elise.E2.IsReady() && EliseMenu.CheckBox(EliseMenu.Draw, "E2"))
            {
                Circle.Draw(Color.DeepSkyBlue, Elise.E2.Range, Player.Instance.Position);
            }

            // Cooldown

            if (EliseMenu.CheckBox(EliseMenu.Draw, "Cooldowns"))
            {
                var Center = Drawing.WorldToScreen(Player.Instance.Position);

                if (!Elise.CheckForm())
                {
                    //Drawing.DrawLine(PosX, PosX + 150, 3, System.Drawing.Color.Red);

                    if (Cooldowns._HQCD == 0)
                    {
                        Drawing.DrawText(Center[0] - 60, Center[1], System.Drawing.Color.LimeGreen, "Human (Q) - OK");
                    }
                    else
                    {
                        Drawing.DrawText(Center[0] - 60, Center[1], System.Drawing.Color.Orange, "Human (Q) - " + Cooldowns._HQCD.ToString("0.0"));
                    }

                    if (Cooldowns._HWCD == 0)
                    {
                        Drawing.DrawText(Center[0] - 30, Center[1] + 30, System.Drawing.Color.LimeGreen, "Human (W) - OK");
                    }
                    else
                    {
                        Drawing.DrawText(Center[0] - 30, Center[1] + 30, System.Drawing.Color.Orange, "Human (W) - " + Cooldowns._HWCD.ToString("0.0"));
                    }

                    if (Cooldowns._HECD == 0)
                    {
                        Drawing.DrawText(Center[0] + 60, Center[1], System.Drawing.Color.LimeGreen, "Human (E) - OK");
                    }
                    else
                    {
                        Drawing.DrawText(Center[0] + 60, Center[1], System.Drawing.Color.Orange, "Human (E) - " + Cooldowns._HECD.ToString("0.0"));
                    }
                }
                else
                {
                    if (Cooldowns._SQCD == 0)
                    {
                        Drawing.DrawText(Center[0] - 60, Center[1], System.Drawing.Color.LimeGreen, "Spider (Q) - OK");
                    }
                    else
                    {
                        Drawing.DrawText(Center[0] - 60, Center[1], System.Drawing.Color.Orange, "Spider (Q) - " + Cooldowns._SQCD.ToString("0.0"));
                    }

                    if (Cooldowns._SWCD == 0)
                    {
                        Drawing.DrawText(Center[0] - 30, Center[1] + 30, System.Drawing.Color.LimeGreen, "Spider (W) - OK");
                    }
                    else
                    {
                        Drawing.DrawText(Center[0] - 30, Center[1] + 30, System.Drawing.Color.Orange, "Spider (W) - " + Cooldowns._SWCD.ToString("0.0"));
                    }

                    if (Cooldowns._SECD == 0)
                    {
                        Drawing.DrawText(Center[0] + 60, Center[1], System.Drawing.Color.LimeGreen, "Spider (E) - OK");
                    }
                    else
                    {
                        Drawing.DrawText(Center[0] + 60, Center[1], System.Drawing.Color.Orange, "Spider (E) - " + Cooldowns._SECD.ToString("0.0"));
                    }
                }
            }
        }
	}
}
