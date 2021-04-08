using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithRoomControl : MonoBehaviour {
    #region UI
    [Header ("UI Section")]
    public Image weaponPict;
    public Image armorPict;
    public Image accPict;

    public Text[] enchantLevel; // 0.weapon 1.armor 2.acc
    public Text[] status; //0.atk 1.def 2.hp

    #endregion
    [SerializeField] EquipOnBattle weapon;
    [SerializeField] EquipOnBattle armor;
    [SerializeField] EquipOnBattle acc;

    System.Action<int>[] avaiableAction;
    ExploreControler exploreControler;

    private void Awake () {
        SetEquipment (DB_EquipmentInventory.GetEquipmentStatus ());
        exploreControler = FindObjectOfType<ExploreControler> ();

        avaiableAction = new System.Action<int>[] {
            UpgradeWeapon,
            UpgradeArmor,
            UpgradeAccesory
        };
    }

    public EquipOnBattle[] GetBattleEquip () {
        return new EquipOnBattle[3] {
            weapon,
            armor,
            acc
        };
    }

    public EquipmentStatus GetAllStat () {
        return new EquipmentStatus (weapon.GetTotalStat (), armor.GetTotalStat (), acc.GetTotalStat ());
    }

    public void GetActions () {
        List<int> avaiableNumber = new List<int> () {
            0,
            1,
            2
        };

        for (int i = 0; i < 3; i++) {
            int selectAction = Random.Range (0, avaiableNumber.Count);
            avaiableAction[avaiableNumber[selectAction]] (i);

            avaiableNumber.RemoveAt (selectAction);
        }
    }
    void SetEquipment (EquipedEquipment playerEquipment) {
        weapon = new EquipOnBattle (playerEquipment.weapon.GetBaseData (), 0);
        armor = new EquipOnBattle (playerEquipment.armor.GetBaseData (), 0);
        acc = new EquipOnBattle (playerEquipment.accecories.GetBaseData (), 0);

        weaponPict.sprite = weapon.baseData.itemPict;
        armorPict.sprite = armor.baseData.itemPict;
        accPict.sprite = acc.baseData.itemPict;

        UpdateStatusView ();
    }
    void UpdateStatusView () {
        enchantLevel[0].text = "Level " + (weapon.enchanLevel + 1).ToString ();
        enchantLevel[1].text = "Level " + (armor.enchanLevel + 1).ToString ();
        enchantLevel[2].text = "Level " + (acc.enchanLevel + 1).ToString ();

        EquipmentStatus totalStat = new EquipmentStatus (
            weapon.GetTotalStat (),
            armor.GetTotalStat (),
            acc.GetTotalStat (),
            // Base Player Stat
            new EquipmentStatus (EquipType.none, 0, 0, 5));

        status[0].text = totalStat.attack.ToString ();
        status[1].text = totalStat.defense.ToString ();
        status[2].text = totalStat.health.ToString ();

    }
    void UpgradeWeapon (int buttonIndex) {
        EnchantData nextStat = weapon.baseData.GetEnchantData (weapon.enchanLevel);
        if (nextStat == null) {
            exploreControler.SetButtonAction (buttonIndex, false);
            exploreControler.SetButtonAction (buttonIndex, "");
            return;
        }

        string message = "Weapon" +
            "\nATK : " + nextStat.atk +
            "\nDEF : " + nextStat.def +
            "\nHP : " + nextStat.health;

        exploreControler.SetButtonAction (buttonIndex,
            //Upgrade Event
            delegate {
                if (weapon.enchanLevel < 5) {
                    weapon.enchanLevel++;
                    Debug.Log ("Level Weapon Increase");
                }
                else {
                    Debug.Log ("Reach Max Upgrade");
                }

                UpdateStatusView ();
                exploreControler.SetButtonAction (buttonIndex, false);
            },

            message, null);

    }
    void UpgradeArmor (int buttonIndex) {
        EnchantData nextStat = armor.baseData.GetEnchantData (armor.enchanLevel);
        if (nextStat == null) {
            exploreControler.SetButtonAction (buttonIndex, false);
            exploreControler.SetButtonAction (buttonIndex, "");
            return;
        }

        string message = "Armor" +
            "\nATK : " + nextStat.atk +
            "\nDEF : " + nextStat.def +
            "\nHP : " + nextStat.health;

        exploreControler.SetButtonAction (buttonIndex,

            //Upgrade Event
            delegate {
                if (armor.enchanLevel < 5) {
                    armor.enchanLevel++;
                    Debug.Log ("Level Armor Increase");
                }
                else {
                    Debug.Log ("Reach Max Upgrade");
                }
                UpdateStatusView ();
                exploreControler.SetButtonAction (buttonIndex, false);
            },

            message, null);

    }
    void UpgradeAccesory (int buttonIndex) {
        EnchantData nextStat = acc.baseData.GetEnchantData (acc.enchanLevel);
        if (nextStat == null) {
            exploreControler.SetButtonAction (buttonIndex, false);
            exploreControler.SetButtonAction (buttonIndex, "");
            return;
        }

        string message = "Accesories" +
            "\nATK : " + nextStat.atk +
            "\nDEF : " + nextStat.def +
            "\nHP : " + nextStat.health;

        exploreControler.SetButtonAction (buttonIndex,

            //Upgrade Event
            delegate {
                if (acc.enchanLevel < 5) {
                    acc.enchanLevel++;
                    Debug.Log ("Level Accesories Increase");
                }
                else {
                    Debug.Log ("Reach Max Upgrade");
                }
                UpdateStatusView ();
                exploreControler.SetButtonAction (buttonIndex, false);
            },

            message, null);

    }
}

[System.Serializable]
public class EquipOnBattle {
    public EquipmentData baseData; //{ private set; get; }
    public int enchanLevel;

    public EquipmentStatus GetTotalStat () {
        return new EquipmentStatus (baseData.status, baseData.GetEnchantStat (enchanLevel));
    }
    public EquipOnBattle (EquipmentData baseData, int enchanLevel) {
        this.baseData = baseData;
        this.enchanLevel = enchanLevel;
    }
}