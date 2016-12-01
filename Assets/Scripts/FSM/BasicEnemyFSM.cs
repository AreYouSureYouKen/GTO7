using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class FSMStateTransition
{
    public FSMState FSMState;
    public List<TransitionState> Transitions;

}

/// <summary>
/// Wrapper class for the Transitions and State ID's
///  <param name="Transition">Enum ID of Transition</param>
///   <param name="FSMStateID">Enum ID of State</param>
/// </summary>
[System.Serializable]
public class TransitionState
{
    public Transition Transition;
    public FSMStateID FSMStateID;
}
public class BasicEnemyFSM : AdvancedFSM {

    [SerializeField]
    private float StartingHealth;
    private float _currentHealth;
    [SerializeField]
    private RectTransform CanvasRect;
    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private Image currentHealthBar;
    [SerializeField]
    private Weapon enemWeap;

    [SerializeField]
    private Transform[] _waypoints;

    private float elapsedTime;
    private bool wantToAttack = false;
    private bool alive = true;

    private Transform _player;
    private Animator anim;

    [SerializeField]
    List<FSMStateTransition> StateTransitions;

    protected override void Initialize()
    {
        _currentHealth = StartingHealth;
        anim = GetComponent<Animator>();
        elapsedTime = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _charController = GetComponent<CharacterController>();
        if (!CanvasRect) CanvasRect = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
        // If user forgot to add a healthbar to this FSM.
        if (!HealthBar)
        {
            Debug.LogWarning("No healthbar found, assigning a new one.");
            GameObject hb = Instantiate(Resources.Load("Prefabs/Healthbar")) as GameObject;
            hb.transform.SetParent(CanvasRect);
            HealthBar = hb.GetComponent<Image>();
            // get the current healthbar image in the child object asshole code.
            currentHealthBar = hb.gameObject.GetComponentInChildren<Image>();
        }



        if (!_player)
        {
            Debug.LogWarning("No player found!?");
        }
        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        if (alive)
        {
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(this.gameObject.transform.position + Vector3.up * 2f);
            Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            HealthBar.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
            Vector3 velocity = _charController.velocity;
            float z = velocity.z;
            float x = velocity.x;
            Vector3 horizontalVelocity = new Vector3(x, 0, z);
            float horizontalSpeed = horizontalVelocity.magnitude;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(horizontalVelocity), Time.deltaTime * rotateSpeed);
            anim.SetFloat("speed", horizontalSpeed);
            enemWeap.SetAttacking(anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"));
            if (wantToAttack && attackSpeed <= elapsedTime)
            {
                anim.SetBool("Attack", true);
                elapsedTime = 0f;
                wantToAttack = false;
            }
            else
            {
                anim.SetBool("Attack", false);
            }



            elapsedTime += Time.deltaTime;
        }
    }

    protected override void FSMFixedUpdate()
    {
        if (alive)
        {
            Debug.Log(CurrentState);
            CurrentState.Reason(_player, transform);
            CurrentState.Act(_player, transform);
        }
    }

    public void PubMoveFSM(Vector3 targetPos, float moveSpeed, float rotateSpeed)
    {
        // adapted from http://answers.unity3d.com/questions/550472/move-character-controller-to-a-point.html
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;


            Vector3 offset = targetPos - transform.position;
        if (offset.magnitude > .1f)
        {
            offset = offset.normalized * moveSpeed;
 
            if (!_charController.isGrounded)
            {
                offset.y -= 5f * Time.deltaTime;
            }

            _charController.Move(offset * Time.deltaTime);
            
        }
        
    }

    public void TryAttack()
    {
        wantToAttack = true;
    }

    public void SetTransition(Transition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        foreach (FSMStateTransition FSMST in StateTransitions)
        {
            FSMState State = FSMST.FSMState;

            System.Object ActualState = System.Convert.ChangeType(State, FSMST.FSMState.GetType());
            FSMST.FSMState.Setwaypoints(_waypoints);
            foreach (TransitionState TST in FSMST.Transitions)
            {
                FSMST.FSMState.AddTransition(TST.Transition, TST.FSMStateID);
            }

            AddFSMState(FSMST.FSMState);
        }
    }

    public void Damage(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0f)
        {
            anim.Play("Dead");


            Destroy(this.gameObject,3f);
            Destroy(HealthBar.gameObject,3f);
        }

        float percentageLeft = _currentHealth / StartingHealth;
        currentHealthBar.fillAmount = percentageLeft;
    }
}
