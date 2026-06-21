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


    internal class TSKNeverTappedOut_ConstructorPatch
    {
        // TargetConstructor tells Harmony to look for the ctor instead of a regular method
        [HarmonyTargetMethod]
        private static System.Reflection.MethodBase TargetConstructor()
        {
            return AccessTools.Constructor(typeof(global::TSKNeverTappedOut), new Type[0]);
        }

        [HarmonyPostfix]
        private static void Postfix(global::TSKNeverTappedOut __instance)
        {
            __instance.resistancePerOrgasm = 0.03f;
            __instance.resistancePerOrgasmPerLevel = 0.02f;
        }

    }

    internal class TSKNeverTappedOut_DescriptionPatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::TSKNeverTappedOut), "UpdateDescription")]
        [HarmonyPrefix]
        public static bool TSKNeverTappedOut_UpdateDescriptionPatch(TSKNeverTappedOut __instance)
        {
            __instance.description = string.Concat(new string[]
            {
            __instance.name,
            "\n[ ",
            UIManager.instance.ColorBlue("Passive "),
            "] Each successive orgasm increases Cibelle's ",
            UIManager.instance.ColorBlue("Pleasure Resistance"),
            ".\nThis bonus resets after battle" // upon resting. -> after battle
            });
            var m_resMethod = HarmonyLib.AccessTools.MethodDelegate<System.Func<int, float>>(HarmonyLib.AccessTools.Method(typeof(TSKNeverTappedOut), "_res"), __instance);

            string text = UIManager.instance.ColorBlue("Pleasure Res. per orgasm: ") + "+" + Utilf.FloatToStringPercent(m_resMethod(0), "F0");
            string text2 = UIManager.instance.ColorBlue("Pleasure Res. per orgasm: ") + "+" + Utilf.FloatToStringPercent(m_resMethod(1), "F0");
            __instance.AppendCurrentLevel(__instance.ThisSkillLevel(), new string[] { text });
            __instance.AppendNextLevel(__instance.ThisSkillLevel(), new string[] { text2 });

            return false;
        }
    }




}