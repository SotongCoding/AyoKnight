using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_EnemyPictControl : MonoBehaviour {
    public Image enemypict;
    public void SetPict (Sprite targetSprite) {
        enemypict.sprite = targetSprite;
        enemypict.SetNativeSize ();

        this.transform.localPosition = Vector3.zero;
    }
}