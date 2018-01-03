using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameLog : MonoBehaviour {

    public GameObject textField;
    public GameObject viewField;
    public GameObject scrollBar;
    public Text logText;

    private const string _tagetHitNote = "Rocket destroyed the meteorite";
    private const string _teleportNote = "Spaceship moved through the portal";
    private const string _collisionNote = "Spaceship collided with a meteorite";
    private const int lineHeight = 32;

    private int _countLines = 0;

    public enum _NoteType { TELEPORT, COLLISION, HIT };
    

    // Use this for initialization
    void Start () {

        var needHeight = lineHeight * 6;
        var textFieldWidth = textField.GetComponent<RectTransform>().sizeDelta.x;
        Vector2 needSize = new Vector2(textFieldWidth, needHeight);
        textField.GetComponent<RectTransform>().sizeDelta = needSize;

        logText = textField.GetComponent<Text>();
        logText.text = "";
        scrollBar.GetComponent<Scrollbar>().value = 1;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addNote(_NoteType addNoteType) {

        if(_countLines > 0) {
            logText.text += "\n";
        }

        switch (addNoteType) {

            case _NoteType.COLLISION:
                logText.text += _collisionNote;
                break;

            case _NoteType.HIT:
                logText.text += _tagetHitNote;
                break;

            case _NoteType.TELEPORT:
                logText.text += _teleportNote;
                break;

        }
        _countLines++;      
       
        var textFieldHeight = textField.GetComponent<RectTransform>().sizeDelta.y;
        if (_countLines * lineHeight > textFieldHeight) {

            var textFieldWidth = textField.GetComponent<RectTransform>().sizeDelta.x;           
            textFieldHeight += lineHeight;           
            Vector2 newSize = new Vector2(textFieldWidth, textFieldHeight);
            textField.GetComponent<RectTransform>().sizeDelta = newSize;
            scrollBar.GetComponent<Scrollbar>().value = 0;

        } else {
            scrollBar.GetComponent<Scrollbar>().value = 1;
        }        

    }   

}
