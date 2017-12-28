using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameGenerator : MonoBehaviour {

    public GameObject firstAidKit;
    public GameObject portal;
    public GameObject asteroid;
    public GameObject rocket;
    public GameObject spaceShip;
    public int maxAsteroidAmount = 100;

    private int _asteroidCount;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        System.Random randCoord = new System.Random();       
      
        if (Input.GetMouseButtonDown(0)) {

            Instantiate(rocket, spaceShip.transform.position, spaceShip.transform.rotation);

        }

        if (_asteroidCount < maxAsteroidAmount) {


            Vector3 spawnPosition;
            float x, y, z, raduis;
            raduis = asteroid.GetComponent<asteroidController>().maxAsteroidLiveRadiusAmoutSpaceShip;
            do {
                x = (float)randCoord.NextDouble() * raduis - raduis / 2;

                spawnPosition = new Vector3();

            } while (spawnPosition != Vector3.zero);

            _asteroidCount++;

        }

    }

    void decrementCount() {

        _asteroidCount--;

    }

}
