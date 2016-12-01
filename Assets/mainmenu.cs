using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Newtonsoft.Json;

public class mainmenu : MonoBehaviour
{

    [SerializeField]
    private InputField _username;
    [SerializeField]
    private InputField _password;
    [SerializeField]
    private Button _btnLogin;

    public void btnPressLogin()
    {
        Debug.Log("pressed the login button " + _username.text + " and " + _password.text);
        if (_username.text != "" && _password.text != "")
        {
            StartCoroutine(doWebRequestGet("localhost:666/TestClean/api/users/" + _username.text + "/" + _password.text));
        }
    }


    private IEnumerator doWebRequestGet(string url)
    {
        WWW webReq = new WWW(url);
        yield return webReq;
        handleWebRequest(webReq.text);
    }

    private void handleWebRequest(string result)
    {
        if(result == null)
        {
            Debug.Log("No results found");
        }
        Debug.Log(result);
        GameUser gu = JsonUtility.FromJson<GameUser>(result);
        Debug.Log(gu.getUsername());
    }
}
