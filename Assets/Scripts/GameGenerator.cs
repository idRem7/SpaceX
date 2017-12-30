using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameGenerator : MonoBehaviour {

    public GameObject firstAidKit;    
    public GameObject asteroid;
    public GameObject rocket;
    public GameObject spaceShip;
    public GameObject rocketSpawn;
    public int maxAsteroidAmount = 100;

    private int _asteroidCount;
    private float _minAsteroidSpawnDistance = 10.0f;
    // Use this for initialization
    void Start () {
		       
	}

    // Update is called once per frame
    void Update() {

        System.Random randCoord = new System.Random();       
      
        if (Input.GetMouseButtonDown(0)) {

            //Здесь спавнятся ракеты

            Instantiate(rocket, rocketSpawn.transform.position, rocketSpawn.transform.rotation);

        }

        if (_asteroidCount < maxAsteroidAmount) {

            //Тут спавним астероиды
            //пока что всегда добиваем до максимума
            //Спавн
            //Определить позицию спавна
            //Спавним астероид
            //Дальше он уже сам следит за собой

            Vector3 spawnPosition = new Vector3();
            float raduis;
            raduis = asteroid.GetComponent<asteroidController>().maxAsteroidLiveRadiusAmoutSpaceShip;
            do {

                spawnPosition.x = raduis * (float)(randCoord.NextDouble() - 0.5);
                spawnPosition.y = raduis * (float)(randCoord.NextDouble() - 0.5);
                spawnPosition.z = raduis * (float)(randCoord.NextDouble() - 0.5);

            } while (spawnPosition.sqrMagnitude < Mathf.Pow(_minAsteroidSpawnDistance, 2));
            spawnPosition += spaceShip.transform.position;

            Quaternion spawnRotation = new Quaternion();
            Vector3 rotationAngles;
            rotationAngles.x = (float)randCoord.NextDouble() * 720 - 360;
            rotationAngles.y = (float)randCoord.NextDouble() * 720 - 360;
            rotationAngles.z = (float)randCoord.NextDouble() * 720 - 360;
            spawnRotation.eulerAngles = rotationAngles;

            GameObject spawnAsteroid = Instantiate(asteroid, spawnPosition, spawnRotation);
            spawnAsteroid.GetComponent<asteroidController>().setReferences(spaceShip, gameObject);

            _asteroidCount++;

        }

    }

    public void decrementCount() {

        _asteroidCount--;

    }

}
