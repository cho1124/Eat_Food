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
    [SerializeField] private float map_scale;
    [Space]
    [Header("큐브 관련")]
    [SerializeField] private float cube_size;


    //private float seed;

    private void Start()
    {
        map = new GameObject[map_size, map_size];
        //seed = Random.Range(0, 10000f);
        //var noiseArr = await Task.Run(Generate_Noise);
        //Setting_Map(noiseArr);
        Setting_Map();
    }

    /*
    private float[,] Generate_Noise()
    {
        float[,] noiseArr = new float[map_size, map_size];
        for (int x = 0; x < map_size; x++)
        {
            for (int y = 0; y < map_size; y++)
            {
                noiseArr[x, y] = Mathf.PerlinNoise(
                    x * cube_size + seed,
                    y * cube_size + seed);
            }
        }
        return noiseArr;
    }
    */

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