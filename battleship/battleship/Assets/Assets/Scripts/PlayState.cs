
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class PlayState : IStateBase
	{


		//Each player takes turn firing at the opponents board until one player runs out of ships

		//using switch to change whos turn it is.









		private StateManager manager;
		
		public PlayState (StateManager manager)
		{
			this.manager = manager;
			Debug.Log ("Constructing PlayState");
		}
		
		public void StateUpdate ()
		{
			if (Input.GetKeyUp (KeyCode.Space))
				manager.SwitchState (new EndState (manager));
			else if (Input.GetKeyUp (KeyCode.A))
				manager.SwitchState (new ConstruktionState (manager));
			
		}
		
		public void ShowIt ()
		{
		}
	}
}