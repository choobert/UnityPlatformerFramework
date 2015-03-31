using UnityEngine;

public enum GameState {NullState, MainMenu, PauseMenu, Game}
public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour {
	
	private static GameManager _instance;
	private static HUD _hud;
	
	public event OnStateChangeHandler OnStateChange;
	public GameState gameState {get; private set;}
	
	
	/*
	* Find/Create/Return our one and only Game Manager object
	* for the game
	**/
	public static GameManager Instance {
		get {
			// if we do not have an instance already, lets look to see
			// if one has already been created for us
			if (_instance == null) {
				_instance = Object.FindObjectOfType<GameManager>();
			}
			
			// If we still dont have an instance, one must not be created
			// so lets create our own and prevent it from being deleted
			// when the level changes
			if (_instance == null)
			{
				GameObject go = new GameObject("_GameManager");
				DontDestroyOnLoad(go);
				_instance = go.AddComponent<GameManager>();
				

				_hud = HUD.Instance;

			}
			
			return _instance;
		}
	}

	/*
	* Update the Game State and call any Game State Change Handlers that have
	* been added
	**/
	public void SetGameState(GameState aGameState) {
		gameState = aGameState;
		
		if(OnStateChange != null) {
			OnStateChange();
		}
	}
}
