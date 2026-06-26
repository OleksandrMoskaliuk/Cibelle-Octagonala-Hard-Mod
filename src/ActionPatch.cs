
//if (this.ClothesHP() > 85)
//{
//    return 0f; // almost new
//}
//if (this.ClothesHP() > 55)
//{
//    return 1f; // torn
//}
//if (this.ClothesHP() > 30)
//{
//    return 2f; // highly damaged
//}
//if (this.ClothesHP() > 0)
//{
//    return 3f; // Compleatly torn clothes/naked
//}
//return 4f; // Naked

using System;
using HarmonyLib;
using UnityEngine;

namespace CibelleHardMode.src
{
    internal class CibelleActionPatch
    {
        [HarmonyPatch(typeof(HRequirements), "CanKiss")]
        [HarmonyPrefix]
        private static bool override_can_kiss(ref bool __result)
        {
            __result = 
                CibelleStats.instance.MouthHeat() >= HRequirements.KissHR() &&
                CibelleStats.instance.Corruption() >= 15;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanHandjob")]
        [HarmonyPrefix]
        private static bool override_can_handjob(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 1f &&
                CibelleStats.instance.MouthHeat() >= HRequirements.HandjobHR() &&
                CibelleStats.instance.Corruption() >= 30;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanPaizuri")]
        [HarmonyPrefix]
        private static bool override_can_paizuri(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 2f &&
                CibelleStats.instance.MouthHeat() >= HRequirements.PaizuriHR() &&
                CibelleStats.instance.Corruption() >= 50;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanFellatio")]
        [HarmonyPrefix]
        private static bool override_can_fellatio(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 1f &&
                CibelleStats.instance.MouthHeat() >= HRequirements.FellatioHR() &&
                CibelleStats.instance.Corruption() >= 50;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanDeepthroat")]
        [HarmonyPrefix]
        private static bool override_can_deepthroat(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 1f &&
                CibelleStats.instance.MouthHeat() >= HRequirements.DeepthroatHR() &&
                CibelleStats.instance.Corruption() >= 75;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanHotdogging")]
        [HarmonyPrefix]
        private static bool override_can_hotdogging(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 2f &&
                CibelleStats.instance.AssHeat() >= HRequirements.HotdoggingHR() &&
                CibelleStats.instance.Corruption() >= 50;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanAnal")]
        [HarmonyPrefix]
        private static bool override_can_anal(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 3f &&
                CibelleStats.instance.AssHeat() >= HRequirements.AnalHR() &&
                CibelleStats.instance.Corruption() >= 50;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanVaginal")]
        [HarmonyPrefix]
        private static bool override_can_vaginal(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 3f &&
                CibelleStats.instance.PussyHeat() >= HRequirements.VaginalHR() &&
                CibelleStats.instance.Corruption() >= 75;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanThighSex")]
        [HarmonyPrefix]
        private static bool override_can_thigh_sex(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 1f &&
                CibelleStats.instance.PussyHeat() >= HRequirements.ThighSexHR() &&
                CibelleStats.instance.Corruption() >= 40;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanBreastGrab")]
        [HarmonyPrefix]
        private static bool override_can_breast_grab(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 1f &&
                CibelleStats.instance.MouthHeat() >= HRequirements.CalculateHeatReq(15);
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanBreastSuck")]
        [HarmonyPrefix]
        private static bool override_can_breast_suck(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 1f &&
                CibelleStats.instance.MouthHeat() >= HRequirements.CalculateHeatReq(40) &&
                CibelleStats.instance.Corruption() >= 25;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanAssSlap")]
        [HarmonyPrefix]
        private static bool override_can_ass_slap(ref bool __result)
        {
            __result = CibelleStats.instance.AssHeat() >= 0;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanAssGrab")]
        [HarmonyPrefix]
        private static bool override_can_ass_grab(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 0f &&
                CibelleStats.instance.AssHeat() >= HRequirements.CalculateHeatReq(10);
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanAssLick")]
        [HarmonyPrefix]
        private static bool override_can_ass_lick(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 1f &&
                CibelleStats.instance.AssHeat() >= HRequirements.CalculateHeatReq(30) &&
                CibelleStats.instance.Corruption() >= 30;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanAnalLick")]
        [HarmonyPrefix]
        private static bool override_can_anal_lick(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 2f &&
                CibelleStats.instance.AssHeat() >= HRequirements.CalculateHeatReq(50) &&
                CibelleStats.instance.Corruption() >= 40;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanAnalFingering")]
        [HarmonyPrefix]
        private static bool override_can_anal_fingering(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 2f &&
                CibelleStats.instance.AssHeat() >= HRequirements.CalculateHeatReq(60) &&
                CibelleStats.instance.Corruption() >= 40;
            return false;
        }

        [HarmonyPatch(typeof(HRequirements), "CanVaginalFingering")]
        [HarmonyPrefix]
        private static bool override_can_vaginal_fingering(ref bool __result)
        {
            __result = CibelleStats.instance.GetClothesRipValue() >= 3f &&
                CibelleStats.instance.PussyHeat() >= HRequirements.CalculateHeatReq(60) &&
                CibelleStats.instance.Corruption() >= 40;
            return false;
        }
    }
}