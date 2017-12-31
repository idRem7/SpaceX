using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class skyshipMove : MonoBehaviour {

    public float mouseSensitivity = 30.0f;
    public float spaceShipSpeed = 1.0f;
    public float flameChangeSpeed = 2.0f;   
    public ParticleSystem rocketFlame;


    private float _maxSpeedFlame = 7.0f;
    private float _minSpeedFlame = 2.0f;


    // Use this for initialization
    void Start () {

        rocketFlame = GetComponent<ParticleSystem>();
        rocketFlame.startSpeed = _minSpeedFlame;

	}
	
	// Update is called once per frame
	void Update () {

        moveShip();
        rotateShip();

    }

    void FixedUpdate() {
        
    }

    void rotateShip() {

        if (Input.GetMouseButton(1)){

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;            

            var mouseMoveX = mouseSensitivity * Input.GetAxis("Mouse X");
            var mouseMoveY = mouseSensitivity * Input.GetAxis("Mouse Y");
            Quaternion pitchRotation;
            Quaternion yawRotation;

            Rigidbody spaceShipPhysicProperties = gameObject.GetComponent<Rigidbody>();

            if (mouseMoveX != 0) {

                spaceShipPhysicProperties.Sleep();
                yawRotation = Quaternion.AngleAxis(mouseMoveX, Vector3.up);
                transform.DORotate(yawRotation.eulerAngles, 0.0f, RotateMode.LocalAxisAdd);

            }

            if (mouseMoveY != 0) {

                spaceShipPhysicProperties.Sleep();
                pitchRotation = Quaternion.AngleAxis(mouseMoveY, Vector3.right);
                transform.DORotate(pitchRotation.eulerAngles, 0.0f, RotateMode.LocalAxisAdd);

            }            

        } else {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }

    }

    void moveShip() {

        bool moveFlag = false;
        Rigidbody spaceShipPhysicProperties = gameObject.GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.W)) {

            spaceShipPhysicProperties.Sleep();
            transform.DOMove(transform.position + transform.rotation * Vector3.back, 1 / spaceShipSpeed, false);
            //spaceShipPhysicProperties.DOLocalMove(transform.position + transform.rotation * Vector3.back, 1 / spaceShipSpeed, false);
            moveFlag = true;

        }
        if (Input.GetKey(KeyCode.S)) {

            spaceShipPhysicProperties.Sleep();
            transform.DOMove(transform.position + transform.rotation * Vector3.forward, 1 / spaceShipSpeed, false);           
            moveFlag = true;

        }
        if (Input.GetKey(KeyCode.A)) {

            spaceShipPhysicProperties.Sleep();
            transform.DOMove(transform.position + transform.rotation * Vector3.right, 1 / spaceShipSpeed, false);         
            moveFlag = true;

        }
        if (Input.GetKey(KeyCode.D)) {

            spaceShipPhysicProperties.Sleep();
            transform.DOMove(transform.position + transform.rotation * Vector3.left, 1 / spaceShipSpeed, false);         
            moveFlag = true;

        }


        if (moveFlag) {
           
            additiveFlame();

        } else {

            substractiveFlame();

        }
    }

    void additiveFlame() {

        if (rocketFlame.startSpeed < _maxSpeedFlame) {

            rocketFlame.startSpeed += flameChangeSpeed;

        }
        if (rocketFlame.startSpeed > _maxSpeedFlame) {

            rocketFlame.startSpeed = _maxSpeedFlame;

        }

    }

    void substractiveFlame() {

        if (rocketFlame.startSpeed > _minSpeedFlame) {

            rocketFlame.startSpeed -= flameChangeSpeed;

        }
        if (rocketFlame.startSpeed < _minSpeedFlame) {

            rocketFlame.startSpeed = _minSpeedFlame;

        }

    }

}
