using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour {

    public const int MAX_HP = 100;
    public const int MIN_HP = 0;

    public GameObject scoreText;
    public GameObject hpText;
    public int healVolume = 20;
    public int damageVolume = 10;

    private int _hitPoints;
    private int _score;
    private const string _scoreLabelTemplate = "Score: ";
    private const string _hpLabelTemplate = "HP: ";
    

	// Use this for initialization
	void Start () {

        _hitPoints = 100;
        _score = 0;

        scoreText.GetComponent<Text>().text = _scoreLabelTemplate + _score.ToString();
        hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();
        //начальные значения текста


    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void getHeal() {

        _hitPoints += healVolume;
        _hitPoints %= (MAX_HP + 1);

        //обновить надпись
        hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();

    }

    public void getDamage() {

        _hitPoints -= damageVolume;

        if (_hitPoints <= MIN_HP) {

            _hitPoints = MIN_HP;

            //гейм овер
            //выводим сообщение
            //надпись меняем
            hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();


        }

        //меняем надпись хп
        hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();

    }

    public void hitTarget() {

        //увеличить счетчик
        //поменять надпись
        _score++;
        scoreText.GetComponent<Text>().text = _scoreLabelTemplate + _score.ToString();

        //запись в лог

    }

}
