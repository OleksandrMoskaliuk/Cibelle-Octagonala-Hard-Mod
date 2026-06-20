using System;
using System.Buffers.Text;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace cibelle_hard_mod
{
    internal class EnemyPatch
    {

        [global::HarmonyLib.HarmonyPatch(typeof(global::EnStats), "Start")]
        [global::HarmonyLib.HarmonyPrefix]
        private static bool pleasure_damage_rebalance(EnStats __instance, ref int ___behaviorState, ref CibelleBattleActions ___Cibelle,
            ref List<EnemySkill> ___m_skills, ref int ___m_originalBehaviorState)
        {
            float num = 0.0075f;
            int num2 = (int)((float)__instance.m_stam * (1f + (float)CibelleStats.instance.level * num));
            __instance.m_enstam = new Attribute(num2, true);
            __instance.m_enpl = new Attribute(100, true);
            __instance.m_enpl.SetTo(0);

            float min_speed = 0.08f;
            __instance.baseSpeed += (float)CibelleStats.instance.level * min_speed;
            float en_speed = UnityEngine.Random.Range(min_speed, 2.0f);
            __instance.baseSpeed *= en_speed;

            if (__instance.randomizeName)
            {
                string text = BattleManager.instance.GetComponent<RandomEnemyNames>().GenerateRandomName(__instance.enemyType);
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

            AccessTools.Method(typeof(global::EnStats), "InstantiateSkills").Invoke(__instance, null);
            AccessTools.Method(typeof(global::EnStats), "InstantiateUniqueHAction").Invoke(__instance, null);
            AccessTools.Method(typeof(global::EnStats), "InstantiateHSkills").Invoke(__instance, null);

            return false;
        }
    }
}