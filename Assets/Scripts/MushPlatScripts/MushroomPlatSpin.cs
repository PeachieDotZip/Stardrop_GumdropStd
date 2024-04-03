using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPlatSpin : MonoBehaviour
{
	void FixedUpdate()
	{
		transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);


	}
	void OnCollisionEnter(Collision collision)
	{
		collision.transform.SetParent(transform);
	}

	void OnCollisionExit(Collision collision)
	{
		collision.transform.SetParent(null);

	}
}