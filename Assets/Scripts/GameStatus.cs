using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

static public class GameInformationSaver {

    static public int score = 0;
    static public int hitPoints = 100;
    static public bool needGetGameInformation = false;
    static public string logText = "";
    static public int countLogLines = 0;

}

public class GameStatus : MonoBehaviour {

    enum graphicsQuality { LOW, MEDIUM, HIGH };
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
    public GameObject settingsMenu;
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

        if (GameInformationSaver.needGetGameInformation) {
            loadGameDataFromSaver();
        } else {

            _hitPoints = 100;
            _score = 0;

        }        
        
        scoreText.GetComponent<Text>().text = _scoreLabelTemplate + _score.ToString();
        hpText.GetComponent<Text>().text = _hpLabelTemplate + _hitPoints.ToString();        

        gameUserInterface.SetActive(true);
        mainMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        settingsMenu.SetActive(false);
        _currentUI = uiType.GAME_UI;

        disablePauseMode();

        if (gameLog == null)
            Application.Quit();

        _isLogEnable = false;
        gameLog.SetActive(_isLogEnable);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab) && !_isPause) {

            _isLogEnable = !_isLogEnable;            
            gameLog.SetActive(_isLogEnable);

        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            leaveFromCurrentWindow();
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

    void loadGameDataFromSaver() {

        if (gameLog == null)
            Application.Quit();

        _hitPoints = GameInformationSaver.hitPoints;
        _score = GameInformationSaver.score;
        gameLog.GetComponent<GameLog>().countLines = GameInformationSaver.countLogLines;       
        GameInformationSaver.needGetGameInformation = false;
        gameLog.GetComponent<GameLog>().setLogText(GameInformationSaver.logText);

    }

    void loadGameDataToSaver() {
       
        GameInformationSaver.hitPoints = _hitPoints;
        GameInformationSaver.score = _score;        
        GameInformationSaver.needGetGameInformation = true;
        GameInformationSaver.countLogLines = gameLog.GetComponent<GameLog>().countLines;
        GameInformationSaver.logText = gameLog.GetComponent<GameLog>().getLogText();

    }

    //------------------------------------------------------------------

    public void setGraphicsQuality(int  setQuality) {

        graphicsQuality needQuality = (graphicsQuality)setQuality;

        switch (needQuality) {

            case graphicsQuality.LOW:
                QualitySettings.SetQualityLevel((int)QualityLevel.Fastest);               
                break;
            case graphicsQuality.MEDIUM:
                QualitySettings.SetQualityLevel((int)QualityLevel.Simple);
                break;
            case graphicsQuality.HIGH:
                QualitySettings.SetQualityLevel((int)QualityLevel.Fantastic);
                break;

        }

    }

    public void leaveFromCurrentWindow() {

        switch (_currentUI) {

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

                settingsMenu.SetActive(false);
                mainMenu.SetActive(true);
                _currentUI = uiType.MAIN_MENU;

                break;
            case uiType.GAME_OVER_SCREEN:

                restartGame();

                break;

        }

    }   

    public void showSettingsScreen() {

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        _currentUI = uiType.SETTINGS_MENU;

    }

    public void exitGame() {

        Application.Quit();

    }

    public void restartGame() {
        
        Application.LoadLevel("SceneA");
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

            //Нужно сократить

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

        gameLog.GetComponent<GameLog>().addNote(GameLog._NoteType.TELEPORT);
        loadGameDataToSaver();

        if (Application.loadedLevelName == "SceneA") {
            Application.LoadLevel("SceneB");

        } else {
            Application.LoadLevel("SceneA");
        }

    }

}
