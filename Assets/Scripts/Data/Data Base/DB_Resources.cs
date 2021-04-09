using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Resources : MonoBehaviour {

    public static DB_Resources _instance;
    [SerializeField] List<ResItemInven> inventory = new List<ResItemInven> ();

    public static void ModifQuantity (ResourcesItem item, int amount) {
        int index = _instance.inventory.FindIndex (x => x.id == item.itemID);
        if (index < 0) {
            _instance.inventory.Add (new ResItemInven () {
                id = item.itemID,
                    quantity = amount
            });
        }
        else _instance.inventory[index].quantity += amount;
    }
    public static ResItemInven GetItem (int itemID) {
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

    #region SaveLoad
#if UNITY_ANDROID
    static string savePath { get => Application.persistentDataPath + "/"; }
#elif UNITY_EDITOR
    static string savePath { get => Application.dataPath + "/SaveData/"; }
#else
    static string savePath { get => Application.persistentDataPath + "/"; }
#endif

    static string fileName { get => "resourcesData.akdat"; }

    public static void SaveResoucesData () {
        List<SaveData_DBRes> saved = new List<SaveData_DBRes> ();
        foreach (var item in _instance.inventory) {
            saved.Add (new SaveData_DBRes (item));
        }
        SaveLoadManager.SaveData<SaveData_DBRes> (saved, savePath, fileName);
    }
    public static void LoadResourcesData () {
        SaveLoadManager.LoadData<SaveData_DBRes> (savePath, fileName, out List<SaveData_DBRes> loadedData);
        if (loadedData.Count > 0) {
            foreach (var item in loadedData) {
                int index = _instance.inventory.IndexOf (GetItem (item.id));
                _instance.inventory[index].quantity = item.quantity;
            }
        }
        ResourcesUIControl.SetResoucesValue ();
    }
    #endregion
}
[Serializable]
public class SaveData_DBRes {
    public int id;
    public int quantity;

    public SaveData_DBRes () { }

    public SaveData_DBRes (ResItemInven invData) {
        this.id = invData.id;
        this.quantity = invData.quantity;
    }
}