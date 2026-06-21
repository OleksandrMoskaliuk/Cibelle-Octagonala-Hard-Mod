using System;
using System.Buffers.Text;
using HarmonyLib;
using UnityEngine;

namespace cibelle_hard_mod
{
    internal class FortifyTechConstructorPatch
    {
        // TargetConstructor tells Harmony to look for the ctor instead of a regular method
        [HarmonyTargetMethod]
        private static System.Reflection.MethodBase TargetConstructor()
        {
            return AccessTools.Constructor(typeof(global::FortifyTech), new Type[0]);
        }

        [HarmonyPostfix]
        private static void Postfix(global::FortifyTech __instance)
        {
            __instance.duration = 4;
        }
    }

    internal class MasochismTechConstructorPatch
    {
        // TargetConstructor tells Harmony to look for the ctor instead of a regular method
        [HarmonyTargetMethod]
        private static System.Reflection.MethodBase TargetConstructor()
        {
            return AccessTools.Constructor(typeof(global::TSKMasochism), new Type[0]);
        }

        [HarmonyPostfix]
        private static void Postfix(global::TSKMasochism __instance)
        {
            __instance.damageBase = 8;
            __instance.damagePerLevel = 6;
        }
    }
}