using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseItem : ScriptableObject {
    public string baseItemID;
    public string itemName;
    public Sprite itemPict;
    [TextArea]
    public string itemDescription;
}