using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MData_BattleRoom {
    // Start is called before the first frame update
    public int killAmount;
    public int tierUse;
    public List<EnemyData> enemies = new List<EnemyData> ();
    [SerializeField] List<LootDropData> dropData = new List<LootDropData> ();

    public List<LootDropData> GetDropData () {
        return dropData;
    }
    public List<EnemyData> GetEnemies () {
        return enemies;
    }
}