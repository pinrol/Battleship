using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManger : MonoBehaviour {

    public List<Vector2> shotTiles = new List<Vector2>();
    public bool play = false;
    public bool playerTurn = true;

    void Update (){
        Debug.Log(shotTiles.Count);
    }


}
