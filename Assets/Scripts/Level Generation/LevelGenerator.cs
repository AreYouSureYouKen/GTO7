using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField]
    CameraController _cameraControl;
    [SerializeField]
    private List<GameObject> _levelPrefabs;

    [SerializeField]
    private int _numberOfLevelPieces;

    // Use this for initialization
    void Start()
    {
        generateLevel();
    }
    [ContextMenu("Generate")]
    private void generateLevel()
    {
        _cameraControl.SetFirstLevelPiece(this.transform.position);

        Vector3 _startPos = this.transform.position;
        Vector3 _currentPos = _startPos;
        Vector3 _PFBwidth = this.transform.localScale;
        GameObject _prefPFB = null;
        int i = 0;
        while (i < _numberOfLevelPieces)
        {
            GameObject _chosenPFB = _levelPrefabs[Random.Range(0, _levelPrefabs.Count - 1)];
            if (_prefPFB == _chosenPFB)
            {
                _chosenPFB = _levelPrefabs[Random.Range(0, _levelPrefabs.Count - 1)];
            }
            _prefPFB = _chosenPFB;
            _currentPos += Vector3.right * _PFBwidth.x;
            GameObject currentObj = (GameObject)Instantiate(_chosenPFB, _currentPos, _chosenPFB.transform.rotation);
            currentObj.GetComponent<LevelBlockRandomizer>().RandomizeLevelObjects();
            i++;
        }
        _cameraControl.SetLastLevelPiece(_currentPos);
        GameManager _gameManager = FindObjectOfType<GameManager>();
        _gameManager.StartTimer();
    }

}
