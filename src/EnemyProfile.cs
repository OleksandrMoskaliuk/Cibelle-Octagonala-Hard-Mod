using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


namespace CibelleHardMode.src
{
    public class EnemyProfile
    {
        private readonly Dictionary<EnemyType, EnemyProfile> m_profiles = new Dictionary<EnemyType, EnemyProfile>();

        public EnemyProfile()
        {
            // -------------------------------------------------------------------------
            // ADJUST PERMANENT RULES & DEVIATIONS HERE
            // -------------------------------------------------------------------------
            Register(new EnemyProfile(EnemyType.OldMan)
                .SetLevels(1, 3)
                .SetEssenceReward(250)
                .SetTimesToEjaculate(1)
                .SetAttack(15f)
                .SetPleasureAttackMultiplier(0.9f)
                .SetSpeed(2.5f)
                .SetMaxStamina(60f)
                .SetMaxPleasure(50f));

            Register(new EnemyProfile(EnemyType.Villager)
                .SetLevels(1, 5)
                .SetEssenceReward(300)
                .SetTimesToEjaculate(1)
                .SetAttack(20f)
                .SetPleasureAttackMultiplier(1.2f)
                .SetSpeed(3.5f)
                .SetMaxStamina(90f)
                .SetMaxPleasure(85f));

            Register(new EnemyProfile(EnemyType.Soldier)
                .SetLevels(1, 8)
                .SetEssenceReward(450)
                .SetTimesToEjaculate(2)
                .SetAttack(25f)
                .SetPleasureAttackMultiplier(1.4f)
                .SetSpeed(4.5f)
                .SetMaxStamina(160f)
                .SetMaxPleasure(100f));

            Register(new EnemyProfile(EnemyType.Bandit)
                .SetLevels(1, 10)
                .SetEssenceReward(450)
                .SetTimesToEjaculate(2)
                .SetAttack(30f)
                .SetPleasureAttackMultiplier(1.5f)
                .SetSpeed(5.0f)
                .SetMaxStamina(200f)
                .SetMaxPleasure(110f));

            Register(new EnemyProfile(EnemyType.Roughman)
                .SetLevels(1, 12)
                .SetEssenceReward(750)
                .SetTimesToEjaculate(3)
                .SetAttack(40f)
                .SetPleasureAttackMultiplier(2.1f)
                .SetSpeed(8f)
                .SetMaxStamina(300f)
                .SetMaxPleasure(130f));

            Register(new EnemyProfile(EnemyType.Barroso)
                .SetLevels(3, 14)
                .SetEssenceReward(1200)
                .SetTimesToEjaculate(4)
                .SetAttack(60f)
                .SetPleasureAttackMultiplier(4.2f)
                .SetSpeed(10f)
                .SetMaxStamina(450f)
                .SetMaxPleasure(180f));

            Register(new EnemyProfile(EnemyType.Goblin)
                .SetLevels(1, 10)
                .SetEssenceReward(900)
                .SetTimesToEjaculate(2)
                .SetAttack(30f)
                .SetPleasureAttackMultiplier(4.0f)
                .SetSpeed(22f)
                .SetMaxStamina(250f)
                .SetMaxPleasure(150f));

            Register(new EnemyProfile(EnemyType.Orc)
                .SetLevels(2, 12)
                .SetEssenceReward(1200)
                .SetTimesToEjaculate(3)
                .SetAttack(60f)
                .SetPleasureAttackMultiplier(4.9f)
                .SetSpeed(15f)
                .SetMaxStamina(420f)
                .SetMaxPleasure(200f));

            Register(new EnemyProfile(EnemyType.Werewolf)
                .SetLevels(3, 14)
                .SetEssenceReward(1400)
                .SetTimesToEjaculate(4)
                .SetAttack(65f)
                .SetPleasureAttackMultiplier(5.2f)
                .SetSpeed(18f)
                .SetMaxStamina(380f)
                .SetMaxPleasure(220f));

            Register(new EnemyProfile(EnemyType.Drakkma)
                .SetLevels(4, 16)
                .SetEssenceReward(2200)
                .SetTimesToEjaculate(5)
                .SetAttack(80f)
                .SetPleasureAttackMultiplier(6.4f)
                .SetSpeed(20f)
                .SetMaxStamina(800f)
                .SetMaxPleasure(300f));

            Register(new EnemyProfile(EnemyType.Baron)
                .SetLevels(5, 20)
                .SetEssenceReward(3500)
                .SetTimesToEjaculate(6)
                .SetAttack(85f)
                .SetPleasureAttackMultiplier(7.2f)
                .SetSpeed(23f)
                .SetMaxStamina(950f)
                .SetMaxPleasure(350f));
        }

