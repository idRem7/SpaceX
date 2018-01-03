using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class asteroidController : MonoBehaviour {

    public GameObject spaceShip;
    public GameObject spawnController;
    public GameObject gameController;

    public float asteroidDistantion = 1000.0f;
    public float asteroidSpeed = 100.0f;
    public float maxAsteroidLiveRadiusAmoutSpaceShip = 1000.0f;


    // Use this for initialization
    void Start () {

        //задать движение анимашку
        System.Random randomizer = new System.Random();

        Quaternion spawnRotation = new Quaternion();
        Vector3 rotationAngles;
        rotationAngles.x = (float)randomizer.NextDouble() * 720 - 360;
        rotationAngles.y = (float)randomizer.NextDouble() * 720 - 360;
        rotationAngles.z = (float)randomizer.NextDouble() * 720 - 360;
        spawnRotation.eulerAngles = rotationAngles;
        
        Rigidbody r = GetComponent<Rigidbody>();      
        r.AddForce(spawnRotation * Vector3.forward * 100);
        r.AddTorque(spawnRotation * Vector3.forward * 100);

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 distanceToSpaceShip = transform.position - spaceShip.transform.position;

        if (distanceToSpaceShip.sqrMagnitude > Mathf.Pow(maxAsteroidLiveRadiusAmoutSpaceShip, 2)) {

            spawnController.GetComponent<GameGenerator>().decrementAsteroidCount();
            Destroy(gameObject);

        }
        		
	}

    public void setReferences(GameObject spaceShipReference, GameObject controllerReference, GameObject gameControllerReference) {

        spaceShip = spaceShipReference;
        spawnController = controllerReference;
        gameController = gameControllerReference;

    }

    public void destroyFromRocket() {

        spawnController.GetComponent<GameGenerator>().decrementAsteroidCount();
        Destroy(gameObject);

    }

    void OnCollisionEnter(Collision collisionWithAsteroid) {      

        if(collisionWithAsteroid.gameObject == spaceShip) {
            gameController.GetComponent<GameStatus>().getDamage();            
        }

    }

}
