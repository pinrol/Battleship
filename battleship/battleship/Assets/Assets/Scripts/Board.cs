
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {
	public GameObject tile;
	public int size;
	public int xpos;
	public int ypos;
	public bool hidden = false;
	public bool initiator = false;
	public GameObject parentTile;
	public int[] tileType;
	public List<Vector2> tilesP1 = new List<Vector2>();
	public List<Vector2> tilesP2 = new List<Vector2>();
	public Vector2 position;
	public bool active = true;
    public bool shotAt = false;
    
    
	public void setup(){

		int sizex = size + xpos;
		int sizey = size + ypos;
		


		for (int x = xpos; x < sizex; x++) {
			for (int y = ypos; y < size; y++) {
				GameObject tempTile = Instantiate (tile, new Vector2 (x, y), Quaternion.identity) as GameObject;
				if (!hidden){
					tilesP1.Add(new Vector2(x,y));
					parentTile = GameObject.Find("Board1");
					tempTile.transform.parent = parentTile.transform;
					tempTile.GetComponent<Tile>().position = new Vector2(x,y);
					tempTile.name = (tempTile.transform.position.x.ToString() + " , " + tempTile.transform.position.y.ToString());


				}else{
					tilesP2.Add(new Vector2(x,y));
					parentTile = GameObject.Find("Board2");
					tempTile.transform.parent = parentTile.transform;
					tempTile.GetComponent<Tile>().position = new Vector2(x,y);
					tempTile.name = (tempTile.transform.position.x.ToString() + " , " + tempTile.transform.position.y.ToString());
					tempTile.GetComponent<Tile>().hidden = true;
				}
			}
		}
	}			
}
