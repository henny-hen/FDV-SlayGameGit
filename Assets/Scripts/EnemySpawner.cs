using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float xlimit;
    public GameObject asteroidPre;


    public float spawnRatePerMin = 30f;
    public float spawnRateInc = 1f;
    private float spawnNext = 0;
    public float maxTimeLife = 4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > spawnNext)
        {
            float rand = Random.Range(-xlimit, xlimit); 

            spawnNext = Time.time + 60 / spawnRatePerMin;

            spawnRatePerMin += spawnRateInc;
            
            Vector2 spawnPosition = new Vector2(rand, 5f);

            GameObject meteor = Instantiate(asteroidPre, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxTimeLife);
        }
        }
}
