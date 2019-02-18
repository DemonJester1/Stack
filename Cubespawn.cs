using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubespawn : MonoBehaviour {

	public bool Right;
	public GameObject Cube;
	public Vector3 Position;
	public Vector3 Sizel;
	public Vector3 LastPos;
	public Vector3 center;
	public Color StartCol;
	public float H;
	public float S;
	public float V;
	public Renderer rend;
	public GameObject start;
	public Renderer rend1;
	public int score;
	public GameObject PlayButton;
	public GameObject PauseButton;
	public GameObject GameOverTxt;
	public GameObject ScoreTxt;
	public Cubemovement cm;
	public bool gamestart;
	public UI ui;


	// Use this for initialization
	void Start () {
		gamestart = false;
		Right = true;
		Position = Cube.transform.position;
		Sizel = new Vector3 (5f,0.5f,5f);
		center = new Vector3 (0,0,0);
		S = Random.Range (0.5f,1f);
		V = Random.Range (0.5f,1f);
		H = 0.04f;
		StartCol = Color.HSVToRGB (H,V,S);
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = Color.HSVToRGB(H-0.04f,V,S);
		Renderer rend1 = start.GetComponent<Renderer> ();
		rend1.material.color = Color.HSVToRGB(H-0.02f,V,S);
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if(score > ui.highscore){
			PlayerPrefs.SetInt ("highscore",score);
		}

	}

	public IEnumerator CouRight(){
		yield return new WaitForSeconds (0.7f);
		Instantiate (Cube,new Vector3(center.x,Position.y,Position.z), transform.rotation);
		Position.y += 0.5f;
		Right = false;
	}
	public IEnumerator CouLeft(){
		yield return new WaitForSeconds (0.7f);
		Instantiate (Cube, new Vector3(Position.z,Position.y,center.z), transform.rotation);
		Position.y += 0.5f;
		Right = true;
	}
	public void StartButton(){
		StartCoroutine (CouRight ());
		PlayButton.SetActive (false);
		ScoreTxt.SetActive (true);
		ui.highscoretxt.gameObject.SetActive (false);
		gamestart = true;
	}
}
