﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "_EqW_Data", menuName = "EquipData/Weapon", order = 0)]
public class WeaponBase : EquipmentData {
    [Header ("Active Note")]

    public bool left;
    public bool right, diagonalRight, diagonalLeft;
    [HideInInspector] public bool up, down;
    public int[] getActiveNote () {
        List<int> activeNote = new List<int> ();

        if (up) activeNote.Add (1);
        if (down) activeNote.Add (2);
        if (left) activeNote.Add (3);
        if (right) activeNote.Add (4);
        if (diagonalRight) activeNote.Add (5);
        if (diagonalLeft) activeNote.Add (6);

        return activeNote.ToArray ();
    }
}