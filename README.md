# Cibelle Hard Mod v1.0.0

A complete difficulty and tactical overhaul for **Cibelle**, built on the BepInEx framework. This mod decreases your starting stats, boosts enemy scaling, and introduces dynamic combat mechanics based on outfit durability and desire levels.

---

## 🎮 Key Features

* **Hardcore Progression:** Cibelle starts much weaker, making the game much harder. However, keeping her virginity intact acts as a great investment that heavily buffs your desire damage.
* **Randomized Enemy Stats:** Every enemy type has its own unique minimum and maximum stat caps. Because their stats are completely randomized, you might occasionally beat a high-tier enemy easily, but other times you will be forced to run from combat to survive.
* **Brutal Factions & Bosses:** Boss fights are extremely difficult. Factions like Orcs and Goblins now have vastly increased Stamina and HP.
* **Dynamic Clothing Rule:** * When clothing is pristine and clean, enemies cannot grab Cibelle or do lewd actions with her.
* Adult actions require your clothing to be damaged first.
* Cibelle takes more damage when her clothing is torn, but gains solid damage reduction when her outfit is brand new.


* **Desire Attack Debuff:** High desire makes Cibelle weaker. When her desire is high, she cannot attack with full power, causing her normal attacks to deal heavily reduced damage.
* **Skill Adjustments:** * *Fortify* has been tweaked to have a longer duration.
* *Masochism* has been buffed to deal more pleasure damage since enemies have much more stamina now.



---

# Cibelle Hard Mod v1.1.0 — The Lewd Build Rebalance Update

This update fully overhauls the heroine's pleasure damage calculations and rebalances the lewd skill tree. Passive resistance stacking has been heavily restricted, while enemy offensive capabilities have been increased.

⚔️ **Enemy Mechanics & Pleasure Damage Rebalance**

* **Multiple Ejaculations:** Enemies are no longer limited to a single cycle. Each enemy type can now ejaculate multiple times, depending on their specific type.
* **Unique Damage Multipliers:** Every enemy type now has its own unique pleasure damage dealt multiplier.
* **High-Tier Threat Scaling:** High-tier enemies have been explicitly rebalanced to deal significantly more pleasure damage.

🧠 **Lewd Skill Tree & Passives Overhaul**

* **Global Passive Nerf:** Total rebalance of the lewd skill tree. Every passive skill that grants baseline pleasure resistance has been nerfed.
* **NeverTappedOut Adjustments:** Nerfed the base resistance values because the skill previously provided too much mitigation. Added a strict hard cap: The skill now stacks up to a maximum of 18% Pleasure Resistance.
* **Orgasmic Resistance Adjustments:** This skill now provides exactly +2% pleasure resistance per orgasm, lasting strictly until the current battle ends.
* **Skill Tree Progression Unlock:** Removed mutual exclusivity from the skill tree. Because baseline difficulty is significantly higher, you can now acquire all available skills on a single profile without having to choose between branches, maximizing build variety.

---

# Cibelle Hard Mod v1.2.0 — The Dynamic Enemy Scaling Update

Welcome to version 1.2.0! This update completely reimagines how enemies spawn, scale, and reward you. Enemies are no longer tied directly to Cibelle's level; instead, they have their own independent stats, unique levels, and completely customizable difficulty settings.

---

## 🚀 Key Features

### 📊 Dynamic Enemy Generation & Independent Scaling

* **Independent Levels:** Enemies no longer mirror Cibelle’s level. Every enemy type now features its own natural level bracket and independent attributes.
* **Unpredictable Stats ($\pm$35% Damage Variance):** Combat is much more unpredictable. A 35% random variation is now applied to all base damage outputs, affecting Cibelle's physical attacks, magical spells, and incoming enemy strikes.
* **Full Blueprint Customization:** You can now fully control the base difficulty of the mod. Every single one of the 11 enemy types can be individually tweaked via the `CibelleHardMod.cfg` file (adjust their spawn levels, damage, speed, stamina, pleasure limits, and more).

### ⚔️ Contextual Combat Multipliers

Enemy performance shifts dynamically in real-time based on their psychological state, environment, and physical fatigue:

* **Emotional Status:** *Angry* enemies deal 25% more damage, while *Horny* enemies suffer a 25% damage penalty.
* **Night Cycle:** Monsters become significantly more dangerous after dark, gaining a 25% damage boost.
* **Post-Orgasm Fatigue:** Every ejaculation achieved by an enemy inflicts a cumulative 10% base performance debuff, which scales further based on your Charisma stat (up to a massive 90% total damage reduction).

### 💰 Dynamic Victory Rewards

* **Strength-Based Essence Payouts:** The old flat-rate rewards are gone. The victory screen now calculates your Essence reward dynamically based on the exact rolled strength, stats, and final level of the enemy you defeated. Facing a higher-level, heavily buffed elite variant will yield significantly more Essence than taking down a weaker version of the same enemy type.

### 👗 Outfit Durability & Pleasure Mechanics

