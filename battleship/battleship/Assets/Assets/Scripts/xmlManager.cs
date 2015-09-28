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
			int isHoriz = (int)thisShip.GetComponent<PlaceShips>().isHoriz;
			boardHuman.Element("ships").Add (new XElement ("ship", new XAttribute("name",shipname), new XAttribute("x",xpos), new XAttribute("y",ypos), new XAttribute("ishoriz",0)));
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
				boardComputer.Element("ships").Add (new XElement ("ship", new XAttribute("name",shipname), new XAttribute("x",0), new XAttribute("y",0), new XAttribute("ishoriz",0)));

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
		string[] shiplist={"submarine","destroyer","cruiser","battleship"};

		XDocument xdoc = XDocument.Load (fileName);

		// scores and boards
		string[] actors = {"human","computer"};
		foreach (string user_id in actors) {
			int score = Int32.Parse (xdoc.XPathSelectElement ("//players/player[@id='" + user_id + "']").Attribute ("score").Value);
			Console.WriteLine ("id=" + user_id + ", score=" + score);
			//foreach (string shiptype in shiplist) {
				foreach (XElement childElem in xdoc.XPathSelectElements("//boards/board[@id='" + user_id + "']/ships/ship")) {
					string shipname = childElem.Attribute ("name").Value; 
					int x = Int32.Parse (childElem.Attribute ("x").Value);
					int y = Int32.Parse (childElem.Attribute ("y").Value);
					bool isHoriz = (Int32.Parse (childElem.Attribute ("y").Value) == 1);
					Console.WriteLine ("shipname=" + shipname + ", x=" + x + ", y=" + y + ", isHoriz=" + isHoriz);
				}
			//}
		}

		// shots
		foreach (XElement childElem in xdoc.XPathSelectElements("//shot")) {
			int x = Int32.Parse (childElem.Attribute ("x").Value);
			int y = Int32.Parse (childElem.Attribute ("y").Value);
			Console.WriteLine ("shot: x=" + x + ", y=" + y);
		}
	}

	
}

