using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "_lv_Data", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject, IEquatable<LevelData> {

    public int levelID;
    public string levelName;
    public int killAmount;
    public int tierUse;
    public List<EnemyData> enemies = new List<EnemyData> ();
    public int[] unlockedlevelID;
    [SerializeField] List<LootDropData> dropData = new List<LootDropData> ();

    public int[] GetUnlockedLevel () {
        return unlockedlevelID;
    }
    public List<LootDropData> GetDropList () {
        return dropData;
    }

    public bool Equals (LevelData other) {
        return other.levelID == levelID;
    }
}