using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleData_Enemy {
    public event Action OnEnemyDeath;
    int tier;

    [SerializeField] int health { get; }

    [SerializeField] int attack { get; }
    public int turnAmount { get; }
    public int noteAmount { get; }

    [SerializeField] public int health_fix;
    [SerializeField] public int attack_fix;
    public BattleData_Enemy (int tier, EnemyData baseData) {
        this.tier = tier;
        this.health = ((int) (tier) * baseData.health) + baseData.health;
        this.attack = ((int) (tier) * baseData.attack) + baseData.attack;

        this.turnAmount = baseData.turnAmount;
        this.noteAmount = baseData.noteAmount;

        health_fix = health;
        attack_fix = attack;

    }

    public void TakeDamage (int comeDamage) {
        health_fix -= comeDamage;
        if (health_fix <= 0) { BattleController._instance.CallOnEnemyDeath (); Debug.Log ("death"); }
        Debug.Log ("Enemy Take Damage");
    }
    public int CalculateDamage (int correctNote, int playerDefense) {
        return Mathf.Clamp ((attack_fix * noteAmount) - (playerDefense * correctNote), 0, 9999);
    }

    public override string ToString () {
        return health_fix + " : " + attack_fix + " : " + noteAmount + " : " + turnAmount;
    }
}