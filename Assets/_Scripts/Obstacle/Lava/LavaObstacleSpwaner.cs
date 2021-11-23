using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaObstacleSpwaner : MonoBehaviour
{
    [Header("Lava")]
    [SerializeField] private GameObject lavaPrefab;
    [SerializeField] private GameObject lavaParent;
    [SerializeField] private GameObject[] lavaZone;
    [SerializeField] private float lavaDelayTime;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanLava(lavaDelayTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpwanLava(float delaytime)
    {
        while (true)
        {
            float randX = Random.Range(lavaZone[0].transform.position.x, lavaZone[1].transform.position.x);
            float randZ = Random.Range(lavaZone[1].transform.position.z, lavaZone[2].transform.position.z);
            Vector3 spawnPos = new Vector3(randX, 10f, randZ);

            GameObject lava = Instantiate(lavaPrefab, spawnPos, Quaternion.identity) as GameObject;
            lava.transform.SetParent(lavaParent.transform, true);

            yield return new WaitForSeconds(delaytime);
        }
    }
}
