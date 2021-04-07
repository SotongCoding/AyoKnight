﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "_lv_Data", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject, IEquatable<LevelData> {

    public int levelID;
    public string levelName;

    //Battle Data
    // public int killAmount;
    // public int tierUse;
    // public List<EnemyData> enemies = new List<EnemyData> ();
    // [SerializeField] List<LootDropData> dropData = new List<LootDropData> ();
    #region Room 
    //Room Data
    public int roomAmount;
    // Battle
    [Header ("Battle Data")]
    [SerializeField] MData_BattleRoom battleRoom;

    public MData_BattleRoom GetBattleRoomData () {
        return battleRoom;
    }
    //Mining
    [Header ("MiningRoom")]
    [SerializeField] MData_MiningRoom miningRoom;
     public MData_MiningRoom GetMiningRoomData () {
        return miningRoom;
    }
    #endregion

    //Other

    [Header("Other")]
    public int[] unlockedlevelID;

    public int[] GetUnlockedLevel () {
        return unlockedlevelID;
    }

    public bool Equals (LevelData other) {
        return other.levelID == levelID;
    }
}