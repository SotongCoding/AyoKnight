using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DropItemControl : MonoBehaviour {
    public Image picture;
    public Text amount;
    // Start is called before the first frame update
    public void Initial (LootDropData data){
        picture.sprite = data.GetData ().itemPict;
        amount.text = data.GetFixDrop ().ToString ();
    }
}