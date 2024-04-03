using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingScript : MonoBehaviour
{
	Transform Player;
	private CharacterController _controller; 

	private void Start()
    {
		_controller = FindObjectOfType<CharacterController>();
		Player = PlayerManager.instance.player.transform;
	}
	void FixedUpdate()
	{
		//transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
	}

	public void OnCollisionStay(Collision collision)
	{
		collision.transform.SetParent(transform);
	}

	public void OnCollisionExit(Collision collision)
	{
		collision.transform.SetParent(null);

	}
}
