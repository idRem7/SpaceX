using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour {

    public Transform lookTarget;
    public float cameraAnswerSpeed = 1.0f;
    public float cameraAnswerRotation = 1.0f;
    public float cameraAnswerZoom = 1.0f;   
    public float maxDistance;
    public float cameraVerticalCorrectFactor = 1.0f;
    public float cameraHorizontalCorrectFactor = 1.0f;
    public GameObject lightForPlayer;

    private Vector3 _pozitionAboutSpaceShip;
    private float _minDistance;


    // Use this for initialization
    void Start () {

        _pozitionAboutSpaceShip = lookTarget.InverseTransformPoint(transform.position);
        _minDistance = Mathf.Sqrt(_pozitionAboutSpaceShip.sqrMagnitude);        

	}
	
	// Update is called once per frame
	void Update () {                   

    }

    void LateUpdate() {
      
        var currentPozition = lookTarget.TransformPoint(_pozitionAboutSpaceShip);
        var worldUpVectorAboutSpaceShip = lookTarget.rotation * Vector3.up;
                     
        transform.DOMove(currentPozition, 1.0f / cameraAnswerSpeed);
        transform.DOLookAt(lookTarget.position, 1.0f / cameraAnswerRotation, AxisConstraint.None, worldUpVectorAboutSpaceShip);

        changeCameraDistance();


        lightForPlayer.transform.DOMove(transform.position, 1.0f / cameraAnswerSpeed);
        lightForPlayer.transform.DOLookAt(lookTarget.position, 1.0f / cameraAnswerRotation, AxisConstraint.None, worldUpVectorAboutSpaceShip);

    }

    void changeCameraDistance() {

        var mouseMoveWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseMoveWheel != 0) {

            Vector3 newPozitionAboutSpaceShip;

            if (mouseMoveWheel > 0) {
               
                newPozitionAboutSpaceShip = _pozitionAboutSpaceShip + new Vector3(0, cameraVerticalCorrectFactor, -cameraHorizontalCorrectFactor);

                
                if (newPozitionAboutSpaceShip.sqrMagnitude <= Mathf.Pow(maxDistance, 2)) {  
                    
                    _pozitionAboutSpaceShip = newPozitionAboutSpaceShip;

                }

            }

            if (mouseMoveWheel < 0) {
                
                newPozitionAboutSpaceShip = _pozitionAboutSpaceShip - new Vector3(0, cameraVerticalCorrectFactor, -cameraHorizontalCorrectFactor);
                                
                if (newPozitionAboutSpaceShip.sqrMagnitude >= Mathf.Pow(_minDistance, 2)) {
                                     
                    _pozitionAboutSpaceShip = newPozitionAboutSpaceShip;

                }

            }

        }

    }

}
