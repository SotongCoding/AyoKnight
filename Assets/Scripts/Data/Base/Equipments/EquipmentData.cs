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
    [SerializeField] int fullRepairCost;
    #region Getter
    public int GetDurability () {
        return status.durability;
    }
    public int GetUnlockCost () {
        return unlockCost;
    }
    public int GetFullRepairCost () {
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

        for (int i = 0; i < level - 1; i++) {
            sumAtk += enchantStatus[i].atk;
            sumDef += enchantStatus[i].def;
            sumHealth += enchantStatus[i].health;
        }
        return new EquipmentStatus (status.type, sumAtk, sumDef, sumHealth, status.durability);
    }
    public bool CanEnchant (int reqValue, int enchantLevel, out int ReqAmount) {
        ReqAmount = enchantStatus[enchantLevel - 1].reqAmount;
        return enchantStatus[enchantLevel - 1].reqAmount >= reqValue;
    }
    #endregion
}

[Serializable]
public class EnchantData {
    public int reqAmount;
    public int atk;
    public int def;
    public int health;

    public EnchantData () { }

    public EnchantData (int reqAmount, int atk, int def, int health) {
        this.reqAmount = reqAmount;
        this.atk = atk;
        this.def = def;
        this.health = health;
    }
    public EnchantData (EnchantData data) {
        this.reqAmount = data.reqAmount;
        this.atk = data.atk;
        this.def = data.def;
        this.health = data.health;
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
    public CostRequirement requirement1;
    public CostRequirement requirement2;
    public CostRequirement requirement3;
    public CostRequirement requirement4;
    [Serializable]
    public class CostRequirement {
        public int resourcesID;
        public int resourcesAmount;
    }
}