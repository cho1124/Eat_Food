using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    ������,
    ����ŷ,
    ����
}


[CreateAssetMenu(fileName = "Player_Data", menuName = "Scriptable Object/Player_Data")]
public class Player_Data : ScriptableObject
{
    //movespeed, type, prefab,
    public GameObject character_prefab;
    public string character_name;
    public CharacterType CharacterType;
    public float moveSpeed;
    public float cooltime;
}



