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
            int enemy_base_reward = 0;
            Plugin.BattleReward = 0;

            // Clamp each factor strictly between 0.1 and 1.0 via dynamic remapping
            float strength_factor = 0;
            float pleasure_factor = 0;
            float speed_factor = 0;
            float calculated_reward_bonus = 0;

            switch (__instance.enemyType)
            {
                case EnemyType.OldMan:
                    final_strength = UnityEngine.Random.Range(base_strength / 4, base_strength);
                    en_speed = UnityEngine.Random.Range(min_speed, 1.0f);
                    base_max_pleasure = 25;
                    enemy_base_reward = 25;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength / 4, base_strength);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 1.0f);
                    break;
                case EnemyType.Villager:
                    final_strength = UnityEngine.Random.Range(base_strength / 2, base_strength);
                    en_speed = UnityEngine.Random.Range(min_speed, 0.8f);
                    base_max_pleasure = 50;
                    enemy_base_reward = 75;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength / 2, base_strength);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 0.8f);
                    break;
                case EnemyType.Soldier:
                    final_strength = UnityEngine.Random.Range(base_strength, base_strength * 2);
                    base_max_pleasure = 95;
                    en_speed = UnityEngine.Random.Range(min_speed, 1.2f);
                    enemy_base_reward = 95;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength, base_strength * 2);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 1.2f);
                    break;
                case EnemyType.Bandit:
                    final_strength = UnityEngine.Random.Range(base_strength, base_strength * 2);
                    en_speed = UnityEngine.Random.Range(min_speed, 1.2f);
                    base_max_pleasure = 110;
                    enemy_base_reward = 110;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength, base_strength * 2);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 1.2f);
                    break;
                case EnemyType.Roughman:
                    final_strength = UnityEngine.Random.Range(base_strength, base_strength * 2);
                    base_max_pleasure = 125;
                    en_speed = UnityEngine.Random.Range(min_speed, 0.8f);
                    enemy_base_reward = 225;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength, base_strength * 2);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 0.8f);
                    break;
                case EnemyType.Barroso:
                    final_strength = UnityEngine.Random.Range(base_strength * 4, base_strength * 6);
                    base_max_pleasure = 225;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    enemy_base_reward = 475;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength * 4, base_strength * 6);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 2f);
                    break;
                case EnemyType.Goblin:
                    final_strength = UnityEngine.Random.Range(base_strength * 2, base_strength * 4);
                    base_max_pleasure = 175;
                    en_speed = UnityEngine.Random.Range(min_speed, 1.5f);
                    enemy_base_reward = 275;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength * 2, base_strength * 4);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 1.5f);
                    break;
                case EnemyType.Orc:
                    final_strength = UnityEngine.Random.Range(base_strength * 3, base_strength * 6);
                    base_max_pleasure = 225;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    enemy_base_reward = 380;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength * 3, base_strength * 6);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 2f);
                    break;
                case EnemyType.Werewolf:
                    final_strength = UnityEngine.Random.Range(base_strength * 3, base_strength * 6);
                    base_max_pleasure = 200;
                    en_speed = UnityEngine.Random.Range(min_speed, 2.2f);
                    enemy_base_reward = 475;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength * 3, base_strength * 6);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 2.2f);
                    break;
                case EnemyType.Drakkma:
                    final_strength = UnityEngine.Random.Range(base_strength * 5, base_strength * 10);
                    base_max_pleasure = 245;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    enemy_base_reward = 750;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength * 5, base_strength * 10);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 2f);
                    break;
                case EnemyType.Baron:
                    final_strength = UnityEngine.Random.Range(base_strength * 8, base_strength * 15);
                    base_max_pleasure = 275;
                    en_speed = UnityEngine.Random.Range(min_speed, 2f);
                    enemy_base_reward = 900;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength * 8, base_strength * 15);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 2f);
                    break;
                default:
                    final_strength = base_strength;
                    en_speed = UnityEngine.Random.Range(min_speed, 1.2f);
                    base_max_pleasure = 100;
                    enemy_base_reward = 100;
                    strength_factor = Plugin.NormalizeFactor(final_strength, base_strength, base_strength);
                    speed_factor = Plugin.NormalizeFactor(en_speed, min_speed, 1.2f);
                    break;
            }

            // Finalize attributes
            int min_pleasure_range = (int)((float)base_max_pleasure * 0.65f);
            int max_pleasure_range = (int)((float)base_max_pleasure * 1.35f);
            int max_pleasure = UnityEngine.Random.Range(min_pleasure_range, max_pleasure_range);

            __instance.m_enstam = new Attribute(final_strength, true);
            __instance.m_enpl = new Attribute(max_pleasure, true);
            __instance.baseSpeed *= en_speed;
            __instance.m_enpl.SetTo(0);

            // ___REWARD___
            pleasure_factor = Plugin.NormalizeFactor(max_pleasure, min_pleasure_range, max_pleasure_range);

            // Sum the factors together and divide by 3 to calculate a clean average multiplier (0.1 to 1.0)
            calculated_reward_bonus = (strength_factor + pleasure_factor + speed_factor) / 3f;

            // Apply the bonus percentage to the baseline reward configuration
            enemy_base_reward = enemy_base_reward + (int)((float)enemy_base_reward * calculated_reward_bonus);

            // Addively contribute this unique monster's profile to the global encounter payoff pool
            Plugin.BattleReward += enemy_base_reward;
            //Debug.Log("Plugin calculated_bonus = " + calculated_reward_bonus.ToString());
            // ___REWARD___

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

    internal class WinEventReferencePatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::CibelleBattleWin), "WinEvent")]
        [HarmonyPrefix]
        private static bool Prefix(CibelleBattleWin __instance, ref int ___baseEssenceGain)
        {
            ___baseEssenceGain = 250; // By default
            //Debug.Log("Plugin BattleReward = " + Plugin.BattleReward.ToString());
            //Debug.Log("Plugin Total Essence gain = " + ___baseEssenceGain.ToString());
            ___baseEssenceGain += Plugin.BattleReward;
            return true;
        }
    }

    // --- CLEANUP PATCH ---
    [HarmonyPatch(typeof(global::BattleManager), "EndBattle")]
    internal class BattleCleanupPatch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {

        }
    }
}