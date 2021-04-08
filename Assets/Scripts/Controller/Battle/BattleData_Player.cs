using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleData_Player {
    [SerializeField] int health { get; }

    [SerializeField] int attack { get; }

    [SerializeField] int defense { get; }
    public int turnAmount = 1;
    public int[] activeNote_weap { get; }
    public int[] activeNote_armor { get; }
    public int heatlth_fix;
    public int defense_fix;
    public int attack_fix;

    public BattleData_Player (EquipOnBattle weapon, EquipOnBattle armor, EquipOnBattle accecories) {
        int[] activeNote_weap = new int[4], activeNote_armor = new int[4];
        // Status 
        EquipmentStatus allStat = new EquipmentStatus (
            weapon.GetTotalStat (), armor.GetTotalStat (), accecories.GetTotalStat (),
            new EquipmentStatus (EquipType.none, 0, 0, 5));

        this.heatlth_fix = allStat.health;
        this.defense_fix = allStat.defense;
        this.attack_fix = allStat.attack;

        // Note
        this.activeNote_weap = weapon.baseData.GetActiveNote ();
        this.activeNote_armor = armor.baseData.GetActiveNote ();

    }

    public void TakeDamage (int comeDamage) {
        heatlth_fix -= comeDamage;
        if (comeDamage > 0) {
            BattleController._instance.CallOnGetDamage (true);
        }

        if (heatlth_fix <= 0) { BattleController._instance.CallOnBattleDone (false); }
    }

    public int CalculateDamage (int correctNote) {
        return attack_fix * correctNote;
    }

}