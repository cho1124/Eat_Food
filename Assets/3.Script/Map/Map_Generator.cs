using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map_Generator : MonoBehaviour
{

    [Header("맵 관련")]
    [SerializeField] private GameObject[,] map;
    [SerializeField] private GameObject cube_original;
    [SerializeField] private int map_size;
    [Space]
    [Header("큐브 관련")]
    [SerializeField] private float cube_size;

    public float map_width_get;


    private void Awake()
    {
        map = new GameObject[map_size, map_size];
        map_width_get = cube_size * map_size;
        Setting_Map();
    }

    private void Setting_Map()
    {
        Vector3 position = new Vector3();
        Vector3 scale = new Vector3(cube_size, cube_size, cube_size);

        for (int x = 0; x < map_size; x++)
        {
            for (int y = 0; y < map_size; y++)
            {
                position.Set(cube_size * (-(map_size / 2) + x), 0, cube_size * (-(map_size / 2) + y));
                map[x, y] = Instantiate(cube_original, position, Quaternion.identity, transform);
                map[x, y].transform.localScale = scale;
            }
        }
    }
}