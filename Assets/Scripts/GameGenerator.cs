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
    public GameObject rocketSpawn;
    public int maxAsteroidAmount = 100;

    private int _asteroidCount;
    private float _minAsteroidSpawnDistance = 10.0f;
    // Use this for initialization
    void Start () {
		
        //спавним портал
        //На уровне есть всегда 1 портал
        //сначала спавнится в случайном месте, но не более чем на макс растояние от игрока
        //если игрок далеко улетает, то портал переспавнивается в другое место по критерию растсояния
        //здесь его только спавним, хотя его моно спавнить отдельно
        //Портал сам следит за базаром (за расстоянием от игрока)

	}

    // Update is called once per frame
    void Update() {

        System.Random randCoord = new System.Random();       
      
        if (Input.GetMouseButtonDown(0)) {

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

                spawnPosition.x = (float)randCoord.NextDouble() * raduis - raduis / 2;
                spawnPosition.y = (float)randCoord.NextDouble() * raduis - raduis / 2;
                spawnPosition.z = (float)randCoord.NextDouble() * raduis - raduis / 2;

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
