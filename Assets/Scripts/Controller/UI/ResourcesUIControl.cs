using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUIControl : MonoBehaviour {
    [Header ("ResorcesUI")]
    public Text redMatAmount;
    public Text blueMatAmount;
    public GameObject resorcesUI_obj;

    public static ResourcesUIControl _instance;
    private void Awake () {
        if (_instance == null) {
            _instance = this;
        }
        else if (_instance != this) {
            Destroy (gameObject);
        }
    }

    public static void SetResoucesValue () {
        _instance.redMatAmount.text = DB_Resources.GetItem (1).quantity.ToString ();
        _instance.blueMatAmount.text = DB_Resources.GetItem (2).quantity.ToString ();
    }

    public static void TurnUION (bool isON) {
        if(isON) SetResoucesValue();
        _instance.resorcesUI_obj.SetActive (isON);
    }
}