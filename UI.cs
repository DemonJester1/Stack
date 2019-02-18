using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public int highscore;
	public Text highscoretxt;

	// Use this for initialization
	void Start () {
		

		if (PlayerPrefs.HasKey ("highscore")) {
			highscore = PlayerPrefs.GetInt ("highscore");
		} else {
			PlayerPrefs.SetInt ("highscore", 0);
		}

		highscoretxt.text = "" + highscore;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pause(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
		}else if(Time.timeScale == 0){
			Time.timeScale = 1;
		}
	}

}
