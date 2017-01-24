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
    private float _verticalSpeed = 0;
    [SerializeField]
    private float _gravity = 10;

    private Transform _player;
    private Animator anim;
    private GameManager _gameManager;

    [SerializeField]
    List<FSMStateTransition> StateTransitions;

    protected override void Initialize()
    {
        _currentHealth = StartingHealth;
        if (!_gameManager) _gameManager = FindObjectOfType<GameManager>();
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
            currentHealthBar = hb.gameObject.transform.GetChild(0).GetComponent<Image>();
        }

        this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);

        if (!_player)
        {
            Debug.LogWarning("No player found!?");
        }
        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        //healthbar section
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(this.gameObject.transform.position + Vector3.up * 2f);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        HealthBar.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
        if (alive)
        {


            // Horizontal speed section
            Vector3 velocity = _charController.velocity;
            float z = velocity.z;
            float x = velocity.x;
            Vector3 horizontalVelocity = new Vector3(x, 0, z);
            float horizontalSpeed = horizontalVelocity.magnitude;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(horizontalVelocity), Time.deltaTime * rotateSpeed);
            anim.SetFloat("speed", horizontalSpeed);
            
            //Attack section
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
            CurrentState.Reason(_player, transform);
            CurrentState.Act(_player, transform);
        }
    }

    public void PubMoveFSM(Vector3 targetPos, float moveSpeed, float rotateSpeed)
    {
        if (alive)
        {
            // adapted from http://answers.unity3d.com/questions/550472/move-character-controller-to-a-point.html
            this.moveSpeed = moveSpeed;
            this.rotateSpeed = rotateSpeed;


            Vector3 offset = targetPos - transform.position;
            if (offset.magnitude > .1f)
            {
                offset = offset.normalized * moveSpeed;
                _charController.Move(offset * Time.deltaTime);

            }
        }
        
    }

    public void SetAttackTrue()
    {
        enemWeap.SetAttacking(true);
    }

    public void SetAttackFalse()
    {
        enemWeap.SetAttacking(false);
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
        _gameManager.AddHitDone();
        if (_currentHealth <= 0f)
        {
            anim.Play("Dead");
            alive = false;
            _gameManager.AddEnemyDefeated();
            Destroy(this.gameObject,3f);
            Destroy(HealthBar.gameObject,3f);
        }

        float percentageLeft = _currentHealth / StartingHealth;
        currentHealthBar.fillAmount = percentageLeft;
    }
}
