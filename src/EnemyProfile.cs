using System;
using System.Collections.Generic;
using System.Data;
using BepInEx.Configuration;
using cibelle_hard_mod;
using UnityEngine;


namespace CibelleHardMode.src
{
    public class EnemyProfile
    {
        private readonly Dictionary<EnemyType, EnemyProfile> m_profiles = new Dictionary<EnemyType, EnemyProfile>();

        public EnStats m_instance = null;

        // ---- Blueprint Definition Fields ----
        public EnemyType Type { get; private set; }

        public int MinLevel { get; private set; }
        public int MaxLevel { get; private set; }
        public int Reward { get; private set; }
        public int TimesToEjaculate { get; private set; }
        public float Attack { get; private set; }
        public float PleasureAttackMult { get; private set; }
        public float Speed { get; private set; }
        public float MaxStamina { get; private set; }
        public float MaxPleasure { get; private set; }
        public int Level { get; private set; }

        // Master manager layout blueprint constructor
        public EnemyProfile()
        {
            // -------------------------------------------------------------------------
            // ADJUST PERMANENT RULES & DEVIATIONS HERE
            // -------------------------------------------------------------------------
            //Register(new EnemyProfile(EnemyType.OldMan)
            //    .SetLevels(1, 3)
            //    .SetEssenceReward(250)
            //    .SetTimesToEjaculate(1)
            //    .SetAttack(15f)
            //    .SetPleasureAttackMultiplier(0.9f)
            //    .SetSpeed(2.5f)
            //    .SetMaxStamina(60f)
            //    .SetMaxPleasure(50f));

            //Register(new EnemyProfile(EnemyType.Villager)
            //    .SetLevels(1, 5)
            //    .SetEssenceReward(300)
            //    .SetTimesToEjaculate(1)
            //    .SetAttack(20f)
            //    .SetPleasureAttackMultiplier(1.1f)
            //    .SetSpeed(3.5f)
            //    .SetMaxStamina(90f)
            //    .SetMaxPleasure(85f));

            //Register(new EnemyProfile(EnemyType.Soldier)
            //    .SetLevels(1, 8)
            //    .SetEssenceReward(450)
            //    .SetTimesToEjaculate(1)
            //    .SetAttack(25f)
            //    .SetPleasureAttackMultiplier(1.2f)
            //    .SetSpeed(4.5f)
            //    .SetMaxStamina(160f)
            //    .SetMaxPleasure(100f));

            //Register(new EnemyProfile(EnemyType.Bandit)
            //    .SetLevels(1, 10)
            //    .SetEssenceReward(450)
            //    .SetTimesToEjaculate(1)
            //    .SetAttack(30f)
            //    .SetPleasureAttackMultiplier(1.3f)
            //    .SetSpeed(6f)
            //    .SetMaxStamina(160f)
            //    .SetMaxPleasure(110f));

            //Register(new EnemyProfile(EnemyType.Roughman)
            //    .SetLevels(1, 12)
            //    .SetEssenceReward(750)
            //    .SetTimesToEjaculate(2)
            //    .SetAttack(40f)
            //    .SetPleasureAttackMultiplier(2.2f)
            //    .SetSpeed(6f)
            //    .SetMaxStamina(230f)
            //    .SetMaxPleasure(130f));

            //Register(new EnemyProfile(EnemyType.Barroso)
            //    .SetLevels(3, 14)
            //    .SetEssenceReward(1200)
            //    .SetTimesToEjaculate(3)
            //    .SetAttack(60f)
            //    .SetPleasureAttackMultiplier(2.8f)
            //    .SetSpeed(10f)
            //    .SetMaxStamina(450f)
            //    .SetMaxPleasure(180f));

            //Register(new EnemyProfile(EnemyType.Goblin)
            //    .SetLevels(1, 10)
            //    .SetEssenceReward(900)
            //    .SetTimesToEjaculate(2)
            //    .SetAttack(45f)
            //    .SetPleasureAttackMultiplier(2.5f)
            //    .SetSpeed(18f)
            //    .SetMaxStamina(250f)
            //    .SetMaxPleasure(150f));

            //Register(new EnemyProfile(EnemyType.Orc)
            //    .SetLevels(2, 12)
            //    .SetEssenceReward(1200)
            //    .SetTimesToEjaculate(3)
            //    .SetAttack(60f)
            //    .SetPleasureAttackMultiplier(3.0f)
            //    .SetSpeed(15f)
            //    .SetMaxStamina(420f)
            //    .SetMaxPleasure(200f));

            //Register(new EnemyProfile(EnemyType.Werewolf)
            //    .SetLevels(3, 14)
            //    .SetEssenceReward(1400)
            //    .SetTimesToEjaculate(3)
            //    .SetAttack(65f)
            //    .SetPleasureAttackMultiplier(3.4f)
            //    .SetSpeed(18f)
            //    .SetMaxStamina(380f)
            //    .SetMaxPleasure(220f));

            //Register(new EnemyProfile(EnemyType.Drakkma)
            //    .SetLevels(4, 16)
            //    .SetEssenceReward(2200)
            //    .SetTimesToEjaculate(4)
            //    .SetAttack(80f)
            //    .SetPleasureAttackMultiplier(4.2f)
            //    .SetSpeed(20f)
            //    .SetMaxStamina(800f)
            //    .SetMaxPleasure(300f));

            //Register(new EnemyProfile(EnemyType.Baron)
            //    .SetLevels(5, 20)
            //    .SetEssenceReward(3500)
            //    .SetTimesToEjaculate(4)
            //    .SetAttack(85f)
            //    .SetPleasureAttackMultiplier(5.1f)
            //    .SetSpeed(23f)
            //    .SetMaxStamina(950f)
            //    .SetMaxPleasure(350f));
        }

