using System;
using System.Buffers.Text;
using HarmonyLib;
using UnityEngine;

namespace cibelle_hard_mod
{
    internal class CibelleStatPatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::CibelleStats), "Awake")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void cibelle_stats_rebalance(CibelleStats __instance)
        {
            // Read the BepInEx configuration string
            string selectedDifficulty = Plugin.Difficulty.Value.ToLower().Trim();

            if (selectedDifficulty == "hard")
            {
                __instance.vit = 1;
                __instance.agi = 1;
                __instance.fth = 1;
                __instance.cha = 1;
                __instance.baseHP = 90;
                Plugin.Log.LogInfo("CibelleStats patched: Hard stats applied.");
            }
            else if (selectedDifficulty == "normal")
            {
                __instance.vit = 5;
                __instance.agi = 3;
                __instance.fth = 6;
                __instance.cha = 8;
                __instance.baseHP = 90;
                Plugin.Log.LogInfo("CibelleStats patched: Normal stats applied.");
            }
            else
            {
                // Default or 'vanilla' - keep game defaults untouched
                Plugin.Log.LogInfo("CibelleStats patched: Vanilla values retained.");
            }
        }


        // vanilla pleasure damage around 20%
        // Modded: If cibelle still virgin, pleasure damage 2.5x
        [global::HarmonyLib.HarmonyPatch(typeof(global::CibelleStats), "PleasureDamageMod")]
        [global::HarmonyLib.HarmonyPrefix]
        private static bool pleasure_damage_rebalance(CibelleStats __instance, ref float __result)
        {
            if (__instance.virgin) 
            {
                __result = (1f + (float)__instance.cha * 1.5f) * __instance.GetComponent<HStats>().pleasureDamageModifier * 2.5f;
                return false; // override original
            }
            return true; // skip this buff, since she is not virgin anymore :(
        }
    }
}