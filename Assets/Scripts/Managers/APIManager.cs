using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public enum ApiRequestType {Register, Login, SaveHighscore, LoadHighscore}

public class highScoreList
{
    public List<Highscore> highscores;
}

public class APIManager : MonoBehaviour{
    [SerializeField]
    private GameManager _gameManager;
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Login(string username, string password)
    {
        StartCoroutine(doWebRequestGet("localhost:666/TestClean/api/users/" + username + "/" + password, ApiRequestType.Login));
    }

    public void Register(string username, string password)
    {
            Dictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add("username", username);
            parameters.Add("password", password);
            Debug.Log("starting a register attempt");
            StartCoroutine(doWebRequestPOST("localhost:666/TestClean/api/users/register", ApiRequestType.Register, parameters));
    }


    public void SaveHighscore(GameUser gu, int time, int hitsDone, int hitsTaken, int enemiesDefeated, int longestCombo)
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("username", gu.getUsername());
        parameters.Add("seconds", time.ToString());
        parameters.Add("hitsDone", hitsDone.ToString());
        parameters.Add("hitsTaken", hitsTaken.ToString());
        parameters.Add("enemiesDefeated", enemiesDefeated.ToString());
        parameters.Add("longestCombo", longestCombo.ToString());
        StartCoroutine(doWebRequestPOST("localhost:666/TestClean/api/users/saveHighscore", ApiRequestType.SaveHighscore, parameters));
    }

    public void getHighscores()
    {
        StartCoroutine(doWebRequestGet("localhost:666/TestClean/api/users/highscores", ApiRequestType.LoadHighscore));
    }

    private IEnumerator doWebRequestPOST(string url, ApiRequestType type, Dictionary<string,string> parameters)
    {
        WWWForm webReqForm = new WWWForm();
        foreach(KeyValuePair<string,string> kvp in parameters)
        {
            webReqForm.AddField(kvp.Key, kvp.Value);
        }

        UnityWebRequest webReq = UnityWebRequest.Post(url,webReqForm);
        yield return webReq.Send();
        if (webReq.error == null)
            handleWebRequestPOST(webReq.downloadHandler.text, type);
        else
            Debug.Log(webReq.error);
    }

    private void handleWebRequestPOST(string result, ApiRequestType type)
    {
        if (!_gameManager) _gameManager = FindObjectOfType<GameManager>();
        switch (type)
        {
            case ApiRequestType.Register:
                _gameManager.GetRegisterReturn(JsonUtility.FromJson<GameUser>(result));
                break;
            case ApiRequestType.SaveHighscore:
                break;
            default:
                break;
        }
    }


    private IEnumerator doWebRequestGet(string url, ApiRequestType type)
    {
        UnityWebRequest webReq = UnityWebRequest.Get(url);
        yield return webReq.Send();
        handleWebRequestGet(webReq.downloadHandler.text, type);
    }

private void handleWebRequestGet(string result, ApiRequestType type)
{
    if (result == null)
    {
        Debug.Log("No results found");
    }
        Debug.Log(result);
        if (!_gameManager) _gameManager = FindObjectOfType<GameManager>();
    switch(type)
        {
            case ApiRequestType.Login:
                _gameManager.GetLoginReturn(JsonUtility.FromJson<GameUser>(result));
                break;
            case ApiRequestType.LoadHighscore:
                List<Highscore> hs = JsonUtility.FromJson<highScoreList>(result).highscores;
                _gameManager.GetHighscores(hs);
                break;
            default:
                break;
        }
}

}
