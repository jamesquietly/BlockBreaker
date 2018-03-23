using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	public Paddle paddle;

	private Vector3 paddleToBallVector;
	private bool hasStarted = false;

	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasStarted) {
			this.transform.position = paddle.transform.position + paddleToBallVector;

			if(Input.GetMouseButtonDown(0)) {
				hasStarted = true;
				Text instructions = GameObject.FindObjectOfType<Text>();
				Destroy(instructions.gameObject);
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 13f);
		    }
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
		if (hasStarted) {
			this.GetComponent<AudioSource>().Play();
			this.gameObject.GetComponent<Rigidbody2D>().velocity += tweak;
		}
	}
}
