using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DB_EquipmentInventory : MonoBehaviour {
    public static DB_EquipmentInventory _instance;
    [SerializeField] List<EquipmentInv> inventory = new List<EquipmentInv> ();
    [SerializeField] EquipedEquipment equipedEquipment = new EquipedEquipment ();

    private void Awake () {

        if (_instance == null) {
            _instance = this;
        }
        else if (_instance != this) {
            Destroy (gameObject);
        }
    }
    public static EquipmentInv GetItem (int equipmentId) {
        return _instance.inventory.Find (x => x.id == equipmentId);
    }
    public static void UpdateEquip (int itemId, bool isEquip) {
        int index = _instance.inventory.IndexOf (GetItem (itemId));
        _instance.inventory[index].isEquip = isEquip;
    }
    public void FirstInitial () {
        for (int i = 0; i < 3; i++) {
            inventory[i].UnlockItem ();
        }
    }

    public static EquipedEquipment GetEquipmentStatus () {
        return _instance.equipedEquipment;
    }
    public static List<EquipmentInv> GetAllItem () {
        return _instance.inventory;
    }
    #region SaveLoad
#if UNITY_ANDROID
    static string savePath { get => Application.persistentDataPath + "/"; }
#elif UNITY_EDITOR
    static string savePath { get => Application.dataPath + "/SaveData/"; }
#else
    static string savePath { get => Application.persistentDataPath + "/"; }
#endif
    static string fileName { get => "equipData.akdat"; }

    public static void SaveEquipData () {
        List<SaveData_DBInv> saved = new List<SaveData_DBInv> ();
        foreach (var item in _instance.inventory) {
            saved.Add (new SaveData_DBInv (item));
        }
        SaveLoadManager.SaveData<SaveData_DBInv> (saved, savePath, fileName);
    }
    public static void LoadEquipData () {
        SaveLoadManager.LoadData<SaveData_DBInv> (savePath, fileName, out List<SaveData_DBInv> loadedData);
        if (loadedData.Count > 0) {

            foreach (var item in loadedData) {
                int index = _instance.inventory.IndexOf (GetItem (item.id));

                _instance.inventory[index].isAvaiable = item.isAvaiable;
                _instance.inventory[index].isEquip = item.isEquip;

                if (item.isEquip) {
                    if (_instance.inventory[index].GetEquipBaseType () == typeof (WeaponBase)) {
                        _instance.equipedEquipment.SwitchWeapon (_instance.inventory[index], out int lastID, out bool success);
                    }
                    else if (_instance.inventory[index].GetEquipBaseType () == typeof (ArmorBase)) {
                        _instance.equipedEquipment.SwitchArmor (_instance.inventory[index], out int lastID, out bool success);
                    }
                    else if (_instance.inventory[index].GetEquipBaseType () == typeof (AccecoriesBase)) {
                        _instance.equipedEquipment.SwitchAcc (_instance.inventory[index], out int lastID, out bool success);
                    }
                }

            }
        }

    }
    #endregion
}

[Serializable]
public class EquipedEquipment {
    [SerializeField] int weaponID = -1;
    public EquipmentInv weapon { get; private set; }

    [SerializeField] int armorID = -1;
    public EquipmentInv armor { get; private set; }

    [SerializeField] int accID = -1;
    public EquipmentInv accecories { get; private set; }

    public EquipmentStatus CountAllStat () {
        // Status 
        EquipmentStatus allStat = new EquipmentStatus (weapon.GetStatus (), armor.GetStatus (), accecories.GetStatus ());

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

[Serializable]
public class EquipmentInv : IEquatable<EquipmentInv> {
    public int id;
    [SerializeField] EquipmentData data;
    public bool isAvaiable;
    public bool isEquip;
    #region Enchant
    //enchant Data
    #endregion

    #region Getter Data
    public EquipmentStatus GetStatus () {
        return data.status;
    }
    public EquipmentStatus GetEnchantStatus (int enchantLevel) {
        return data.GetEnchantStat (enchantLevel);
    }
    public Type GetEquipBaseType () {
        return data.GetType ();
    }
    public EquipmentData GetBaseData () {
        return data;
    }
    #endregion
    public void UnlockItem () {
        isAvaiable = true;
    }
    // public void Repair () {
    //     cur_durability = GetStatus ().durability;
    // }

    public override bool Equals (object obj) {
        if (obj == null) return false;
        EquipmentInv item = obj as EquipmentInv;
        if (item == null) return false;
        else return Equals (item);
    }
    public bool Equals (EquipmentInv other) {
        if (other == null) return false;
        return (this.id.Equals (other.id));
    }
    public override int GetHashCode () {
        return base.GetHashCode ();
    }
}

[Serializable]
public class SaveData_DBInv {
    public int id;
    public int cur_durability;
    public bool isAvaiable;
    public bool isEquip;

    public SaveData_DBInv () { }

    public SaveData_DBInv (EquipmentInv invData) {
        this.id = invData.id;
        this.isAvaiable = invData.isAvaiable;
        this.isEquip = invData.isEquip;
    }
}