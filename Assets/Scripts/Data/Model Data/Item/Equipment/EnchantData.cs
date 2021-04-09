using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnchantData {
    [Header ("Status will Add")]
    public int atk;
    public int def;
    public int health;

    public EnchantData () { }
    public EnchantData (EnchantData data) {
        this.atk = data.atk;
        this.def = data.def;
        this.health = data.health;
    }
    public EquipmentStatus GetStatus () {
        return new EquipmentStatus (EquipType.none, atk, def, health);
    }
}


