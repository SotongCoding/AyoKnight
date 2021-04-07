using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Battle : MonoBehaviour {
    [SerializeField] int weaponID = -1;
    EquipmentInv weapon;

    [SerializeField] int armorID = -1;
    EquipmentInv armor;

    [SerializeField] int accID = -1;
    EquipmentInv accecories;

    BattleData_Player CountAllStat () {
        int addHp = 0, addAtk = 0, addDef = 0;
        int[] activeNote_weap = new int[4], activeNote_armor = new int[4];
        //Weapon
        addHp += weapon != null ? weapon.GetAllStat ().health : 0;
        addAtk += weapon != null ? weapon.GetAllStat ().attack : 0;
        addDef += weapon != null ? weapon.GetAllStat ().defense : 0;

        //Armor
        addHp += armor != null ? armor.GetAllStat ().health : 0;
        addAtk += armor != null ? armor.GetAllStat ().attack : 0;
        addDef += armor != null ? armor.GetAllStat ().defense : 0;

        //Acc
        addHp += accecories != null ? accecories.GetAllStat ().health : 0;
        addAtk += accecories != null ? accecories.GetAllStat ().attack : 0;
        addDef += accecories != null ? accecories.GetAllStat ().defense : 0;

        // Note
        if (weaponID != -1) activeNote_weap = weapon.GetBaseData ().GetActiveNote ();
        if (armorID != -1) activeNote_armor = armor.GetBaseData ().GetActiveNote ();

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

    #region Switch Equip
    public void SwitchWeapon (EquipmentInv InvData, out int lastItemID, out bool success) {
        success = false;
        lastItemID = weaponID;
        if (weaponID != InvData.id) {
            weaponID = InvData.id;
            success = true;
            weapon = InvData;
        }
    }
    public void SwitchArmor (EquipmentInv InvData, out int lastItemID, out bool success) {
        success = false;
        lastItemID = armorID;

        if (armorID != InvData.id) {
            armorID = InvData.id;
            success = true;
            armor = InvData;
        }
    }
    public void SwitchAcc (EquipmentInv InvData, out int lastItemID, out bool success) {
        success = false;
        lastItemID = accID;
        if (accID != InvData.id) {
            accID = InvData.id;
            success = true;
            accecories = InvData;
        }
    }
    #endregion
    public BattleData_Player GetPlayerData () {
        return CountAllStat ();
    }

    public void ReduceItemDurability (bool isWin) {

        if (weapon != null) {
            weapon.ChangeDurability (isWin, out bool isDestroy);
            if (isDestroy) {
                weaponID = -1;
                weapon = null;
            }
        }
        if (armor != null) {
            armor.ChangeDurability (isWin, out bool isDestroy);
            if (isDestroy) {
                armorID = -1;
                armor = null;
            }
        }
        if (accecories != null) {
            accecories.ChangeDurability (isWin, out bool isDestroy);
            if (isDestroy) {
                accID = -1;
                accecories = null;
            }
        }
        DB_EquipmentInventory.SaveEquipData ();
    }
}