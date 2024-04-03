using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRADCONTROL : MonoBehaviour
{
    public bool GodMode;

    [SerializeField]
    public float _moveSpeed = 12f;
    [SerializeField]
    public float _gravity = 1.0f;
    [SerializeField]
    public float _jumpSpeed = 3.5f;
    [SerializeField]
    public float _dashSpeed = 25f;
    [SerializeField]
    public float _bounceSpeed = 33f;
    [SerializeField]
    public float _doubleJumpMultiplier = 0.7f;
    [SerializeField]
    public float _burstSpeed = 13f;

    public CharacterController _controller;

    private float _directionX;
    public float _directionY;
    private float _directionZ;
    private bool _canDoubleJump = true;

    Vector3 dashVector;
    public float dashSlowDownSpeed = .5f;
    public float currentDash = 0f;

    private Camera _camera;

    private bool _canDash = true;
    public bool _canWallBump = true;

    public Animator anim;

    Vector3 bounceVector;
    public float bounceSlowDownSpeed = .5f;
    public float currentBounce = 0f;

    public int maxHealth = 3;
    public int currentHealth;
    public HealthBar healthBar;

    public Transform player;
    public Transform respawnPoint;

    [SerializeField]
    Animator UIanim;
    [SerializeField]
    Animator Camanim;

    public SphereCollider scollider;
    public Rigidbody rb;

    public Vector3 direction;
    public bool _isDashing = false;

    public ParticleSystem dashParticle;
    public ParticleSystem slideParticle;
    public ParticleSystem leapParticle;
    public ParticleSystem dustParticle;
    public ParticleSystem sparkleParticle;
    public ParticleSystem basic2Particle;
    public ParticleSystem jumpParticle;

    public GameObject Mouse1Icon;

    public bool AllowedToLook = true;
    private DialogueLookAt dla;

    public Door Door;

    public bool isHolding;
    public bool canalsoInteract;
    public GrabbableObjectScript Obj;
    public Transform HoldPoint;

    //private bool isBurstRunning = false;

    public bool _isShortHopping;
    private bool _canShortHop;

    private CameraRotation camrot;

    private float GroundedTime = 0f;
    private float GroundedRememberTime = 0.25f;

    public float _leapSpeed = 3f;

    public float _springSpeed = 1.75f;

    public PostProcessingScript postEffects;

    private bool isCrouching;

    [SerializeField] private GameObject hitEffect;

    public float gameSpeed;

    [SerializeField]private bool isSleeping = false;
    private bool isDancing = false;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        scollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        dla = GetComponent<DialogueLookAt>();
        camrot = FindObjectOfType<CameraRotation>();
    }


    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction = _camera.transform.TransformDirection(direction);
        direction.y = 0;

        if (direction.magnitude > 0.1 && (AllowedToLook == true))
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
        }
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }
        if (GameManager.isPaused == false)
        {
            Time.timeScale = gameSpeed;
        }

        if (GodMode == true)
        {
            currentHealth = 1738;
            _moveSpeed = 21f;
            _canDash = true;
            _canDoubleJump = true;

            if (Input.GetButton("Jump"))
            {
                _directionY = _jumpSpeed;
            }
        }
        //VVV freezes player. is used all over the game
        if (gameObject.CompareTag("PlayerTalking"))
        {
            direction.x = 0;
            direction.z = 0;
            _canDoubleJump = false;
            _canDash = false;
            _moveSpeed = 0f;
            AllowedToLook = false;
            currentDash = 0f;
            _jumpSpeed = 0f;
            anim.SetBool("isTalking", true);
        }
        else
        {
            anim.SetBool("isTalking", false);
        }
        //important shit
        if (_controller.isGrounded)
        {
            _gravity = .75f;
            _moveSpeed = 12f;
            _canDoubleJump = true;
            _canDash = true;
            anim.SetBool("Bloat 0", false);
            _directionY = Mathf.Clamp(_directionY, -.77f, 21f);
            anim.SetBool("Jump", false);
            rb.useGravity = false;
            _canWallBump = true;
            _isDashing = false;
            anim.ResetTrigger("WallBump");
            anim.ResetTrigger("EnemyWallBump");
            anim.SetBool("isBouncing", false);
            currentBounce = 0f;
            _isShortHopping = false;
            _canShortHop = true;
            StopSparkleParticle();
            StopBasic2Particle();
            GroundedTime = GroundedRememberTime;

                if (isCrouching)
                {
                    _moveSpeed = 4f;
                    _jumpSpeed = 0f;
                }
                else if (!gameObject.CompareTag(("PlayerTalking")))
                {
                    _moveSpeed = 12f;
                    _jumpSpeed = 2f;
                }

            if (Input.GetButtonDown("Jump") && isCrouching == false)
            {
                _gravity = 4f;
                _directionY = _jumpSpeed;
                _canDash = true;
                anim.SetBool("Jump", true);
                currentBounce = 0f;
                anim.SetBool("isBouncing", false);
                gameObject.transform.SetParent(null);
                _isShortHopping = false;
                GroundedTime = 0f;
            }
            if (Input.GetKeyDown(KeyCode.E) && _canShortHop == true)
            {
                _isShortHopping = true;
                _directionY = 1f;
                _gravity = 5f;
                _canDash = true;
                GroundedTime = 0;
            }
        }
        else
        {
            _directionY = Mathf.Clamp(_directionY, -3.5f, 21f);

            if (_isShortHopping == true)
            {
                if (Input.GetButtonDown("Dash") && _canDash)
                {
                    _directionY = -.5f;
                    _gravity = 10f;
                }

            }

            if (Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _gravity = 3.85f;
                _moveSpeed = 12f;
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
                anim.SetBool("Bloat 0", false);
                anim.SetBool("isBouncing", false);
                gameObject.transform.SetParent(null);
                _isShortHopping = false;
                StopSparkleParticle();
            }
            if (Input.GetButtonDown("Dash") && _canDash)
            {
                currentDash += _dashSpeed;
                _moveSpeed = 12.5f;
                _canDash = false;
                anim.SetBool("Bloat 0", false);
                dashParticle.Play();
                anim.SetBool("isBouncing", false);
                anim.SetTrigger("Wavedash");
                StartCoroutine(WavedashTiming(0.21f));
                _isDashing = true;

                if (_isShortHopping == false)
                {
                    _gravity = 4f;
                }
            }

            //testing for a possible twirl mechanic, ala new super mario bros
            //if (Input.GetKeyDown(KeyCode.E))
            //    {
            //    if (_directionY < 0)
            //    {
            //        _directionY = 0f;
            //    }
            //    else
            //    {
            //        _directionY += 0.25f;
            //    }
           //}
        }
        GroundedTime -= Time.deltaTime;
        if (GroundedTime > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _gravity = 4f;
                _directionY = _jumpSpeed;
                _canDoubleJump = true;
                _canDash = true;
                anim.SetBool("Jump", true);
                currentBounce = 0f;
                anim.SetBool("isBouncing", false);
                gameObject.transform.SetParent(null);
                _isShortHopping = false;
                GroundedTime = 0f;
            }
        }
        
        if (currentHealth <= 0)
        {
            StartCoroutine(Resp(gameObject, 3f));
            anim.SetBool("Dead", true);
            anim.SetBool("Hurt", false);
            anim.SetBool("isBouncing", false);
            gameObject.tag = "PlayerDead";
            _canDoubleJump = false;
            _canDash = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }
        //^^^debug code, remove when needed^^^

        _directionY -= _gravity * Time.deltaTime;
        direction.y = _directionY;

        //dash

        dashVector = new Vector3(0, 0, currentDash);
        dashVector = transform.TransformDirection(dashVector);

        _controller.Move(_moveSpeed * Time.deltaTime * (direction + dashVector));

        currentDash = Mathf.Lerp(currentDash, 0, dashSlowDownSpeed * Time.deltaTime);

        //bounce

        bounceVector = new Vector3(0, currentBounce, 0);
        bounceVector = transform.TransformDirection(bounceVector);

        _controller.Move(_doubleJumpMultiplier * Time.deltaTime * (direction + bounceVector));

        currentBounce = Mathf.Lerp(currentBounce, 0, bounceSlowDownSpeed * Time.deltaTime);


        //simplified animator bools

        anim.SetBool("isGrounded", _controller.isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("Dash", (Input.GetButtonDown("Dash")));
        anim.SetBool("canDash", _canDash);
        anim.SetBool("DoubleJump", (Input.GetButtonDown("Jump")));
        anim.SetBool("canDJ", _canDoubleJump);
        anim.SetBool("canWallBump", _canWallBump);
        anim.SetBool("Dashing", _isDashing);
        anim.SetBool("isCrouching", isCrouching);
        anim.SetBool("shiftHeld", (Input.GetKey(KeyCode.LeftShift)));
        anim.SetFloat("GroundedTime", GroundedTime);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GumdropDash") || (anim.GetCurrentAnimatorStateInfo(0).IsName("GumdropDash 0")
            && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.25f)))
        {
            _isDashing = true;
            //fix this later so isDashing is controlled via the animation itself

        }
        else
        {
            _isDashing = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SlideEnd") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f))
        {
            slideParticle.Stop();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GumdropSleep") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f))
        {
            isSleeping = true;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GumdropLeap") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f))
        {
            leapParticle.Stop();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GumdropWavedash") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f))
        {
            anim.SetBool("canLeap", true);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GumdropCrouch") || (anim.GetCurrentAnimatorStateInfo(0).IsName("GumdropCrouch_Loop")
            && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0f)))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        //VVV for cute little sleep anim
        if (isSleeping == true)
        {
            gameObject.tag = ("PlayerTalking");
            if (Input.GetButtonDown("Interact"))
            {
                anim.SetTrigger("wakeup");
            }
        }

        if (anim.GetBool("canInteract") == true)
        {
            Mouse1Icon.SetActive(true);
            dla.enabled = true;
        }
        if (anim.GetBool("canInteract") == false)
        {
            Mouse1Icon.SetActive(false);
            dla.enabled = false;
        }

        if (gameObject.CompareTag("PlayerHurt"))
        {
            anim.SetBool("Sliding", false);
        }
        if (gameObject.CompareTag("PlayerSlide"))
        {
            _gravity = 4f;
        }

       // rb.AddForce(_moveSpeed * 2 * direction);
        //^^^ for sliding mech 
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        sparkleParticle.Stop();
    }
    public void PlaySparkleParticle()
    {
        sparkleParticle.Play();
    }
    public void StopSparkleParticle()
    {
        sparkleParticle.Stop();
    }
    public void PlayBasic2Particle()
    {
        basic2Particle.Play();
    }
    public void StopBasic2Particle()
    {
        basic2Particle.Stop();
    }
    public void JumpFix()
    {
        _canDash = true;
        _canDoubleJump = true;
        jumpParticle.Play();
    }
    public void BloatGravityFix()
    {
        _gravity = 2f;
    }
    public void BounceFix()
    {
        anim.SetBool("isBouncing", false);
        leapParticle.Stop();
    }
    public void BounceFix2()
    {
        currentBounce = .8f;
        leapParticle.Stop();
    }
    public void BumpMech3EnemyOnly()
    {
        _canDash = true;
        _canDoubleJump = true;
        _canWallBump = true;
        gameObject.tag = "Player";
        _moveSpeed = 12f;
        anim.ResetTrigger("EnemyWallBump");
        currentDash = 1f;
        _gravity = 3.75f;
    }
    public void BumpMech2EnemyOnly()
    {
        _canDash = true;
        _canWallBump = false;
        _moveSpeed = 8f;
        currentDash = .7f;
        _directionY = 2f;
        camrot._CameraDistance += 1.5f;
    }
    public void BumpMech3()
    {
        _canDash = true;
        _canDoubleJump = true;
        _canWallBump = false;
        gameObject.tag = "Player";
        _moveSpeed = 9f;
        anim.ResetTrigger("WallBump");
        currentDash = -0.3f;
        _gravity = 3.75f;
    }
    public void BumpMech3_Boss()
    {
        _canDash = true;
        _canDoubleJump = true;
        _canWallBump = false;
        gameObject.tag = "Player";
        _moveSpeed = 9f;
        anim.ResetTrigger("BossBump");
        currentDash = -0.4f;
        _gravity = 3.75f;
    }
    public void BumpMech2()
    {
        _isDashing = false;
        _canDash = false;
        _canDoubleJump = false;
        _canWallBump = false;
        _moveSpeed = 5f;
        currentDash = -4.3f;
        _directionY = 2f;
        sparkleParticle.Play();
        camrot._CameraDistance += 1.5f;
    }
    public void BumpMech2_Boss()
    {
        _isDashing = false;
        _canDash = false;
        _canDoubleJump = false;
        _canWallBump = false;
        _moveSpeed = 5f;
        currentDash = -5f;
        _directionY = 2.1f;
        sparkleParticle.Play();
        camrot._CameraDistance += 3f;
    }
    public void BumpMech2_Bad()
    {
        _isDashing = false;
        _canDash = false;
        _canWallBump = false;
        _moveSpeed = 5f;
        currentDash = -3.5f;
        _directionY = 1f;
        basic2Particle.Play();
        camrot._CameraDistance += 1.5f;
    }
    public void BumpMech1_Bad()
    {
        _canDash = false;
        _canDoubleJump = false;
        _isDashing = false;
        _moveSpeed = 3f;
        _directionY = 0.1f;
        currentDash = 0f;
        sparkleParticle.Stop();
        camrot._CameraDistance -= 1.5f;
    }

    public void BumpMech1()
    {
        _canDash = false;
        _canDoubleJump = false;
        _isDashing = false;
        _moveSpeed = 3f;
        _directionY = 0.1f;
        currentDash = 0f;
        sparkleParticle.Stop();
        camrot._CameraDistance -= 1.5f;
    }
    public void BumpMech1_Boss()
    {
        _canDash = false;
        _canDoubleJump = false;
        _isDashing = false;
        _moveSpeed = 3f;
        _directionY = 0.1f;
        currentDash = 0f;
        sparkleParticle.Stop();
        camrot._CameraDistance -= 3f;
    }
    public void SpawnHitEffect()
    {
        Instantiate(hitEffect, gameObject.transform.position, Quaternion.identity);
    }
    public void WavedashBoost()
    {
        currentDash = 0.5f;
        dustParticle.Play();
        sparkleParticle.Stop();
    }
    public void LeapMech1()
    {
        currentDash = _leapSpeed;
        _directionY -= .03f;
        leapParticle.Play();
        sparkleParticle.Play();
        dustParticle.Stop();
    }
    public void LeapMech1Repeated()
    {
        currentDash = _leapSpeed;
    }
    public void LeapMech2()
    {
        currentDash += 1f;
    }
    public void LeapMech3()
    {
        currentDash += .3f;
        leapParticle.Stop();
        sparkleParticle.Stop();
    }
    public void SpringMech1()
    {
        anim.SetBool("springCanTransitionOut", false);
        _canDash = false;
        _canDoubleJump = false;
        _isDashing = false;
        _directionY = -0.1f;
        currentDash = 0f;
        sparkleParticle.Stop();
        camrot._CameraDistance -= 1.5f;
    }
    public void SpringMech2()
    {
        _canDash = false;
        _canDoubleJump = false;
        _isDashing = false;
        _directionY = _springSpeed;
        _jumpSpeed = 0f;
        currentDash = -0.1f;
        camrot._CameraDistance += 1.5f;
    }
    public void SpringMech3()
    {
        _gravity = 4f;
        _jumpSpeed = 2f;
        _moveSpeed = 2f;
        _canDash = true;
        _canDoubleJump = true;
        sparkleParticle.Play();
        jumpParticle.Play();
    }
    public void SpringMech4()
    {
        _moveSpeed = 2f;
        _directionY += 0.3f;
        currentDash = -0.6f;
        _gravity = 3.75f;
        sparkleParticle.Stop();
    }
    public void SpringMech5()
    {
        anim.SetBool("springCanTransitionOut", true);
        _moveSpeed = 12f;
    }
    public void HitLockOn()
    {
        _gravity = 2f;
        _canDoubleJump = false;
        _canDash = false;
        _directionY = 2.2f;
        anim.SetBool("Invuln", true);
        anim.SetBool("Sliding", false);
    }
    public void HitLockOff()
    {
        anim.SetBool("Hurt", false);
        _gravity = 3f;
        _canDoubleJump = true;
        _canDash = true;
        _directionY = 0.5f;
    }
    public void DeadLock()
    {
        anim.SetBool("Invuln", false);
        anim.SetBool("Hurt", false);
        gameObject.tag = "PlayerDead";
        _canDoubleJump = false;
        _canDash = false;
        _gravity = 2.5f;
        _directionY = 1.3f;
        UIanim.SetBool("UIDeath", true);
        if (isHolding == true)
        {
            Obj.ForceDrop();
        }
    }
    public void DeathAnimReset()
    {
        anim.SetBool("Dead", false);
    }
    public void DeathRespawn()
    {
        player.transform.position = respawnPoint.transform.position;
        gameObject.tag = "PlayerHurt";
        anim.SetBool("Invuln", true);
        StartCoroutine(Hurt(gameObject, 5f));
    }
    public void slideParticlePlayFix()
    {
        slideParticle.Play();
    }
    public void WakeUp()
    {
        gameObject.tag = ("Player");
        jumpParticle.Play();
        isSleeping = false;
        _moveSpeed = 1f;
        _directionY = .444f;
        AllowedToLook = true;
        _isDashing = false;
        _canDash = false;
        _canDoubleJump = false;
        _canWallBump = false;
    }


    public void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.CompareTag("Slope"))
        {
            rb.velocity = new Vector3(0, -10, 0);
            _controller.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            scollider.enabled = true;
            _canDoubleJump = false;
            _canDash = false;
            _gravity = 4f;
            rb.useGravity = true;
            gameObject.tag = "PlayerSlide";
        }

        if (collider.gameObject.CompareTag("Ground"))
        {
            if (gameObject.CompareTag("PlayerSlide"))
            {
                StartCoroutine(Slope(gameObject, 0.25f));
                StartCoroutine(GravityFix(gameObject, 30f));
                _gravity = 4f;
                anim.SetBool("Sliding", false);
            }
        }
        if (collider.gameObject.CompareTag("ActualWall"))
        {
            _canDoubleJump = false;
            currentDash = -0.2f;
            if (Input.GetButtonDown("Jump"))
            {
                _jumpSpeed = 0f;
            }
        }
    }
    public void OnCollisionEnter(Collision collider)
    {

        if (collider.gameObject.CompareTag("Slope"))
        {
            rb.velocity = new Vector3(0, -10, 0);
            _controller.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            scollider.enabled = true;
            _canDoubleJump = false;
            _canDash = false;
            _gravity = 4f;
            rb.useGravity = true;
            gameObject.tag = "PlayerSlide";
            StartCoroutine(Slope(gameObject, 1f));
            StartCoroutine(GravityFix(gameObject, 1.10f));
            anim.SetBool("Sliding", true);
        }
        if (collider.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Sliding", false);
            gameObject.transform.SetParent(null);
            slideParticle.Stop();
            _jumpSpeed = 2f;
        }
        if (collider.gameObject.CompareTag("Wall") && _canWallBump)
        {
            if (_isDashing == true)
            {
                _directionY = 0.1f;
                _moveSpeed = 0f;
                anim.SetTrigger("WallBump");
                _canDoubleJump = false;
            }
        }
        if (collider.gameObject.CompareTag("Wall") && _canWallBump == false)
        {
            if (_isDashing == true)
            {
                _directionY = 0.1f;
                _moveSpeed = 0f;
                anim.SetTrigger("WallBump_Bad");
                _canDoubleJump = false;
            }
        }
        if (collider.gameObject.CompareTag("ActualWall"))
        {
            currentDash = -0.2f;
            _moveSpeed = 0.1f;
            if (Input.GetButtonDown("Jump"))
            {
                _jumpSpeed = 0f;
            }
        }
        if (collider.gameObject.CompareTag("Hurt"))
        {

            if (gameObject.CompareTag("Player"))
            {
                TakeDamage(1);
                gameObject.tag = "PlayerHurt";
                _canDoubleJump = true;
                _canDash = true;
                StartCoroutine(Hurt(gameObject, 4f));
                anim.SetBool("Hurt", true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            Door = other.gameObject.GetComponent<Door>();
        }

        if (other.CompareTag("BouncyPlat"))
        {
            Debug.Log("Hello: " + gameObject.name);
            _directionY = 0;
            currentBounce = _bounceSpeed;
            _gravity = 3f;
            anim.SetBool("isBouncing", true);

        }
        if (other.gameObject.CompareTag("Refresh"))
        {
            _canDoubleJump = true;
            _canDash = true;
            _gravity = 1.5f;
            _moveSpeed = 10f;
            anim.SetBool("Bloat 0", true);

        }
        if (other.gameObject.CompareTag("Hurt"))
        {

            if (gameObject.CompareTag("Player"))
            {
                TakeDamage(1);
                gameObject.tag = "PlayerHurt";
                _canDoubleJump = true;
                _canDash = true;
                StartCoroutine(Hurt(gameObject, 4f));
                anim.SetBool("Hurt", true);
            }
        }
        if (other.gameObject.CompareTag("RespawnTrigger"))
        {
            TakeDamage(10);
            anim.SetBool("Sliding", false);
            StartCoroutine(Slope(gameObject, 1f));
        }
        if (other.gameObject.CompareTag("EnemyWall"))
        {
            Debug.Log("Hello: " + gameObject.name);
            if (_isDashing == true)
            {
                _directionY = 0.5f;
                _moveSpeed = 0.5f;
                anim.SetTrigger("EnemyWallBump");
                _canDoubleJump = false;
            }
        }
        if (other.CompareTag("NPC") || (other.CompareTag("Checkpoint")))
        {
            anim.SetBool("canInteract", true);
        }
        if (other.CompareTag("Snail"))
        {
            anim.SetBool("canInteract", true);
        }
        if (other.gameObject.CompareTag("BossWall") && (_canWallBump || _canWallBump == false))
        {
            if (_isDashing == true)
            {
                _directionY = 0.1f;
                _moveSpeed = 0f;
                anim.SetTrigger("BossBump");
                _canDoubleJump = false;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hurt"))
        {

            if (gameObject.CompareTag("Player"))
            {
                TakeDamage(1);
                gameObject.tag = "PlayerHurt";
                _canDoubleJump = true;
                _canDash = true;
                anim.SetBool("Hurt", true);
                StartCoroutine(Hurt(gameObject, 4f));
            }
            if (gameObject.CompareTag("PlayerSlide"))
            {
                TakeDamage(1);
                gameObject.tag = "PlayerHurt";
                _canDoubleJump = true;
                _canDash = true;
                anim.SetBool("Hurt", true);
                anim.SetBool("Sliding", false);
                StartCoroutine(Hurt(gameObject, 4f));
            }
        }
        if (other.CompareTag("Pear") || other.CompareTag("Object"))
        {
            anim.SetBool("canInteract", true);
            Obj = other.gameObject.GetComponent<GrabbableObjectScript>();
            Obj.SelectedObject = true;
        }
         if (other.CompareTag("BrownSeed") || other.CompareTag("GoldenSeed"))
        {
            anim.SetBool("canInteract", true);
            Obj = other.gameObject.GetComponent<GrabbableObjectScript>();
            Obj.SelectedObject = true;
        }
        if (other.CompareTag("Snail"))
        {
            anim.SetBool("canInteract", true);
            Obj = other.gameObject.GetComponent<GrabbableObjectScript>();
            Obj.SelectedObject = true;
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BouncyPlat"))
        {
            _canDash = true;
            _canDoubleJump = true;
            _gravity = 2.75f;
        }
        if (other.CompareTag("NPC") || (other.CompareTag("Checkpoint")))
        {
            anim.SetBool("canInteract", false);
        }
        if (other.CompareTag("GolfBall"))
        {
            
        }
        if (other.CompareTag("BrownSeed") || other.CompareTag("GoldenSeed"))
        {
            anim.SetBool("canInteract", false);
            if (Obj != null)
            {
                Obj.SelectedObject = false;
                Obj = null;
            }
        }
        if (other.CompareTag("Pear") || other.CompareTag("Object"))
        {
            anim.SetBool("canInteract", false);
            if (Obj != null)
            {
                Obj.SelectedObject = false;
                Obj = null;
            }
        }
        if (other.CompareTag("Snail"))
        {
            anim.SetBool("canInteract", false);
            if (Obj != null)
            {
                Obj.SelectedObject = false;
                Obj = null;
            }
        }
    }
    private IEnumerator Hurt(GameObject target, float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.tag = "Player";
        anim.SetBool("Invuln", false);
    }

    private IEnumerator Slope(GameObject target, float time)
    {
        yield return new WaitForSeconds(time);
        _controller.enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        scollider.enabled = false;
        gameObject.tag = "Player";
        _gravity = 4f;
    }
    private IEnumerator GravityFix(GameObject target, float time)
    {
        yield return new WaitForSeconds(time);
        _gravity = 4f;
    }
    private IEnumerator WavedashTiming(float time)
    {
        yield return new WaitForSeconds(time);
        anim.ResetTrigger("Wavedash");
    }
    private IEnumerator Resp(GameObject target, float time)
    {

        yield return new WaitForSeconds(time);
        healthBar.SetHealth(3);
        currentHealth = maxHealth;
        anim.SetBool("Hurt", false);
        anim.SetBool("Dead", false);
        UIanim.SetBool("UIDeath", false);
        _gravity = 4f;
    }
    public IEnumerator EnableGD(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("Disabled", false);
        gameObject.tag = "Player";
        AllowedToLook = true;
        camrot.ApplySavedFloats();
        _jumpSpeed = 2f;
    }
}