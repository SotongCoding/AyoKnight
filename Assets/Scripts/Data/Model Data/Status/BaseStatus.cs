using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BaseStatus {
    public int attack;
    public int defense;
    public int health;

    public BaseStatus () { } 

    public BaseStatus (int attack, int defense, int health) {
        this.attack = attack;
        this.defense = defense;
        this.health = health;
    }

    public BaseStatus (BaseStatus data) {
        this.attack = data.attack;
        this.defense = data.defense;
        this.health = data.health;
    }

    /// <summary>
    /// SUM All input Variable
    /// </summary>
    /// <param name="statuses"></param>
    public BaseStatus (params BaseStatus[] statuses) {
        int atk = 0, def = 0, health = 0;

        foreach (var item in statuses) {
            atk += item.attack;
            def += item.defense;
            health += item.health;
        }

        this.attack = atk;
        this.defense = def;
        this.health = health;
    }
    public override string ToString () {
        return "A :" + attack + " D :" + defense + " H :" + health;
    }
}