using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameUser {

    [SerializeField]
    private long id;
    [SerializeField]
    private string username;
    [SerializeField]
    private string password;

    public GameUser()
    {

    }

    public GameUser(long id, string username, string password)
    {
        this.id = id;
        this.username = username;
        this.password = password;
    }

    public long getId()
    {
        return this.id;
    }

    public void setId(long id)
    {
        this.id = id;
    }

    public string getUsername()
    {
        return this.username;
    }

    public void setUsername(string username)
    {
        this.username = username;
    }

    public string getPassword()
    {
        return this.password;
    }

    public void setPassword(string password)
    {
        this.password = password;
    }
}
