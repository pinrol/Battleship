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
		int boardSize;
		Vector2 endTile;


		
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
			
			
			ComputerShipPlacement ();
			Debug.Break ();
			foreach(GameObject ship in gameObjectList){
				Debug.Log(ship.name);
				Debug.Break();
				ship.gameObject.GetComponent<PlaceShips>().allowPlacement = true;
			}

		}

		public void ComputerShipPlacement(){
			validTilesPc = GameObject.Find ("Boards/Board2").GetComponent<Board> ().tilesP2;
			GameObject[] PcSubmarine = GameObject.FindGameObjectsWithTag ("PcSubmarine");

			//GameObject tempObject = GameObject.Find(mouseObject.transform.position.x + " , " + mouseObject.transform.position.y);
			///*if(this.name == "PcSubmarine"){
			//	endTile = this.transform.position;
			//}else{
			//	endTile = GameObject.Find(this.name+"/end").transform.position;
			//}*/

			int listNumber = Random.Range (0, validTilesPc.Count);
			Vector2 randomisedTile = validTilesPc [listNumber];
			GameObject tempObject = GameObject.Find (randomisedTile.x + " , " + randomisedTile.y);
			//PcSubmarine[0].transform.position = GameObject.Find( 15 + " , " + 0).transform.position);
			foreach (GameObject ship in PcSubmarine) {
				//ship.transform.position = GameObject.Find( (int)Random.Range(15f, 25f) + " , " + 0).transform.position;
				ship.gameObject.transform.position = tempObject.transform.position;
				endTile = ship.gameObject.transform.position;

				int loopXstart; 
				int loopYstart;
				int loopXend;
				int loopYend;
				bool isHoriz = ship.GetComponent<PlaceShips>().isHoriz;
				boardSize = GameObject.Find("Boards/Board2").GetComponent<Board>().size;


				if(isHoriz) {
					loopXstart = (int)Mathf.Max(0, randomisedTile.x - 1);
					loopYstart = (int)Mathf.Max(0, randomisedTile.y - 1);
					loopXend   = (int)Mathf.Min(boardSize - 1, endTile.x + 1);
					loopYend   = (int)Mathf.Min(boardSize - 1, endTile.y + 1);
				} else {
					loopXstart = (int)Mathf.Max(0, endTile.x - 1);
					loopYstart = (int)Mathf.Max(0, endTile.y - 1);
					loopXend   = (int)Mathf.Min(boardSize - 1, endTile.x/*start should be here*/ + 1);
					loopYend   = (int)Mathf.Min(boardSize - 1, endTile.y/*start should be here*/ + 1);
				}


				for (int x = loopXstart; x <= loopXend; x++) {
					for (int y = loopYstart; y <= loopYend; y++) {


						validTilesPc.RemoveAt(listNumber);
						tempObject = GameObject.Find(x + " , " + y);
						tempObject.GetComponent<Tile>().active = false;
						tempObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

						//remove this tile from list
					}
				}
			}
		}

	















		public void StateUpdate ()
		{
			Debug.Log (" in construktionstate update");
			Debug.Log ("enter the matrix");
			
			
			

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
