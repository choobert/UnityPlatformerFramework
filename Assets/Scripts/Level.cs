using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public GameState levelLoadGameState;
	private static GameManager _gm;

	void Awake() {
		_gm = GameManager.Instance;
		_gm.OnStateChange += HandleOnStateChange;
		
		Debug.Log ("Level:Awake - Game State: " + _gm.gameState);
	}

	void Start () {
		Debug.Log ("Level:Start - Setting Game State: " + levelLoadGameState);
		_gm.SetGameState(levelLoadGameState);
	}
	
	void HandleOnStateChange ()
	{
		Debug.Log ("Game State changing to: " + _gm.gameState);
	}
}
