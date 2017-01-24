using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 firstLevelPiece;
    public Vector3 LastLevelPiece;

    public void SetFirstLevelPiece(Vector3 pos)
    {
        firstLevelPiece = pos;
    }

    public void SetLastLevelPiece(Vector3 pos)
    {
        LastLevelPiece = pos;
    }




    void Update()
    {
        float newxPosition = Mathf.Clamp(target.position.x, firstLevelPiece.x, LastLevelPiece.x);


        this.transform.position = new Vector3(newxPosition, transform.position.y, transform.position.z);
    }
}