        // Dynamic specific combatant configuration constructor
        public EnemyProfile(EnemyType m_type)
        {
            Type = m_type;
            MinLevel = 1; MaxLevel = 2; Reward = 250; TimesToEjaculate = 1;
            Attack = 10.0f; PleasureAttackMult = 1.0f; Speed = 1.0f; MaxStamina = 100.0f; MaxPleasure = 100.0f;
        }

        private void Register(EnemyProfile m_profile) { m_profiles[m_profile.Type] = m_profile; }
        public EnemyProfile Get(EnemyType m_type) => m_profiles.TryGetValue(m_type, out var m_p) ? m_p : new EnemyProfile(m_type);

        // ---- Separated Fluent Setters ----
        public EnemyProfile SetLevels(int m_min, int m_max) { MinLevel = m_min; MaxLevel = m_max; return this; }
        public EnemyProfile SetEssenceReward(int m_reward) { Reward = m_reward; return this; }
        public EnemyProfile SetTimesToEjaculate(int m_times) { TimesToEjaculate = m_times; return this; }
        public EnemyProfile SetAttack(float m_val) { Attack = m_val; return this; }
        public EnemyProfile SetPleasureAttackMultiplier(float m_val) { PleasureAttackMult = m_val; return this; }
        public EnemyProfile SetSpeed(float m_val) { Speed = m_val; return this; }
        public EnemyProfile SetMaxStamina(float m_val) { MaxStamina = m_val; return this; }
        public EnemyProfile SetMaxPleasure(float m_val) { MaxPleasure = m_val; return this; }

        // ---- Dynamic Configuration Generation Engine ----
        public void InitializeConfigurableProfiles(ConfigFile m_config)
        {
            // Process configuration pipeline registration across all 11 default values
            Register(BindProfileConfig(m_config, EnemyType.OldMan,   1,   3,  250,  1,  15f,  0.9f,  2.5f,   60f,    50f));
            Register(BindProfileConfig(m_config, EnemyType.Villager, 1,   5,  300,  1,  20f,  1.1f,  3.5f,   90f,    85f));
            Register(BindProfileConfig(m_config, EnemyType.Soldier,  1,   8,  450,  1,  25f,  1.2f,  4.5f,  160f,   100f));
            Register(BindProfileConfig(m_config, EnemyType.Bandit,   1,  10,  450,  1,  30f,  1.3f,    6f,  160f,   110f));
            Register(BindProfileConfig(m_config, EnemyType.Roughman, 1,  12,  750,  2,  40f,  2.2f,    6f,  230f,   130f));
            Register(BindProfileConfig(m_config, EnemyType.Barroso,  3,  14, 1200,  3,  60f,  2.8f,   10f,  450f,   180f));
            Register(BindProfileConfig(m_config, EnemyType.Goblin,   1,  10,  900,  2,  45f,  2.5f,   18f,  250f,   150f));
            Register(BindProfileConfig(m_config, EnemyType.Orc,      2,  12, 1200,  3,  60f,  3.0f,   15f,  420f,   200f));
            Register(BindProfileConfig(m_config, EnemyType.Werewolf, 3,  14, 1400,  3,  65f,  3.4f,   18f,  380f,   220f));
            Register(BindProfileConfig(m_config, EnemyType.Drakkma,  4,  16, 2200,  4,  80f,  4.2f,   20f,  800f,   300f));
            Register(BindProfileConfig(m_config, EnemyType.Baron,    5,  20, 3500,  4,  85f,  5.1f,   23f,  950f,   350f));
        }

