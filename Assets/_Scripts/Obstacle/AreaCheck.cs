using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCheck : MonoBehaviour
{
    int Area;
    public GameObject[] RestartPoint;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("CastleArea"))
        {
            Debug.Log("Area number set to Castle");
            Area = 0;
        }
        else if (other.CompareTag("LavaArea"))
        {
            Area = 1;
        }
        else if (other.CompareTag("IceArea"))
        {
            Area = 2;
        }
        else if (other.CompareTag("SpaceArea"))
        {
            Area = 3;
        }
        else if (other.CompareTag("JungleArea"))
        {
            Area = 4;
        }
        else if (other.CompareTag("DesertArea"))
        {
            Area = 5;
        }

        if (other.CompareTag("DeadZone"))
        {
            this.gameObject.transform.position = RestartPoint[Area].transform.position;
        }
    }

    private void OnCollisionEnter(Collision other) {
        
    }
}
