using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIController : MonoBehaviour {

	private Rigidbody rb;
	public float AIspeed;		//it will show up on the panel and we can set it over there directly
	public Text winText;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		float moveVertical = 0;
		float moveHorizontal = 1;
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.velocity = movement * AIspeed;
		StartCoroutine(WaitASec());
	}
	
	void FixedUpdate ()
	{
		if (((rb.velocity.x < 1 && rb.velocity.x > 0) || (rb.velocity.x > -1 && rb.velocity.x < 0) )
		    && ((rb.velocity.z < 1 && rb.velocity.z > 0) || (rb.velocity.z > -1 && rb.velocity.z < 0) ) ){
			randomMov();
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			StartCoroutine(Lost());
			col.gameObject.SetActive(false);
		}
		else if(col.gameObject.tag == "Wall" || col.gameObject.tag == "Player"){
			randomMov();
		}
	}

	void randomMov(){
		float val1 = Random.Range(0,3);
		float val2 = Random.Range(0,3);
		float moveVertical = 0;
		float moveHorizontal = 0;
		if(val1 == 0 && val2 == 0){
			moveVertical = -1;
			moveHorizontal = 0;
		}
		else if(val1 == 0 && val2 == 1){
			moveVertical = 1;
			moveHorizontal = 0;
		}
		else if(val1 == 1 && val2 == 0){
			moveVertical = 0;
			moveHorizontal = -1;
		}
		else if((val1 == 1 || val1 == 2)  && (val2 == 1 || val2 == 2)){
			moveVertical = 0;
			moveHorizontal = 1;
		}
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * AIspeed;
	}

	IEnumerator Lost() {
		winText.text = "YOU LOST!";
		yield return new WaitForSeconds(5);
		Application.Quit();
	}

	IEnumerator WaitASec() {
		yield return new WaitForSeconds(1);
	}

}

