using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputVisuals : MonoBehaviour {
    [SerializeField]
    Image _ctrl;
    [SerializeField]
    Image _z;
    [SerializeField]
    Image _leftArrow;
    [SerializeField]
    Image _rightArrow;
    [SerializeField]
    Image _upArrow;
    [SerializeField]
    Image _downArrow;
    [SerializeField]
    Image _shift;

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            _ctrl.color = Color.white;
        }

        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            _ctrl.color = Color.grey;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            _z.color = Color.white;
        }

        if(Input.GetKeyUp(KeyCode.Z))
        {
            _z.color = Color.grey;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _leftArrow.color = Color.white;
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _leftArrow.color = Color.grey;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rightArrow.color = Color.white;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _rightArrow.color = Color.grey;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _upArrow.color = Color.white;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _upArrow.color = Color.grey;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _downArrow.color = Color.white;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _downArrow.color = Color.grey;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _shift.color = Color.white;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _shift.color = Color.grey;
        }
    }
}
