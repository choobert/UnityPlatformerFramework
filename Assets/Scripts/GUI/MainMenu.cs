using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private static MainMenu _instance;
	private static GameManager _gm;

	private static GameObject mainMenuPanel;
	private static Button playButton;
	private static Button settingsButton;

	
	public static MainMenu Instance {
		get {
			// if we do not have an instance already, lets look to see
			// if one has already been created for us
			if (_instance == null) {
				_instance = Object.FindObjectOfType<MainMenu>();
			}
			
			// If we still dont have an instance, one must not be created
			// so lets create our own and prevent it from being deleted
			// when the level changes
			if (_instance == null)
			{
				_gm = GameManager.Instance;
				_gm.OnStateChange += HandleOnStateChange;
				
				// Lets find our canvas that we must add ourselves to
				GameObject guiCanvas = GameObject.Find ("_GUI");
				
				mainMenuPanel = (GameObject) Instantiate(Resources.Load ("GUI/MainMenu"), new Vector3(), Quaternion.identity);
				mainMenuPanel.transform.SetParent(guiCanvas.transform);
				mainMenuPanel.transform.position = new Vector3(0,0,0);
				mainMenuPanel.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				
				// Lets hook up the buttons!
				playButton = GameObject.Find ("MainMenu_PlayButton").GetComponent<Button>();
				playButton.onClick.AddListener(() => OnPlayButtonPress());
				
				settingsButton = GameObject.Find ("MainMenu_SettingsButton").GetComponent<Button>();
				settingsButton.onClick.AddListener(() => OnSettingsButtonPress());
			}			
			return _instance;
		}
	}
	
	private static void HandleOnStateChange ()
	{
		Debug.Log ("Game State changing to: " + _gm.gameState);
		if (_gm.gameState == GameState.MainMenu) {
			mainMenuPanel.SetActive(true);
		}
		else {
			mainMenuPanel.SetActive(false);
		}
	}
	
	private static void OnPlayButtonPress() {
		Debug.Log("MainMenu:OnPlayButtonPress = Setting Game State: " + GameState.Game);
		_gm.SetGameState(GameState.Game);
	}
	
	private static void OnSettingsButtonPress() {
		Debug.Log("Pressed Settings");
		Settings.displaySettings(true);
	}
}
