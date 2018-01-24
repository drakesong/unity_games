using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;	
	
	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			GameObject.DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}		

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
