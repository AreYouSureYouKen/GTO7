using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

    private CharacterController _characterController;
    private Animator _animator;
    private Weapon _weapon;
    public bool canJump = true;
    public float jumpHeight = 2.0F;
    public float gravity = 25.0F;
    public float StartingHealth = 100f;


    public float walkSpeed = 4.0F;
    public float runSpeed = 6.0F;
    public float dampTime = 3.0F;
    public float rotateSpeed = 8.0f;

    public Image CurrentHealth;
    public Transform player;
    public Transform chest;

    public Transform shield;
    public Transform weapon;
    public Transform leftHandPos;
    public Transform rightHandPos;
    public Transform chestPosShield;
    public Transform chestPosWeapon;
    public AudioClip equipSound;
    public AudioClip holsterSound;
    public AudioClip[] wooshSounds;
    public AudioSource myAudioSource;



    private bool _fightModus = false;
    private bool _didSelect;
    private bool _canAttack = false;

    private bool _running = false;
    private bool _canRun = true;
    private bool _canJump = false;
    private bool _isJumping = false;
    private Vector3 moveDirection = Vector3.zero;
    private float _verticalSpeed = 0;
    private float _currentHealth;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _currentHealth = StartingHealth;
    }

    void Update()
    {


        moveDirection = new Vector3(Input.GetAxis("Vertical") * -1, _verticalSpeed, Input.GetAxis("Horizontal"));
        moveDirection = transform.TransformDirection(moveDirection);
        if (_canRun && _characterController.isGrounded && Input.GetButton("Fire3"))
        {
            moveDirection *= runSpeed;
        }
        else
        {
            moveDirection *= walkSpeed;
        }

        if (_fightModus) 
        {
            bool attackstate = _animator.GetCurrentAnimatorStateInfo(0).IsName("attacks");
            _weapon.SetAttacking(attackstate);
            if (Input.GetKeyDown("z") && !attackstate)
            {
                myAudioSource.clip = wooshSounds[Random.Range(0, wooshSounds.Length)];
                myAudioSource.pitch = 0.98f + 0.1f * Random.value;
                myAudioSource.Play();

                _animator.SetBool("attack", true);
            }
            else
            {
                _animator.SetBool("attack", false);
            }
        }
        



        Vector3 velocity = _characterController.velocity;
        float z = velocity.z;
        float x = velocity.x;
        Vector3 horizontalVelocity = new Vector3(x, 0, z);
        float horizontalSpeed = horizontalVelocity.magnitude;
        Vector3 localMagnitude = transform.InverseTransformDirection(horizontalVelocity);

        _animator.SetFloat("hor", (localMagnitude.x), dampTime, 0.8f);
        _animator.SetFloat("ver", (localMagnitude.z), dampTime, 0.8f);
        if (_characterController.isGrounded)
        {
            if (Input.GetButton("Fire1"))
            {
                StartCoroutine(weaponSelect());
            }
            _animator.SetFloat("speed", horizontalSpeed, dampTime, 0.2f);
            if (Input.GetButton("Jump"))
            {
                _animator.SetBool("Jump", true);
                _verticalSpeed = jumpHeight;
            }
            else
            {
                _animator.SetBool("Jump", false);
            }
        }
        else
        {
            _verticalSpeed -= gravity * Time.deltaTime;
        }
        
        if (horizontalVelocity.magnitude > 0.01)
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(horizontalVelocity), Time.deltaTime * rotateSpeed);
        _characterController.Move(moveDirection * Time.deltaTime);
        _animator.SetBool("grounded", _characterController.isGrounded);
    }

    public IEnumerator weaponSelect ()
    {
        if (_fightModus)
        {

            _animator.CrossFade("Holster", 0.15f, 0, 0);
            _fightModus = false;
            yield return new WaitForSeconds(1);
        }
        else
        {

            _animator.CrossFade("Equip", 0.15f, 0, 0);
            _fightModus = true;
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }



    public void equip()
    {
        weapon.parent = rightHandPos;
        weapon.position = rightHandPos.position;
        weapon.rotation = rightHandPos.rotation;
        myAudioSource.clip = equipSound;
        myAudioSource.loop = false;
        myAudioSource.pitch = 0.9f + 0.2f * Random.value;
        myAudioSource.Play();
        shield.parent = leftHandPos;
        shield.position = leftHandPos.position;
        shield.rotation = leftHandPos.rotation;
        _fightModus = true;
        _canRun = false;

    }

    public void OnAnimatorIK()
    {


    }

    public  void holster()
    {
        shield.parent = chestPosShield;
        shield.position = chestPosShield.position;
        shield.rotation = chestPosShield.rotation;
        myAudioSource.clip = holsterSound;
        myAudioSource.loop = false;
        myAudioSource.pitch = 0.9f + 0.2f * Random.value;
        myAudioSource.Play();
        _fightModus = false;
        weapon.parent = chestPosWeapon;
        weapon.position = chestPosWeapon.position;
        weapon.rotation = chestPosWeapon.rotation;
        _canRun = true;
    }

    public void Damage(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0f)
        {
            _animator.Play("Dead");
        }

        float percentageLeft = _currentHealth / StartingHealth;
        CurrentHealth.fillAmount = percentageLeft;
    }
}
