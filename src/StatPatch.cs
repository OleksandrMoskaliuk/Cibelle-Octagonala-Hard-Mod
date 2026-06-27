using System;
using System.Buffers.Text;
using cibelle_hard_mod;
using HarmonyLib;
using UnityEngine;

namespace CibelleHardMode.src
{
    internal class CibelleStatPatch
    {
        [HarmonyPatch(typeof(CibelleStats), "Awake")]
        [HarmonyPostfix]
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
        // Modded: If cibelle still virgin, pleasure damage 3x
        [HarmonyPatch(typeof(CibelleStats), "PleasureDamageMod")]
        [HarmonyPrefix]
        private static bool VirginityBuff(CibelleStats __instance, ref float __result)
        {
            if (__instance.virgin) 
            {
                __result = (1f + __instance.cha * 1.5f) * __instance.GetComponent<HStats>().pleasureDamageModifier * 3f;
                return false; // override original
            }
            return true; // skip this buff, since she is not virgin anymore :(
        }

        [HarmonyPatch(typeof(CibelleStats), "IncreasePleasure")]
        [HarmonyPrefix]
        public static bool CibellePleasureDamageTaken(CibelleStats __instance, ref int val, PleasureDamageType type)
        {
            // Pleasure damage multiplier, the stronger enemies deal more pleasure damage.
            float m_enemyMultiplier = 1.0f;
            
            // Calculate and assign the newly calibrated value directly to the reference parameter
            if (Plugin.m_Enemy != null) 
                m_enemyMultiplier = Plugin.m_Enemy.PleasureAttackMult; //Each time this function triggers it will read last PleasureAttackMultiplier from Start()

            val *= (int)m_enemyMultiplier; // Some deviation, same as for damage multipliers

            //Debug.Log(" --- Hard Mod  ---  m_enemyMultiplier = " + m_enemyMultiplier.ToString());

            // Return true so the original IncreasePleasure function runs with your new val
            return true;
        }


    }

}