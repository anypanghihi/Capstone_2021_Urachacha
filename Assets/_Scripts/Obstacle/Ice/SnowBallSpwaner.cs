using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallSpwaner : MonoBehaviour
{
    [Header("SnowBall")]
    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private GameObject snowParent;
    [SerializeField] private Transform snowBallZone1;
    [SerializeField] private Transform snowBallZone2;
    [SerializeField] private float snowBallDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanSnowBall(snowBallDelayTime));
    }

    IEnumerator SpwanSnowBall(float delaytime)
    {
        while (true)
        {
            float randZ = Random.Range(snowBallZone1.position.z, snowBallZone2.position.z);

            Vector3 newPos = new Vector3(snowBallZone1.position.x,
                snowBallZone1.position.y, randZ);

            GameObject snowBall = Instantiate(snowBallPrefab, newPos, Quaternion.identity);
            snowBall.transform.SetParent(snowParent.transform, true);

            yield return new WaitForSeconds(delaytime);
        }
    }
}