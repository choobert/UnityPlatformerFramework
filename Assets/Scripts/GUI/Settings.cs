using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour {

	private static Settings _instance;	
	private static GameManager _gm;
	
	private static GameObject settingsPanel;
	private static Button confirmButton;
	private static Button cancelButton;
	
	/*
	* Find/Create/Return our one and only Pause Menu object
	* for the game
	**/
	public static Settings Instance {
		get {
			// if we do not have an instance already, lets look to see
			// if one has already been created for us
			if (_instance == null) {
				_instance = Object.FindObjectOfType<Settings>();
			}
			
			// If we still dont have an instance, one must not be created
			// so lets create our own and prevent it from being deleted
			// when the level changes
			if (_instance == null)
			{
				_gm = GameManager.Instance;
				_gm.OnStateChange += onStateChange;

				_instance = _gm.gameObject.AddComponent<Settings> ();
				
				// Lets find our canvas that we must add ourselves to
				GameObject guiCanvas = GameObject.Find ("_GUI");
				
				// Now that we have created the Base object we need to add our HUD components to it
				settingsPanel = (GameObject) Instantiate(Resources.Load ("GUI/SettingsPanel"), new Vector3(), Quaternion.identity);
				settingsPanel.transform.SetParent(guiCanvas.transform);
				settingsPanel.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				
				// Lets hook up the buttons!
				confirmButton = GameObject.Find ("ConfirmSettings").GetComponent<Button>();
				confirmButton.onClick.AddListener(() => onConfirmClick());
				
				cancelButton = GameObject.Find ("CancelSettings").GetComponent<Button>();
				cancelButton.onClick.AddListener(() => onCancelClick());
			}
			
			return _instance;
		}
	}
	
	public static void displaySettings(bool aEnable) {
		settingsPanel.SetActive( aEnable );
	}
	
	private static void onConfirmClick() {
		Debug.Log ("Confirmed Settings!");
		displaySettings(false);
	}
	
	private static void onCancelClick() {
		Debug.Log ("Canceled Settings!");
		displaySettings(false);
	}

	private static void onStateChange ()
	{
		Debug.Log ("Game State changing to: " + _gm.gameState);
	}
}
