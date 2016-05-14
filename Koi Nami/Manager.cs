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
    class Manager
    {
        public static void Init()
        {
            foreach(var Ally in EntityManager.Heroes.Allies.Where(x => x.Distance(Player.Instance) <= Nami.W.Range))
            {
                if(NamiMenu.CheckBox(NamiMenu.Ally, Ally.ChampionName + "/W"))
                {
                    if(Ally.HealthPercent <= NamiMenu.Slider(NamiMenu.Ally, "Min"))
                    {
                        Nami.W.Cast(Ally);
                    }
                }
            }
        }

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if(sender.IsAlly)
            {
                if(sender.Type != GameObjectType.obj_AI_Minion)
                {
                    var Missile = (MissileClient)sender;

                    if (Missile.Target.Type == GameObjectType.obj_AI_Minion)
                        return;

                    if (!Missile.SpellCaster.IsMelee && !Missile.SpellCaster.IsEnemy)
                    {
                        if (Missile.Target.IsEnemy)
                        {
                            var Caster = (AIHeroClient)Missile.SpellCaster;

                            if (Nami.E.IsReady() && NamiMenu.CheckBox(NamiMenu.Ally, Caster.ChampionName + "/E"))
                            {
                                if (Caster.IsValidTarget(Nami.E.Range))
                                {
                                    Nami.E.Cast(Caster);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
