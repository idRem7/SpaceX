using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketController : MonoBehaviour {

    public GameObject gameController;

    public float rocketTime = 30.0f;
    public float rocketDistance = 200.0f;
    

	// Use this for initialization
	void Start () {

        transform.DOMove(transform.position + transform.rotation * Vector3.forward * rocketDistance, rocketTime, false);
        Destroy(gameObject, rocketTime + 0.01f );

    }
	
	// Update is called once per frame
	void Update () { }

    void OnCollisionEnter(Collision collisionWithRocket) {        

        AsteroidController isAsteroid = collisionWithRocket.gameObject.GetComponent<AsteroidController>();

        if(isAsteroid != null) {

            isAsteroid.destroyFromRocket();
            gameController.GetComponent<GameStatus>().hitTarget();
            
        }

        Destroy(gameObject);

    }

    public void setReferences(GameObject gameControllerReference) {
   
        gameController = gameControllerReference;

    }

}
