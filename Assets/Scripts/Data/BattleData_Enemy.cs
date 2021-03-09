using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleData_Enemy {

    int tier;

    [SerializeField] int health { get; }

    [SerializeField] int attack { get; }
    public int turnAmount { get; }
    public int noteAmount { get; }
    public Sprite enemyPict { get; }

    [SerializeField] public int health_fix;
    [SerializeField] public int attack_fix;
    public BattleData_Enemy (int tier, EnemyData baseData) {
        this.tier = tier;
        this.health = ((int) (tier) * baseData.health);
        this.attack = ((int) (tier) * baseData.attack);

        this.turnAmount = baseData.turnAmount;
        this.noteAmount = baseData.noteAmount;

        health_fix = health;
        attack_fix = attack;

        enemyPict = baseData.picture;

    }

    public void TakeDamage (int comeDamage) {
        health_fix -= comeDamage;
        BattleController._instance.CallOnGetDamage (false);
        if (health_fix <= 0) { BattleController._instance.CallOnEnemyDeath ();}
    }
    public int CalculateDamage (int correctNote, int playerDefense) {
        return Mathf.Clamp ((attack_fix * noteAmount) - (playerDefense * correctNote), 0, 9999);
    }

    public override string ToString () {
        return health_fix + " : " + attack_fix + " : " + noteAmount + " : " + turnAmount;
    }
}