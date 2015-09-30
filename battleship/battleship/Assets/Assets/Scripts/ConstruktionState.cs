using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
	public class ConstruktionState : IStateBase
	{


		//build boards
		public List<Vector2> validTilesPc = new List<Vector2>();
		private StateManager manager;
		private Board board;
		int shipCount = 10;
		int placedShips;
		GameObject[] gameObjectList;
		const int boardSize = 9;
		Vector2 endTile;
		Vector2 startTile;


		
		public ConstruktionState (StateManager manager)
		{
			this.manager = manager;
			GameObject tempObject = GameObject.Find ("Boards/Board1");
			tempObject.GetComponent<Board> ().setup ();

			GameObject tempObject2 = GameObject.Find("Boards/Board2");
			tempObject2.GetComponent<Board> ().setup ();
			Debug.Log ("Constructing ConstruktionState");



			gameObjectList = GameObject.FindGameObjectsWithTag ("Ship");
			//		should shiplength = 10 : usererror;
			//shipCount = gameObjectList.Length;*/
			

			 ComputerShips ();

			foreach(GameObject ship in gameObjectList){

				ship.gameObject.GetComponent<PlaceShips>().allowPlacement = true;
			}

		}


		private void  ComputerShips(){

			validTilesPc = GameObject.Find ("Boards/Board2").GetComponent<Board> ().tilesP2;

			GameObject[] PcDestroyer = GameObject.FindGameObjectsWithTag ("PcDestroyer");
			GameObject[] PcCruiser = GameObject.FindGameObjectsWithTag ("PcCruiser");
			GameObject[] PcBattleship = GameObject.FindGameObjectsWithTag ("PcBattleship");
			//GameObject tempObject = GameObject.Find(mouseObject.transform.position.x + " , " + mouseObject.transform.position.y);
			///*if(this.name == "PcSubmarine"){
			//	endTile = this.transform.position;
			//}else{
			//	endTile = GameObject.Find(this.name+"/end").transform.position;
			//}*/

			GameObject[] PcSubmarine = GameObject.FindGameObjectsWithTag ("PcSubmarine");

			int listNumber;
			Vector2 randomisedTile;
			GameObject tempObject;
			//int shipSize ;
			//PcSubmarine[0].transform.position = GameObject.Find( 15 + " , " + 0).transform.position);

			/*foreach ( GameObject ship in PcBattleship){
				startTile = GameObject.Find(ship.name+"/start");
			}*/


			List<Vector2> variableValidTiles = new List<Vector2> ();
			int RandomIsHoriz;
			Vector2 border = new Vector2 (0, 0);

			bool isActive;


			foreach (GameObject ship in PcCruiser) {

				int shipSize = ship.GetComponent<PlaceShips> ().shipSize;


				RandomIsHoriz = (int)Mathf.Round (Random.Range (0, 2));

			

				if (RandomIsHoriz == 0) { // vertikal
					ship.GetComponent<PlaceShips> ().isHoriz = false;
					border = new Vector2 (0, shipSize - 1);
					var rotation = Quaternion.Euler(0,0,90);
					ship.gameObject.transform.rotation = Quaternion.Slerp(ship.gameObject.transform.rotation,rotation, Time.deltaTime * 90);
				} else { // horiz
					border = new Vector2 (shipSize - 1, 0);
				}




				Debug.Log ("for (int x = 15; x < " + boardSize + " + 15 - " + border.x + "; x++)");
				int boundX = boardSize + 15 - (int)border.x;
				int boundY = boardSize - (int)border.y;
				for (int x = 15; x <= boundX; x++) { // loopar x men drar ifrån "border" om skeppet är horisontellt
					for (int y = 0; y <= boundY; y++) { // loopar y men drar ifrån "border" om skeppet är vertikalt

						if(ship.GetComponent<PlaceShips>().isHoriz){
							bool isValidPlacement = true;
							for (int index = 0; index < ship.GetComponent<PlaceShips>().shipSize; index++) {
								int tempX = x + index;

								isActive = GameObject.Find ("Boards/Board2/" + tempX + " , " + y).GetComponent<Tile> ().active;

								if (!isActive) {
									isValidPlacement = false;
								} 							
							}
							if (isValidPlacement) {
								variableValidTiles.Add (new Vector2 (x, y));
							}
						} else {
							bool isValidPlacement = true;
							for (int index = 0; index < ship.GetComponent<PlaceShips>().shipSize; index++) {

								int tempY = y + index;

								isActive = GameObject.Find ("Boards/Board2/" + x + " , " + tempY).GetComponent<Tile> ().active;

								if (!isActive) {
									isValidPlacement = false;
								} 
							}
							if (isValidPlacement) {
								variableValidTiles.Add (new Vector2 (x, y));
							}
						}
					}
				}
			

				listNumber = Random.Range (0, variableValidTiles.Count);

				randomisedTile = variableValidTiles [listNumber];


				tempObject = GameObject.Find (variableValidTiles [listNumber].x + " , " + variableValidTiles [listNumber].y);
				ComputerShipPlacement (ship, tempObject, randomisedTile, listNumber);
				variableValidTiles.Clear ();
			}

		}
		

		
	 	private void ComputerShipPlacement( GameObject ship, GameObject tempObject, Vector2 randomisedTile, int listNumber){
			ship.gameObject.transform.position = tempObject.transform.position;


			if (ship.CompareTag ("PcSubmarine")) {
				endTile = ship.gameObject.transform.position;
				startTile = endTile;
			} else {
				endTile = ship.transform.Find("end").position;
				startTile = ship.transform.Find("start").position;
			}



			
			
			int loopXstart; 
			int loopYstart;
			int loopXend;
			int loopYend;
			bool isHoriz = ship.GetComponent<PlaceShips>().isHoriz;

			
			

			loopXstart = (int)Mathf.Max(15, randomisedTile.x - 1);
			loopYstart = (int)Mathf.Max(0, randomisedTile.y - 1);
			loopXend   = (int)Mathf.Min(boardSize + 15, endTile.x + 1);
			loopYend   = (int)Mathf.Min(boardSize, endTile.y + 1);
		
			
			
			for (int x = loopXstart; x <= loopXend; x++) {
				for (int y = loopYstart; y <= loopYend; y++) {
					 

					validTilesPc.RemoveAt(listNumber);

					tempObject = GameObject.Find(x + " , " + y);
					tempObject.GetComponent<Tile>().active = false;
					tempObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;



				}

			}
		}















		public void StateUpdate ()
		{
			Debug.Log (" in construktionstate update");

			
			
			

			foreach(GameObject ship in gameObjectList){
				//Debug.Log(ship.name);
				if(ship.gameObject.GetComponent<PlaceShips>().allowPlacement == false){
					placedShips++;
					//Debug.Log(placedShips + " this is placed ships");
					//Debug.Log(shipCount);
					if (shipCount == placedShips) {

						manager.SwitchState (new PlayState (manager));	
					}

					//Debug.Log("");
				}
				
			}
			placedShips = 0;
		}
		
		public void ShowIt ()
		{
		}


	}

}
