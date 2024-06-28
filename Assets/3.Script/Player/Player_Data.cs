using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    a,
    b,
    c
}


[CreateAssetMenu(fileName = "Player_Data", menuName = "Scriptable Object/Player_Data")]
public class Player_Data : ScriptableObject
{
    //movespeed, type, prefab,
    public GameObject character_prefab;
    public string character_name;
    public CharacterType CharacterType;
    public float moveSpeed;
    

    public void Skill()
    {
        switch(CharacterType)
        {
            case CharacterType.a:
                break;
            case CharacterType.b:
                break;
            case CharacterType.c:
                break;

        }
    }

}