        private EnemyProfile BindProfileConfig(ConfigFile m_config, EnemyType m_type, int m_defMinLvl, int m_defMaxLvl, int m_defReward, int m_defEjac, float m_defAtk, float m_defPlsMult, float m_defSpd, float m_defStam, float m_defPls)
        {
            string m_section = $"Enemy Profile - {m_type}";

            int m_cfgMinLvl = m_config.Bind(m_section, "Min Level", m_defMinLvl, $"Minimum base spawn level for {m_type}").Value;
            int m_cfgMaxLvl = m_config.Bind(m_section, "Max Level", m_defMaxLvl, $"Maximum base spawn level for {m_type}").Value;
            int m_cfgReward = m_config.Bind(m_section, "Essence Reward", m_defReward, $"Base essence reward given by {m_type}").Value;
            int m_cfgEjac = m_config.Bind(m_section, "Times To Ejaculate", m_defEjac, $"Base orgasm iterations required to defeat {m_type}").Value;
            float m_cfgAtk = m_config.Bind(m_section, "Attack Damage", m_defAtk, $"Base physical combat attack damage for {m_type}").Value;
            float m_cfgPlsMult = m_config.Bind(m_section, "Pleasure Attack Multiplier", m_defPlsMult, $"Base sexual damage multiplier for {m_type}").Value;
            float m_cfgSpd = m_config.Bind(m_section, "Movement Speed", m_defSpd, $"Base action speed for {m_type}").Value;
            float m_cfgStam = m_config.Bind(m_section, "Max Stamina", m_defStam, $"Base stamina capacity pool for {m_type}").Value;
            float m_cfgPls = m_config.Bind(m_section, "Max Pleasure", m_defPls, $"Base maximum pleasure ceiling for {m_type}").Value;

            return new EnemyProfile(m_type)
                .SetLevels(m_cfgMinLvl, m_cfgMaxLvl)
                .SetEssenceReward(m_cfgReward)
                .SetTimesToEjaculate(m_cfgEjac)
                .SetAttack(m_cfgAtk)
                .SetPleasureAttackMultiplier(m_cfgPlsMult)
                .SetSpeed(m_cfgSpd)
                .SetMaxStamina(m_cfgStam)
                .SetMaxPleasure(m_cfgPls);
        }

