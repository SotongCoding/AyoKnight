using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBSaveControl : MonoBehaviour {
    public static DBSaveControl _instance;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        }
        else if (_instance != this) {
            Destroy (gameObject);
        }
    }
    public static void SaveData () {
        DB_EquipmentInventory.SaveEquipData ();
        DB_LevelData.SaveLevelData ();
        DB_Resources.SaveResoucesData ();
    }
    public static void LoadData () {
        DB_EquipmentInventory.LoadEquipData ();
        // DB_LevelData.LoadLevelData ();
        DB_Resources.LoadResourcesData ();
    }
}