        private void Register(EnemyProfile m_profile) { m_profiles[m_profile.Type] = m_profile; }
        public EnemyProfile Get(EnemyType m_type) => m_profiles.TryGetValue(m_type, out var m_p) ? m_p : new EnemyProfile(m_type);

        // ---- Blueprint Definition Fields ----
        public EnemyType Type { get; private set; }
        public int MinLevel { get; private set; }
        public int MaxLevel { get; private set; }
        public int Reward { get; private set; }
        public int TimesToEjaculate { get; private set; }
        public float Attack { get; private set; }
        public float PleasureAttackMult { get; private set; }
        public float MaxSpeed { get; private set; }
        public float MaxStamina { get; private set; }
        public float MaxPleasure { get; private set; }
        public int Level { get; private set; }

        // ---- Blueprint Default Constructor ----
        public EnemyProfile(EnemyType m_type)
        {
            Type = m_type;
            MinLevel = 1; MaxLevel = 1; Reward = 1; TimesToEjaculate = 1;
            Attack = 1.0f; PleasureAttackMult = 1.0f; MaxSpeed = 1.0f; MaxStamina = 1.0f; MaxPleasure = 1.0f;
        }

        // ---- Separated Fluent Setters ----
        public EnemyProfile SetLevels(int m_min, int m_max) { MinLevel = m_min; MaxLevel = m_max; return this; }
        public EnemyProfile SetEssenceReward(int m_reward) { Reward = m_reward; return this; }
        public EnemyProfile SetTimesToEjaculate(int m_times) { TimesToEjaculate = m_times; return this; }
        public EnemyProfile SetAttack(float m_val) { Attack = m_val; return this; }
        public EnemyProfile SetPleasureAttackMultiplier(float m_val) { PleasureAttackMult = m_val; return this; }
        public EnemyProfile SetSpeed(float m_val) { MaxSpeed = m_val; return this; }
        public EnemyProfile SetMaxStamina(float m_val) { MaxStamina = m_val; return this; }
        public EnemyProfile SetMaxPleasure(float m_val) { MaxPleasure = m_val; return this; }

        // ---- Instance Generation Engine ----
        // Call this method whenever a specific combatant spawns to populate the "Current" stat fields.
        public void RollInstance(EnemyType eType)
        {
            
            UnityEngine.Random.InitState(Guid.NewGuid().GetHashCode());

            this.Level = UnityEngine.Random.Range(Get(eType).MinLevel, Get(eType).MaxLevel);
            float m_levelProgress = Level - 1;
            float m_statCurveMultiplier = 1.0f + 0.078f * MathF.Pow(m_levelProgress, 1.35f);

            Attack = Get(eType).Attack + RunRandomWalk(Get(eType).Attack * m_statCurveMultiplier, Get(eType).Attack * m_statCurveMultiplier);
            MaxSpeed = Get(eType).MaxSpeed + RunRandomWalk(Get(eType).MaxSpeed * m_statCurveMultiplier, Get(eType).MaxSpeed * m_statCurveMultiplier);
            MaxStamina = Get(eType).MaxStamina + RunRandomWalk(Get(eType).MaxStamina * m_statCurveMultiplier, Get(eType).MaxStamina * m_statCurveMultiplier);
            PleasureAttackMult = Get(eType).PleasureAttackMult + RunRandomWalk(Get(eType).PleasureAttackMult * m_statCurveMultiplier, Get(eType).PleasureAttackMult * m_statCurveMultiplier);
            MaxPleasure = Get(eType).MaxPleasure + RunRandomWalk(Get(eType).MaxPleasure * m_statCurveMultiplier, Get(eType).MaxPleasure * m_statCurveMultiplier);
            TimesToEjaculate = Get(eType).TimesToEjaculate + (int)RunRandomWalk(Get(eType).TimesToEjaculate * m_statCurveMultiplier, Get(eType).TimesToEjaculate * m_statCurveMultiplier);


            Reward = 0;
            float m_atkRatio = Attack / Get(eType).Attack;
            float m_pAtkRatio = PleasureAttackMult / Get(eType).PleasureAttackMult;
            float m_spdRatio = MaxSpeed / Get(eType).MaxSpeed;
            float m_stamRatio = MaxStamina / Get(eType).MaxStamina;
            float m_pleasRatio = MaxPleasure / Get(eType).MaxPleasure;
            float m_ejRatio = TimesToEjaculate / Get(eType).TimesToEjaculate;
            float m_levelFactor = Level / Get(eType).MaxLevel;
            float m_averageRatio = (m_atkRatio + m_pAtkRatio + m_spdRatio + m_stamRatio + m_pleasRatio + m_ejRatio + m_levelFactor) / 7f;

            Reward = Get(eType).Reward + (int)(Get(eType).Reward * m_averageRatio);
            Level = (int)(Level * m_averageRatio);
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