using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;		//it will show up on the panel and we can set it over there directly
	private int cnt;
	private float t;
	private float ellapsed;
	public Text cntText;
	public Text winText;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		cnt = 0;
		ellapsed = Time.deltaTime;
		t= Time.time;
		SetCntText();
		winText.text = "";
	}
	
	void FixedUpdate ()
	{
		//input left/right up/down buttons
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//float moveVertical = 1;
		//float moveHorizontal = 0;

		ellapsed= Time.time - t;

		if (Input.GetKey("escape"))
			Application.Quit();

		//rb.velocity = Vector3.zero;
		//rb.angularVelocity = Vector3.zero; 
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.velocity = movement * speed;
		SetCntText ();
		//rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive(false);
			cnt = cnt + 1;
			SetCntText();
		}
	}

	void SetCntText(){
		cntText.text = "Count: " + cnt.ToString() + "\ntime:" + ellapsed + " s";
		if (cnt == 137) {
			StartCoroutine(Finish());
		}
	}

	IEnumerator Finish() {
		winText.text = "YOU WON!!!!!!!!";
		yield return new WaitForSeconds(5);
		Application.Quit();
	}

}

