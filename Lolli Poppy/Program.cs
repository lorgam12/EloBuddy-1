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
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead)
                return;

            if(Poppy.Q.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawQ"))
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
                Circle.Draw(Color.Green, Poppy.R.Range, Player.Instance.Position);
            }

            var Target = TargetSelector.GetTarget(Poppy.E.Range, DamageType.Mixed);

            if (Target == null)
                return;

            var Position = Target.ServerPosition.Extend(Target.ServerPosition, -360);

            if (Poppy.E.IsReady() && PoppyMenu.CheckBox(PoppyMenu.Draw, "DrawEWall"))
            {
                if(Position.IsWall())
                {
                    Circle.Draw(Color.Red, 300, Position.To3D());
                }
            }
        }
    }
}
