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
        Vector3 zero_position = new Vector3(cube_size / 2, 0, cube_size / 2);
        Vector3 position = new Vector3();
        Vector3 scale = new Vector3(cube_size, cube_size, cube_size);

        for (int x = 0; x < map_size; x++)
        {
            for (int y = 0; y < map_size; y++)
            {
                position.Set(cube_size * x, 0, cube_size * y);
                map[x, y] = Instantiate(cube_original, zero_position + position, Quaternion.identity, transform);
                map[x, y].transform.localScale = scale;
            }
        }
    }

    public Vector2Int Position_To_Index(Vector3 position)
    {
        float x = position.x;
        float z = position.z;

        int index_x = Mathf.RoundToInt((x - (cube_size / 2)) / cube_size);
        int index_z = Mathf.RoundToInt((z - (cube_size / 2)) / cube_size);

        return new Vector2Int(index_x, index_z);
    }

    public bool Index_To_Position(Vector2Int index, out Vector3 position_output)
    {
        int x = Mathf.Clamp(index.x, 0, map_size - 1);
        int y = Mathf.Clamp(index.y, 0, map_size - 1);

        position_output = map[x, y].transform.position;
        
        if (map[x, y].activeSelf) return true;
        else return false;
    }

    public void Repair_Floor(Vector2Int index, int area)
    {
        int x;
        int y;
        
        for(int i = index.x - area; i <= index.x + area; i++)
        {
            for(int j = index.y - area; j <= index.y + area; j++)
            {
                x = Mathf.Clamp(i, 0, map_size - 1);
                y = Mathf.Clamp(j, 0, map_size - 1);
                
                map[x, y].SetActive(false);
                map[x, y].SetActive(true);
            }
        }
    }
}