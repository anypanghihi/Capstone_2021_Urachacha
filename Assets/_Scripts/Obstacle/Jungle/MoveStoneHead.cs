using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStoneHead : MonoBehaviour
{
    float heightMax = 1.5f;  // 최대 높이
    float heightMin = -1f;  // 최소 높이
    float currentY;  // 현재 높이
    float direction = 5.0f;  // 방향이자 속도

    float coolTime = 0;  // 쿨타임

    void Start()
    {
        currentY = transform.localPosition.y;
        RandTime();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while(true)
        {
            currentY += Time.deltaTime * direction;

            if (currentY >= heightMax)
            {
                direction *= -1;
                currentY = heightMax;
            }

            if (currentY <= heightMin)
            {
                currentY = heightMin;
                yield return new WaitForSeconds(coolTime);  // 올라갔다 내려올 때마다 coolTime만큼 쉼
                direction *= -1;
            }

            transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);

            yield return null;
        }
    }

    // 시작하면 랜덤으로 쿨타임 결정
    void RandTime()
    {
        coolTime = Random.Range(0.5f, 1.5f);
    }
}
