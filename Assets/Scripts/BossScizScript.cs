using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScizScript : MonoBehaviour
{
	public float radius;
	public Transform gumdrop;
	public BossSnipper bossScript;

    // Update is called once per frame
    void Update()
    {
		// Get the distance to the player
		float distance = Vector3.Distance(gumdrop.position, transform.position);

		// If inside the radius
		if (distance <= radius)
		{
			if (bossScript.attackState == 2)
			{
				bossScript.CarryOn();
			}
			if (bossScript.attackState == 3 || bossScript.attackState == 4)
            {
				bossScript.Clamp();
            }
		}
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
