using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseItem : ScriptableObject {
    public string baseItemID;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public string itemPath;
}