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
        player_data = players[2];

        player = Instantiate(player_data.character_prefab);
        playerControl = player.AddComponent<PlayerControl>();
        playerControl.speed = player_data.moveSpeed;


    }

}
