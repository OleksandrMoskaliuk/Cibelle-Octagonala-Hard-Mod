using System;
using HarmonyLib;
using UnityEngine;


namespace cibelle_hard_mod
{


    internal class CibelleStatPatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::CibelleStats), "Awake")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void ChangeCamerazoom(CibelleStats __instance)
        {
            __instance.vit = 10;
            __instance.agi = 2;
            __instance.fth = 6;
            __instance.cha = 10;
        }

        public CibelleStatPatch() 
        { }
    }
}
