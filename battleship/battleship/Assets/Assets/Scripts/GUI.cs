using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {



	//Vad denna kod ska göra:
	//new game, load game, save game, quit game.



	/*

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

	/*	int shipCount;
		int placedShips;
		GameObject[] gameObjectList;
		//placeships and related.






		private StateManager manager;
		
		public EditState (StateManager manager)
		{	
			this.manager = manager; 
			gameObjectList = GameObject.FindGameObjectsWithTag ("Ship");
			shipCount = gameObjectList.Length;


			

			foreach(GameObject ship in gameObjectList){
				ship.gameObject.GetComponent<PlaceShips>().allowPlacement = true;


			}

			///current
/// 
/// 
			Debug.Log ("Constructing EditState");
			//StateUpdate ();
			//manager.SwitchState (new PlayState (manager));
		}

		
		public void StateUpdate ()
		//{
			Debug.Log ("enter the matrix");



			/*
			foreach(GameObject ship in gameObjectList){
				Debug.Log(ship.name);
				if(ship.gameObject.GetComponent<PlaceShips>().allowPlacement == false){
					placedShips++;
					Debug.Log(placedShips + " this is placed ships");
					if (shipCount == placedShips) {

						manager.SwitchState (new PlayState (manager));	
					}

					//Debug.Log("");
				}
				
			}
		placedShips = 0;
	}*/
	//teachercode////////////////////////////////////////////
	/*
		private StateManager manager;
		private float triggerTime;
		
		public ConstruktionState (StateManager manager)
		{
			this.manager = manager;
			triggerTime = Time.time + 10.0f;
			Debug.Log ("Constructing BeginState");
		}
		
		public void AutoTransfer() 
		{
			manager.SwitchState (new EditState (manager));
		}
		
		public void StateUpdate ()
		{
			if (Input.GetKeyUp (KeyCode.Space) || Time.time > triggerTime)
				manager.SwitchState (new EditState (manager));
			
		}
		
		public void ShowIt ()
		{
		}*/
}
