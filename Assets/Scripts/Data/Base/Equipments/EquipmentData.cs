using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EquipmentData : BaseItem {

    public int equipmentID;
    [Header ("Status Equipment")]
    public EquipmentStatus status;

    [Header ("Item Cost")]
    [SerializeField] int unlockCost;
    [SerializeField] CostData fullRepairCost;
    #region Getter
    public int GetDurability () {
        return status.durability;
    }
    public int GetUnlockCost () {
        return unlockCost;
    }
    public CostData GetFullRepairCost () {
        return fullRepairCost;
    }
    public virtual int[] GetActiveNote () {
        return new int[0];
    }
    #endregion

    #region Enchant
    [Header ("Echant Status")]
    [SerializeField] List<EnchantData> enchantStatus;
    public EquipmentStatus GetEnchantStat (int level) {
        int sumAtk = 0;
        int sumDef = 0;
        int sumHealth = 0;

        for (int i = 0; i < level; i++) {
            sumAtk += enchantStatus[i].atk;
            sumDef += enchantStatus[i].def;
            sumHealth += enchantStatus[i].health;
        }
        return new EquipmentStatus (status.type, sumAtk, sumDef, sumHealth, status.durability);
    }

    public EnchantData GetEnchantData (int enchantLevel) {
        if (enchantLevel < enchantStatus.Count) return new EnchantData (enchantStatus[enchantLevel]);
        return null;
    }
    #endregion
}

[Serializable]
public class EnchantData {
    [Header ("Resources Need")]
    public CostData requirement;
    [Header ("Status will Add")]
    public int atk;
    public int def;
    public int health;

    public EnchantData () { }

    public EnchantData (CostData requirement, int atk, int def, int health) {
        this.requirement = requirement;
        this.atk = atk;
        this.def = def;
        this.health = health;
    }
    public EnchantData (EnchantData data) {
        this.requirement = data.requirement;
        this.atk = data.atk;
        this.def = data.def;
        this.health = data.health;
    }
    public EquipmentStatus GetStatus () {
        return new EquipmentStatus (EquipType.weapon, atk, def, health, 0);
    }
}

[Serializable]
public class EquipmentStatus {
    public EquipType type;
    public int attack;
    public int defense;
    public int health;
    public int durability;

    public EquipmentStatus () { }

    public EquipmentStatus (EquipType type, int attack, int defense, int health, int durability) {
        this.type = type;
        this.attack = attack;
        this.defense = defense;
        this.health = health;
        this.durability = durability;
    }

    public EquipmentStatus (EquipmentStatus data) {
        this.type = data.type;
        this.attack = data.attack;
        this.defense = data.defense;
        this.health = data.health;
        this.durability = data.durability;
    }
}

[Serializable]
public class CostData {
    public CostRequirement[] resources = new CostRequirement[4];
    public CostData () { }

    public CostData (CostData data) {
        for (int i = 0; i < data.resources.Length; i++) {
            resources[0] = data.resources[0];
        }
    }

    [Serializable]
    public class CostRequirement {
        public int resourcesID = -1;
        public int resourcesAmount = -1;

        public CostRequirement () { }

        public CostRequirement (int resourcesID, int resourcesAmount) {
            this.resourcesID = resourcesID;
            this.resourcesAmount = resourcesAmount;
        }
        public bool isEnough (CostRequirement inputValue) {
            if (inputValue.resourcesID == -1) {
                return true;
            }

            if (inputValue.resourcesID == resourcesID) {
                return inputValue.resourcesAmount >= resourcesAmount;
            }
            return false;
        }
    }
}