using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelBlockRandomizer : MonoBehaviour {
    private GameManager _gameManager;
    [ContextMenu("Randomize")]
    public void RandomizeLevelObjects()
    {
        foreach (Transform obj in this.transform)
        {
            bool trueOrFalse;
            if (Random.Range(0, 4) <= 2) trueOrFalse = true; else trueOrFalse = false;
            if (obj.name.Equals("EnemySpawn"))
            {
                if(trueOrFalse)
                {
                    GameObject hb = Instantiate(Resources.Load("Prefabs/Skeleton")) as GameObject;
                    hb.transform.position = obj.transform.position;
                    if(_gameManager)
                    _gameManager.AddToTotalEnemies();
                }
            }
            else
            {
                obj.gameObject.SetActive(trueOrFalse);
            }
        }
    }
	
}
