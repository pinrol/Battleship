using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class EndState : IStateBase
	{

		//end game. restart, load game



		private StateManager manager;
		
		public EndState (StateManager manager)
		{
			this.manager = manager;
			Debug.Log ("Constructing EndState   " + manager);
		}
		
		public void StateUpdate ()
		{
			if (Input.GetKeyUp (KeyCode.Space))
				manager.SwitchState (new ConstruktionState (manager));
			
		}
		
		public void ShowIt ()
		{
		}
	}
}