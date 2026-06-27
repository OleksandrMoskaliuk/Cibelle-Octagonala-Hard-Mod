using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using cibelle_hard_mod;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Audio;

namespace CibelleHardMode.src
{
    internal class EnemyPatch
    {
        [HarmonyPatch(typeof(EnStats), "Start")]
        [HarmonyPrefix]
        private static bool pleasure_damage_rebalance(EnStats __instance, ref int ___behaviorState, ref CibelleBattleActions ___Cibelle,
            ref List<EnemySkill> ___m_skills, ref int ___m_originalBehaviorState)
        {

            // Finalize attributes

            Plugin.m_Enemy.RollInstance(__instance.enemyType);

            __instance.m_attackdamage = Plugin.m_Enemy.Attack;

            __instance.baseSpeed = Plugin.m_Enemy.Speed;

            __instance.m_enstam = new Attribute((int)Plugin.m_Enemy.MaxStamina, true);

            __instance.m_enpl = new Attribute((int)Plugin.m_Enemy.MaxPleasure, true);

            __instance.timesToEjaculate = Plugin.m_Enemy.TimesToEjaculate;

            __instance.m_enpl.SetTo(0);


            if (__instance.randomizeName)
            {
                string text = BattleManager.instance.GetComponent<RandomEnemyNames>().GenerateRandomName(__instance.enemyType) + " Lv" + Plugin.m_Enemy.Level.ToString();
                if (__instance.GetComponent<Character>() != null)
                {
                    __instance.GetComponent<Character>().m_name = text;
                }
            }
            __instance.name = __instance.Name();
            int num5 = UnityEngine.Random.Range(0, 5);
            __instance.m_hpreference = (EnStats.HPreference)num5;
            if (UnityEngine.Random.value > 0.95f)
            {
                ___behaviorState = 150;
            }
            else if (UnityEngine.Random.value < 0.05f)
            {
                ___behaviorState = -50;
            }
            ___Cibelle = BattleManager.instance.GetComponent<CibelleBattleActions>();
            ___m_skills.Add(new EnemyAttack(__instance));
            __instance.timesEjaculated = 0;
            ___m_originalBehaviorState = ___behaviorState;

            AccessTools.Method(typeof(EnStats), "InstantiateSkills").Invoke(__instance, null);
            AccessTools.Method(typeof(EnStats), "InstantiateUniqueHAction").Invoke(__instance, null);
            AccessTools.Method(typeof(EnStats), "InstantiateHSkills").Invoke(__instance, null);

            return false;
        }

    }

    internal class PleasureDamageMod 
    {
        [HarmonyPatch(typeof(EnStats), "IncreasePleasureRandomGeneric")]
        [HarmonyPrefix]
        private static bool IncreasePleasureRandomGenericPrefix(EnStats __instance)
        {
            int num = (int)(8f * 1f);
            int num2 = (int)(12f * 1f);
            __instance.IncreasePl(UnityEngine.Random.Range(num, num2), PleasureDamageType.Raw);
            return false;
        }
    }

    internal class WinEventReferencePatch
    {
        [HarmonyPatch(typeof(CibelleBattleWin), "WinEvent")]
        [HarmonyPrefix]
        private static bool Prefix(
            CibelleBattleWin __instance,
            float essenceGivenMod,
            ref IEnumerator __result,
            GameObject ___m_winScreen,
            Transform ___m_winScreenParent,
            int ___baseEssenceGain)
        {
            // Assign our custom routine to the result and return false to completely skip vanilla execution
            __result = CustomWinEventRoutine(__instance, essenceGivenMod, ___m_winScreen, ___m_winScreenParent, ___baseEssenceGain);
            return false;
        }

        private static IEnumerator CustomWinEventRoutine(
            CibelleBattleWin __instance,
            float essenceGivenMod,
            GameObject m_winScreen,
            Transform m_winScreenParent,
            int baseEssenceGain)
        {
            GameObject wscreen = UnityEngine.Object.Instantiate<GameObject>(m_winScreen, m_winScreenParent);
            WinScreen script = wscreen.GetComponent<WinScreen>();

            int essenceGiven = (int)(Plugin.m_Enemy.Reward + Plugin.m_Enemy.Reward);
            Debug.Log("--- HARD MOD --- essenceGivenMod: " + essenceGivenMod.ToString());
            if (essenceGivenMod > 0f)
            {
                essenceGiven =  (int)(Plugin.m_Enemy.Reward + Plugin.m_Enemy.Reward * essenceGivenMod / 25f);
            }

            script.essenceGained = essenceGiven;
            AudioManager.instance.PlaySoundEffect("MiscSoundEffects/Retro Event UI 01", 1);

            for (; ; )
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (!script.Animating)
                    {
                        break;
                    }
                    script.SkipAnimation();
                }
                yield return null;
            }

            UnityEngine.Object.Destroy(wscreen);

            if (essenceGiven > 0)
            {
                CibelleStats.instance.GainEssence(essenceGiven);
            }

            yield break;
        }
    }




    // --- CLEANUP PATCH ---
    [HarmonyPatch(typeof(BattleManager), "EndBattle")]
    internal class BattleCleanupPatch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            // Check SkillPatch TSKNeverTappedOut reset resistance after battle
            //HStats.instance.timesClimaxedSinceResting = 0;
        }
    }
}