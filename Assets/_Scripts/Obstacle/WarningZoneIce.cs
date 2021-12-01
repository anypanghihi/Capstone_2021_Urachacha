using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningZoneIce : MonoBehaviour
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
            Debug.Log(hit.ToString());
            Vector3 warningPos = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.59f, hit.transform.position.z);
            Instantiate(warningZone, warningPos, Quaternion.Euler(90, 0, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WarningZone"))
        {
            Destroy(other.gameObject);
        }
    }
}
