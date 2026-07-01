using System.Collections;
using System.Collections.Generic;
using cibelle_hard_mod;
using HarmonyLib;
using UnityEngine;

namespace CibelleHardMode.src
{
    internal class EnemyPatch
    {

        private static readonly object m_rollLock = new object();
        [HarmonyPatch(typeof(BattleManager), "StartBattle")]
        [HarmonyPrefix]
        private static bool StartBattle_PrefixPatch(ref EnStats _enemy)
        {
            Debug.LogWarning(" --- HARD MOD --- BattleManager->StartBattle");

            lock (m_rollLock)
            {
                Plugin.m_Enemy.m_instance = _enemy;
                Plugin.m_Enemy.RollInstance();

                _enemy.m_attackdamage = Plugin.m_Enemy.Attack;
                _enemy.baseSpeed = Plugin.m_Enemy.Speed;
                _enemy.m_enstam = new Attribute((int)Plugin.m_Enemy.MaxStamina, true);
                _enemy.m_enpl = new Attribute((int)Plugin.m_Enemy.MaxPleasure, true);
                _enemy.timesToEjaculate = (int)Plugin.m_Enemy.TimesToEjaculate;
                _enemy.m_enpl.SetTo(0);

                if (_enemy.randomizeName)
                {
                    string text = BattleManager.instance.GetComponent<RandomEnemyNames>().GenerateRandomName(_enemy.enemyType) + " Lv" + Plugin.m_Enemy.Level.ToString();
                    if (_enemy.GetComponent<Character>() != null)
                    {
                        _enemy.GetComponent<Character>().m_name = text;
                    }
                }
                _enemy.name = _enemy.Name();
            }

            // Prooceed to work with original function
            return true;
        }


        [HarmonyPatch(typeof(EnStats), "Start")]
        [HarmonyPrefix]
        public static void pleasure_damage_rebalance(EnStats __instance, ref int ___behaviorState, ref CibelleBattleActions ___Cibelle,
            ref List<EnemySkill> ___m_skills, ref int ___m_originalBehaviorState)
        {
            //Debug.LogWarning(" --- HARD MOD --- EnStats->Start");
            CibelleStatPatch.UpdateSP(CibelleStats.instance);
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

            int essenceGiven = (int)(Plugin.m_Enemy.Reward);
            Debug.Log("--- HARD MOD --- essenceGivenMod: " + essenceGivenMod.ToString());
            if (essenceGivenMod > 1f)
            {
                essenceGiven =  (int)(Plugin.m_Enemy.Reward * CibelleStats.instance.essenceGainModifierH);
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
            CibelleStatPatch.UpdateSP(CibelleStats.instance);
            //// Restore HP(Stamina) after battle 
            //int MaxHp = CibelleStats.instance.MaxHP();
            //if (CibelleStats.instance.HasSpell(new HealingSpell()))
            //    CibelleStats.instance.m_hp.IncreaseBy(MaxHp / 3);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKRestoration())) 
            //    CibelleStats.instance.m_hp.IncreaseBy(MaxHp / 3);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKVirtue()))
            //    CibelleStats.instance.m_hp.IncreaseBy(MaxHp / 3);


            //// Resore Pleasure after battle
            //if (CibelleStats.instance.HasPassiveSkill(new TSKConstancy()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKEndureDesire()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKPleasureResistant()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKNeverTappedOut()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKPleasureResistant()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);


            //// Restore HP(Stamina) after battle 
            //int MaxHp = CibelleStats.instance.MaxHP();
            //if (CibelleStats.instance.HasSpell(new HealingSpell()))
            //    CibelleStats.instance.m_hp.IncreaseBy(MaxHp / 3);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKRestoration())) 
            //    CibelleStats.instance.m_hp.IncreaseBy(MaxHp / 3);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKVirtue()))
            //    CibelleStats.instance.m_hp.IncreaseBy(MaxHp / 3);


            //// Resore Pleasure after battle
            //if (CibelleStats.instance.HasPassiveSkill(new TSKConstancy()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKEndureDesire()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKPleasureResistant()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKNeverTappedOut()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);

            //if (CibelleStats.instance.HasPassiveSkill(new TSKPleasureResistant()))
            //    CibelleStats.instance.m_pleasure.ReduceBy(100 / 5);
        }
    }
}
