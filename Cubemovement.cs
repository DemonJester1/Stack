using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cubemovement : MonoBehaviour {

	Vector3 Position;
	public float speed;
	public Cubespawn CS;
	int counter = 1;
	public Vector3 center;
	public Vector3 newCenter;
	public Vector3 size;
	public Vector3 newSize;
	public Renderer rend;
	Color StartCol;
	public int score;
	public bool gameover;

	private MeshRenderer myMesh;
	private BoxCollider myBox;

	public GameObject middle;
	public GameObject leftover;

	private Middle M;
	private Leftover L;

	public Vector3 lefPos;
	public Vector3 lefSize;

	// Use this for initialization
	void Start () {
		speed = 9f;
		Position = transform.position;
		CS = GameObject.Find ("Cube").GetComponent<Cubespawn>();
		transform.localScale = CS.Sizel;
		center = CS.center;
		size = CS.Sizel;
		Renderer rend = GetComponent<Renderer> ();
		StartCol = CS.StartCol;
		rend.material.color = StartCol;
		score = 0;
		gameover = false;
		L = leftover.GetComponent<Leftover> ();
		M = middle.GetComponent<Middle> ();
		myMesh = gameObject.GetComponent<MeshRenderer> ();
		myBox = gameObject.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.UpArrow)){
			Debug.Log (Mathf.Abs (transform.position.x - center.x) );}
		Vector3 PositionA = new Vector3 (Position.x, Position.y, -6f);
		Vector3 PositionB = new Vector3 (Position.x, Position.y, 6f);
		Vector3 PositionC = new Vector3 (-6f, Position.y,Position.z);
		Vector3 PositionD = new Vector3 (6f, Position.y,Position.z);


		transform.position = Vector3.MoveTowards (transform.position,Position,speed *Time.deltaTime);

		if (transform.position == PositionB) {
			Position = PositionA;
			}
		if (transform.position == PositionA) {
			Position = PositionB;
		}
		if (transform.position == PositionC) {
			Position = PositionD;
		}
		if (transform.position == PositionD) {
			Position = PositionC;
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && counter == 1 && !CS.Right) {
			speed = 0;
			counter = 0;
			if (Mathf.Abs (center.z - transform.position.z) > 0.05f) {
				newCenter = new Vector3 (transform.position.x, transform.position.y, (center.z + transform.position.z) / 2);
				newSize = new Vector3 (size.x, size.y, size.z - Mathf.Abs (center.z - transform.position.z));
				lefSize = new Vector3 (size.x,size.y,(size.z-newSize.z));
				if(newCenter.z < center.z){
					lefPos = new Vector3 (transform.position.x,transform.position.y,(newCenter.z-newSize.z/2-lefSize.z/2));
				}else if(newCenter.z > center.z){
					lefPos = new Vector3 (transform.position.x,transform.position.y,(-newCenter.z+newSize.z/2+lefSize.z/2));
				}
				M.position = newCenter;
				M.size = newSize;
				L.size = lefSize;
				L.position = lefPos;
				Instantiate (middle,transform.position,transform.rotation);
				Instantiate (leftover,transform.position,transform.rotation);
			} else {
				newCenter = new Vector3(center.x,transform.position.y,center.z);
				newSize = size;
				M.position = newCenter;
				M.size = newSize;
				Instantiate (middle,transform.position,transform.rotation);
			}
			if (newSize.z < 0) {
				newSize = new Vector3 (0, 0, 0);
				CS.GameOverTxt.SetActive (true);
				StartCoroutine (Cougameover ());

			} else {
				CS.score += 1;
			}
			transform.position = newCenter;
			transform.localScale = newSize;
			CS.center = transform.position;
			CS.Sizel = transform.localScale;
			CS.StartCol = Color.HSVToRGB (CS.H+0.02f,CS.V,CS.S);
			CS.H += 0.02f;
			StartCoroutine (CS.CouLeft());
			StartCoroutine (CouDestroy());
			myBox.enabled = !myBox.enabled;
			myMesh.enabled = !myMesh.enabled;
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && counter == 1 && CS.Right) {
			speed = 0;
			counter = 0;
			if(Mathf.Abs (center.x-transform.position.x) > 0.05f){
				newCenter = new Vector3 ((center.x + transform.position.x)/2 , transform.position.y, transform.position.z);
				newSize = new Vector3 (size.x - Mathf.Abs (center.x-transform.position.x) , size.y, size.z);
				lefSize = new Vector3 ((size.x-newSize.x),size.y,size.z);
				if(newCenter.x < center.x){
					lefPos = new Vector3 ((newCenter.x-newSize.x/2-lefSize.x/2),transform.position.y,transform.position.z);
				}else if(newCenter.x > center.x){
					lefPos = new Vector3 ((newCenter.x+newSize.x/2+lefSize.x/2),transform.position.y,transform.position.z);
				}
				M.position = newCenter;
				M.size = newSize;
				L.size = lefSize;
				L.position = lefPos;
				Instantiate (middle,transform.position,transform.rotation);
				Instantiate (leftover,transform.position,transform.rotation);
			}else{
				newCenter = new Vector3(center.x,transform.position.y,center.z);
				newSize = size;
				M.position = newCenter;
				M.size = newSize;
				Instantiate (middle,transform.position,transform.rotation);
			}
			if (newSize.x < 0) {
				newSize = new Vector3 (0, 0, 0);
				CS.GameOverTxt.SetActive (true);
				StartCoroutine (Cougameover ());
			} else {
				CS.score += 1;
			}
			transform.position = newCenter;
			transform.localScale = newSize;
			CS.center = transform.position;
			CS.Sizel = transform.localScale;
			CS.StartCol = Color.HSVToRGB (CS.H+0.02f,CS.V,CS.S);
			CS.H += 0.02f;
			StartCoroutine (CS.CouRight ());
			StartCoroutine (CouDestroy());
			myBox.enabled = !myBox.enabled;
			myMesh.enabled = !myMesh.enabled;
		}
	
	}

	IEnumerator Cougameover(){
		
		CS.enabled = !CS.enabled;
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene ("Slack");

	}

	IEnumerator CouDestroy(){

		yield return new WaitForSeconds (0.8f);
		Destroy (gameObject);
	}
}
