using System;
using HarmonyLib;
using UnityEngine;

namespace cibelle_hard_mod
{
    internal class AttackDamageEnemyMultiplier
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::EnStats), "AttackDamageMultiplier")]
        [HarmonyPrefix]
        private static bool Prefix(EnStats __instance, ref float __result)
        {
            // --- YOUR CUSTOM BALANCING ZONE ---
            // Vanilla scaling baseline factor per player level
            float LevelModifier = 0.015f;
            float result = 1f + (float)CibelleStats.instance.level * LevelModifier;

            // Invoke the private status check methods cleanly via the injected delegates
            if (__instance.Horny())
            {
                result *= 0.75f; // Vanilla default modifier
            }

            if (__instance.Angry())
            {
                result *= 1.2f; // Vanilla default modifier
            }

            // Day/Night Cycle status check
            if (GameManager.instance.GetComponent<DayNightCycle>().IsNight())
            {
                result *= 1.15f; // Vanilla default modifier
            }

            float currentDurability = CibelleStats.instance.ClothesDurability();

            // Linear Formula: Max out at 1.25x when broken (0.0), scale down to 0.75x when pristine (1.0)
            float maxDamageMult = 1.25f;
            float minDamageMult = 0.75f;
            float clothingDamageModifier = maxDamageMult - (maxDamageMult - minDamageMult) * Mathf.Clamp01(currentDurability);

            // Apply the linear clothing damage scalar directly to the attack profile
            result *= clothingDamageModifier;

            //Debug.Log(" --- Plugin Hard Mode --- _clothingDamageModifier = " + clothingDamageModifier.ToString());
            // Assign your finalized value to the Harmony return variable framework
            __result = result;

            // Returning false prevents the original vanilla method body from running entirely
            return false;
        }
    }

    internal class CibelleAttackDebuff
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::CibelleStats), "AttackDamage")]
        [HarmonyPrefix]
        private static bool AttackDamagePrefix(CibelleStats __instance, ref float __result)
        {
            // 100 is max, 0 is min 
            float cPleasure = __instance.m_pleasure.current;

            // Linear Formula: Scales from 1.10x at 0 pleasure down to 0.50x at 100 pleasure
            float pleasureDamageModifier = 1.10f - (0.006f * cPleasure);

            //Debug.Log(" --- Plugin Hard Mode --- pleasureDamageModifier = " + pleasureDamageModifier.ToString());

            // original attack damage calculation applied with our custom scaling factor
            __result = ((5f + (float)__instance.fth * 1.4f) * __instance.AttackModifier) * pleasureDamageModifier;

            // Returning false prevents the original vanilla method body from running entirely
            return false;
        }
    }
}