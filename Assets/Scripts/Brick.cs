using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;
	public Sprite[] hitSprites;
	public AudioClip crack;
	public GameObject smoke;

	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;

	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		//Keep track of breakable bricks
		if(isBreakable) {
			breakableCount++;
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(isBreakable) {
			AudioSource.PlayClipAtPoint(crack, transform.position, 2f);
			HandleHits();
		}
	}

	void HandleHits() {
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if(timesHit >= maxHits) {
			breakableCount--;
			levelManager.BrickDestroyed();
			emitSmoke();
			Destroy(gameObject);
		}
		else {
			LoadSprites();
		}
	}

	void emitSmoke() {
		GameObject smokeObj = Instantiate(smoke, gameObject.transform.position, Quaternion.identity);
		smokeObj.GetComponent<ParticleSystem>().startColor = this.gameObject.GetComponent<SpriteRenderer>().color;
	}

	void LoadSprites() {
		int spriteIndex = timesHit - 1;
		if(hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else {
			Debug.LogError("Brick sprite missing");
		}
	}

	void SimulateWin() {
		levelManager.LoadNextLevel();
	}
}
