using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class asteroidController : MonoBehaviour {

    public GameObject spaceShip;
    public GameObject spawnController;

    public float asteroidDistantion = 1000.0f;
    public float asteroidSpeed = 2.0f;
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

        //transform.DOLocalMove(transform.position + transform.rotation * Vector3.forward * rocketDistance, rocketTime, false);
        //transform.DOMove(transform.position + spawnRotation * Vector3.forward * asteroidDistantion, asteroidDistantion / asteroidSpeed);
        //rigidbody.DOMove(transform.position + spawnRotation * Vector3.forward * asteroidDistantion, asteroidDistantion / asteroidSpeed);
        Rigidbody r = GetComponent<Rigidbody>();
        r.DOMove(transform.position + spawnRotation * Vector3.forward * asteroidDistantion, asteroidDistantion / asteroidSpeed);

    }
	
	// Update is called once per frame
	void Update () {

        //чекать расстояние до игрока
        //улетел слишком далеко - уничтожаем
        //уменьшаем счет

        Vector3 distanceToSpaceShip = transform.position - spaceShip.transform.position;

        if (distanceToSpaceShip.sqrMagnitude > Mathf.Pow(maxAsteroidLiveRadiusAmoutSpaceShip, 2)) {

            spawnController.GetComponent<GameGenerator>().decrementCount();
            Destroy(gameObject);

        }
        		
	}

    public void setReferences(GameObject spaceShipReference, GameObject controllerReference) {

        spaceShip = spaceShipReference;
        spawnController = controllerReference;

    }

    public void destroyFromRocket() {

        spawnController.GetComponent<GameGenerator>().decrementCount();
        Destroy(gameObject);

    }

    void OnCollisionEnter(Collision collisionWithRocket) {

        transform.DOKill();

    }

}
