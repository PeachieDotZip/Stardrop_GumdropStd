using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSnipper : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    Transform playerTarget;
    NavMeshAgent agent;
    public int HP = 21;
    public Transform currentTarget;
    [SerializeField] private Transform forwardsTarget;
    [SerializeField] private Transform backwardsTarget;
    [SerializeField] private Transform bulletRightEye;
    [SerializeField] private Transform bulletLeftEye;
    public GameObject bullet;
    public GameObject burst;
    public HelperSpawner helperSpawner;
    public ParticleSystem[] particles;

    [Header("Behaviour-related Variables")]
    [Space]
    //behaviour-related variables:
    public int attackState = 0;
    public int prevState = 0;
    public int prevReactState = 0;
    public int attackCount;
    public bool reactionAttack;
    public bool isFacing; //Is facing player?
    public bool isChasing; //Is currently moving forwards?
    public bool isReversing;//Is currently moving backwards?
    public float angerMultiplier; //Used for increasing the boss' movement speed and animation speed.
    public bool isStunned; //Is the boss currently stunned?
    public bool inFinalPhase; //Is the boss in its final phase? 

    /// <summary>
    /// Assigns starting variables.
    /// </summary>
    public void Start()
    {
        playerTarget = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        helperSpawner = FindObjectOfType<HelperSpawner>();
        currentTarget = playerTarget; //default value is playerTarget
        anim.SetInteger("HP", HP);
        anim.SetFloat("angerMultiplier", angerMultiplier);
    }

    public void Update()
    {
  
        agent.SetDestination(currentTarget.position);

        if (isFacing == true)
        {
            FaceTarget();
        }
        if (isChasing == true)
        {
            currentTarget = playerTarget;
        }
        if (isReversing == true)
        {
            currentTarget = backwardsTarget;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            attackState = 1;
        }
        if (isStunned == true)
        {
            FaceCurrentTarget();
        }
    }


    /// <summary>
    /// Called at the beginning of each state animation. Checks which state it is currently in, then calls SetStateVariables() while
    /// assigning the correct parameters for that animation.
    /// </summary>
    public void CallStateFunction()
    {
        switch (attackState)
        {
            case 0:
                Debug.Log("<color=red>Error: </color>0 should not be a possible attack state at this time!");
                break;
            case 1:
                SetStateVariables(true, true, 3);
                break;
            case 2:
                SetStateVariables(false, true, 0);
                break;
            case 3:
                SetStateVariables(true, true, 0);
                break;
            case 4:
                SetStateVariables(true, false, 0);
                break;
            case 5:
                SetStateVariables(true, false, 0);
                break;
            case 6:
                SetStateVariables(true, true, 0);
                break;
            case 7:
                SetStateVariables(true, true, 0);
                break;
            case 8:
                SetStateVariables(true, true, 0);
                break;
            default:
                Debug.Log("<color=red>Error: </color>Attack state out of possible range!");
                break;
        }
    }

    /// <summary>
    /// Sets up the appropriate behaviour for the beginning of each state.
    /// What it should change and how is determined by CallStateFunction() assigning this function's parameters.
    /// </summary>
    /// <param name="isBossFacing"></param>
    /// <param name="overwriteSpeed"></param>
    /// <param name="newSpeed"></param>
    void SetStateVariables(bool isBossFacing, bool overwriteSpeed, float newSpeed)
    {
        isFacing = isBossFacing;

        if (overwriteSpeed == true)
        {
            agent.speed = newSpeed * angerMultiplier;
        }
    }
    /// <summary>
    /// Called at the end of the Idle animation.
    /// Used for determining what the next attack the boss will perform should be.
    /// </summary>
    public void ChooseAttackState()
    {
        if (reactionAttack == false)
        {
            attackState = Random.Range(1, 6); //normal attack state numbers (1, 2, 3, 4, 5)
        }
        else
        {
            attackState = Random.Range(6, 9); //reaction attack state numbers (6, 7, 8)
        }

        if (attackState != 1 && attackCount >= 4)
        {
            //This check makes sure that if enough attacks have been ed, then the next attack will always be the shield approach one.
            //This is to make sure the player can have a guaranteed chance to damage the boss.
            attackState = 1;
            anim.SetInteger("attackState", attackState);
            anim.SetTrigger("attack");
            prevState = attackState;
            return;
        }
        if (HP >= 7 && attackCount <= 1)
        {
            //This check forces the first attack the boss performs to be the rush attack.
            //This is because the rush attack kinda serves as an introduction to the boss' abilities.
            attackState = 2;
            anim.SetInteger("attackState", attackState);
            anim.SetTrigger("attack");
            prevState = attackState;
            return;
        }
        if (HP == 6 && reactionAttack == true)
        {
            //This check forces the first attack the boss performs to be the rush attack.
            //This is because the rush attack kinda serves as an introduction to the boss' abilities.
            attackState = 7;
            anim.SetInteger("attackState", attackState);
            anim.SetTrigger("attack");
            prevState = attackState;
            return;
        }


        if ((attackState == prevState) || (attackState == prevReactState) || (attackState == 1 && attackCount < 2))
        {
            //^^^ This check does the following:
            //1. If the chosen attack state is the same as the last one logged, then rerun this function and choose a new one until it is different.
            //This is to keep the boss from doing the same attack twice bc that shit's lame.
            //2. If the chosen attack state is the same as the last reaction attack, then rerun this function and choose a new one until it is different.
            //This is to keep the boss from doing the same reaction attack as the last reaction bc that shit's also lame.
            //3. If the chosen attack state is 1 (shield approach) and the amount of attacks performed is less than 2, then rerun this function and choose a new one until it is different.
            //This is to keep the boss from allowing itself to be vulnerable too soon.
            ChooseAttackState();
        }
        else
        {
            //If both of those checks are passed, then the chosen attack in this context is valid and will now be performed.
            anim.SetInteger("attackState", attackState);
            anim.SetTrigger("attack");
            prevState = attackState;
        }
    }

    /// <summary>
    /// Called at the beginning of the Idle animation. Sets correct behaviour for the Idle state, resets certain variables, and increments the attackCount.
    /// </summary>
    public void SetIdleStateVariables()
    {
        attackState = 0;
        agent.updateRotation = true;
        isFacing = true;
        agent.speed = 1 * angerMultiplier;
        reactionAttack = false;
        anim.ResetTrigger("inRange");
        attackCount += 1;
        Debug.Log("Boss Idling");
    }

    /// <summary>
    /// Resets appropriate variables and returns the boss to idle animation/state.
    /// </summary>
    public void ReturnToIdleState()
    {
        reactionAttack = false;
        attackState = 0;
        anim.SetTrigger("ReturnToIdle");
    }

    public void BeginFacingPlayer()
    {
        isFacing = true;
    }

    public void StopFacingPlayer()
    {
        isFacing = false;
    }
    public void UpdateRotationTrue()
    {
        agent.updateRotation = true;
    }

    public void UpdateRotationFalse()
    {
        agent.updateRotation = false;
    }

    //Stops all current movement. Toggles movement-related bools to false and sets the current target to its default value.
    public void StopMovement()
    {
        agent.speed = 0;
        isChasing = false;
        isReversing = false;
        //returns currentTarget to default value (playerTarget)
        currentTarget = playerTarget;
    }

    //Starts a forward movement (moving towards the current target)
    public void StartMovement(float newMovementSpeed)
    {
        agent.speed = newMovementSpeed * angerMultiplier;
        isChasing = true;
    }

    //Continues moving towards target. Function is used to change movement speed without toggling a bool and setting a new target.
    public void ContinueMovement(float newMovementSpeed)
    {
        agent.speed = newMovementSpeed * angerMultiplier;
    }

    //Starts a reversing movement (moving away from the current target)
    public void StartReverseMovement(float newMovementSpeed)
    {
        agent.speed = newMovementSpeed * angerMultiplier;
        isReversing = true;
    }

    //Continues reversing. Function is used to change movement speed without toggling a bool and setting a new target.
    public void ContinueReverseMovement(float newMovementSpeed)
    {
        agent.speed = newMovementSpeed * angerMultiplier;
    }

    /// <summary>
    /// Strictly used to make the boss just move forward. 
    /// </summary>
    /// <param name="newMovementSpeed"></param>
    public void GoForward(float newMovementSpeed)
    {
        currentTarget = forwardsTarget;
        agent.speed = newMovementSpeed * angerMultiplier;
    }

    /// <summary>
    /// This function is used to, well, shoot a bullet. Yeah that's pretty much it.
    /// </summary>
    public void ShootBullet()
    {
        Instantiate(bullet, bulletRightEye.position, Quaternion.identity);
        Instantiate(bullet, bulletLeftEye.position, Quaternion.identity);
    }

    /// <summary>
    /// This function is used to summon an enemy to assist the boss.
    /// This doesn't summon the enemy itself, but rather a "transporter" of sorts that then selects what type of enemy to spawn and where.
    /// </summary>
    public void SummonHelper()
    {
        helperSpawner.SpawnEnemyPod(0);
    }
    /// <summary>
    /// Same as previous function, but this one instead summons a rock. This is due to the parameter being 1 instead of 0;
    /// </summary>
    public void SummonRock()
    {
        if (inFinalPhase) { helperSpawner.SpawnEnemyPod(2); attackCount = 0; }
        else { helperSpawner.SpawnEnemyPod(1); anim.SetBool("canSlam", false); }
    }
    /// <summary>
    /// Next two functions are used to turn on and off a given particle animation.
    /// Particle IDs:
    /// 0 - HeadHurtParticle
    /// 1 - ChargeParticle
    /// 2 - ScizClampParticle
    /// 3 - LeftSwipeParticle
    /// 4 - RightSwipeParticle
    /// 5 - ScizClampParticle_FP
    /// 6 - LeftSwipeParticle_FP
    /// 7 - RightSwipeParticle_FP
    /// </summary>
    public void ParticleOn(int particleID)
    {
        particles[particleID].Play();
    }
    public void ParticleOff(int particleID)
    {
        particles[particleID].Stop();
    }
    /// <summary>
    /// Used during spin animations because its annoying to put two animation events and assign the variables every time in inspector.
    /// </summary>
    public void SpinParticlesOn()
    {
        particles[4].Play();
        particles[6].Play();
    }
    public void SpinParticlesOff()
    {
        particles[4].Stop();
        particles[6].Stop();
    }

    /// <summary>
    /// Activated by child script of shield collider. Tells Boss to enter "confused" state. This allows the player to attack the boss.
    /// </summary>
    public void PlayerCollidedWithShield()
    {
        Debug.Log("BossConfused");
        anim.SetTrigger("Confused");
        isFacing = false;
        agent.speed = 0;
    }

    /// <summary>
    /// This function is used at the end of reaction attack animations and its purpose is to keep the boss from picking the same reaction attack twice in a row.
    /// </summary>
    public void SetPreviousReactionState()
    {
        prevReactState = attackState;
    }

    public void ResetVariables()
    {
        anim.ResetTrigger("Confused");
        anim.ResetTrigger("Hurt");
        anim.ResetTrigger("inRange");
        //VVV Resets the attack count back two if it is over. This is to insure that the player
        //    doesn't get the shield attack over and over again if they have already interacted with it.
        if (attackCount > 2)
        {
            attackCount = 2;
        }
    }

    /// <summary>
    /// Continues moving boss in the direction it is currently going. Ignores player position.
    /// </summary>
    public void CarryOn()
    {
        agent.updateRotation = true;
        isFacing = false;
        isChasing = false;
        currentTarget = forwardsTarget;
    }

    public void Clamp()
    {
        anim.SetTrigger("inRange");
        //maybe expand this function to be a general "inRange" trigger setter.
        //Maybe some anims trigger this "inRange" trigger and some activate the "CarryOn" function.
    }

    /// <summary>
    /// Called during the "Spin_Stop" animation in the boss' final phase.
    /// Used to decide whether the boss should spin again or spawn the rocks from the floor.
    /// </summary>
    public void SpinDecide()
    {
        attackCount += 1;
        int nextMove = Random.Range(1, 4); //spin again: (1, 2) summon rocks: (3)
        if (attackCount <= 1)
        {
            anim.SetTrigger("FP_restartSpin");
            return;
        }
        if (nextMove <= 2 && attackCount < 4)
        {
            anim.SetTrigger("FP_restartSpin");
            return;
        }
        if (nextMove == 3 && attackCount < 4)
        {
            anim.SetTrigger("FP_summonRocks");
            return;
        }
        if (attackCount >= 4)
        {
            anim.SetTrigger("FP_summonRocks");
            return;
        }
    }
    public void SpawnDirtBurst(int where)
    {
        switch (where)
        {
            case 0:
                Instantiate(burst, GameObject.Find("ScizLeftBone").transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(burst, GameObject.Find("ScizLeftBone_FP").transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("I'm actually going to shit myself");
                break;
        }
    }
    /// <summary>
    /// Simple function called during final phase introduction animation.
    /// Used to set inFinalPhase to true and the attackCount to 0;
    /// </summary>
    public void EnterFinalPhase()
    {
        inFinalPhase = true;
        attackCount = 0;
    }
    /// <summary>
    /// Called by an outside script in the event that the boss collides with a pillar during its final phase.
    /// </summary>
    public void BossStunned()
    {
        agent.speed = 0;
        anim.SetTrigger("hitPillar");
        isFacing = false;
        isStunned = true;
        Debug.Log("Boss Stunned!");
    }
    public void BossDamaged()
    {
        Debug.Log("BOSS DAMAGED");
        reactionAttack = true;
        HP -= 1;
        anim.SetInteger("HP", HP);
        anim.SetTrigger("Hurt");
        ParticleOn(0);
        attackCount = 0;
        agent.speed = 0;
        anim.SetBool("canSlam", true);
        switch (HP)
        {
            //Speeds up the boss' animations and movement speed at low HP!
            case 0:
                Debug.Log("Boss health is at zero!");
                break;
            case 1:
                angerMultiplier = 1.5f;
                Debug.Log("Boss health is at " + HP + "!");
                break;
            case 2:
                angerMultiplier = 1.5f;
                Debug.Log("Boss health is at " + HP + "! Final phase activated!");
                break;
            case 3:
                angerMultiplier = 1.4f;
                Debug.Log("Boss health is at " + HP + "!");
                break;
            case 4:
                angerMultiplier = 1.3f;
                Debug.Log("Boss health is at " + HP + "! Switching Confused for Confused2!");
                break;
            case 5:
                angerMultiplier = 1.2f;
                Debug.Log("Boss health is at " + HP + "!");
                break;
            case 6:
                angerMultiplier = 1.1f;
                Debug.Log("Boss health is at " + HP + "! Switching RushAttack for quicker version!");
                break;
            case 7:
                angerMultiplier = 1f;
                Debug.Log("Boss health is at " + HP + "!");
                break;
            default:
                Debug.Log("<color=red>Error: </color>Boss HP outside of knowable range! Current Boss HP: " + HP);
                break;
        }
        anim.SetFloat("angerMultiplier", angerMultiplier);
        //^^^ Sets the float in the animator to the float in this script. Better than calling it every frame in Update.
    }

    // Point towards the player
    public void FaceTarget()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Point towards current target
    public void FaceCurrentTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }
}