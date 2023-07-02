using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    /* create a variable for collider which name is spawnarea */
    private Collider spawnArea;
    public GameObject[] fruitPrefabs;

    public GameObject bombPrefab;
    [Range(0f, 1f)]
    public float bombChance = 0.05f;


    /* time to get spawn*/
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;


    /* when spawn on fruits then some variation in terms of it and get launched diagonally*/
    public float minAngle = -15f;
    public float maxAngle = 15f;


    /* forces for launching fruits*/
    public float minForce = 18f;
    public float maxForce = 22f;

    /*fruits life when spawn*/

    public float maxLifetime = 5f;


    /*assign this awake function*/
     private void Awake()
     {
        spawnArea = GetComponent<Collider>();

     }

     private void OnEnable()
     {
        StartCoroutine( Spawn());
     }

     private void OnDisable()
     {
        StopAllCoroutines();
     }

     private IEnumerator Spawn()
     {
        yield return new WaitForSeconds(2f);

        /* for generating fruits in loop*/
        while(enabled)
        {
            /* pick fruits from fruits_prefabs array randomly */
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if(Random.value < bombChance)
            {
               prefab = bombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            /* actual spawning start due to instantiate function in which object pass and clones prefabs*/
            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit , maxLifetime); 

            /* initial force for fruits launched in air by the force and some physics applied on it*/
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force , ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

        }
        
     }

    
}
