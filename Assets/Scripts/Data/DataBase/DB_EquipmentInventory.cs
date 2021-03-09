using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DB_EquipmentInventory : MonoBehaviour {
    const string path = "UI/Inventory/Equipments/";

    public static Sprite GetItemSrpite (string itemNamePaths) {
        return Resources.Load<Sprite> (path + itemNamePaths);
    }

    public static DB_EquipmentInventory _instance;
    [SerializeField] List<EquipmentInv> inventory = new List<EquipmentInv> ();

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
    public static List<EquipmentInv> GetAllItem () {
        return _instance.inventory;
    }

    #region SaveLoad
    static string savePath { get => Application.dataPath + "/SaveData/"; }
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
            PlayerData_Battle player = FindObjectOfType<PlayerData_Battle> ();

            foreach (var item in loadedData) {
                int index = _instance.inventory.IndexOf (GetItem (item.id));

                _instance.inventory[index].cur_durability = item.cur_durability;
                _instance.inventory[index].isAvaiable = item.isAvaiable;
                _instance.inventory[index].isEquip = item.isAvaiable;

                if (item.isEquip) {
                    if (_instance.inventory[index].data.GetType () == typeof (WeaponBase)) {
                        player.SwitchWeapon (_instance.inventory[index], out int lastID, out bool success);
                    }
                    else if (_instance.inventory[index].data.GetType () == typeof (ArmorBase)) {
                        player.SwitchArmor (_instance.inventory[index], out int lastID, out bool success);
                    }
                    else if (_instance.inventory[index].data.GetType () == typeof (AccecoriesBase)) {
                        player.SwitchAcc (_instance.inventory[index], out int lastID, out bool success);
                    }
                }

            }
        }

    }
    #endregion
}

[Serializable]
public class EquipmentInv : IEquatable<EquipmentInv> {
    public int id;
    public EquipmentData data;
    public int cur_durability;
    public bool isAvaiable;
    public bool isEquip;

    public void UpdateDurability (int increaseAmount) {
        cur_durability += data.durability;
    }
    public void UnlockItem () {
        isAvaiable = true;
        cur_durability = data.durability;
    }
    public void Repair () {
        cur_durability = data.durability;
    }

    public void ChangeDurability (bool isWin) {
        int ran_amout = UnityEngine.Random.Range (1, Mathf.CeilToInt (((isWin ? 0.2f : 0.1f) * data.durability)));
        cur_durability -= ran_amout;
    }
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
        this.cur_durability = invData.cur_durability;
        this.isAvaiable = invData.isAvaiable;
        this.isEquip = invData.isEquip;
    }
}