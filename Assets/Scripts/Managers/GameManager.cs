using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private int _hitsDone=0;
    private int _hitsTaken=0;
    private int _longestCombo=0;
    private int _currentCombo = 0;
    private int _nrTotalEnemies = 0;
    private int _enemiesDefeated=0;
    private int _time=0;
    [SerializeField]
    private APIManager _apiManager;
    private mainmenu _mainmenu;
    private HighscoreManager _highscoreManager;
    private GameUser _currentUser;

    private Coroutine _timer;

    public void AddHitDone()
    {
        _hitsDone++;
        _currentCombo++;
        checkCombo();
    }

    public void AddHitTaken()
    {
        _hitsTaken++;
        _currentCombo = 0;
    }

    public void AddToTotalEnemies()
    {
        _nrTotalEnemies++;
    }

    public void AddEnemyDefeated()
    {
        _enemiesDefeated++;
        if (_enemiesDefeated == _nrTotalEnemies) EndGame();
    }

    public void StartTimer()
    {
        Debug.Log("starting timer");
        
        _timer = StartCoroutine(TimerTick());
    }

    public void SetEndTime()
    {
        StopCoroutine(_timer);
    }

    private void checkCombo()
    {
        if(_currentCombo > _longestCombo)
        {
            _longestCombo = _currentCombo;
        }
    }

    private IEnumerator TimerTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _time++;
        }
    }

    public void Login(string username, string password)
    {
        if (!_apiManager) _apiManager = FindObjectOfType<APIManager>();
        _apiManager.Login(username, password);
    }

    public void GetLoginReturn(GameUser gu)
    {
        if(!_mainmenu) _mainmenu = FindObjectOfType<mainmenu>();
        if (gu != null)
        {
            _currentUser = gu;
            SceneManager.LoadScene("Scenes/MainGame");
        }
        else
        {
            // return something to notify player of failed login?
        }
    }

    public void Register(string username, string password)
    {
        if (!_apiManager) _apiManager = FindObjectOfType<APIManager>();
        _apiManager.Register(username, password);
    }

    public void GetRegisterReturn(GameUser gu)
    {
        if (!_mainmenu) _mainmenu = FindObjectOfType<mainmenu>();
        if(gu != null)
        {
            _currentUser = gu;
            SceneManager.LoadScene("Scenes/MainGame");
        }
        else
        {
            // return something to notify player something is wronk.
        }
    }

    public void EndGame()
    {
        this.SetEndTime();
        if (!_apiManager) _apiManager = FindObjectOfType<APIManager>();
        Debug.Log("saving highscore now with time: " +_time);
        _apiManager.SaveHighscore(_currentUser, _time, _hitsDone, _hitsTaken, _enemiesDefeated, _longestCombo);
    }

    public void GetHighscores(List<Highscore> highscores)
    {
        if (!_highscoreManager) _highscoreManager = FindObjectOfType<HighscoreManager>();
        _highscoreManager.getHighscores(highscores);
    }
}
