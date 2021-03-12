using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootDropControler : MonoBehaviour {

    private void Awake () {
        BattleController._instance.onBattleDone += GetAllDrop;
    }

    List<LootDropData> dropItems = new List<LootDropData> ();
    [SerializeField] List<LootDropData> getItem = new List<LootDropData> ();
    public void Initial (List<LootDropData> data) {
        dropItems.AddRange (data);
        CalculateDrop ();
    }
    void GetAllDrop (bool isWin) {
        if (isWin) {
            FindObjectOfType<WinLosePU> ().GenerateItem (getItem);
            foreach (var item in getItem) {
                BaseItem target = item.GetData ();
                if (target.GetType () == (typeof (ResourcesItem)) ||
                    target.GetType ().IsSubclassOf (typeof (ResourcesItem))) {

                    Debug.Log ("Adding Item Resources");
                    DB_Resources.ModifQuantity ((ResourcesItem) target, item.GetFixDrop ());
                }
                else if (target.GetType () == (typeof (EquipmentData)) ||
                    target.GetType ().IsSubclassOf (typeof (EquipmentData))) {

                    Debug.Log ("Adding equipment");
                    DB_EquipmentInventory.AddItem ((EquipmentData) target, item.GetFixDrop ());
                }
            }
        }
        getItem.Clear ();
        dropItems.Clear ();
        DB_Resources.SaveResoucesData ();
        DB_EquipmentInventory.SaveEquipData ();
    }
    void CalculateDrop () {
        foreach (var item in dropItems) {
            int dropChance = Random.Range (0, 101);

            if (item.GetDrop (dropChance)) {
                int[] minMax = item.GetMINMAX ();
                int dropAmount = Random.Range (minMax[0], minMax[1] + 1);

                item.SetFixDrop (dropAmount);
                getItem.Add (item);
            }
        }
    }
}