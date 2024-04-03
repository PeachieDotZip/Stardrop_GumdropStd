using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class KiwiNPC : MonoBehaviour
{

	public float followRadius = 30f;

	public Transform Destination;
	NavMeshAgent agent;
	private Animator anim;
	private Rigidbody rb;
	public bool follower;
	public bool notfollower;
	public bool following;
	private DialogueInteraction Dinter;
	public Animator Destanim;
	public Transform OG_Dest;
	public float OG_navmeshspeed;
	public float OG_Destanimspeed;
	private bool Noticable;
	public GameObject Distraction;
	public bool exception = false;
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		rb = GetComponentInChildren<Rigidbody>();
		Dinter = GetComponent<DialogueInteraction>();
		if (exception == false)
		{
			OG_navmeshspeed = agent.speed;
			if (follower == true)
            {
				return;
            }
			OG_Destanimspeed = Destanim.speed;
			// Dest, navmeshspeed, and Destanimspeed are for restoring
			//the original destination object, navmeshagent speed, and the speed of the destination object's animation after a conversation
			//hence the "OG_"
		}
		if (exception == true)
        {
			Destanim = null;
			//fixes an UnassignedReferenceException error; kiwis who dont need a destanim at Start() will have their variable set to null
		}

		if (follower == true)
		{
			Destination = PlayerManager.instance.player.transform;
		}

	}
	//i sincerely apologize for how absolutely fucking ass this script is

	void FixedUpdate()
	{
		rb.MovePosition(transform.position * Time.deltaTime);
	}
	void Update()
	{
		if (notfollower == true)
		{
			if (rb.velocity.magnitude >= 0.21)
			{
				anim.SetBool("isWalking", true);
			}
			if (rb.velocity.magnitude < 0.21)
			{
				anim.SetBool("isWalking", false);
			}
		}

		// Get the distance to the player
		float distance = Vector3.Distance(Destination.position, transform.position);

		// If inside the radius
		if (distance <= followRadius && follower == true)
		{
			// Move towards the player
			anim.SetBool("isIdle", false);
			FaceDestination();
			anim.SetTrigger("Notice");
			StartCoroutine(UnNoticable(.15f));

			if (distance <= agent.stoppingDistance)
			{
				anim.SetBool("isIdle", true);
				anim.SetBool("isRunning", false);
				agent.speed = 0f;
				rb.velocity = new Vector3(0, 0, 0);
			}
			if (distance > agent.stoppingDistance)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("isRunning", true);
				agent.speed = OG_navmeshspeed;
			}
		}
		if (distance > followRadius && follower == true)
		{
			anim.ResetTrigger("Notice");
			Noticable = true;
			rb.velocity = new Vector3(0, 0, 0);
			anim.SetBool("isRunning", false);
			if (rb.velocity == new Vector3(0, 0, 0))
			{
				anim.SetBool("isIdle", true);
			}
		}

		if (following == true)
		{
			agent.SetDestination(Destination.position);
			FaceDestination();
			agent.speed = OG_navmeshspeed;
		}

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
		{
			agent.SetDestination(Destination.position);
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            agent.SetDestination(agent.transform.position);
		}

			anim.SetBool("Noticable", Noticable);


		//dialogue stuff VVV

		if (Dinter.midconvo == true)
		{
			anim.SetBool("isTalking", true);
			following = false;
			Destination = PlayerManager.instance.player.transform;
			FaceDestination();
			agent.speed = 0f;
			//^^^ keeps the kiwi from moving while talking
			if (Destanim != null)
            {
				Destanim.speed = 0;
				//^^^ Pauses current destination  animator, therefore making the kiwi and the destination not fall out of sync
			}
		}
		else
		{
			anim.SetBool("isTalking", false);

			if (notfollower == true)
			{
				following = true;
				StartCoroutine(followingresume(.1f));
			}
			if (notfollower == false & follower == false & following == false)
            {
				FaceDistraction();
				anim.SetBool("isIdle", true);
			}
		}

	}

		// Point towards the player
	void FaceDestination()
	{
		Vector3 direction = (Destination.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
	void FaceDistraction()
	{
		if (Distraction != null)
        {
			Vector3 direction = (Distraction.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}
	}

	public void Stop000()
    {
		rb.velocity = new Vector3(0, 0, 0);
	}
    private IEnumerator followingresume(float time)
    {
		yield return new WaitForSeconds(time);
		if (following == true)
		{
			Destination = OG_Dest;
			Destanim.speed = OG_Destanimspeed;
		}
	}
	private IEnumerator UnNoticable(float time)
	{
		yield return new WaitForSeconds(time);
		Noticable = false;
	}

		void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.gray;
		Gizmos.DrawWireSphere(transform.position, followRadius);
	}
}