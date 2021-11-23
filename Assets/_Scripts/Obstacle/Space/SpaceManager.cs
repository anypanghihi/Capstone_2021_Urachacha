using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -10f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
