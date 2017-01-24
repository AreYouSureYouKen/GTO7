using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public GameObject impactPrefab;
    public float Damage = 20f;
    private bool _isAttacking = false;

    public void OnTriggerEnter(Collider other)
    { 
        Debug.Log("this weapon is " + _isAttacking);
        if (_isAttacking)
        {
                Instantiate(impactPrefab, this.transform.position, Quaternion.FromToRotation(Vector3.up, Vector3.up));
            Debug.Log("sending damage to " + other.gameObject.name);
            other.gameObject.SendMessage("Damage", Damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SetAttacking(bool newVal)
    {
        _isAttacking = newVal;
    }
}
