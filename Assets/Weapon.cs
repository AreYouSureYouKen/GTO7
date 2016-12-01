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

                //Instantiate(impactPrefab, contact.point, Quaternion.FromToRotation(Vector3.up, contact.normal));
            Debug.Log("sending damage to " + other.gameObject.name);
            other.gameObject.SendMessage("Damage", Damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SetAttacking(bool newVal)
    {
        _isAttacking = newVal;
    }
}
