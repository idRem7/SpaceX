using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalController : MonoBehaviour {

    public GameObject spaceShip;
    public float maxLiveDistance = 100.0f;

    private float _minSpawnDistance = 40.0f;

    //спавним портал
    //На уровне есть всегда 1 портал
    //сначала спавнится в случайном месте, но не более чем на макс растояние от игрока
    //если игрок далеко улетает, то портал переспавнивается в другое место по критерию растсояния  
    //Портал сам следит за базаром (за расстоянием от игрока)

   

    // Use this for initialization
    void Start () {

        //спавн
        randomRespawn();

    }
	
	// Update is called once per frame
	void Update () {

        //если игрок слишком далеко - респавн

        Vector3 distanceBetweenPortalAndPlayer = transform.position - spaceShip.transform.position;
        if (distanceBetweenPortalAndPlayer.sqrMagnitude > Mathf.Pow(maxLiveDistance,2)) {
            
            randomRespawn();
            
        }

	}

    //void OnTriggerEnter(GameObject triggerObject) {

    //    if (triggerObject == spaceShip) {

    //        //тут делаем переход в другое пространство (сцену)
    //        //Напишу потом
    //        //И кровью

    //    }

    //}

    void randomRespawn() {

        System.Random randomizer = new System.Random();
        Vector3 spawnPosition = new Vector3();

        do {

            spawnPosition.x = maxLiveDistance * (float)(randomizer.NextDouble() - 0.5);
            spawnPosition.y = maxLiveDistance * (float)(randomizer.NextDouble() - 0.5);
            spawnPosition.z = maxLiveDistance * (float)(randomizer.NextDouble() - 0.5);

        } while (spawnPosition.sqrMagnitude < Mathf.Pow(_minSpawnDistance, 2));
        spawnPosition += spaceShip.transform.position;

        Quaternion spawnRotation = new Quaternion();
        Vector3 rotationAngles;
        rotationAngles.x = (float)randomizer.NextDouble() * 720 - 360;
        rotationAngles.y = (float)randomizer.NextDouble() * 720 - 360;
        rotationAngles.z = (float)randomizer.NextDouble() * 720 - 360;
        spawnRotation.eulerAngles = rotationAngles;

        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

    }

}
