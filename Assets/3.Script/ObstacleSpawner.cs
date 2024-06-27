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


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canISpawn = true;
        for(int i = 0; i<10; i++)
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
                xPos = Random.Range(-30, 30);
                zPos = Random.Range(-30, 30);
                randomVector = new Vector3(xPos, 30, zPos);
                GameObject obs = GetQueue();
                obs.transform.position = gameObject.transform.position + randomVector;

                var tmp = Random.Range(320f, 800f);
                randomScale = new Vector3(tmp, tmp, tmp);
                obs.transform.localScale = randomScale;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }*/
}
