using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResItemInven {
    public int id;
    public int quantity;
    public ResourcesItem baseData;

    public override bool Equals (object obj) {
        if (obj == null) return false;
        ResItemInven item = obj as ResItemInven;
        if (item == null) return false;
        else return Equals (item);
    }
    public override int GetHashCode () {
        return id;
    }

    public bool Equals (ResItemInven other) {
        if (other == null) return false;
        return (this.id.Equals (other.id));
    }

    public override string ToString () {
        return id + " : " + quantity;
    }
}