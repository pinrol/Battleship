
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Assets.Scripts
{
	public class PlayState : IStateBase
	{
        [SerializeField]
        private GameObject gameManagerGO;
        public List<Vector2> firingTiles = new List<Vector2>();
        //Each player takes turn firing at the opponents board until one player runs out of ships

        //using switch to change whos turn it is.
        public GameManger gameManagerScript;
        








        private StateManager manager;
		
		public PlayState (StateManager manager)
		{
            GameObject gameManagerGO =  GameObject.Find("GameManager");
            gameManagerScript = gameManagerGO.GetComponent<GameManger>();
            gameManagerScript.playerTurn = true;
            
            gameManagerGO.GetComponent<GameManger>().play = (gameManagerGO.GetComponent<GameManger>().play = true);
			this.manager = manager;
            foreach (Vector2 tile in GameObject.Find("Boards/Board1").GetComponent<Board>().tilesP1) { 
                firingTiles.Add(tile);
            }
            Debug.Log ("Constructing PlayState");
            
		}
		
		public void StateUpdate ()
		{
            bool xbool = gameManagerScript.GetComponent<GameManger>().playerTurn;
            Debug.Log(xbool);
            
            if (!xbool) {
                gameManagerScript.GetComponent<GameManger>().playerTurn = true;

                int listNumber = Random.Range(0, firingTiles.Count);
                Debug.Log(firingTiles.Count);
                Debug.Log(firingTiles[listNumber]);
                GameObject shotTile = GameObject.Find(firingTiles[listNumber].x + " , " + firingTiles[listNumber].y);


                GameObject.Find(firingTiles[listNumber].x + " , " + firingTiles[listNumber].y).gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                
                
                GameObject.Find("GameManager").GetComponent<GameManger>().shotTiles.Add(shotTile.transform.position);

                //Vector2 shotTile = firingTiles(listNumber);

            }

            if (Input.GetKeyDown("s"))
            {
                Debug.Log("input s");
                xmlManager.SaveGame("savegame.xml");
            }
            if (Input.GetKeyDown("l"))
            {
                Debug.Log("input l");
                xmlManager.LoadGame("savegame.xml");
            }
                        			
		}
		

		public void ShowIt ()
		{
		}
	}
}