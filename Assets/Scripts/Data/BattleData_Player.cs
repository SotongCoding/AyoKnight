using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleData_Player {
    public event Action onTakeDamage;

    void CallOnTakeDamage () {
        if (onTakeDamage != null) onTakeDamage ();
    }

    [SerializeField] int health { get; }

    [SerializeField] int attack { get; }

    [SerializeField] int defense { get; }
    public int turnAmount = 1;
    public int[] activeNote_weap { get; }
    public int[] activeNote_armor { get; }
    public int heatlth_fix;
    public int defense_fix;
    public int attack_fix;

    public BattleData_Player (int health, int attack, int defense, int[] activeNote_weap, int[] activeNote_armor) {
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.activeNote_weap = activeNote_weap;
        this.activeNote_armor = activeNote_armor;

        this.heatlth_fix = health;
        this.attack_fix = attack;
        this.defense_fix = defense;
    }

    public void TakeDamage (int comeDamage) {
        heatlth_fix -= comeDamage;
        CallOnTakeDamage ();

        if (heatlth_fix <= 0) { BattleController._instance.CallOnBattleDone (false); }
    }

    public int CalculateDamage (int correctNote) {
        return attack_fix * correctNote;
    }

}