using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelBlockRandomizer : MonoBehaviour {
    [ContextMenu("Randomize")]
    public void RandomizeLevelObjects()
    {
        foreach(Transform obj in this.transform)
        {
            bool trueOrFalse;
            if (Random.Range(0, 4) <= 2) trueOrFalse = true; else trueOrFalse = false;

            obj.gameObject.SetActive(trueOrFalse);
        }
    }
	
}
