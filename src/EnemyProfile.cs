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
                .SetBaseEssenceReward(10)
                .SetTimesToEjaculate(1)
                .SetBaseAttackMultiplier(0.9f)
                .SetBasePleasureAttackMultiplier(0.9f)
                .SetBaseSpeed(1.0f)
                .SetBaseStamina(40f)
                .SetBasePleasure(50f));

            Register(new EnemyProfile(EnemyType.Villager)
                .SetLevels(2, 5)
                .SetBaseEssenceReward(20)
                .SetTimesToEjaculate(1)
                .SetBaseAttackMultiplier(1.1f)
                .SetBasePleasureAttackMultiplier(1.2f)
                .SetBaseSpeed(1.0f)
                .SetBaseStamina(50f)
                .SetBasePleasure(85f));

            Register(new EnemyProfile(EnemyType.Soldier)
                .SetLevels(4, 8)
                .SetBaseEssenceReward(45)
                .SetTimesToEjaculate(2)
                .SetBaseAttackMultiplier(1.4f)
                .SetBasePleasureAttackMultiplier(1.4f)
                .SetBaseSpeed(1.1f)
                .SetBaseStamina(80f)
                .SetBasePleasure(100f));

            Register(new EnemyProfile(EnemyType.Bandit)
                .SetLevels(1, 10)
                .SetBaseEssenceReward(350)
                .SetTimesToEjaculate(2)
                .SetBaseAttackMultiplier(1.3f)
                .SetBasePleasureAttackMultiplier(1.5f)
                .SetBaseSpeed(10.0f)
                .SetBaseStamina(60f)
                .SetBasePleasure(110f));

            Register(new EnemyProfile(EnemyType.Roughman)
                .SetLevels(5, 10)
                .SetBaseEssenceReward(60)
                .SetTimesToEjaculate(2)
                .SetBaseAttackMultiplier(1.8f)
                .SetBasePleasureAttackMultiplier(2.1f)
                .SetBaseSpeed(1.1f)
                .SetBaseStamina(90f)
                .SetBasePleasure(130f));

            Register(new EnemyProfile(EnemyType.Barroso)
                .SetLevels(8, 14)
                .SetBaseEssenceReward(110)
                .SetTimesToEjaculate(3)
                .SetBaseAttackMultiplier(2.5f)
                .SetBasePleasureAttackMultiplier(4.2f)
                .SetBaseSpeed(1.2f)
                .SetBaseStamina(150f)
                .SetBasePleasure(180f));

            Register(new EnemyProfile(EnemyType.Goblin)
                .SetLevels(6, 12)
                .SetBaseEssenceReward(80)
                .SetTimesToEjaculate(1)
                .SetBaseAttackMultiplier(1.6f)
                .SetBasePleasureAttackMultiplier(4.0f)
                .SetBaseSpeed(1.3f)
                .SetBaseStamina(70f)
                .SetBasePleasure(200f));

            Register(new EnemyProfile(EnemyType.Orc)
                .SetLevels(9, 16)
                .SetBaseEssenceReward(140)
                .SetTimesToEjaculate(3)
                .SetBaseAttackMultiplier(2.8f)
                .SetBasePleasureAttackMultiplier(4.9f)
                .SetBaseSpeed(0.9f)
                .SetBaseStamina(180f)
                .SetBasePleasure(250f));

            Register(new EnemyProfile(EnemyType.Werewolf)
                .SetLevels(10, 18)
                .SetBaseEssenceReward(160)
                .SetTimesToEjaculate(2)
                .SetBaseAttackMultiplier(3.0f)
                .SetBasePleasureAttackMultiplier(5.2f)
                .SetBaseSpeed(1.4f)
                .SetBaseStamina(140f)
                .SetBasePleasure(280f));

            Register(new EnemyProfile(EnemyType.Drakkma)
                .SetLevels(12, 22)
                .SetBaseEssenceReward(220)
                .SetTimesToEjaculate(4)
                .SetBaseAttackMultiplier(3.5f)
                .SetBasePleasureAttackMultiplier(6.4f)
                .SetBaseSpeed(1.2f)
                .SetBaseStamina(200f)
                .SetBasePleasure(300f));

            Register(new EnemyProfile(EnemyType.Baron)
                .SetLevels(15, 25)
                .SetBaseEssenceReward(350)
                .SetTimesToEjaculate(5)
                .SetBaseAttackMultiplier(4.5f)
                .SetBasePleasureAttackMultiplier(7.2f)
                .SetBaseSpeed(1.0f)
                .SetBaseStamina(250f)
                .SetBasePleasure(350f));
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
        public float Speed { get; private set; }
        public float Stamina { get; private set; }
        public float maxPleasure { get; private set; }
        public int Level { get; private set; }

        // ---- Blueprint Default Constructor ----
        public EnemyProfile(EnemyType m_type)
        {
            Type = m_type;
            MinLevel = 1; MaxLevel = 1; Reward = 1; TimesToEjaculate = 1;
            Attack = 1.0f; PleasureAttackMult = 1.0f; Speed = 1.0f; Stamina = 1.0f; maxPleasure = 1.0f;
        }

        // ---- Separated Fluent Setters ----
        public EnemyProfile SetLevels(int m_min, int m_max) { MinLevel = m_min; MaxLevel = m_max; return this; }
        public EnemyProfile SetBaseEssenceReward(int m_reward) { Reward = m_reward; return this; }
        public EnemyProfile SetTimesToEjaculate(int m_times) { TimesToEjaculate = m_times; return this; }
        public EnemyProfile SetBaseAttackMultiplier(float m_val) { Attack = m_val; return this; }
        public EnemyProfile SetBasePleasureAttackMultiplier(float m_val) { PleasureAttackMult = m_val; return this; }
        public EnemyProfile SetBaseSpeed(float m_val) { Speed = m_val; return this; }
        public EnemyProfile SetBaseStamina(float m_val) { Stamina = m_val; return this; }
        public EnemyProfile SetBasePleasure(float m_val) { maxPleasure = m_val; return this; }

        // ---- Instance Generation Engine ----
        // Call this method whenever a specific combatant spawns to populate the "Current" stat fields.
        public void RollInstance(EnemyType eType)
        {
            
            UnityEngine.Random.InitState(Guid.NewGuid().GetHashCode());

            Level = UnityEngine.Random.Range(Get(eType).MinLevel, Get(eType).MaxLevel);
            float m_levelProgress = Level - 1;
            float m_statCurveMultiplier = 1.0f + 0.078f * MathF.Pow(m_levelProgress, 1.35f);

            Attack = Get(eType).Attack + RunRandomWalk(Get(eType).Attack * m_statCurveMultiplier, Get(eType).Attack * m_statCurveMultiplier);
            Speed = Get(eType).Speed + RunRandomWalk(Get(eType).Speed * m_statCurveMultiplier, Get(eType).Speed * m_statCurveMultiplier);
            Stamina = Get(eType).Stamina + RunRandomWalk(Get(eType).Stamina * m_statCurveMultiplier, Get(eType).Stamina * m_statCurveMultiplier);
            PleasureAttackMult = Get(eType).PleasureAttackMult + RunRandomWalk(Get(eType).PleasureAttackMult * m_statCurveMultiplier, Get(eType).PleasureAttackMult * m_statCurveMultiplier);
            maxPleasure = Get(eType).maxPleasure + RunRandomWalk(Get(eType).maxPleasure * m_statCurveMultiplier, Get(eType).maxPleasure * m_statCurveMultiplier);
            TimesToEjaculate = Get(eType).TimesToEjaculate + (int)RunRandomWalk(Get(eType).TimesToEjaculate * m_statCurveMultiplier, Get(eType).TimesToEjaculate * m_statCurveMultiplier);


            Reward = 0;
            float m_atkRatio = Attack / Get(eType).Attack;
            float m_pAtkRatio = PleasureAttackMult / Get(eType).PleasureAttackMult;
            float m_spdRatio = Speed / Get(eType).Speed;
            float m_stamRatio = Stamina / Get(eType).Stamina;
            float m_pleasRatio = maxPleasure / Get(eType).maxPleasure;
            float m_ejRatio = TimesToEjaculate / Get(eType).TimesToEjaculate;

            float m_averageRatio = (m_atkRatio + m_pAtkRatio + m_spdRatio + m_stamRatio + m_pleasRatio + m_ejRatio) / 6f;

            float m_levelSpan = MaxLevel - MinLevel;
            float m_levelFactor = m_levelSpan > 0 ? (Level - MinLevel) / m_levelSpan : 0f;
            float m_levelBonus = 1f + m_levelFactor * 0.25f;

            Reward = Get(eType).Reward + (int)(Get(eType).Reward * m_averageRatio);
        }

        private float RunRandomWalk(float m_base, float m_dev)
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