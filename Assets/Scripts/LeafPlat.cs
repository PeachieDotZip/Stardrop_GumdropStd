using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafPlat : MonoBehaviour
{
	public Vector3 Rotation;

	void FixedUpdate()
	{
		transform.Rotate(Rotation * Time.deltaTime);

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