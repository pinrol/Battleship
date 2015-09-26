using UnityEngine;
using System.Collections;

public class PlaceShips : MonoBehaviour {

	public bool allowPlacement;
	public bool attached = false;
	public GameObject mouseObject;
	public GameObject tempObject;
	int boardSize;
	public bool placed = false;
	public int endX;
	public int endY;
	public Vector2 endTile;
	public bool isHoriz;
	public int shipSize;
	public Vector2 homeCords;


	// Use this for initialization
	void Start () {
		allowPlacement = false;
		boardSize = GameObject.Find ("Board1").GetComponent<Board> ().size;
		isHoriz = true;
		homeCords = this.gameObject.transform.position;



	

	}
	// Update is called once per frame
	void Update () {
		if (attached) {

			//mouseObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseObject.transform.position = Vector2.Lerp(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f);
			if(Input.GetKeyDown(KeyCode.R)){

				var rotation = Quaternion.LookRotation(this.transform.position);

				if(isHoriz){
					rotation = Quaternion.Euler(0,0,-90);
					isHoriz = false;
				}else{
					rotation = Quaternion.Euler(0,0,0);
					isHoriz = true;
				}


				transform.rotation = Quaternion.Slerp(transform.rotation,rotation, Time.deltaTime * 90);

			}

		}

	} 
	void OnMouseDown() {
		if (allowPlacement) {

			mouseObject = this.gameObject;
			attached = true;


		}


	}
	void OnMouseUp() {
		if (attached) {
			/*GameObject[] gameObjectList;
			gameObjectList = GameObject.FindGameObjectsWithTag ("Ship");
			
			foreach(GameObject ship in gameObjectList){
				ship.gameObject.GetComponent<PlaceShips>().allowPlacement = true;
				//tempObj.GetComponents<PlaceShips>().allowPlacement = true;
			}*/

			attached = false;
			Vector2 tempV2 = mouseObject.transform.position;
			mouseObject.transform.position = new Vector2(Mathf.Round(tempV2.x),Mathf.Round(tempV2.y)); 
			GameObject tempObject = GameObject.Find(mouseObject.transform.position.x + " , " + mouseObject.transform.position.y);
			if(this.name == "Submarine"){
				endTile = this.transform.position;
			}else{
				endTile = GameObject.Find(this.name+"/end").transform.position;
			}



			/*endX = (int) GameObject.Find(this.name+"/end").transform.position.x;
			endY = (int) GameObject.Find(this.name+"/end").transform.position.y;*/

			if ( tempObject != null ){
				if (tempObject.GetComponent<Tile>().active == true){

					if (mouseObject.transform.position.x < boardSize && endTile.x < boardSize && mouseObject.transform.position.x >= 0
					    && mouseObject.transform.position.y < boardSize && endTile.y < boardSize && endTile.y >= 0 && mouseObject.transform.position.y >= 0){

						if(GameObject.Find("Boards/Board1/" + endTile.x + " , " + endTile.y).GetComponent<Tile>().active == true 
						   && GameObject.Find("Boards/Board1/" + endTile.x + " , " + endTile.y).GetComponent<Tile>().active == true){

							//GameObject.FindGameObjectWithTag("Manager").GetComponent<StateManager>().activeState.
							allowPlacement = false;



							tempObject.GetComponent<Tile>().active = false;

							int tempX = (int) tempObject.transform.position.x;

							int tempY = (int) tempObject.transform.position.y;

						


							int loopXstart;
							int loopYstart;
							int loopXend;
							int loopYend;
							if(isHoriz) {
								loopXstart = (int)Mathf.Max(0, tempX - 1);
								loopYstart = (int)Mathf.Max(0, tempY - 1);
								loopXend   = (int)Mathf.Min(boardSize - 1, endTile.x + 1);
								loopYend   = (int)Mathf.Min(boardSize - 1, endTile.y + 1);
							} else {
								loopXstart = (int)Mathf.Max(0, endTile.x - 1);
								loopYstart = (int)Mathf.Max(0, endTile.y - 1);
								loopXend   = (int)Mathf.Min(boardSize - 1, tempX + 1);
								loopYend   = (int)Mathf.Min(boardSize - 1, tempY + 1);
							}


							for (int x = loopXstart; x <= loopXend; x++) {
								for (int y = loopYstart; y <= loopYend; y++) {



									tempObject = GameObject.Find(x + " , " + y);
									tempObject.GetComponent<Tile>().active = false;
									tempObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

								}
							}

						}else{
							mouseObject.transform.position = homeCords;
						}
					}else{

						mouseObject.transform.position = homeCords;
					}
				}else{

					mouseObject.transform.position = homeCords;
				}
			}else{
				mouseObject.transform.position = homeCords;
			}

		


		}

	}

}
