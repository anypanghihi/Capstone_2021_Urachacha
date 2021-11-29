using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZone : MonoBehaviour
{
    public GameObject warningZone;

    // Start is called before the first frame update
    void Start()
    {
        obstacleWarning();
    } 

    void obstacleWarning()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            Vector3 warningPos = new Vector3(hit.transform.position.x,hit.transform.position.y+0.58f,hit.transform.position.z);
            Instantiate(warningZone, warningPos, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "WarningZone"){
            Destroy(other.collider.gameObject);
        }
    }
}