* **Armor Protection:** Keeping your clothes intact matters more than ever. Having fully durable clothing reduces incoming physical hits and cuts incoming pleasure damage by up to 25%.
* **Lewd Action Thresholds:** Initiating lewd actions is now harder to do on a whim—they scale contextually and require your clothes to be more heavily damaged before they can be triggered.
* **Balance Adjustments:** Decreased the default base `PleasureAttack` multiplier for most common enemy types, and nerfed the *Constancy* passive skill to keep combat challenging.

---

# Cibelle Hard Mod v1.2.3 — Stability, Mechanics & Balance Update

This update incorporates extensive playtest adjustments based on the last release, resolving critical skill calculation bugs, fine-tuning the high-stakes nighttime mechanics, and implementing a comprehensive stat rebalance to provide a smooth yet challenging difficulty curve.

> ⚠️ **Notice:** Because base stats, enemy profiles, and stat tracking systems have been heavily rewritten, **a New Game is required** for this update to work correctly. Additionally, you **MUST delete your old config file** at `BepInEx\config\CibelleHardMod.cfg` before starting so a fresh, optimized configuration can generate properly on startup.

---
ℹ️ Compatibility Note: This plugin was developed and tested specifically for game version 0.22.
---

## 🛠️ Core Fixes & Technical Improvements

* **Fixed Unpredictable Enemy Stats:** Found a bug where enemy stats were rolling at the wrong time (global launch), causing them to behave wildly or glitch out. Stats now roll correctly through independent `EnemyProfile` instances right when a battle starts, making enemy attributes and rewards perfectly stable.
* **Skill Calculation Fix (`TSKNeverTappedOut`):** Resolved an underlying calculation error in the `TSKNeverTappedOut` skill that was causing pleasure resistance stacks to apply incorrectly.
* **Level Cap System:** Implemented an explicit leveling cap using per-enemy minimum and maximum brackets. This keeps early gameplay fair by preventing high-level, unbeatable enemies from spawning while Cibelle's own level is low.
* **Ejaculation Mechanic Precision:** Converted `TimesToEjaculate` internally to a decimal (float) value to properly support random variations and multipliers during enemy generation. For example, if an enemy's initial roll gets multiplied to a 1.9 or 2.1 at the start of battle, it will round naturally to 2.
* **Correct SpellPoints Update:** Resolved an underlying bug in the data tracking loops to ensure your total Spell Points now update and display flawlessly alongside level gains.

---

## ⚔️ New Features & Gameplay Tweaks

* **New Running Mechanic:** Escaping from combat via the new `TryToRunFromBattle` logic is now noticeably easier to pull off successfully, but running away comes at a cost—Cibelle will take a final penalty hit from the enemy as she flees.
* **Fleeing Low-Health Fix:** Fixed a critical bug that was breaking or locking up the running logic when trying to retreat from a battle while at low health.
* **Spell Point Resource Update:** Maximum Spell Points (SP) now scale alongside Cibelle’s level. Additionally, all of your SP will be fully restored automatically at the end of a battle, letting you head into the next encounter prepared.
* **Balanced Night Cycles:** Reworked nighttime exploration mechanics. Enemies encountered after dark are now **25% stronger** and have an **increased chance to spawn at higher levels**. Surviving these high-risk encounters now awards **25% more victory essence**.

---

## ⚖️ Balance & Skill Adjustments

* **`TSKNeverTappedOut` Rebalance:** Completely reworked the skill behavior. It now grants "Max Pleasure Resistance" directly, and the total amount of pleasure resistance you can stack per forced orgasm is strictly capped based on your invested skill points.
* **Skill Adjustments & Buffs:** Rebalanced multiple skill nodes across the unlocked tree. This includes boosting the overall effectiveness magnitude of **EndureDesireTech** and adjusting **FirecrackerTech** (increased maximum damage potential, slightly lower minimum baseline damage, and introduced a **1-turn cooldown** restriction).
* **Extended Pleasure Resistance Cap:** Raised the hard ceiling for maximum pleasure resistance, allowing it to scale all the way up to **99** to give defensive vitality/corruption builds more breathing room.
* **Total Enemy Attribute & MaxPleasure Tune-up:** Performed a global balance sweep across all enemy profiles. Enemy `MaxPleasure` pools and core base statistics have been thoroughly re-tuned to be more reasonable—eliminating artificial spikes while ensuring encounters remain strictly tactical and demanding.

* ([GitHub](https://github.com/OleksandrMoskaliuk) | X/Twitter: [@Dru9Dealer](https://twitter.com/Dru9Dealer))

## 🛠️ Installation

### Step 1: Install BepInEx

1. Download **BepInEx 5.x** (64-bit).
2. Extract it directly into your game root directory (where `Cibelle.exe` is located).
3. Run the game once to let it generate folders, then close it.

### Step 2: Install Mod

1. Place `CibelleHardMode.dll` into your **`BepInEx\plugins`** folder:

```text
📁 Game Root Folder/
└── 📁 BepInEx/
    └── 📁 plugins/
        └── 📄 CibelleHardMode.dll

```