        // ---- Instance Generation Engine ----
        public void RollInstance()
        {
            if (m_instance == null)
                return;

            EnemyType eType = this.m_instance.enemyType;

            UnityEngine.Random.InitState(Guid.NewGuid().GetHashCode());

            this.Level = UnityEngine.Random.Range(Get(eType).MinLevel, Get(eType).MaxLevel);
            float m_levelProgress = Level;
            float m_statCurveMultiplier = 1.0f + 0.078f * MathF.Pow(m_levelProgress, 1.35f);

            // =========================================================================
            // DYNAMIC SCALED PARAMETER ASSIGNMENTS
            // =========================================================================
            Attack = Get(eType).Attack * m_statCurveMultiplier * Plugin.CustomFloatRandomWalk(1, 0.35f);
            Speed = Get(eType).Speed * m_statCurveMultiplier * Plugin.CustomFloatRandomWalk(1, 0.35f);
            MaxStamina = Get(eType).MaxStamina * m_statCurveMultiplier * Plugin.CustomFloatRandomWalk(1, 0.35f);
            PleasureAttackMult = Get(eType).PleasureAttackMult * m_statCurveMultiplier * Plugin.CustomFloatRandomWalk(1, 0.35f);
            MaxPleasure = Get(eType).MaxPleasure * m_statCurveMultiplier * Plugin.CustomFloatRandomWalk(1, 0.35f);
            TimesToEjaculate = UnityEngine.Mathf.Max((int)(Get(eType).TimesToEjaculate * m_statCurveMultiplier * Plugin.CustomFloatRandomWalk(1, 0.35f)), 1);

            float m_atkRatio = Attack / Get(eType).Attack;
            float m_pAtkRatio = PleasureAttackMult / Get(eType).PleasureAttackMult;
            float m_spdRatio = Speed / Get(eType).Speed;
            float m_stamRatio = MaxStamina / Get(eType).MaxStamina;
            float m_pleasRatio = MaxPleasure / Get(eType).MaxPleasure;
            float m_ejRatio = TimesToEjaculate / (float)Get(eType).TimesToEjaculate;
            float m_levelFactor = Level / (float)Get(eType).MaxLevel;
            float m_averageRatio = (m_atkRatio + m_pAtkRatio + m_spdRatio + m_stamRatio + m_pleasRatio + m_ejRatio + m_levelFactor) / 7f;

            Reward = 0;

            if (GameManager.instance.GetComponent<DayNightCycle>().IsNight())
                Reward = (int)(Get(eType).Reward * m_averageRatio * 1.25f);
            else
                Reward = (int)(Get(eType).Reward * m_averageRatio);

            // Enemy level to display
            Level = UnityEngine.Mathf.Max((int)(Level * m_averageRatio), 1);

            if (true) 
            {
                UnityEngine.Debug.LogWarning($"====== [CIBELLE HARD MOD] LIVE ROLL ENGINE DEBUG ======");
                UnityEngine.Debug.Log($"Enemy Spawning Type : {eType}");
                UnityEngine.Debug.Log($"Dynamic Final Level : {this.Level} (Initial Walk Progress Roll: {m_levelProgress + 1})");
                UnityEngine.Debug.Log($"Stat Curve Multiplier : {m_statCurveMultiplier:F4}");
                UnityEngine.Debug.Log($"Calculated Instance Intensity Ratio : {m_averageRatio * 100f:F1}%");
                UnityEngine.Debug.Log($"-------------------------------------------------------");
                UnityEngine.Debug.Log($"Attack                : {this.Attack:F2}  (Base blueprint: {Get(eType).Attack:F2})");
                UnityEngine.Debug.Log($"Pleasure Attack Mult  : {this.PleasureAttackMult:F2}  (Base blueprint: {Get(eType).PleasureAttackMult:F2})");
                UnityEngine.Debug.Log($"Speed                 : {this.Speed:F2}  (Base blueprint: {Get(eType).Speed:F2})");
                UnityEngine.Debug.Log($"Max Stamina           : {this.MaxStamina:F1}  (Base blueprint: {Get(eType).MaxStamina:F1})");
                UnityEngine.Debug.Log($"Max Pleasure          : {this.MaxPleasure:F1}  (Base blueprint: {Get(eType).MaxPleasure:F1})");
                UnityEngine.Debug.Log($"Times To Ejaculate    : {this.TimesToEjaculate}  (Base blueprint: {Get(eType).TimesToEjaculate})");
                UnityEngine.Debug.Log($"Essence Reward Output : {this.Reward}  (Base blueprint: {Get(eType).Reward})");
                UnityEngine.Debug.Log($"=======================================================");
            }

        }

        public static float RunRandomWalk(float m_base, float m_dev)
        {
            return m_base + UnityEngine.Random.Range(-m_dev, m_dev);
        }
    }


}

// --- FORMULA ---
//Multiplier
//  (%)
//  300% |                                               / [Exponential: Explodes upward]
//       |                                             /
//  250% |                                         * <-- Level 10 Target (250%)
//       |                                      * /
//  200% |                                   * /
//       |                                 * /
//  170% |                         * <----------- <-- Level 5 Target (170%)
//       |                       * /
//  110% |             * * <-------------------- <-- Level 2 Target (110%)
//  100% |       * * /________________/________
//       |_______________________________________
//       0       1  2  3  4  5  6  7  8  9  10    Enemy Level
//
// Calculate our smooth, non-exponential growth multiplier curve
// float m_levelProgress = (float)(this.CurrentLevel - 1);
// float m_statCurveMultiplier = 1.0f + 0.078f * MathF.Pow(m_levelProgress, 1.35f);