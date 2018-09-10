using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMove : MonoBehaviour 
{
	[SerializeField]
	private float speed;
	
	// Update is called once per frame
	private void Update () 
	{
		transform.Translate(Vector2.right * speed * Time.deltaTime);
	}
}
