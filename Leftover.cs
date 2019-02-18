using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leftover : MonoBehaviour {

	public Vector3 size;
	public Vector3 position;

	// Use this for initialization
	void Start () {
		transform.position = position;
		transform.localScale = size;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
