using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICon_InvItem : MonoBehaviour {
    UICon_Inventory inventoryEquip;

    [SerializeField] int equipmentId;
    EquipmentInv equipment;
    public Image picture;
    public Text atk, def, hp;

    //Action UI
    int unlockCost, repairCost;
    public Button unlock_btn, equip_btn;
    public Button repair_btn;
    public Text unlockCost_text;
    public Text repairCost_text;

    //Durability Images
    public Image durability_img;
    public Gradient durability_color;

    private void Start () {
        inventoryEquip = FindObjectOfType<UICon_Inventory> ();
        equipment = DB_EquipmentInventory.GetItem (equipmentId);
        Initial ();
    }

    public int GetID () {
        return equipmentId;
    }
    void Initial () {
        picture.sprite = DB_EquipmentInventory.GetItemSrpite (equipment.data.itemPath);
        atk.text = equipment.data.attack.ToString ();
        def.text = equipment.data.defense.ToString ();
        hp.text = equipment.data.health.ToString ();

        unlock_btn.gameObject.SetActive (!equipment.isAvaiable);
        equip_btn.gameObject.SetActive (equipment.isAvaiable);
        unlockCost_text.transform.parent.gameObject.SetActive (!equipment.isAvaiable);
        repair_btn.gameObject.SetActive (equipment.isAvaiable);

        repair_btn.interactable = equipment.cur_durability < equipment.data.durability;
        equip_btn.interactable = !equipment.isEquip;

        float fill_durability = ((float) equipment.cur_durability / (float) equipment.data.durability);
        durability_img.fillAmount = fill_durability;
        durability_img.color = durability_color.Evaluate (fill_durability);

        int cost = (int) equipment.data.fullRepairCost - (int) (equipment.data.fullRepairCost * fill_durability);
        if (equipment.cur_durability <= 0) {
            cost = cost * 2;
        }
        repairCost = cost;
        repairCost_text.text = "-" + repairCost.ToString ();

        unlockCost = equipment.data.unlockCost;
        unlockCost_text.text = unlockCost.ToString ();

    }
    public void ReInitial () {
        Initial ();
    }

    //Btn Action
    public void Unlock () {
        if (DB_Resources.GetItem (1).quantity >= unlockCost) {
            DB_Resources.GetItem (1).quantity -= unlockCost;
            equipment.UnlockItem ();
            Initial ();

            ResourcesUIControl.SetResoucesValue ();

            DB_EquipmentInventory.SaveEquipData ();
            DB_Resources.SaveResoucesData ();
        }
    }

    public void Repair () {
        if (DB_Resources.GetItem (2).quantity >= repairCost) {
            DB_Resources.GetItem (2).quantity -= repairCost;
            equipment.Repair ();
            Initial ();

            ResourcesUIControl.SetResoucesValue ();

            DB_EquipmentInventory.SaveEquipData ();
            DB_Resources.SaveResoucesData ();
        }

    }

    public void Equip () {
        inventoryEquip.SwitchEquip (equipment);
        Initial ();
        
        DB_EquipmentInventory.SaveEquipData ();
        DB_Resources.SaveResoucesData ();
    }
}