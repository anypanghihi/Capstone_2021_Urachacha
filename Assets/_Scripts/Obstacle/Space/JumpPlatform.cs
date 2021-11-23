using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] jumpPlatforms;
    [SerializeField] private GameObject[] jumpPlatformPos;
    [SerializeField] private float jumpPlatformSpeed;
    
    bool moveLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveLeft){
            jumpPlatforms[0].transform.position
            = Vector3.MoveTowards(jumpPlatforms[0].transform.position, jumpPlatformPos[0].transform.position, Time.deltaTime * jumpPlatformSpeed);

            jumpPlatforms[1].transform.position
            = Vector3.MoveTowards(jumpPlatforms[1].transform.position, jumpPlatformPos[2].transform.position, Time.deltaTime * jumpPlatformSpeed);

            if(jumpPlatforms[0].transform.position.z >= 31){
                moveLeft = false;
            }
        }
        if(!moveLeft){
            jumpPlatforms[0].transform.position
            = Vector3.MoveTowards(jumpPlatforms[0].transform.position, jumpPlatformPos[1].transform.position, Time.deltaTime * jumpPlatformSpeed);

            jumpPlatforms[1].transform.position
            = Vector3.MoveTowards(jumpPlatforms[1].transform.position, jumpPlatformPos[3].transform.position, Time.deltaTime * jumpPlatformSpeed);

            if (jumpPlatforms[0].transform.position.z <= 25)
            {
                moveLeft = true;
            }
        }


    }
}
