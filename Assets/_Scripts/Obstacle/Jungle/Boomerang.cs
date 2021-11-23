using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private Transform startPos;  // 부메랑 날릴 위치
    [SerializeField] private Transform endPos;  // 날아갈 위치
    [SerializeField] private float direction = 18.0f;  // 방향이자 속도
    [SerializeField] private string whos = "Big";  // 큰 인디안과 작은 인디안의 부메랑 방향이 다르기 때문에

    float currentX;  // 현재 위치
    float coolTime;  // 쿨타임
    float rotate = 700f;  // 회전 속도

    void Start()
    {
        currentX = transform.localPosition.x;
        RandTime();

        switch(whos)
        {
            case "Big":
                StartCoroutine(BigMonsterBmr());
                break;
            case "Small":
                StartCoroutine(SmallMonsterBmr());
                break;
        }
    }

    IEnumerator BigMonsterBmr()
    {
        while (true)
        {
            currentX += Time.deltaTime * direction;

            if (currentX >= endPos.localPosition.x)
            {
                direction *= -1;
                currentX = endPos.localPosition.x;
            }

            if (currentX <= startPos.localPosition.x)
            {
                currentX = startPos.localPosition.x;
                yield return new WaitForSeconds(coolTime);  // 한 번 날릴 때마다 coolTime만큼 쉼
                direction *= -1;
            }

            transform.Rotate(new Vector3(rotate * Time.deltaTime, 0, 0));
            transform.localPosition = new Vector3(currentX, transform.localPosition.y, transform.localPosition.z);

            yield return null;
        }
    }

    // 위의 BigMonsterBmr()과는 >=, <= 만 다름
    IEnumerator SmallMonsterBmr()
    {
        while (true)
        {
            currentX += Time.deltaTime * direction;

            if (currentX <= endPos.localPosition.x)
            {
                direction *= -1;
                currentX = endPos.localPosition.x;
            }

            if (currentX >= startPos.localPosition.x)
            {
                currentX = startPos.localPosition.x;
                yield return new WaitForSeconds(coolTime);
                direction *= -1;
            }

            transform.Rotate(new Vector3(rotate * Time.deltaTime, 0, 0));
            transform.localPosition = new Vector3(currentX, transform.localPosition.y, transform.localPosition.z);

            yield return null;
        }
    }

    // 시작하면 랜덤으로 쿨타임 결정
    void RandTime()
    {
        int rand = Random.Range(0, 4);

        switch(rand)
        {
            case 0:
                coolTime = 0.3f;
                break;
            case 1:
                coolTime = 0.6f;
                break;
            case 2:
                coolTime = 0.9f;
                break;
            case 3:
                coolTime = 1.2f;
                break;
        }
    }
}
