using System;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class xmlManager : MonoBehaviour 
{



	public static void SaveGame (string fileName) {

		Debug.Log ("save");

		//string[] shiplist={"submarine","destroyer","cruiser","battleship"};
		GameObject[] shipList = GameObject.FindGameObjectsWithTag ("Ship");


		List<GameObject[]> PcLists = new List<GameObject[]>();

		GameObject[] PcSubmarine = GameObject.FindGameObjectsWithTag ("PcSubmarine");
		GameObject[] PcDestroyer = GameObject.FindGameObjectsWithTag ("PcDestroyer");
		GameObject[] PcCruiser = GameObject.FindGameObjectsWithTag ("PcCruiser");
		GameObject[] PcBattleship = GameObject.FindGameObjectsWithTag ("PcBattleship");
		PcLists.Add (PcSubmarine);
		PcLists.Add (PcDestroyer);
		PcLists.Add (PcCruiser);
		PcLists.Add (PcBattleship);



		XDocument xdoc = new XDocument(new XElement("savegame"));
		xdoc.Element("savegame").Add(new XElement("players"));
		xdoc.Element ("savegame").Element ("players").Add (new XElement ("player",new XAttribute("id", "human"), new XAttribute("score",0)));
		xdoc.Element ("savegame").Element ("players").Add (new XElement ("player",new XAttribute("id", "computer"), new XAttribute("score",0)));

		xdoc.Element("savegame").Add(new XElement("boards"));

		XElement boardHuman = new XElement ("board", new XAttribute ("id", "human"));
		boardHuman.Add (new XElement ("ships"));
		boardHuman.Add (new XElement ("shots"));
		XElement boardComputer = new XElement ("board", new XAttribute ("id", "computer"));
		boardComputer.Add (new XElement ("ships"));
		boardComputer.Add (new XElement ("shots"));

		xdoc.Element ("savegame").Element ("boards").Add (boardHuman);
		xdoc.Element ("savegame").Element ("boards").Add (boardComputer);

		//human



		foreach (GameObject thisShip in shipList) {
			//OLIVER: loopa genom skeppen i unity
			string shipname = thisShip.name;
			int xpos = (int)thisShip.transform.position.x;
			int ypos = (int)thisShip.transform.position.y;
            int isHoriz = 0;
            if (thisShip.GetComponent<PlaceShips>().isHoriz){
                isHoriz = 1;    
            }
			
			boardHuman.Element("ships").Add (new XElement ("ship", new XAttribute("name",shipname), new XAttribute("x",xpos), new XAttribute("y",ypos), new XAttribute("isHoriz",isHoriz)));
		}
		//OLIVER: lägg ut koordinaterna från skjutna rutor
		for(int x=0; x < 3; x++) {
			for (int y=0; y < 3; y++) {
				boardHuman.Element("shots").Add(new XElement("shot", new XAttribute("x",x), new XAttribute("y",y)));
			}
		}
			
		foreach (GameObject[] PcList in PcLists) {
			foreach(GameObject Pcship in PcList){
				string shipname = Pcship.name;

                int xpos = (int)Pcship.transform.position.x;
                int ypos = (int)Pcship.transform.position.y;
                int isHoriz = 0;
                if (Pcship.GetComponent<PlaceShips>().isHoriz)
                {
                    isHoriz = 1;
                }


                boardComputer.Element("ships").Add (new XElement ("ship", new XAttribute("name",shipname), new XAttribute("x",xpos), new XAttribute("y",ypos), new XAttribute("isHoriz",isHoriz)));

			}
		}

		//computer
		foreach (GameObject shiptype in shipList) {

		}
		for(int x=0; x < 3; x++) {
			for (int y=0; y < 3; y++) {
				boardComputer.Element("shots").Add(new XElement("shot", new XAttribute("x",x), new XAttribute("y",y)));
			}
		}

		xdoc.Save (fileName);
	}

	public static void LoadGame (string fileName) {
        Debug.Log("Load Game");
		

		XDocument xdoc = XDocument.Load (fileName);
        
        foreach(XElement xboard in xdoc.Elements("savegame").Elements("boards").Elements("board")) {
            string board_id = xboard.Attribute("id").Value;
            foreach(XElement xship in xboard.Elements("ships").Elements("ship") ) {

                GameObject ship = GameObject.Find(xship.Attribute("name").Value);
                int x = Convert.ToInt32(xship.Attribute("x").Value);
                int y = Convert.ToInt32(xship.Attribute("y").Value);
                ship.gameObject.transform.position =new Vector3( x, y, 0f);

                if(xship.Attribute("isHoriz").Value == "0") {
                    var rotation = Quaternion.LookRotation(ship.transform.position);
                    rotation = Quaternion.Euler(0, 0, -90);
                    ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation, rotation, Time.deltaTime * 90);
                }


                Debug.Log("board_id=" + board_id + 
                        ", name=" + xship.Attribute("name").Value +
                        ", x=" + xship.Attribute("x").Value +
                        ", y=" + xship.Attribute("y").Value +
                        ", isHoriz=" + xship.Attribute("isHoriz").Value);
            }
        }
        
        
            // shots
            foreach (XElement childElem in xdoc.XPathSelectElements("//shot")) {
			int x = Int32.Parse (childElem.Attribute ("x").Value);
			int y = Int32.Parse (childElem.Attribute ("y").Value);
			Debug.Log("shot: x=" + x + ", y=" + y);
		}
	}

	
}

