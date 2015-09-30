using UnityEngine;
using System.Collections;

public class Tile : Board{

	public AudioClip soundFile;
	AudioSource audio;
	public bool start = false;
	public bool end = false;
    [SerializeField]
    private GameObject gameManager;
    


    void Start () {
        
        gameManager = GameObject.Find("GameManager");
        Debug.Log(gameManager.name);
        audio = GetComponent<AudioSource>();
		active = true;
	}

   
    void Update () {
        if (hidden){

        }

	}
	void OnMouseDown() {
		Debug.Log (this.tag);
		Debug.Log (position);


		if (hidden && gameManager.GetComponent<GameManger>().play == true && gameManager.GetComponent<GameManger>().playerTurn == true && shotAt == false){
            shotAt = true;
            if(this.tag != "ShipTile") {
                GameObject.Find(this.transform.position.x + " , " + this.transform.position.y).GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            gameManager.GetComponent<GameManger>().playerTurn = false;
			audio.PlayOneShot (soundFile, 1f);
			Debug.Log("entered hidden territory");
            GameObject.Find("GameManager").GetComponent<GameManger>().shotTiles.Add(this.transform.position);
		}
	}

}
