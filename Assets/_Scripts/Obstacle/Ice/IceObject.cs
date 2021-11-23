using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag == "DeadIceObj")
        {
            Destroy(gameObject);
        }
    }
}
