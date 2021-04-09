using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipedEquipment {
    [SerializeField] int weaponID = -1;
    public EquipmentInv weapon { get; private set; }

    [SerializeField] int armorID = -1;
    public EquipmentInv armor { get; private set; }

    [SerializeField] int accID = -1;
    public EquipmentInv accecories { get; private set; }

    public BaseStatus CountAllStat () {
        // Status 
        BaseStatus allStat = new BaseStatus (weapon.GetStatus (), armor.GetStatus (), accecories.GetStatus ());

        return allStat;
    }
    /// <summary>
    /// 0=Weapon 1=Armor 2=Acc
    /// </summary>
    /// <returns></returns>
    public int[] GetEqStatus () {
        return new int[] { weaponID, armorID, accID };
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
}