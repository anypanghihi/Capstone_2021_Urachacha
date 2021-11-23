using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleSpawner : MonoBehaviour
{
    [Header("Ice")]
    [SerializeField] private GameObject icePrefab;
    [SerializeField] private GameObject iceParent;
    [SerializeField] private Transform iceZone1;
    [SerializeField] private Transform iceZone2;
    [SerializeField] private float iceDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanIce(iceDelayTime));
    }

    IEnumerator SpwanIce(float delaytime)
    {
        while (true)
        {
            float randX = Random.Range(iceZone1.position.x, iceZone2.position.x);
            float randZ = Random.Range(iceZone1.position.z, iceZone2.position.z);

            Vector3 newPos = new Vector3(randX, iceZone1.position.y, randZ);

            GameObject ice = Instantiate(icePrefab, newPos, Quaternion.identity);
            ice.transform.SetParent(iceParent.transform, true);

            yield return new WaitForSeconds(delaytime);
        }
    }
}
