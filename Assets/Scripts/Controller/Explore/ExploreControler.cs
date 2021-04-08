using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExploreControler : MonoBehaviour {
    public int curRoomNumber;

    [Header ("UI Element")]
    public Button[] choiceBtn = new Button[3];
    public void SetButtonAction (int index, System.Action buttonEvent, string message, Sprite buttonIcon) {
        choiceBtn[index].interactable = true;
        choiceBtn[index].GetComponentInChildren<Text> ().text = message;

        choiceBtn[index].onClick.RemoveAllListeners ();
        choiceBtn[index].onClick.AddListener (delegate {
            buttonEvent ();
        });
    }
    public void SetButtonAction (int index, bool interactable) {
        choiceBtn[index].interactable = interactable;
    }
    public void SetButtonAction (int index, string message) {
        choiceBtn[index].GetComponentInChildren<Text> ().text = message;
    }
    public Button backButton;
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
        for (int i = 1; i < roomsUI.Length; i++) {
            roomsUI[i].enabled = false;
        }

        GetNextRooms ();
        backButton.onClick.RemoveAllListeners ();
        backButton.onClick.AddListener (delegate {
            SceneLoader.LoadScene (1);
        });
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

            System.Action buttonCall = new System.Action (roomAction[avaiableRoom[roomNumber]]);
            SetButtonAction (i, buttonCall, roomDescription[avaiableRoom[roomNumber]], null);

            avaiableRoom.RemoveAt (roomNumber);
        }

    }
    void LoadBattle () {
        SceneLoader.LoadScene (3);
        roomsUI[0].enabled = false;

    }
    void LoadSmith () {
        FindObjectOfType<SmithRoomControl> ().GetActions ();
        backButton.onClick.RemoveAllListeners ();
        backButton.onClick.AddListener (delegate { LoadMainRoad (); });

        roomsUI[1].enabled = true;
        Debug.Log ("This will Load Smith");
    }

    void LoadBless () {
        FindObjectOfType<BlessRoomControl> ().GetActions ();
        backButton.onClick.RemoveAllListeners ();
        backButton.onClick.AddListener (delegate { LoadMainRoad (); });

        roomsUI[3].enabled = true;
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