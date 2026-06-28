using System;
using CibelleHardMode.src;
using HarmonyLib;
using UnityEngine;


namespace cibelle_hard_mod
{
    [global::BepInEx.BepInPlugin("CibelleHardMod", "[X/Twitter @Dru9Dealer] Cibelle_Hard_Mod", "1.2.0")]
    [global::BepInEx.BepInProcess("Cibelle.exe")]
    public class Plugin : BepInEx.BaseUnityPlugin
    {
        public static global::BepInEx.Configuration.ConfigEntry<string> Difficulty;
        internal static global::BepInEx.Logging.ManualLogSource Log;
        public static string LoggerMessage01;

        private LogData m_LogDat1;

        private void Awake()
        {
            Log = base.Logger;
            m_Enemy = new EnemyProfile();

            // Initialize the logging data object
            m_LogDat1 = new LogData();
            LoggerMessage01 = "[X/Twitter @Dru9Dealer] Cibelle Hard Mod";
            m_LogDat1.LastMessage = LoggerMessage01;
            m_LogDat1.TimeRamained = 20f;

            // Starting stats
            Difficulty = base.Config.Bind<string>("Game Difficulty, just gives few extra stats points", "Difficulty", "hard", "vanilla, normal, hard");
            // Automatically build, bind, and register all 11 configurables from local config
            m_Enemy.InitializeConfigurableProfiles(base.Config);


            // Setup random layout rules if needed
            UnityEngine.Random.InitState(117411);
            m_LogDat1.rectangle = new global::UnityEngine.Rect(10f, 10f, 350f, 24f);

            // Apply the Harmony patches
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(CibelleStatPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(CibelleActionPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(EnemyPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(WinEventReferencePatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(BattleCleanupPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(AttackDamageEnemyMultiplier), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(CibelleAttackDebuff), null);
            //global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(PleasureDamageMod), null);


            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(FortifyTechConstructorPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(MasochismTechConstructorPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(TSKNeverTappedOut_ConstructorPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(TSKNeverTappedOut_DescriptionPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(TSKConstancy_ConstructorPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(TSKOrgasmicResistance_ConstructorPatch), null); // Less resistance making game far more diffucult
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(TreeSkill_SetAvailability_Patch), null); // Remove skill lock

            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(TSKPleasureResistance_ConstructorPatch), null); // Remove skill lock
        }

        private void Update()
        {
        }

        private void OnGUI()
        {
            HandleLoggers(true);
        }

        private void HandleLoggers(bool on)
        {
            if (!on) { return; }

            // Logger 01            
            if (m_LogDat1.TimeRamained > 0)
            {
                UnityEngine.GUI.Box(m_LogDat1.rectangle, " " + LoggerMessage01);
                m_LogDat1.LastMessage = LoggerMessage01;
                m_LogDat1.TimeRamained -= UnityEngine.Time.deltaTime;
            }

            // Update time if new message was assigned
            if (!LoggerMessage01.Equals(m_LogDat1.LastMessage))
            {
                // Prevents messages flickering
                if (m_LogDat1.TimeRamained > (20f - 0.8f))
                {
                    LoggerMessage01 = m_LogDat1.LastMessage;
                }
                else
                {
                    m_LogDat1.TimeRamained = 20f;
                }
            }
        }

        public static float NormalizeFactor(float value, float minPossible, float maxPossible)
        {
            // Prevent divide-by-zero errors if min and max happen to be identical
            if (Mathf.Approximately(minPossible, maxPossible))
            {
                return 1.0f;
            }

            // 1. Linearly scale the raw value between 0.0 and 1.0
            float normalized = (value - minPossible) / (maxPossible - minPossible);

            // 2. Clamp the value to ensure rogue numbers don't push outside the boundaries
            normalized = Mathf.Clamp01(normalized);

            // 3. Remap from [0.0, 1.0] to your custom [0.1, 1.0] target range
            return 0.1f + (normalized * 0.9f);
        }

        public static float CustomFloatRandomWalk(float val, float stepSize)
        {
            // Generates a random multiplier drifting between (1 - stepSize) and (1 + stepSize)
            float m_randomStep = UnityEngine.Random.Range(1f - stepSize, 1f + stepSize);
            return val * m_randomStep;
        }

        public static EnemyProfile m_Enemy = null;

    }

    // Data container class for UI logging
    public class LogData
    {
        public Rect rectangle;
        public string LastMessage = string.Empty;
        public float TimeRamained;
    }

}