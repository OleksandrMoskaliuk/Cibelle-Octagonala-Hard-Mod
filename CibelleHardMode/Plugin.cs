using System;
using HarmonyLib;
using UnityEngine;

namespace cibelle_hard_mod
{
    [global::BepInEx.BepInPlugin("CibelleHardMod", "[twitter @Dru9Dealer] Cibelle_Hard_Mod", "1.0.0")]
    [global::BepInEx.BepInProcess("Cibelle.exe")]
    public class Plugin : BepInEx.BaseUnityPlugin
    {
        private void Awake()
        {
            Difficulty = base.Config.Bind<String>("Game Difficulty", "Difficulty", "hard", "vanilla , normal , hard");
            Log = base.Logger;
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(CibelleStatPatch), null);
            LoggerMessage01 = "[Twitter @Dru9Dealer] Cibelle Hard Mod";
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
            if (LogDat1.TimeRamained > 0)
            {
                UnityEngine.GUI.Box(LogDat1.rectangle, " " + LoggerMessage01);
                LogDat1.LastMessage = LoggerMessage01;
                LogDat1.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage01.Equals(LogDat1.LastMessage))
            {
                // Prevents messages flickering
                if (LogDat1.TimeRamained > (20f - 0.8f))
                {
                    LoggerMessage01 = LogDat1.LastMessage;
                }
                else
                {
                    LogDat1.TimeRamained = 20f;
                }
            }
        }

        public Plugin()
        {
            // Logger 01
            LogDat1.LastMessage = LoggerMessage01;
            UnityEngine.Random.InitState(117411);
            float x = UnityEngine.Random.Range(10, 350);
            float y = UnityEngine.Random.Range(10, 350);
            LogDat1.rectangle = new global::UnityEngine.Rect(10f + 350f, 10, 350f, 24f);
        }
        
        public static global::BepInEx.Configuration.ConfigEntry<string> Difficulty;

        internal static global::BepInEx.Logging.ManualLogSource Log;

        // Logger 01
        public static string LoggerMessage01;
        private LogData LogDat1;

    }
    // Data for Logging messages
    public struct LogData
    {
        public Rect rectangle;
        public string LastMessage;
        public float TimeRamained;
    }
}
