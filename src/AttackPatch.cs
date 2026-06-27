using System;
using cibelle_hard_mod;
using HarmonyLib;
using UnityEngine;

namespace CibelleHardMode.src
{
    internal class AttackDamageEnemyMultiplier
    {
        [HarmonyPatch(typeof(EnStats), "AttackDamageMultiplier")]
        [HarmonyPrefix]
        private static bool Prefix(EnStats __instance, ref float __result)
        {
            // --- YOUR CUSTOM BALANCING ZONE ---
            // Vanilla scaling baseline factor per player level
            float LevelModifier = 0.015f;
            float result = 1f + CibelleStats.instance.level * LevelModifier;

            // Invoke the private status check methods cleanly via the injected delegates
            if (__instance.Horny())
            {
                result *= 0.45f; // Vanilla default modifier
            }

            if (__instance.Angry())
            {
                result *= 1.25f; // Vanilla default modifier
            }

            // Day/Night Cycle status check
            if (GameManager.instance.GetComponent<DayNightCycle>().IsNight())
            {
                result *= 1.15f; // Vanilla default modifier
            }

            float currentDurability = CibelleStats.instance.ClothesDurability();

            // Linear Formula: Max out at 1.35x when broken (0.0), scale down to 0.35x when pristine (1.0)
            float maxDamageMult = 1.35f; // Torned clothes
            float minDamageMult = 0.35f; // Near mint  
            float clothingDamageModifier = maxDamageMult - (maxDamageMult - minDamageMult) * Mathf.Clamp01(currentDurability);

            // Apply the linear clothing damage scalar directly to the attack profile
            result *= clothingDamageModifier;


            //Pleasure attack debuff same as for Cibelle
            float maxEnemyPleasure = Plugin.m_Enemy.Get(Plugin.GlobalEnemyType).MaxPleasure; 
            float pleasureDamageModifier = 1f - Plugin.NormalizeFactor(__instance.CurrentPleasure(), 0f, maxEnemyPleasure * 2); // Max debuff on high pleasure 50% damage loss
            __result *= pleasureDamageModifier;
            //Debug.Log(" --- Plugin Hard Mode --- _clothingDamageModifier = " + clothingDamageModifier.ToString());
            // Assign your finalized value to the Harmony return variable framework
            __result = EnemyProfile.RunRandomWalk(result, 0.35f); //~35% damage so it can be deviated

            // Returning false prevents the original vanilla method body from running entirely
            return false;
        }
    }

    internal class CibelleAttackDebuff
    {
        [HarmonyPatch(typeof(CibelleStats), "AttackDamage")]
        [HarmonyPrefix]
        public static bool AttackDamagePrefix(CibelleStats __instance, ref float __result)
        {
            // 100 is max, 0 is min 
            float cPleasure = __instance.m_pleasure.current;

            float VirginDamageModifier = 1f;

            if (__instance.virgin) 
            {
                VirginDamageModifier *= 2.5f; //Fight for yout virginity! I's imprtant after all !
            }

            float pleasureDamageModifier = 1f - Plugin.NormalizeFactor(cPleasure, 0f, 200f); // Max debuff 0.5 damage loss
            //Debug.Log(" --- Plugin Hard Mode --- CpleasureValue = " + cPleasure.ToString());
            //Debug.Log(" --- Plugin Hard Mode --- pleasureDamageModifier = " + pleasureDamageModifier.ToString());

            // original attack damage calculation applied with our custom scaling factor
            __result = (5f + __instance.fth * 1.4f) * __instance.AttackModifier * VirginDamageModifier * pleasureDamageModifier;
            __result = EnemyProfile.RunRandomWalk(__result, 0.35f); // Damage deviation ~35% same as for enemies

            // Returning false prevents the original vanilla method body from running entirely
            return false;
        }
    }
}