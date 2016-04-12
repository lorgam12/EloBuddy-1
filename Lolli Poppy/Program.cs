using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Lolli_Poppy
{
    class Program
    {
        private static GameObject Passiva = null;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Poppy")
                return;

            PoppyMenu.Load();
            Poppy.Load();
            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Game.OnWndProc += Game_OnWndProc;
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if(args.Msg == (uint)WindowMessages.LeftButtonDown)
            {
                var T = TargetSelector.GetTarget(200f, DamageType.Physical, Game.CursorPos);

                if(T.IsValidTarget())
                {
                    Poppy.TargetUlt = T;
                }
            }
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if(sender.HasBuff("poppypassiveshield"))
            {
                Passiva = null;
            }
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if(sender.IsAlly)
            {
                if(sender.Name.ToLower() == "shield")
                {
                    Passiva = null;
                }
            }
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if(sender.IsAlly)
            {
                if(sender.Name.ToLower() == "shield")
                {
                    Passiva = sender;
                }
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if(PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawTarget") && Poppy.TargetUlt != null)
            {
                Circle.Draw(Color.Yellow, 120, Poppy.TargetUlt.Position);
            }

            if(PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawP") && Passiva != null)
            {
                Circle.Draw(Color.Red, 60, Passiva.Position);
                Line.DrawLine(System.Drawing.Color.Red, Passiva.Position, Player.Instance.Position);
            }

            if (Poppy.Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawQ"))
            {
                Circle.Draw(Color.Yellow, Poppy.Q.Range, Player.Instance.Position);
            }

            if (Poppy.W.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawW"))
            {
                Circle.Draw(Color.Green, Poppy.W.Range, Player.Instance.Position);
            }

            if (Poppy.E.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawE"))
            {
                Circle.Draw(Color.Yellow, Poppy.E.Range, Player.Instance.Position);
            }

            if (Poppy.R.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawR"))
            {
                Circle.Draw(Color.Green, Poppy.R.MinimumRange, Player.Instance.Position);
            }

            if (Poppy.R.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawR"))
            {
                Circle.Draw(Color.Green, Poppy.R.MaximumRange, Player.Instance.Position);
            }
        }
    }
}
