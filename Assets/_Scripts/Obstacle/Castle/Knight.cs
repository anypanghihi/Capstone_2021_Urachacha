using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] private float direction;  // 방향이자 속도
    [SerializeField] private float destroyTime;
    float currentZ;  // 현재 위치

    void Start()
    {
        currentZ = transform.localPosition.z;
        StartCoroutine(MoveKnight());
        Destroy(this.gameObject, destroyTime);
    }

    IEnumerator MoveKnight()
    {
        while (true)
        {
            currentZ += Time.deltaTime * direction;

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentZ);

            yield return null;
        }
    }
}
