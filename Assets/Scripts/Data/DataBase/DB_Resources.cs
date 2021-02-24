using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Resources : MonoBehaviour {

    public static DB_Resources _instance;

   const string path = "UI/Inventory/ResourcesItems/";
    [SerializeField] List<ResItemInven> inventory = new List<ResItemInven> ();
    public static Sprite GetItemSrpite (string itemNamePaths) {
        return Resources.Load<Sprite> (path + itemNamePaths);
    }

    public static void ModifQuantity (ResourcesItem item, int amount) {
        int index = _instance.inventory.FindIndex (x => x.Equals (item));
        if (index < 0) {
            _instance.inventory.Add (new ResItemInven () {
                id = item.itemID,
                    quantity = amount
            });
        }
        else _instance.inventory[index].quantity += amount;
    }
    public static ResItemInven GetItem(int itemID){
         return _instance.inventory.Find (x => x.id == itemID);
    }
    // Start is called before the first frame update
    private void Awake () {
        if (_instance == null) {
            _instance = this;
        }
        else if (_instance != this) {
            Destroy (gameObject);
        }
    }
}

[Serializable]
public class ResItemInven {
    public int id;
    public int quantity;
    public ResourcesItem baseData;

    public override bool Equals (object obj) {
        if (obj == null) return false;
        ResItemInven item = obj as ResItemInven;
        if (item == null) return false;
        else return Equals (item);
    }
    public override int GetHashCode () {
        return id;
    }

    public bool Equals (ResItemInven other) {
        if (other == null) return false;
        return (this.id.Equals (other.id));
    }

    public override string ToString () {
        return id + " : " + quantity;
    }
}