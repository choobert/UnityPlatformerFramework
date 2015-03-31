using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	GameManager gm;

	void Awake() {
		gm = GameManager.Instance;
		gm.OnStateChange += HandleOnStateChange;
		
		Debug.Log ("Level:Awake - Game State: " + gm.gameState);
	}

	void Start () {
		Debug.Log ("Level:Start - Game State: " + gm.gameState);
		gm.SetGameState(GameState.Game);
	}
	
	void HandleOnStateChange ()
	{
		Debug.Log ("Game State changing to: " + gm.gameState);
	}
}
