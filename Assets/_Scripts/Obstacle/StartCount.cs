using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCount : MonoBehaviour
{
    public GameObject StartCountingWall;
    public GameObject[] Counting;
    int count;

    private void Start() {
        count = 4;
        Destroy(StartCountingWall,6f);
        InvokeRepeating("startCounting", 1f, 1f);
    }

    void startCounting(){
        if(count >= 0){
            Destroy(Counting[count]);
            count--;
        }
    }
}
