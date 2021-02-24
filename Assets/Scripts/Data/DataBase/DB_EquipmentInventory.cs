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
    class DB_EqInvent_SaveData {
        public int cur_number;
        public List<EquipmentInv> inventory;

        public DB_EqInvent_SaveData (int cur_number, List<EquipmentInv> inventory) {
            this.cur_number = cur_number;
            this.inventory = inventory;
        }
    }
}

[Serializable]
public class EquipmentInv : IEquatable<EquipmentInv> {
    public int id;
    public EquipmentData data;
    public int cur_durability{ private set; get; }
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