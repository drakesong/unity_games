using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount;
	public GameObject smoke;
	
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;
	

	
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if (isBreakable) {
			breakableCount++;
		}
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		timesHit = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		AudioSource.PlayClipAtPoint(crack, transform.position);
		if (isBreakable) {
			HandleHits();
		}
	}
	
	void HandleHits() {
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (maxHits <= timesHit) {
			breakableCount--;
			Destroy(gameObject);
			GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
			smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
			levelManager.BrickDestroyed();
		}
		else {
			LoadSprites();
		}
	}
	
	void LoadSprites() {
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
	
	void SimulateWin() {
		levelManager.LoadNextLevel();
	}
}
