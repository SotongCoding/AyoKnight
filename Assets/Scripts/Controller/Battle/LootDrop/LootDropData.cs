using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootDropData { 
    [Header ("Drop Data")]
    [SerializeField] BaseItem itemData;
    [Tooltip ("Between 0-100.")]
    [Range (0, 100)]
    [SerializeField] int dropChance;

    [SerializeField] int minDrop, maxDrop;
    int fixDrop;

    public bool GetDrop (int checkValue) {
        if (checkValue <= dropChance) return true;
        else return false;
    }
    public int[] GetMINMAX () {
        return new int[2] {
            minDrop,
            maxDrop
        };
    }
    public int GetFixDrop () {
        return fixDrop;
    }
    public void SetFixDrop (int fixDrop) {
        this.fixDrop = fixDrop;
    }
    public BaseItem GetData () {
        return itemData;
    }
}