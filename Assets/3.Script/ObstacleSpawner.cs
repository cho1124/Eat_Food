using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject obsPref;

    public  ObstacleSpawner instance;
    public Queue<GameObject> o_queue = new Queue<GameObject>();

    public bool canISpawn = false;
    
    public float xPos;
    public float zPos;
    private Vector3 randomVector;
    private Vector3 randomScale;
    public float SpawnInterval = 1f;
    public float ObjRotateSpeed = 3f;

    private float map_width;


    // Start is called before the first frame update
    void Start()
    {
        map_width = GameObject.Find("Map_Generator").GetComponent<Map_Generator>().map_width_get;
        instance = this;
        canISpawn = true;
        for(int i = 0; i < 10; i++)
        {
            GameObject obs = Instantiate(obsPref, this.gameObject.transform);
            o_queue.Enqueue(obs);
            obs.SetActive(false);
        }

        StartCoroutine(ObsSpawn());
    }

    public GameObject GetQueue()
    {
        GameObject obs = o_queue.Dequeue();
        obs.SetActive(true);

        return obs;
    }

    public void InsertQueue(GameObject p_object)
    {
        o_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    IEnumerator ObsSpawn()
    {
        while(canISpawn)
        {
            if(o_queue.Count!=0)
            {
                xPos = Random.Range(-(map_width / 2), (map_width / 2));
                zPos = Random.Range(-(map_width / 2), (map_width / 2));
                randomVector = new Vector3(xPos, 30, zPos);
                GameObject obs = GetQueue();
                obs.transform.position =  randomVector;

                var tmp = Random.Range(0.5f, 3f);
                randomScale = new Vector3(tmp, tmp, tmp);
                obs.transform.localScale = randomScale;
            }
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }*/
}
