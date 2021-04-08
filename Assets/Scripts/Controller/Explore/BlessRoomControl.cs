using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessRoomControl : MonoBehaviour {

    public EquipmentStatus playerBaseStat { get => new EquipmentStatus (EquipType.none, 0, 0, 5); }
    List<EquipmentStatus> modifStat = new List<EquipmentStatus> ();

    
    public EquipmentStatus GetAllStatus () {
        int atk = 0, def = 0, health = 0;
        foreach (var status in modifStat) {
            atk += status.attack;
            def += status.defense;
            health += status.health;
        }

        return new EquipmentStatus (playerBaseStat,
            new EquipmentStatus (EquipType.none, atk, def, health));
    }

    public void GetAction(){

    }
}