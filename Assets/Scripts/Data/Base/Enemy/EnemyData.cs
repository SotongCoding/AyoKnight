using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "_En_Data", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject {
    public int health;
    public int attack;
    public int turnAmount;
    public int noteAmount;
    public Sprite picture;
}