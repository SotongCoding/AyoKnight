using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICon_Inventory : MonoBehaviour {
    public Transform itemPlace;

    PlayerData_Battle player;

    public Image equipedWeapon, equipedArmor, equipedAcc;
    public Text atkVal, defVal, hpVal;
    List<UICon_InvItem> itemsView = new List<UICon_InvItem> ();

    private void Awake () {
        itemsView = FindObjectsOfType<UICon_InvItem> ().ToList ();
        player = FindObjectOfType<PlayerData_Battle> ();
    }

    private void Start () {
        SetItemView ();
        UpdateStatusValue();
    }
    // Start is called before the first frame update
    public void CloseInventory () {
        SceneLoader.LoadScene (2);
    }
    void SetItemView () {
        int[] eqStatus = player.GetEqStatus ();

        if (eqStatus[0] != -1) equipedWeapon.sprite = DB_EquipmentInventory.GetItemSrpite (
            DB_EquipmentInventory.GetItem (eqStatus[0]).data.itemPath
        );
        if (eqStatus[1] != -1) equipedArmor.sprite = DB_EquipmentInventory.GetItemSrpite (
            DB_EquipmentInventory.GetItem (eqStatus[1]).data.itemPath
        );
        if (eqStatus[2] != -1) equipedAcc.sprite = DB_EquipmentInventory.GetItemSrpite (
            DB_EquipmentInventory.GetItem (eqStatus[2]).data.itemPath
        );
    }

    public void SwitchEquip (EquipmentInv data) {
        int lastID = -1;

        if (data.data.GetType () == typeof (WeaponBase)) {
            player.SwitchWeapon (data, out int lasItemID, out bool isSuccess);
            if (isSuccess) {
                lastID = lasItemID;
                equipedWeapon.sprite = DB_EquipmentInventory.GetItemSrpite (data.data.itemPath);
            }
        }
        else if (data.data.GetType () == typeof (ArmorBase)) {
            player.SwitchArmor (data, out int lasItemID, out bool isSuccess);
            if (isSuccess) {
                equipedArmor.sprite = DB_EquipmentInventory.GetItemSrpite (data.data.itemPath);
                lastID = lasItemID;
            }
        }
        else if (data.data.GetType () == typeof (AccecoriesBase)) {
            player.SwitchAcc (data, out int lasItemID, out bool isSuccess);
            if (isSuccess) {
                equipedAcc.sprite = DB_EquipmentInventory.GetItemSrpite (data.data.itemPath);
                lastID = lasItemID;
            }
        }

        if (lastID != -1) {
            DB_EquipmentInventory.UpdateEquip (lastID, false);
            GetLastEquip(lastID).ReInitial();
        }

        DB_EquipmentInventory.UpdateEquip (data.id, true);
        UpdateStatusValue ();
    }

    UICon_InvItem GetLastEquip (int id) {
        foreach (var item in itemsView) {
            if (item.GetID () == id) {
                return item;
            }
        }

        return null;
    }

    void UpdateStatusValue () {
        int[] stat = player.GetBattleData ();

        atkVal.text = stat[0].ToString ();
        defVal.text = stat[1].ToString ();
        hpVal.text = stat[2].ToString ();
    }

}