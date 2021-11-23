using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] private float speed;    
    [SerializeField] private float destroyTime;

    public GameObject player;
    
    private Rigidbody knightRigidbody;
    private Rigidbody playerRigidbody;

    void Start()
    {
        knightRigidbody = GetComponent<Rigidbody>();
        playerRigidbody = GetComponent<Rigidbody>();
        Destroy(this.gameObject, destroyTime);
    }

    void Update()
    {
        knightRigidbody.AddForce(0, 0, -speed * Time.deltaTime);       
        
    }

    private void OnCollisionStay(Collision other) {
        if(other.collider.tag == "Player"){
            playerRigidbody.AddForce(new Vector3(0,0,-350));
        }
    }
}
