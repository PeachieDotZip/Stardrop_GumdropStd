using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class ScoutController : MonoBehaviour
{

	public float lookRadius = 10f;

	Transform target;
	NavMeshAgent agent;
	private ScoutSnipper EyeScript;
	private Animator anim;
	private Rigidbody rb;

	void Start()
	{
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		EyeScript = GetComponentInChildren<ScoutSnipper>();
		anim = GetComponent<Animator>();
		rb = GetComponentInChildren<Rigidbody>();
	}

    void FixedUpdate()
    {
        rb.MovePosition(transform.position * Time.deltaTime);
	}
    void Update()
	{
        if (rb.velocity.magnitude >= 0.5)
        {
			anim.SetBool("moving", true);
        }
        if (rb.velocity.magnitude < 0.5)
		{
			anim.SetBool("moving", false);
		}
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the radius
		if (distance <= lookRadius)
		{
			// Move towards the player
			agent.SetDestination(target.position);
			if (distance <= agent.stoppingDistance)
			{
				FaceTarget();
			}
		}
	}

	// Point towards the player
	void FaceTarget()
	{
        EyeScript.enabled = true;
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.gray;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

	public void SSDead_Destroy()
	{
		Destroy(gameObject);
	}

}