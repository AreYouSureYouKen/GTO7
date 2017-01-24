using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField]
    APIManager _apiManager;

    [SerializeField]
    List<Text> _highScore;

    // Use this for initialization
    void Start () {
        if (!_apiManager) _apiManager = FindObjectOfType<APIManager>();
        _apiManager.getHighscores();
	}
	

    public void getHighscores(List<Highscore> highscores)
    {
        int i = 0;
        foreach (Highscore hs in highscores)
        {
            Debug.Log("Setting highscore to "+ i);
            _highScore[i].text = hs.getUsername() + " Score: " + hs.getTotalScore().ToString();
            i++;
        }
    }
}
