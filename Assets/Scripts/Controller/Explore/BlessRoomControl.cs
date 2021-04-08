using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessRoomControl : MonoBehaviour {

    public EquipmentStatus playerBaseStat { get => new EquipmentStatus (EquipType.none, 0, 0, 5); }
    List<EquipmentStatus> modifStat = new List<EquipmentStatus> ();

    ExploreControler exploreControler;
    SmithRoomControl smithRoom;
    private void Awake () {
        exploreControler = FindObjectOfType<ExploreControler> ();
        smithRoom = FindObjectOfType<SmithRoomControl> ();
    }

    public EquipmentStatus GetAllStatus () {
        int atk = 0, def = 0, health = 0;

        if (modifStat.Count != 0) {
            foreach (var status in modifStat) {
                atk += status.attack;
                def += status.defense;
                health += status.health;
            }
        }

        return new EquipmentStatus (playerBaseStat,
            new EquipmentStatus (EquipType.none, atk, def, health));
    }

    public void GetActions () {

        for (int i = 0; i < 3; i++) {
            EquipmentStatus created = CreateBaseStatus ();
            BlessAction (i, created);
        }
    }
    void BlessAction (int buttonIndex, EquipmentStatus createdStatus) {
        exploreControler.SetButtonAction (buttonIndex,
            delegate {
                AddBaseStatus (createdStatus);
                exploreControler.SetButtonAction (buttonIndex, false);
            },

            "\nATK : " + createdStatus.attack +
            "\nDEF : " + createdStatus.defense +
            "\nHP : " + createdStatus.health,

            null);
    }

    EquipmentStatus CreateBaseStatus () {
        int maxRange = 3;

        int atk = Random.Range (0, maxRange + 1);
        int def = Random.Range (0, maxRange + 1);
        int health = Random.Range (0, maxRange + 1) * 5;

        return new EquipmentStatus (EquipType.none, atk, def, health);
    }

    void AddBaseStatus (EquipmentStatus status) {
        modifStat.Add (status);
    }
}