using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotate : MonoBehaviour
{
    [Header ("Asteroid")]
    [SerializeField] private GameObject[] asteroids;
    [SerializeField] private float asteroidSpeed;

    [Header ("Orbit")]
    [SerializeField] private GameObject orbit;
    [SerializeField] private float orbitSpeed;

    [Header("IceAsteroid")]
    [SerializeField] private GameObject iceAsteroid;
    [SerializeField] private float iceAsteroidSpeed;


    // Update is called once per frame
    void Update()
    {
        orbit.transform.Rotate(new Vector3(0, orbitSpeed, 0) * Time.deltaTime);
        iceAsteroid.transform.Rotate(new Vector3(0, -iceAsteroidSpeed, 0) * Time.deltaTime);

        asteroidRotate();
    }

    void asteroidRotate(){
        asteroids[0].transform.Rotate(new Vector3(0, asteroidSpeed * 0.8f, 0) * Time.deltaTime);
        asteroids[1].transform.Rotate(new Vector3(0, 0, asteroidSpeed) * Time.deltaTime);
        asteroids[2].transform.Rotate(new Vector3(asteroidSpeed * 0.5f, 0, 0) * Time.deltaTime);
    }
}
