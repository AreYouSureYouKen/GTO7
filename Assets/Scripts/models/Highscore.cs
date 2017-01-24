using UnityEngine;
using System.Collections;

[System.Serializable]
public class Highscore{
    [SerializeField]
    private long id;
    [SerializeField]
    private string username;
    [SerializeField]
    private int seconds;
    [SerializeField]
    private int hitsTaken;
    [SerializeField]
    private int hitsDone;
    [SerializeField]
    private int longestCombo;
    [SerializeField]
    private int enemiesDefeated;
    [SerializeField]
    private int totalScore;

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

    public int getSeconds()
    {
        return this.seconds;
    }

    public void setSeconds(int seconds)
    {
        this.seconds = seconds;
    }

    public int getHitsTaken()
    {
        return this.hitsTaken;
    }

    public void setHitsTaken(int hitsTaken)
    {
        this.hitsTaken = hitsTaken;
    }

    public int getHitsDone()
    {
        return this.hitsDone;
    }

    public void setHitsDone(int hitsDone)
    {
        this.hitsDone = hitsDone;
    }

    public int getLongestCombo()
    {
        return this.longestCombo;
    }

    public void setLongestCombo(int longestCombo)
    {
        this.longestCombo = longestCombo;
    }

    public int getEnemiesDefeated()
    {
        return this.enemiesDefeated;
    }

    public void setEnemiesDefeated(int enemiesDefeated)
    {
        this.enemiesDefeated = enemiesDefeated;
    }

    public int getTotalScore()
    {
        return this.totalScore;
    }

    public void setTotalScore(int totalscore)
    {
        this.totalScore = totalscore;
    }
}
