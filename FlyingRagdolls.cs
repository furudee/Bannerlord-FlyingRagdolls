using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MCM.Abstractions.Settings.Base.Global;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace FlyingRagdolls

{
    [HarmonyPatch(typeof(Agent), nameof(Agent.Die))]
    class Patch
    {
        public static readonly Settings settings = GlobalSettings<Settings>.Instance;
        static bool Prefix(ref Blow b, ref Agent.KillInfo overrideKillInfo, Agent __instance)
        {
            // proceed to original method
            if (overrideKillInfo == Agent.KillInfo.Javelin) return true;
            b.BaseMagnitude = 1000f;
            b.DamagedPercentage = 10f;

            // melee kills
            if (b.WeaponRecord.Velocity == Vec3.Zero)   
            {
                b.WeaponRecord.Velocity = b.SwingDirection;
                b.WeaponRecord.Velocity.z = Math.Abs((float)Math.Tan(settings.Angle * (Math.PI/180)) * (new Vec2(b.WeaponRecord.Velocity.x, b.WeaponRecord.Velocity.y)).Length);
                b.WeaponRecord.Velocity *= settings.MeleeVelocityMultiplier;
            }
            // ranged and other kills
            else 
            {
                b.WeaponRecord.Velocity.z = Math.Abs((float)Math.Tan(settings.Angle * (Math.PI / 180)) * (new Vec2(b.WeaponRecord.Velocity.x, b.WeaponRecord.Velocity.y)).Length);
                b.WeaponRecord.Velocity *= settings.RangedVelocityMultiplier;
            }

            // edit blow with ballista bolt
            Agent attackerAgent = __instance.Mission.FindAgentWithIndex(b.OwnerId);
            Mission mission = __instance.Mission;
            MissionWeapon ballista = new MissionWeapon(MBObjectManager.Instance.GetObject<ItemObject>("ballista_projectile"), null, null);
            mission.AddCustomMissile(attackerAgent, ballista, Vec3.Zero, Vec3.Zero, Mat3.CreateMat3WithForward(Vec3.Zero), 0f, 0f, false, null);
            Dictionary<int, Mission.Missile> dict = typeof(Mission).GetField("_missiles", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mission) as Dictionary<int, Mission.Missile>;
            Mission.Missile missile = dict.Values.ElementAt(dict.Count - 1);
            b.WeaponRecord.FillAsMissileBlow(ballista.Item, ballista.CurrentUsageItem, missile.Index, (sbyte)BoneBodyPartType.Chest, attackerAgent.Position, __instance.Position, b.WeaponRecord.Velocity);

            // call function again with modified blow
            __instance.Die(b, Agent.KillInfo.Javelin);

            // exit without original function
            return false;
        }
    }
}