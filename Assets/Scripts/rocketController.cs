using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rocketController : MonoBehaviour {

    public float rocketTime = 30.0f;
    public float rocketDistance = 200.0f;
    

	// Use this for initialization
	void Start () {

        transform.DOMove(transform.position + transform.rotation * Vector3.forward * rocketDistance, rocketTime, false);
        Destroy(gameObject, rocketTime + 0.01f );

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collisionWithRocket) {        

        asteroidController isAsteroid = collisionWithRocket.gameObject.GetComponent<asteroidController>();

        if(isAsteroid != null) {

            isAsteroid.destroyFromRocket();
            
        }

        Destroy(gameObject);

    }



}
