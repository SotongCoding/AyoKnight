using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : BaseItem {

    public int equipmentID;
    [Header ("Status Equipment")]
    public EquipmentStatus equipData;

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
    public BaseStatus GetEnchantStat (int level) {
        int sumAtk = 0;
        int sumDef = 0;
        int sumHealth = 0;

        for (int i = 0; i < level; i++) {
            sumAtk += enchantStatus[i].atk;
            sumDef += enchantStatus[i].def;
            sumHealth += enchantStatus[i].health;
        }

        return new BaseStatus (sumAtk, sumDef, sumHealth);
    }

    public EnchantData GetEnchantData (int enchantLevel) {
        if (enchantLevel < enchantStatus.Count) return new EnchantData (enchantStatus[enchantLevel]);
        return null;
    }
    #endregion
}
