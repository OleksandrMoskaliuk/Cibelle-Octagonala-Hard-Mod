using System;
using HarmonyLib;
using UnityEngine;

namespace cibelle_hard_mod
{
    [global::BepInEx.BepInPlugin("CibelleHardMod", "[twitter @Dru9Dealer] Cibelle_Hard_Mod", "1.0.0")]
    [global::BepInEx.BepInProcess("Cibelle.exe")]
    public class Plugin : BepInEx.BaseUnityPlugin
    {
        public static global::BepInEx.Configuration.ConfigEntry<string> Difficulty;
        internal static global::BepInEx.Logging.ManualLogSource Log;
        public static string LoggerMessage01;

        private LogData m_LogDat1;

        private void Awake()
        {
            Difficulty = base.Config.Bind<string>("Game Difficulty", "Difficulty", "hard", "vanilla, normal, hard");
            Log = base.Logger;

            // Initialize the logging data object
            m_LogDat1 = new LogData();
            LoggerMessage01 = "[Twitter @Dru9Dealer] Cibelle Hard Mod";
            m_LogDat1.LastMessage = LoggerMessage01;
            m_LogDat1.TimeRamained = 20f;

            // Setup random layout rules if needed
            UnityEngine.Random.InitState(117411);
            m_LogDat1.rectangle = new global::UnityEngine.Rect(10f, 10f, 350f, 24f);

            // Apply the Harmony patches
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(CibelleStatPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(CibelleActionPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(EnemyPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(WinEventReferencePatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(BattleCleanupPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(FortifyTechConstructorPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(MasochismTechConstructorPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(AttackDamageEnemyMultiplier), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(CibelleAttackDebuff), null);
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

        public static int BattleReward = 0; 

    }

    // Data container class for UI logging
    public class LogData
    {
        public Rect rectangle;
        public string LastMessage = string.Empty;
        public float TimeRamained;
    }

}