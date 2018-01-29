using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class StarDisplay : MonoBehaviour {

	private Text starText;
	private int stars = 0;
	
	// Use this for initialization
	void Start () {
		starText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void AddStars(int amount) {
		stars += amount;
		UpdateDisplay();
	}
	
	public void UseStars(int amount) {
		stars -= amount;
		UpdateDisplay();
	}
	
	private void UpdateDisplay() {
		starText.text = stars.ToString();
	}
}
