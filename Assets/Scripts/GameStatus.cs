using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameStatus : MonoBehaviour {

    enum uiType {GAME_UI, MAIN_MENU, SETTINGS_MENU, GAME_OVER_SCREEN};

    //------------------------------------------------------------------

    public const int MAX_HP = 100;
    public const int MIN_HP = 0;

    public GameObject scoreText;
    public GameObject hpText;
    public GameObject gameLog;
    public GameObject gameUserInterface;
    public GameObject mainMenu;
    public GameObject gameOverScreen;
    //public GameObject settingsMenu;
    public int healVolume = 20;
    public int damageVolume = 10;       

    //------------------------------------------------------------------

    private int _hitPoints;
    private int _score;
    private const string _scoreLabelTemplate = "Score: ";
    private const string _hpLabelTemplate = "HP: ";
    private bool _isLogEnable;
    private bool _isPause;
    private uiType _currentUI;

    //------------------------------------------------------------------

    // Use this for initialization
    void Start () {

        _isLogEnable = false;
        gameLog.SetActive(_isLogEnable);
        _hitPoints = 100;
        _score = 0;
        scoreText.GetComponent<Text>().text = _scoreLabelTemplate + _score.ToString();
        hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //отключить левые менюшки
        //включить игровой UI
        gameUserInterface.SetActive(true);
        mainMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        _currentUI = uiType.GAME_UI;

        disablePauseMode();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab) && !_isPause) {

            _isLogEnable = !_isLogEnable;            
            gameLog.SetActive(_isLogEnable);

        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            

            switch(_currentUI) {

                case uiType.GAME_UI:
                 
                    enablePauseMode();
                    gameUserInterface.SetActive(false);
                    mainMenu.SetActive(true);
                    _currentUI = uiType.MAIN_MENU;

                    break;
                case uiType.MAIN_MENU:               

                    disablePauseMode();
                    mainMenu.SetActive(false);
                    gameUserInterface.SetActive(true);
                    _currentUI = uiType.GAME_UI;

                    break;
                case uiType.SETTINGS_MENU:

                    //с паузой ничего не делать
                    //скрыть меню настройки
                    //показать главное меню
                    //поменять флаг активного интерфейса

                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    break;
                case uiType.GAME_OVER_SCREEN:

                    restartGame();

                    break;

            }          

        }

    }

    void enablePauseMode() {

        Time.timeScale = 0;
        DOTween.timeScale = 0;
        _isPause = true;

    }

    void disablePauseMode() {

        Time.timeScale = 1;
        DOTween.timeScale = 1;
        _isPause = false;

    }

    //------------------------------------------------------------------

    public void exitGame() {

        Application.Quit();


    }

    public void restartGame() {

        Application.LoadLevel(Application.loadedLevel);

        //disablePauseMode();

    }
   

    public bool getPauseStatus() {

        return _isPause;

    }

    public void getHeal() {

        _hitPoints += healVolume;
        
        if(_hitPoints > MAX_HP) {
            _hitPoints = MAX_HP;
        }

        hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();

    }

    public void getDamage() {

        _hitPoints -= damageVolume;

        if (_hitPoints <= MIN_HP) {

            _hitPoints = MIN_HP;
            hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();

            enablePauseMode();
            gameUserInterface.SetActive(false);
            gameOverScreen.SetActive(true);
            _currentUI = uiType.GAME_OVER_SCREEN;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;           

        }

        hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();
        gameLog.GetComponent<GameLog>().addNote(GameLog._NoteType.COLLISION);

    }

    public void hitTarget() {
        
        _score++;
        scoreText.GetComponent<Text>().text = _scoreLabelTemplate + _score.ToString();
        
        gameLog.GetComponent<GameLog>().addNote(GameLog._NoteType.HIT);

    }

    public bool isLogWindowActive() {

        return _isLogEnable;

    }

    public void teleportToAnotherWorld() {

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //тут просто делаем телепорты
        gameLog.GetComponent<GameLog>().addNote(GameLog._NoteType.TELEPORT);

    }


}
