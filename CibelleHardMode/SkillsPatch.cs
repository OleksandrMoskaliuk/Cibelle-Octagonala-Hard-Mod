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
            __instance.damageBase = 12;
            __instance.damagePerLevel = 12;
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
            __instance.resistancePerOrgasm = 0.01f;
            __instance.resistancePerOrgasmPerLevel = 0.01f;
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
            ".\nThis bonus resets upon resting.",
             ".\nStacks up to 18% Pleasure Resistance."
            });
            var m_resMethod = HarmonyLib.AccessTools.MethodDelegate<System.Func<int, float>>(HarmonyLib.AccessTools.Method(typeof(TSKNeverTappedOut), "_res"), __instance);

            string text = UIManager.instance.ColorBlue("Pleasure Res. per orgasm: ") + "+" + Utilf.FloatToStringPercent(m_resMethod(0), "F0");
            string text2 = UIManager.instance.ColorBlue("Pleasure Res. per orgasm: ") + "+" + Utilf.FloatToStringPercent(m_resMethod(1), "F0");
            __instance.AppendCurrentLevel(__instance.ThisSkillLevel(), new string[] { text });
            __instance.AppendNextLevel(__instance.ThisSkillLevel(), new string[] { text2 });

            return false;
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::TSKNeverTappedOut), "PleasureResist")]
        [HarmonyPrefix]
        private static bool PleasureResistPatch(TSKNeverTappedOut __instance, ref int level, ref float __result)
        {
            float pl_resistance_bonus = (__instance.resistancePerOrgasm + __instance.resistancePerOrgasmPerLevel * (float)level) * (float)HStats.instance.timesClimaxedSinceResting;
            float max_possible_pl_resistance_buff = 18f;
            __result = UnityEngine.Mathf.Min(18f, max_possible_pl_resistance_buff);

            return false;
        }

    }


    internal class TSKOrgasmicResistance_ConstructorPatch
    {
        // TargetConstructor tells Harmony to look for the ctor instead of a regular method
        [HarmonyTargetMethod]
        private static System.Reflection.MethodBase TargetConstructor()
        {
            return AccessTools.Constructor(typeof(global::TSKOrgasmicResistance), new Type[0]);
        }

        [HarmonyPostfix]
        private static void Postfix(global::TSKOrgasmicResistance __instance)
        {
            __instance.resBase = 0.01f;
            __instance.resPerLevel = 0.01f;
        }

    }

    internal class TreeSkill_SetAvailability_Patch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::TreeSkill), "SetAvailability")]
        [HarmonyPrefix]
        private static bool SetAvailabilityPatch(TreeSkill __instance)
        {
            __instance.status = TreeSkillStatus.Unavailable;
            if (__instance.IsAcquired(__instance.name))
            {
                TreeSkill passiveSkill = CibelleStats.instance.GetPassiveSkill(__instance.name);
                if (passiveSkill != null)
                {
                    if (passiveSkill.currentlevel == passiveSkill.maxlevel)
                    {
                        __instance.status = TreeSkillStatus.Maxed;
                    }
                    if (passiveSkill.currentlevel > 0 && passiveSkill.currentlevel < passiveSkill.maxlevel)
                    {
                        __instance.status = TreeSkillStatus.Acquired;
                        return false;
                    }
                }
                else
                {
                    __instance.status = TreeSkillStatus.Available;
                }
                return false;
            }
            if (__instance.status != TreeSkillStatus.Acquired && CibelleStats.instance.skillPointsAvailable <= 0)
            {
                __instance.status = TreeSkillStatus.Unavailable;
                return false; ;
            }
            // && !__instance.IsAcquired(__instance.otherskill) < was removed , now you can get all skills
            if (__instance.status != TreeSkillStatus.Maxed && __instance.RequirementsMet() &&  __instance.IsAcquired(__instance.prevskills))
            {
                __instance.status = TreeSkillStatus.Available;
                return false;
            }
            return false;
        }
    }






}