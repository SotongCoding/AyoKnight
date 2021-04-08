using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : BaseItem {

    public int equipmentID;
    [Header ("Status Equipment")]
    public EquipmentStatus status;

    [Header ("Item Cost")]
    [SerializeField] CostData unlockCost;
    #region Getter
    public CostData GetUnlockCost () {
        return unlockCost;
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

        return new EquipmentStatus (status.type, sumAtk, sumDef, sumHealth);
    }

    public EnchantData GetEnchantData (int enchantLevel) {
        if (enchantLevel < enchantStatus.Count) return new EnchantData (enchantStatus[enchantLevel]);
        return null;
    }
    #endregion
}

[Serializable]
public class EnchantData {
    [Header ("Status will Add")]
    public int atk;
    public int def;
    public int health;

    public EnchantData () { }
    public EnchantData (EnchantData data) {
        this.atk = data.atk;
        this.def = data.def;
        this.health = data.health;
    }
    public EquipmentStatus GetStatus () {
        return new EquipmentStatus (EquipType.none, atk, def, health);
    }
}

[Serializable]
public class EquipmentStatus {
    public EquipType type;
    public int attack;
    public int defense;
    public int health;

    public EquipmentStatus () { }

    public EquipmentStatus (EquipType type, int attack, int defense, int health) {
        this.type = type;
        this.attack = attack;
        this.defense = defense;
        this.health = health;
    }

    public EquipmentStatus (EquipmentStatus data) {
        this.type = data.type;
        this.attack = data.attack;
        this.defense = data.defense;
        this.health = data.health;
    }

    /// <summary>
    /// SUM All input Variable
    /// </summary>
    /// <param name="statuses"></param>
    public EquipmentStatus (params EquipmentStatus[] statuses) {
        this.type = EquipType.none;
        int atk = 0, def = 0, health = 0;

        foreach (var item in statuses) {
            atk += item.attack;
            def += item.defense;
            health += item.health;
        }

        this.attack = atk;
        this.defense = def;
        this.health = health;
    }
    public override string ToString () {
        return "A :" + attack + " D :" + defense + " H :" + health;
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

    public CostData (CostRequirement[] resources) {
        this.resources = resources;
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