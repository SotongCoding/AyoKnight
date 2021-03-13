﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICon_InvItem : MonoBehaviour {
    UICon_Inventory inventoryEquip;

    [SerializeField] int equipmentId;
    EquipmentInv equipment;
    EquipmentData baseData;
    public Image picture;
    public Text atk, def, hp;

    //Action UI
    int unlockCost;
    CostData repairCost;
    public Button unlock_btn, equip_btn;
    public Button repair_btn, enchant_btn;
    public Text unlockCost_text;

    //Durability Images
    public Image durability_img;
    public Gradient durability_color;

    //Requirement Data
    #region Requirement UI
    public Image[] matImage;
    public Text[] matAmountText;
    bool showResReq;

    public GameObject requirementOBJ;
    void ShowRequirement (CostData targetCostData) {
        for (int i = 0; i < targetCostData.resources.Length; i++) {
            if (targetCostData.resources[i].resourcesID != -1 && targetCostData.resources[i].resourcesID >= 0) {
                matImage[i].transform.parent.gameObject.SetActive (true);
                matImage[i].sprite = DB_Resources.GetItem (targetCostData.resources[i].resourcesID).baseData.itemPict;
            }

            matAmountText[i].text = targetCostData.resources[i].resourcesAmount.ToString ();
        }

    }
    #endregion

    private void Start () {
        inventoryEquip = FindObjectOfType<UICon_Inventory> ();
        equipment = DB_EquipmentInventory.GetItem (equipmentId);
        Initial ();
    }

    public int GetID () {
        return equipmentId;
    }
    void Initial () {
        baseData = equipment.GetBaseData ();
        repairCost = baseData.GetFullRepairCost ();
        picture.sprite = baseData.itemPict;
        SetStatText ();
        SetButton ();
        SetDurability ();

        //Unlock
        unlockCost = baseData.GetUnlockCost ();
        unlockCost_text.text = unlockCost.ToString ();

    }
    void SetStatText () {
        EquipmentStatus status = equipment.GetAllStat ();

        atk.text = status.attack.ToString ();
        def.text = status.defense.ToString ();
        hp.text = status.health.ToString ();

        atk.color = Color.white;
        def.color = Color.white;
        hp.color = Color.white;
    }
    void SetButton () {

        unlock_btn.gameObject.SetActive (!equipment.isAvaiable);
        unlockCost_text.transform.parent.gameObject.SetActive (!equipment.isAvaiable);
        enchant_btn.interactable = (baseData.GetEnchantData (equipment.GetEnchantLevel ()) != null);
        repair_btn.interactable = equipment.cur_durability < baseData.GetDurability ();
        equip_btn.interactable = !equipment.isEquip;
    }
    void SetDurability () {

        float fill_durability = ((float) equipment.cur_durability / (float) baseData.GetDurability ());
        durability_img.fillAmount = fill_durability;
        durability_img.color = durability_color.Evaluate (fill_durability);

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

            // DB_EquipmentInventory.SaveEquipData ();
            // DB_Resources.SaveResoucesData ();
        }
    }
    public void Repair () {
        CostData cost = new CostData ();
        cost.resources = new CostData.CostRequirement[repairCost.resources.Length];
        
        for (int i = 0; i < repairCost.resources.Length; i++) {
            int resAmount = (int) (repairCost.resources[i].resourcesAmount);

            cost.resources[i] = new CostData.CostRequirement (
                repairCost.resources[i].resourcesID,
                (int) (resAmount - (resAmount * durability_img.fillAmount)));
        }
        Debug.Log ("Amount Resources : " + cost.resources.Length);
        bool canRepair = CheckRequirement (cost);
        repair_btn.interactable = canRepair;
        if (showResReq) {

            if (canRepair) {
                ReduceItemAmount (repairCost);
                equipment.Repair ();
                //Initial ();
                SetDurability ();
                SetButton ();

                ResourcesUIControl.SetResoucesValue ();
                ShowRequirement (repairCost);

                // DB_EquipmentInventory.SaveEquipData ();
                // DB_Resources.SaveResoucesData ();
            }
        }
        else {
            Debug.Log ("Show resources UI");
            ShowRequirement (cost);
            ShowResReqUI (true);
        }

    }
    public void Equip () {
        inventoryEquip.SwitchEquip (equipment);
        SetStatText ();
        SetButton ();

        // DB_EquipmentInventory.SaveEquipData ();
        // DB_Resources.SaveResoucesData ();
    }
    public void Enchant () {

        CostData cost = equipment.GetBaseData ().GetEnchantData (
            equipment.GetEnchantLevel ()).requirement;

        bool canEnchant = CheckRequirement (cost);

        enchant_btn.interactable = canEnchant;

        if (showResReq) {
            if (canEnchant) {
                ReduceItemAmount (cost);
                equipment.Enchant ();
                SetStatText ();
                SetButton ();

                ResourcesUIControl.SetResoucesValue ();
                if (enchant_btn.interactable) {
                    ShowRequirement (cost);
                }
                else {
                    ShowResReqUI (false);
                }

                // DB_EquipmentInventory.SaveEquipData ();
                // DB_Resources.SaveResoucesData ();
            }
        }
        else {
            CheckNextEnchatStat ();
            ShowRequirement (cost);
            ShowResReqUI (true);
        }
    }

    ////////////////

    void CheckNextEnchatStat () {
        EquipmentStatus status = equipment.GetAllStat ();
        EquipmentStatus nextEnchant = equipment.GetBaseData ().GetEnchantData (equipment.GetEnchantLevel ()).GetStatus ();

        int tempAtk = status.attack + nextEnchant.attack;
        int tempDef = status.defense + nextEnchant.defense;
        int tempHeal = status.health + nextEnchant.health;

        atk.text = tempAtk.ToString ();
        def.text = tempDef.ToString ();
        hp.text = tempHeal.ToString ();

        atk.color = tempAtk > status.attack ? Color.green :
            tempAtk < status.attack ? Color.red : Color.white;

        def.color = tempDef > status.defense ? Color.green :
            tempDef < status.defense ? Color.red : Color.white;

        hp.color = tempHeal > status.health ? Color.green :
            tempHeal < status.health ? Color.red : Color.white;

    }
    bool CheckRequirement (CostData cost) {
        for (int i = 0; i < cost.resources.Length;) {
            if (cost.resources[i].resourcesID != -1) {
                Debug.Log ("Check Amount");
                if (DB_Resources.GetItem (cost.resources[i].resourcesID).quantity >= //your Data
                    cost.resources[0].resourcesAmount) { //cost Data 
                    Debug.Log ("Amount Reach");
                    i++;
                }
                else {
                    Debug.Log ("Amount not Reach");
                    break;
                }
            }
            else {
                Debug.Log ("ID not Correct");
                i++;
            }

            if (i == cost.resources.Length) {
                return true;
            }
        }
        Debug.Log ("One of all resouces not reach amount");

        return false;
    }
    void ReduceItemAmount (CostData cost) {
        for (int i = 0; i < cost.resources.Length; i++) {
            if (cost.resources[i].resourcesID != -1) {
                DB_Resources.GetItem (cost.resources[i].resourcesID).quantity -= cost.resources[i].resourcesAmount;
            }
        }
    }
    public void ShowResReqUI (bool isShowing) {
        if (!isShowing) {
            for (int i = 0; i < 4; i++) {
                matImage[i].transform.parent.gameObject.SetActive (false);
            }
            SetStatText ();
            SetButton ();
        }
        requirementOBJ.SetActive (isShowing);
        showResReq = isShowing;
    }
}