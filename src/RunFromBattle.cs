using System;
using HarmonyLib;
using UnityEngine;


namespace CibelleHardMode.src
{
    internal class TryToRunFromBattle_Patch
    {
        private static readonly object m_rollLock = new object();
        [HarmonyPatch(typeof(CibelleBattleActions), "TryToRunFromBattle")]
        [HarmonyPrefix]
        public static bool TryToRunFromBattle_PrefixPatch(CibelleBattleActions __instance, ref int __result) 
        {
            if (BattleManager.instance.AllEnemiesDead())
                __result =  4;
            
            if (!BattleManager.instance.CurrentEnemy().canBeEscapedFrom)
                __result = 2;
            
            if (BattleManager.instance.IsInHState())
                __result = 3;
            
            int DamageFromEscape = (int)BattleManager.instance.CurrentEnemy().m_attackdamage;
            CibelleStats.instance.TakeDamage(DamageFromEscape);
            __result = 0;
            //Debug.Log(__instance.GetType().ToString() + ": INFO -- Chance to escape from battle: " + num2.ToString() + ".");
            return false;
        }
    }
}
