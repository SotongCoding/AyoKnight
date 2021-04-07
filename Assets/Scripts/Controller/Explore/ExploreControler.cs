using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExploreControler : MonoBehaviour {
    public int curRoomNumber;

    [Header ("UI Element")]
    public Button[] choiceBtn = new Button[3];
    public Canvas[] roomsUI; //0. Main Road 1. Smith 2.Mining 3.Bless
    public Sprite[] roomIcon = new Sprite[4];
    public string[] roomDescription = new string[] {
        "Fight some  Creature",
        "Strengthen your Weapon here",
        "Get Some Ore Here",
        "Bless your self"
    };

    System.Action[] roomAction;

    private void Awake () {
        roomAction = new System.Action[] {
            LoadBattle,
            LoadSmith,
            LoadMining,
            LoadBless
        };
    }

    private void Start () {
        LoadMainRoad ();
    }
    public void LoadMainRoad () {
        roomsUI[0].enabled = true;
        GetNextRooms ();
    }

    #region Random Room
    void GetNextRooms () {
        List<int> avaiableRoom = new List<int> () {
            0,
            1,
            2,
            3
        };

        for (int i = 0; i < 3; i++) {
            int roomNumber = Random.Range (0, avaiableRoom.Count);

            // choiceBtn[i].GetComponentInChildren<SpriteRenderer> ().sprite = roomIcon[i];
            choiceBtn[i].GetComponentInChildren<Text> ().text = roomDescription[avaiableRoom[roomNumber]];

            System.Action buttonCall = new System.Action (roomAction[avaiableRoom[roomNumber]]);
            choiceBtn[i].onClick.AddListener (delegate { buttonCall (); });

            avaiableRoom.RemoveAt (roomNumber);
        }

    }
    void LoadBattle () {
        SceneLoader.LoadScene (3);
        roomsUI[0].enabled = false;

    }
    void LoadSmith () {
        Debug.Log ("This will Load Smith");
    }

    void LoadBless () {
        Debug.Log ("This will Load Bless");
    }

    void LoadMining () {
        Debug.Log ("This will Load Mining");
    }

    void LoadBoss () {
        Debug.Log ("This will Load Boss Battle");
    }

    #endregion
}