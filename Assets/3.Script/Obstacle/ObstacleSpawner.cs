using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public  ObstacleSpawner instance;

    [SerializeField] private List<GameObject> food_prefabs;
    private List<GameObject> food_list = new List<GameObject>();
    public float SpawnInterval = 1f;
    public bool canISpawn = false;
    private int active_count = 0;

    
    



    void Start()
    {
        instance = this;
        canISpawn = true;

        //다른 종류의 음식 추가할 때는 밑에 for문에서 food_prefabs의 인덱스만 바꿔서 복붙하면 됩니당
        for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < food_prefabs.Count; j++)
            {
                GameObject obs = Instantiate(food_prefabs[j], transform);
                food_list.Add(obs);
                obs.SetActive(false);
            }
        }

        StartCoroutine(ObsSpawn());
    }

    public GameObject List_Active_True()
    {
        int index;
        while (true)
        {
            index = Random.Range(0, food_list.Count);
            if(!food_list[index].activeSelf)
            {
                active_count++;
                food_list[index].SetActive(true);
                return food_list[index];
            }
        }
    }

    public void List_Active_False(GameObject food)
    {
        active_count--;
        int index = food_list.IndexOf(food);
        food_list[index].SetActive(false);
    }

    IEnumerator ObsSpawn()
    {
        float map_width = GameObject.Find("Map_Generator").GetComponent<Map_Generator>().map_width_get;
        Vector3 randomVector;
        Vector3 randomScale;
        float xPos;
        float zPos;
        float scale;

        while (canISpawn)
        {
            if(food_list.Count!=0 && active_count != food_list.Count)
            {
                xPos = Random.Range(-(map_width / 2), (map_width / 2));
                zPos = Random.Range(-(map_width / 2), (map_width / 2));
                randomVector = new Vector3(xPos, 30, zPos);

                scale = Random.Range(0.5f, 3f);
                randomScale = new Vector3(scale, scale, scale);
                
                GameObject obs = List_Active_True();
                obs.transform.position =  randomVector;
                obs.transform.localScale = randomScale;
            }
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
