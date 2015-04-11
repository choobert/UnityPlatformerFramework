using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Collections;

public class HUD : MonoBehaviour {

	private static HUD _instance;	
	private static GameManager gm = GameManager.Instance;
	
	private static GameObject hudPanel;
	private static Button pauseButton;
	private static Button settingsButton;
	private static Button quitButton;
	
	private static Sprite playSprite;
	private static Sprite pauseSprite;	
	
	/*
	* Find/Create/Return our one and only Pause Menu object
	* for the game
	**/
	public static HUD Instance {
		get {
			// if we do not have an instance already, lets look to see
			// if one has already been created for us
			if (_instance == null) {
				_instance = Object.FindObjectOfType<HUD>();
			}
			
			// If we still dont have an instance, one must not be created
			// so lets create our own and prevent it from being deleted
			// when the level changes
			if (_instance == null)
			{
				GameObject go 	= new GameObject("_HUD");
				_instance 		= go.AddComponent<HUD>();
				DontDestroyOnLoad(go);

				// Lets go ahead and grab a reference to the GameManager to shorted all the text each time
				gm = GameManager.Instance;
				gm.OnStateChange += onGameStateChange;
				
				// Lets find our canvas that we must add ourselves to
				GameObject guiCanvas = GameObject.Find ("_GUI");				
				
				// Now that we have created the Base object we need to add our HUD components to it
				hudPanel = (GameObject) Instantiate(Resources.Load ("GUI/HUD"), new Vector3(), Quaternion.identity);
				hudPanel.transform.SetParent(guiCanvas.transform);
				hudPanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
				DontDestroyOnLoad(hudPanel);

				pauseButton = GameObject.Find ("PauseButton").GetComponent<Button>();
				pauseButton.onClick.AddListener(() => onPauseButtonClick());
				
				playSprite = Resources.Load<Sprite>("GUI/Icons/Play");
				pauseSprite = Resources.Load<Sprite>("GUI/Icons/Pause");				
				
				settingsButton = GameObject.Find ("SettingsButton").GetComponent<Button>();
				settingsButton.onClick.AddListener(() => onSettingsButtonClick());
				
				quitButton = GameObject.Find ("QuitButton").GetComponent<Button>();
				quitButton.onClick.AddListener(() => onQuitButtonClick());
			}
			
			return _instance;
		}
	}
	
	private static void onGameStateChange() {
		if(gm.gameState == GameState.PauseMenu || gm.gameState == GameState.Game) {
			HUD.displayHUD(true);			
			Quit.displayQuit(false);
			Settings.displaySettings(false);
		}
		else {
			HUD.displayHUD(false);
			Settings.displaySettings(false);
			Quit.displayQuit(false);
		}
	}
	
	private static void displayHUD(bool aEnable) {
	
		if (gm.gameState == GameState.PauseMenu) {
			pauseButton.image.sprite = playSprite;
			
			settingsButton.gameObject.SetActive(true);		
			quitButton.gameObject.SetActive(true);
		}
		else if (gm.gameState == GameState.Game) {
			pauseButton.image.sprite = pauseSprite;
			
			settingsButton.gameObject.SetActive(false);
			quitButton.gameObject.SetActive(false);
		}
	
		hudPanel.SetActive( aEnable );
	}

	private static void onPauseButtonClick() {
		if(gm.gameState == GameState.PauseMenu) {
			gm.SetGameState(GameState.Game);	
		}
		else if (gm.gameState == GameState.Game) {
			gm.SetGameState(GameState.PauseMenu);				
		}		
	}
	
	private static void onSettingsButtonClick() {
		Debug.Log ("Pressed Settings Button!");
		Settings.displaySettings(true);
	}
	
	private static void onQuitButtonClick() {
		Debug.Log ("Clicked Quit Button!");
		Quit.displayQuit(true);
	}
}
