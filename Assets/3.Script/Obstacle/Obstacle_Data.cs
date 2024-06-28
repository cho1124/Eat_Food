using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Obstacle_Data", menuName = "Scriptable Object/Obstacle_Data")]
public class Obstacle_Data : ScriptableObject
{
    // mass, size, prefab, 
    public GameObject foodPrefab;
    public float mass;
    public int attackDamage;
   
}
