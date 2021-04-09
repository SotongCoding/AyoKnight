using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessRoomControl : MonoBehaviour {

    public BaseStatus playerBaseStat { get => new BaseStatus (0, 0, 5); }
    List<BaseStatus> modifStat = new List<BaseStatus> ();

    ExploreControler exploreControler;
    SmithRoomControl smithRoom;
    private void Awake () {
        exploreControler = FindObjectOfType<ExploreControler> ();
        smithRoom = FindObjectOfType<SmithRoomControl> ();
    }

    public BaseStatus GetAllStatus () {
        int atk = 0, def = 0, health = 0;

        if (modifStat.Count != 0) {
            foreach (var status in modifStat) {
                atk += status.attack;
                def += status.defense;
                health += status.health;
            }
        }

        return new BaseStatus (
            playerBaseStat,
            new BaseStatus (atk, def, health));
    }

    public void GetActions () {

        for (int i = 0; i < 3; i++) {
            BaseStatus created = CreateBaseStatus ();
            BlessAction (i, created);
        }
    }
    void BlessAction (int buttonIndex, BaseStatus createdStatus) {
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

    BaseStatus CreateBaseStatus () {
        int maxRange = 3;

        int atk = Random.Range (0, maxRange + 1);
        int def = Random.Range (0, maxRange + 1);
        int health = Random.Range (0, maxRange + 1) * 5;

        return new BaseStatus (atk, def, health);
    }

    void AddBaseStatus (BaseStatus status) {
        modifStat.Add (status);
    }
}