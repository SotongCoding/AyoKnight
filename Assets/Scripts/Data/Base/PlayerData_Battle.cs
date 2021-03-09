using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Battle : MonoBehaviour {
    [SerializeField] int weaponID = -1;
    [SerializeField] WeaponBase weapon;
    EquipmentInv weaponData;

    [SerializeField] int armorID = -1;
    [SerializeField] ArmorBase armor;
    EquipmentInv armorData;

    [SerializeField] int accID = -1;
    [SerializeField] AccecoriesBase accecories;
    EquipmentInv accData;

    BattleData_Player CountAllStat () {
        int addHp = 0, addAtk = 0, addDef = 0;
        int[] activeNote_weap, activeNote_armor;
        //Weapon
        addHp += weapon != null ? weapon.health : 0;
        addAtk += weapon != null ? weapon.attack : 0;
        addDef += weapon != null ? weapon.defense : 0;
        activeNote_weap = weapon != null ? weapon.getActiveNote () : null;
        activeNote_armor = armor != null ? armor.getActiveNote () : null;

        //Armor
        addHp += armor != null ? armor.health : 0;
        addAtk += armor != null ? armor.attack : 0;
        addDef += armor != null ? armor.defense : 0;

        //Acc
        addHp += accecories != null ? accecories.health : 0;
        addAtk += accecories != null ? accecories.attack : 0;
        addDef += accecories != null ? accecories.defense : 0;

        return new BattleData_Player (
            addHp + 5, addAtk, addDef,
            activeNote_weap, activeNote_armor
        );
    }
    /// <summary>
    /// 0=Weapon 1=Armor 2=Acc
    /// </summary>
    /// <returns></returns>
    public int[] GetEqStatus () {
        return new int[] { weaponID, armorID, accID };
    }
    public int[] GetBattleData () {
        BattleData_Player tempValue = CountAllStat ();
        return new int[] {
            tempValue.attack_fix, //atk
                tempValue.defense_fix, //def
                tempValue.heatlth_fix //health
        };
    }

    public void SwitchWeapon (EquipmentInv InvData, out int lastItemID, out bool success) {
        success = false;
        lastItemID = weaponID;
        if (weaponID != InvData.id) {
            weapon = (WeaponBase) InvData.data;
            weaponID = InvData.id;
            success = true;
            weaponData = InvData;
        }
    }

    public void SwitchArmor (EquipmentInv InvData, out int lastItemID, out bool success) {
        success = false;
        lastItemID = armorID;

        if (armorID != InvData.id) {
            armor = (ArmorBase) InvData.data;
            armorID = InvData.id;
            success = true;
            armorData = InvData;
        }
    }

    public void SwitchAcc (EquipmentInv InvData, out int lastItemID, out bool success) {
        success = false;
        lastItemID = accID;
        if (accID != InvData.id) {
            accecories = (AccecoriesBase) InvData.data;
            accID = InvData.id;
            success = true;
            accData = InvData;
        }
    }

    public BattleData_Player GetPlayerData () {
        return CountAllStat ();
    }

    public void ReduceItemDurability (bool isWin) {
        if (weaponData != null) weaponData.ChangeDurability (isWin);
        if (accData != null) accData.ChangeDurability (isWin);
        if (armorData != null) armorData.ChangeDurability (isWin);
    }
}