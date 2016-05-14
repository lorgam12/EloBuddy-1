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
    class Cooldowns
    {
        public static readonly float[] HQCD = { 6, 6, 6, 6, 6};
        public static readonly float[] HWCD = { 12, 12, 12, 12, 12 };
        public static readonly float[] HECD = { 14, 13, 12, 11, 10 };
        public static float HQCD_, HWCD_, HECD_ = 0;
        public static float _HQCD, _HWCD, _HECD = 0;

        public static readonly float[] SQCD = { 6, 6, 6, 6, 6 };
        public static readonly float[] SWCD = { 6, 6, 6, 6, 6 };
        public static readonly float[] SECD = { 26, 23, 20, 17, 14};
        public static float SQCD_, SWCD_, SECD_ = 0;
        public static float _SQCD, _SWCD, _SECD = 0;

        public static void Init()
        {
            _HQCD = ((HQCD_ - Game.Time) > 0) ? (HQCD_ - Game.Time) : 0;
            _HWCD = ((HWCD_ - Game.Time) > 0) ? (HWCD_ - Game.Time) : 0;
            _HECD = ((HECD_ - Game.Time) > 0) ? (HECD_ - Game.Time) : 0;

            _SQCD = ((SQCD_ - Game.Time) > 0) ? (SQCD_ - Game.Time) : 0;
            _SWCD = ((SWCD_ - Game.Time) > 0) ? (SWCD_ - Game.Time) : 0;
            _SECD = ((SECD_ - Game.Time) > 0) ? (SECD_ - Game.Time) : 0;
        }

        public static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if(sender.IsMe)
            {
                switch(args.SData.Name)
                {
                    case "EliseHumanQ":
                        HQCD_ = Game.Time + CalculateCD(HQCD[Elise.Q.Level]);
                        break;

                    case "EliseHumanW":
                        HWCD_ = Game.Time + CalculateCD(HWCD[Elise.W.Level]);
                        break;

                    case "EliseHumanE":
                        HECD_ = Game.Time + CalculateCD(HECD[Elise.E.Level]);
                        break;

                    case "EliseSpiderQCast":
                        SQCD_ = Game.Time + CalculateCD(SQCD[Elise.Q2.Level]);
                        break;

                    case "EliseSpiderW":
                        SWCD_ = Game.Time + CalculateCD(SWCD[Elise.W2.Level]);
                        break;

                    case "EliseSpiderEInitial":
                        SECD_ = Game.Time + CalculateCD(SECD[Elise.E2.Level]);
                        break;
                }
            }
        }

        private static float CalculateCD(float Time)
        {
            return Time + (Time * Player.Instance.PercentCooldownMod);
        }
    }
}
