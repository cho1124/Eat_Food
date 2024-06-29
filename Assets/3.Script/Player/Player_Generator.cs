using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Generator : MonoBehaviour
{
    public List<Player_Data> players;
    //[SerializeField] float player_movemtSpeed;
    private GameObject player;
    private Player_Data player_data;
    private PlayerControl playerControl;

    private void Start()
    {
        PlayerCreate();
    }

    public void PlayerCreate()
    {
        player_data = players[GameManager.instance.selectedCharacter];

        player = Instantiate(player_data.character_prefab, new Vector3(30f, 2f, 30f), Quaternion.identity);
        //player.transform.position = new Vector3(30f, 2f, 30f);
        playerControl = player.AddComponent<PlayerControl>();
        playerControl.moveSpeed = player_data.moveSpeed;
        playerControl.characterType = player_data.CharacterType;
        playerControl.cooltime = player_data.cooltime;

    }

}
