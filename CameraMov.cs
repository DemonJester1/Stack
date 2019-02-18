using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMov : MonoBehaviour {

	public Text score;
	public Cubespawn CS;
	int scorer;

	// Use this for initialization
	void Start () {
		scorer = CS.score;

	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
			transform.position = Vector3.Lerp (transform.position,new Vector3(transform.position.x,transform.transform.position.y+0.5f,transform.position.z),100f*Time.deltaTime);
		}
		score.text = ""+scorer ;
		scorer = CS.score;
	}

	public void Pause(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
		}else if (Time.timeScale == 0){
			Time.timeScale = 1;
		}
	}
}
