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
            int base_strength = (int)((float)__instance.m_stam * (1f + (float)CibelleStats.instance.level * num));
            float min_speed = 0.08f;

            __instance.baseSpeed += (float)CibelleStats.instance.level * min_speed;
            float en_speed = 0;
            int final_strength = 0;
            int base_max_pleasure = 100;

            switch (__instance.enemyType)
            {
                case EnemyType.OldMan:
                    final_strength = UnityEngine.Random.Range(base_strength / 4, base_strength);
                    en_speed = UnityEngine.Random.Range(min_speed, 1.0f);
                    base_max_pleasure = 25;
                    break;
                case EnemyType.Villager:
                    final_strength = UnityEngine.Random.Range(base_strength / 2, base_strength);
                    en_speed = UnityEngine.Random.Range(min_speed, 0.8f);
                    base_max_pleasure = 50;
                    break;
                case EnemyType.Soldier:
                    final_strength = UnityEngine.Random.Range(base_strength, base_strength * 2);
                    base_max_pleasure = 95;
                    en_speed = UnityEngine.Random.Range(min_speed, 1.2f);
                    break;
                case EnemyType.Bandit:
                    final_strength = UnityEngine.Random.Range(base_strength, base_strength * 2);
                    en_speed = UnityEngine.Random.Range(min_speed, 1.2f);
                    base_max_pleasure = 110;
                    break;
                case EnemyType.Roughman:
                    final_strength = UnityEngine.Random.Range(base_strength, base_strength * 2);
                    base_max_pleasure = 125;
                    en_speed = UnityEngine.Random.Range(min_speed, 0.8f);
                    break;
                case EnemyType.Barroso:
                    final_strength = UnityEngine.Random.Range(base_strength * 4, base_strength * 6);
                    base_max_pleasure = 225;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    break;
                case EnemyType.Goblin:
                    final_strength = UnityEngine.Random.Range(base_strength * 2, base_strength * 4);
                    base_max_pleasure = 175;
                    en_speed = UnityEngine.Random.Range(min_speed, 1.5f);
                    break;
                case EnemyType.Orc:
                    final_strength = UnityEngine.Random.Range(base_strength * 3, base_strength * 6);
                    base_max_pleasure = 225;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    break;
                case EnemyType.Werewolf:
                    final_strength = UnityEngine.Random.Range(base_strength * 3, base_strength * 6);
                    base_max_pleasure = 250;
                    en_speed = UnityEngine.Random.Range(min_speed, 2.2f);
                    break;
                case EnemyType.Drakkma:
                    final_strength = UnityEngine.Random.Range(base_strength * 5, base_strength * 10);
                    base_max_pleasure = 300;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    break;
                case EnemyType.Baron:
                    final_strength = UnityEngine.Random.Range(base_strength * 8, base_strength * 15);
                    base_max_pleasure = 300;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    break;
                default:
                    final_strength = base_strength;
                    en_speed = UnityEngine.Random.Range(min_speed, 1.2f);
                    base_max_pleasure = 100;
                    break;
            }

            // --- EASY ADJUSTMENT TUNING ZONE ---
            float min_pleasure_mod = 0.65f;
            float max_pleasure_mod = 1.35f;

            int min_range = (int)((float)base_max_pleasure * min_pleasure_mod);
            int max_range = (int)((float)base_max_pleasure * max_pleasure_mod);

            int max_pleasure = UnityEngine.Random.Range(min_range, max_range + 1);

            // Finalize attributes
            __instance.m_enstam = new Attribute(final_strength, true);
            __instance.m_enpl = new Attribute(max_pleasure, true);
            __instance.baseSpeed *= en_speed;
            __instance.m_enpl.SetTo(0);

            // --- DYNAMIC ESSENCE CALCULATOR (UPDATED FOR SPEED) ---
            // Compares strength, pleasure threshold, and speed scaling against baseline expectations.
            float strength_factor = (float)final_strength / (float)base_strength;
            float pleasure_factor = (float)max_pleasure / (float)base_max_pleasure;

            // en_speed acts as a direct multiplier to the base speed attribute.
            // Baseline is 1.0f, so anything above 1.0f increases danger and payoff.
            float speed_factor = en_speed;

            // Initialize baseline floor if this is the first unit initializing in the fight
            if (CibelleStats.instance.essenceGainModifier <= 1f)
            {
                CibelleStats.instance.essenceGainModifier = 1f;
            }

            // Combines all three random variables into a cohesive difficulty rating
            float calculated_bonus = (strength_factor * pleasure_factor * speed_factor) - 1.0f;
            if (calculated_bonus > 0f)
            {
                // Tweak the 0.95f multiplier if you want rewards to climb faster or slower
                CibelleStats.instance.essenceGainModifier += calculated_bonus * 0.95f;
            }

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

    // --- CLEANUP PATCH ---
    [HarmonyPatch(typeof(global::BattleManager), "EndBattle")]
    internal class BattleCleanupPatch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if (CibelleStats.instance != null)
            {
                CibelleStats.instance.essenceGainModifier = 1f;
            }
        }
    }
}