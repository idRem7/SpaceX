using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class GameGenerator : MonoBehaviour {

    public GameObject asteroid;
    public GameObject rocket;
    public GameObject spaceShip;
    public GameObject rocketSpawn;
    public GameObject healBox;
    public GameObject gameController;
    public int maxAsteroidAmount = 100;
    public int maxHealBoxAmount = 5;

    private int _healBoxCount;
    private int _asteroidCount;
    private float _minSpawnDistance = 10.0f;

    enum _SpaceObject { ASTEROID, HEAL_BOX };

    // Use this for initialization
    void Start () {

        _asteroidCount = 0;
        _healBoxCount = 0;

	}

    // Update is called once per frame
    void Update() {           
      
        if (Input.GetMouseButtonDown(0)) {

            GameObject spawnRocket = Instantiate(rocket, rocketSpawn.transform.position, rocketSpawn.transform.rotation);
            spawnRocket.GetComponent<rocketController>().setReferences(gameController);

        }

        if (_asteroidCount < maxAsteroidAmount) {
            spawnGameObject(_SpaceObject.ASTEROID);
        }

        if(_healBoxCount < maxHealBoxAmount) {
            spawnGameObject(_SpaceObject.HEAL_BOX);
        }

    }

    void spawnGameObject(_SpaceObject objectType) {

        System.Random randCoord = new System.Random();
        Vector3 spawnPosition = new Vector3();
        float raduis;
        raduis = asteroid.GetComponent<asteroidController>().maxAsteroidLiveRadiusAmoutSpaceShip;

        do {

            spawnPosition.x = raduis * (float)(randCoord.NextDouble() - 0.5);
            spawnPosition.y = raduis * (float)(randCoord.NextDouble() - 0.5);
            spawnPosition.z = raduis * (float)(randCoord.NextDouble() - 0.5);

        } while (spawnPosition.sqrMagnitude < Mathf.Pow(_minSpawnDistance, 2));
        spawnPosition += spaceShip.transform.position;

        Quaternion spawnRotation = new Quaternion();
        Vector3 rotationAngles;
        rotationAngles.x = (float)randCoord.NextDouble() * 720 - 360;
        rotationAngles.y = (float)randCoord.NextDouble() * 720 - 360;
        rotationAngles.z = (float)randCoord.NextDouble() * 720 - 360;
        spawnRotation.eulerAngles = rotationAngles;

        switch (objectType) {

            case _SpaceObject.ASTEROID:

                GameObject spawnAsteroid = Instantiate(asteroid, spawnPosition, spawnRotation);
                spawnAsteroid.GetComponent<asteroidController>().setReferences(spaceShip, gameObject, gameController);

                _asteroidCount++;

                break;

            case _SpaceObject.HEAL_BOX:

                GameObject spawnHealBox = Instantiate(healBox, spawnPosition, spawnRotation);
                spawnHealBox.GetComponent<HealBoxController>().setReferences(spaceShip, gameObject, gameController);

                _healBoxCount++; 

                break;

        }

    }

    public void decrementAsteroidCount() {

        _asteroidCount--;

    }

    public void decrementHealBoxCount() {

        _healBoxCount--;

    }

}
