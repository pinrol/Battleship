using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class StateManager : MonoBehaviour {


	private IStateBase activeState;
	 
	// Use this for initialization
	void Start ()
	{
		activeState = new ConstruktionState (this);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (activeState != null){
			activeState.StateUpdate ();
		}
		if (Input.GetKeyDown("s")) {
			Debug.Log("input s");
			xmlManager.SaveGame("savegame.xml");
		}
        if (Input.GetKeyDown("l"))
        {
            Debug.Log("input l");
            xmlManager.LoadGame("savegame.xml");
        }
        Debug.Log (" in statemanager update");
	}
	
	public void SwitchState(IStateBase newState) {
		activeState = newState;
	}
}
