using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePharaoh : MonoBehaviour
{
    float widthMax = -41.0f;  // 움직일 위치 최댓값
    float widthMin = -65.5f;  // 최솟값

    float currentX;  // 현재 위치
    float direction = 20.0f;  // 방향이자 속도

    void Start()
    {
        currentX = transform.localPosition.x;  // 시작할 때 기본적으로 놓여있는 위치를 현재 위치로

        RandDirection();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            currentX += Time.deltaTime * direction;

            if (currentX >= widthMax)
            {
                direction *= -1;
                currentX = widthMax;
            }

            if (currentX <= widthMin)
            {
                direction *= -1;
                currentX = widthMin;
            }

            transform.localPosition = new Vector3(currentX, transform.localPosition.y, transform.localPosition.z);

            yield return null;
        }
    }

    // 시작하면 랜덤으로 속도 결정
    public void RandDirection()
    {
        int rand = Random.Range(0, 3);

        switch(rand)
        {
            case 1:
                direction = 10.0f;
                break;
            case 2:
                direction = 15.0f;
                break;
            case 3:
                direction = 20.0f;
                break;
        }
    }
}
