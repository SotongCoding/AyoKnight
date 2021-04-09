using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentStatus {
    public EquipType type;
    public BaseStatus status;

    public EquipmentStatus () { }

    public EquipmentStatus (EquipType type, int attack, int defense, int health) {
        this.type = type;
        status = new BaseStatus (attack, defense, health);
    }

    public EquipmentStatus (EquipmentStatus data) {
        this.type = data.type;
        this.status = data.status;
    }

    /// <summary>
    /// SUM All input Variable
    /// </summary>
    /// <param name="statuses"></param>
    public EquipmentStatus (params EquipmentStatus[] statuses) {
        this.type = EquipType.none;
        int atk = 0, def = 0, health = 0;

        foreach (var item in statuses) {
            atk += item.status.attack;
            def += item.status.defense;
            health += item.status.health;
        }

        status = new BaseStatus (atk, def, health);
    }
    public override string ToString () {
        return "A :" + status.attack + " D :" + status.defense + " H :" + status.health;
    }
}