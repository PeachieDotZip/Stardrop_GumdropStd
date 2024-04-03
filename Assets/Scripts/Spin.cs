using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
	public Material brown;
	public SkinnedMeshRenderer gumdrop;
	public PlayerCounter pc;
	public bool twenty1;
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
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && twenty1 == true)
		{
			gumdrop.material = brown;
			Debug.Log("21");
			pc.winText.text = "";
		}
	}
	private void OnTriggerExit(Collider other)
	{
		pc.winText.text = "";
	}
	private void OnTriggerStay(Collider other)
	{
		pc.winText.text = "21";
	}
}