using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBoxController : MonoBehaviour {

    public GameObject spaceShip;
    public GameObject spawnController;
    public GameObject gameController;
    public float maxLiveDistance = 100.0f;

    private float _minSpawnDistance = 40.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 distanceBetweenPortalAndPlayer = transform.position - spaceShip.transform.position;
        if (distanceBetweenPortalAndPlayer.sqrMagnitude > Mathf.Pow(maxLiveDistance, 2)) {

            randomRespawn();

        }

    }

    void OnTriggerEnter(Collider triggerObject) {

        
        if (triggerObject.gameObject == spaceShip) {


            //Код прибавления здоровья

            gameController.GetComponent<GameStatus>().getHeal();
            spawnController.GetComponent<GameGenerator>().decrementHealBoxCount();
            Destroy(gameObject);

        }

    }


    public void setReferences(GameObject spaceShipReference, GameObject controllerReference, GameObject gameControllerReference) {

        spaceShip = spaceShipReference;
        spawnController = controllerReference;
        gameController = gameControllerReference;

    }

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
