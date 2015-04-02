using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Collections;

public class HUD : MonoBehaviour {

	private static HUD _instance;	
	private static GameManager gm;
	
	private static Button pauseButton;
	private static Button settingsButton;
	private static Button quitButton;
	
	private static Sprite playSprite;
	private static Sprite pauseSprite;
	
	private static GameObject quitPanel;
	private static Button confirmQuitButton;
	private static Button cancelQuitButton;
	
	private static GameObject settingsPanel;
	
	
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
				
				// Now that we have created the Base object we need to add our HUD components to it
				GameObject mainCanvas = (GameObject) Instantiate(Resources.Load ("HUD/Canvas"), new Vector3(), Quaternion.identity);
				DontDestroyOnLoad(mainCanvas);
				
				// Set up our Event System (this would happen naturally by adding a Canvas through the UI)
				GameObject eventSystem = new GameObject("EventSystem");
				eventSystem.AddComponent<EventSystem>();
				eventSystem.AddComponent<StandaloneInputModule>();
				eventSystem.AddComponent<TouchInputModule>();
				
				pauseButton = GameObject.Find ("PauseButton").GetComponent<Button>();
				pauseButton.onClick.AddListener(() => onPauseButtonClick());
				
				playSprite = Resources.Load<Sprite>("HUD/Play");
				pauseSprite = Resources.Load<Sprite>("HUD/Pause");
				
				
				settingsButton = GameObject.Find ("SettingsButton").GetComponent<Button>();
				settingsButton.onClick.AddListener(() => onSettingsButtonClick());
				
				quitButton = GameObject.Find ("QuitButton").GetComponent<Button>();
				quitButton.onClick.AddListener(() => onQuitButtonClick());
				
				quitPanel = (GameObject) GameObject.Find("QuitPanel");
				confirmQuitButton = GameObject.Find("ConfirmQuit").GetComponent<Button>();
				confirmQuitButton.onClick.AddListener(() => onConfirmQuitButtonClick());
				
				cancelQuitButton = GameObject.Find("CancelQuit").GetComponent<Button>();
				cancelQuitButton.onClick.AddListener(() => onCancelQuitButtonClick());
				
				settingsPanel = (GameObject) GameObject.Find ("SettingsPanel");
			}
			
			return _instance;
		}
	}
	
	private static void onGameStateChange() {
		if(gm.gameState == GameState.PauseMenu) {
			pauseButton.image.sprite = playSprite;
			
			pauseButton.gameObject.SetActive(true);
			settingsButton.gameObject.SetActive(true);		
			quitButton.gameObject.SetActive(true);
			
			quitPanel.SetActive(false);
			settingsPanel.SetActive(false);
		}
		else if (gm.gameState == GameState.Game) {
			pauseButton.image.sprite = pauseSprite;
			
			pauseButton.gameObject.SetActive(true);
			settingsButton.gameObject.SetActive(false);
			quitButton.gameObject.SetActive(false);
			
			quitPanel.SetActive(false);
			settingsPanel.SetActive(false);
		}
		else {
			pauseButton.gameObject.SetActive(false);
			settingsButton.gameObject.SetActive(false);
			quitButton.gameObject.SetActive(false);
			
			quitPanel.SetActive(false);
			settingsPanel.SetActive(false);
		}		
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
		settingsPanel.SetActive(true);
	}
	
	private static void onQuitButtonClick() {
		Debug.Log ("Clicked Quit Button!");
		quitPanel.SetActive(true);
	}
	
	private static void onConfirmQuitButtonClick() {
		Debug.Log ("Confirmed Quit!");
	}
	
	private static void onCancelQuitButtonClick() {
		Debug.Log ("Canceled Quit!");
		quitPanel.SetActive(false);
	}
}
