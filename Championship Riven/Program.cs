using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Championship_Riven
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += delegate
            {
                if (Player.Instance.Hero != Champion.Riven)
                    return;

                if (Extensions.CheckLicense())
                {
                    Chat.Print("[Beta Status] - Open");
                }
                else
                {
                    Chat.Print("[Beta Status] - Closed");
                    return;
                }

                Riven.LoadModules();

                Drawing.OnDraw += delegate
                {
                    if (Menu.CheckBox(Menu.Draw, "Disable"))
                        return;

                    if (Menu.CheckBox(Menu.Draw, "Status"))
                    {
                        Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position).X - 40, Drawing.WorldToScreen(Player.Instance.Position).Y + 20, System.Drawing.Color.White, "ForceR");
                        Drawing.DrawText(Drawing.WorldToScreen(Player.Instance.Position).X + 12, Drawing.WorldToScreen(Player.Instance.Position).Y + 20, Menu.Keybind(Menu.R, "Force") ? System.Drawing.Color.LimeGreen : System.Drawing.Color.Red, Menu.Keybind(Menu.R, "Force") ? "(On)" : "(Off)");
                    }

                    if (Menu.CheckBox(Menu.Draw, "Burst"))
                    {
                        Drawing.DrawCircle(Player.Instance.Position, Extensions.E.Range + Extensions.Flash.Range, System.Drawing.Color.Red);
                    }

                    if (Menu.CheckBox(Menu.Draw, "Q") && Extensions.Q.IsReady())
                    {
                        Drawing.DrawCircle(Player.Instance.Position, Extensions.RealQ(), System.Drawing.Color.CadetBlue);
                    }

                    if (Menu.CheckBox(Menu.Draw, "W") && Extensions.W.IsReady())
                    {
                        Drawing.DrawCircle(Player.Instance.Position, Extensions.RealW(), System.Drawing.Color.CadetBlue);
                    }

                    if (Menu.CheckBox(Menu.Draw, "E") && Extensions.E.IsReady())
                    {
                        Drawing.DrawCircle(Player.Instance.Position, Extensions.E.Range, System.Drawing.Color.CadetBlue);
                    }

                    if (Menu.CheckBox(Menu.Draw, "R") && Extensions.R.IsReady())
                    {
                        Drawing.DrawCircle(Player.Instance.Position, Extensions.R2.Range, System.Drawing.Color.CadetBlue);
                    }
                };
            };
        }
    }
}
