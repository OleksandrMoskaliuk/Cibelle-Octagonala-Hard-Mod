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
            __result = 1; // Base multiplier

             if (__instance.Horny())
                __result *= 0.75f; // -> 25% less damage
            
            if (__instance.Angry())
                __result *= 1.25f; // -> 25% more gamage

            // Day/Night Cycle status check
            if (GameManager.instance.GetComponent<DayNightCycle>().IsNight())
            {
                __result *= 1.25f; // ->25% more damage 
            }

            float clothingDamageModifier = 1f - Plugin.NormalizeFactor(CibelleStats.instance.ClothesHP(), 0f, 100 * 4f);
            __result *= clothingDamageModifier; // -> 0.25 less damage on full clothes durability

            //Pleasure attack debuff same as for Cibelle           
            float maxEnemyPleasure = Plugin.m_Enemy.MaxPleasure;
            float pleasureDamageModifier = 1f - Plugin.NormalizeFactor(__instance.CurrentPleasure(), 0f, maxEnemyPleasure * 4); // Max debuff on high pleasure 25% less damage
            __result *= pleasureDamageModifier;

            // The more times enemie ejaculated the more weak it become in the end
            float CharismaBonus = UnityEngine.Mathf.Min(CibelleStats.instance.cha * 0.005f, 0.25f); // 30 Charisma equal to 0.15 + 0.10 damage reduction per enemy ejaculation 
            float EjaculationDebuffMultiplier = 1f - UnityEngine.Mathf.Min(__instance.timesEjaculated * (0.10f + CharismaBonus), 0.9f);
            __result *= EjaculationDebuffMultiplier;
           
            float DamageDev = Plugin.CustomFloatRandomWalk(1f, 0.35f); /// Damage deviation ~35% same as for Cibelle
            __result *= DamageDev;

            //UnityEngine.Debug.LogWarning($"====== [CIBELLE HARD MOD] ATTACK CALCULATION TRACE ======");
            //UnityEngine.Debug.Log($"Instance Targeted     : {__instance.name} (Ejaculations: {__instance.timesEjaculated})");
            //UnityEngine.Debug.Log($"Initial Roller Attack : {Plugin.m_Enemy.Attack}");
            //UnityEngine.Debug.Log($"-------------------------------------------------------");
            //UnityEngine.Debug.Log($" ClothingDamageModifier =  {clothingDamageModifier}");
            //UnityEngine.Debug.Log($" PleasureDamageModifier = {pleasureDamageModifier}");
            //UnityEngine.Debug.Log($" Cha Bonus: {CharismaBonus} EjaculationDebuffMultiplier: {EjaculationDebuffMultiplier}");
            //UnityEngine.Debug.Log($"-------------------------------------------------------");
            //UnityEngine.Debug.Log($"Final Output : {__result} )");
            //UnityEngine.Debug.Log($"=======================================================");

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
                VirginDamageModifier *= 2.5f; //Fight for yout virginity! I's imprtant after all !
            
            float pleasureDamageModifier = 1f - Plugin.NormalizeFactor(cPleasure, 0f, 400f); // Max debuff 25% damage loss
            float DamageDev = Plugin.CustomFloatRandomWalk(1f, 0.35f); /// Damage deviation ~35% same as for enemies

            __result = (5f + __instance.fth * 1.4f) * __instance.AttackModifier * VirginDamageModifier * pleasureDamageModifier * DamageDev;

            //Debug.Log(" --- Plugin Hard Mode --- pleasureDamageModifier = " + pleasureDamageModifier.ToString());
            //Debug.Log(" --- Plugin Hard Mode --- CpleasureValue = " + cPleasure.ToString());
            // Returning false prevents the original vanilla method body from running entirely
            return false;
        }


        [HarmonyPatch(typeof(CibelleStats), "SpellAmp")]
        [HarmonyPrefix]
        public static bool SpellAmpPatch(CibelleStats __instance, ref float __result)
        {
            float DamageDev = Plugin.CustomFloatRandomWalk(1f, 0.35f);
            __result =  (4f + __instance.additionalSpellAmp + (float)__instance.fth * 3f) * DamageDev;

            // Returning false prevents the original vanilla method body from running entirely
            return false;
        }
    }
}