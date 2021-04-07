using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MData_MiningRoom {
    [SerializeField] List<LootDropData> miningItem = new List<LootDropData> ();

    public List<LootDropData> GetMiningItem () {
        return miningItem;
    }
}