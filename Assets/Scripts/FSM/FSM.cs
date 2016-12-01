using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {

    protected Transform player;
    protected CharacterController _charController;
    protected Vector3 destPos;

    protected GameObject[] pointList;

    protected float attackSpeed;
    protected float moveSpeed;
    protected float rotateSpeed;

    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }
    protected virtual void MoveFSM(Vector3 targetPos, float moveSpeed) { }

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
