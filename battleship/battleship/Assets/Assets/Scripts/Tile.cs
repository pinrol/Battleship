using UnityEngine;
using System.Collections;

public class Tile : Board{

	public AudioClip test;
	AudioSource audio;
	public bool start = false;
	public bool end = false;


	void Start () {
		audio = GetComponent<AudioSource>();
	}
	

	void Update () {
		if (!active) {

		}

	}
	void OnMouseDown() {
		Debug.Log (this.tag);
		Debug.Log (position);


		if (hidden){ // här ska den returnera värdena som den positionen har.
			audio.PlayOneShot (test, 1f);
			Debug.Log("entered hidden territory");
		}
	}

}
