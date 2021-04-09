using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EquipmentInv : IEquatable<EquipmentInv> {
    public int id;
    [SerializeField] EquipmentData data;
    public bool isAvaiable;
    public bool isEquip;
    #region Enchant
    //enchant Data
    #endregion

    #region Getter Data
    public BaseStatus GetStatus () {
        return data.equipData.status;
    }
    public BaseStatus GetEnchantStatus (int enchantLevel) {
        return data.GetEnchantStat (enchantLevel);
    }
    public Type GetEquipBaseType () {
        return data.GetType ();
    }
    public EquipmentData GetBaseData () {
        return data;
    }
    #endregion
    public void UnlockItem () {
        isAvaiable = true;
    }
    // public void Repair () {
    //     cur_durability = GetStatus ().durability;
    // }

    public override bool Equals (object obj) {
        if (obj == null) return false;
        EquipmentInv item = obj as EquipmentInv;
        if (item == null) return false;
        else return Equals (item);
    }
    public bool Equals (EquipmentInv other) {
        if (other == null) return false;
        return (this.id.Equals (other.id));
    }
    public override int GetHashCode () {
        return base.GetHashCode ();
    }
}