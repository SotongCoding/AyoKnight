using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EquipmentData : BaseItem {

    public int equipmentID; 
    public EquipType type;
    public int attack;
    public int defense;
    public int health;
    public int durability;

    //Cost

    public int unlockCost;
    public int fullRepairCost;

}