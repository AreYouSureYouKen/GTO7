using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    [SerializeField]
    GameManager _gameManager;
    [SerializeField]
    private InputField _username;
    [SerializeField]
    private InputField _password;
    [SerializeField]
    private InputField _repeatPassword;
    [SerializeField]
    private Button _btnLogin;

    public void btnPressLogin()
    {
        if (!_gameManager) _gameManager  = FindObjectOfType<GameManager>();
        if(_username.text != "" && _password.text != "") _gameManager.Login(_username.text, _password.text);
    }

    public void btnPressRegister()
    {
        if (!_gameManager) _gameManager = FindObjectOfType<GameManager>();
        if((_username.text != "" && _password.text != "" && _repeatPassword.text != "") && _password.text.Equals(_repeatPassword.text))
        _gameManager.Register(_username.text, _password.text);
    }

    public void btnPressHighscores()
    {
        SceneManager.LoadScene("Scenes/EndGame");
    }
}
