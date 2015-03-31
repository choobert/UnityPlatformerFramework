using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Collections;

public class HUD : MonoBehaviour {

	private static HUD _instance;	
	
	private static Button pauseButton;
	private static Button voilaButton;
	
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

				
				
				// Now that we have created the Base object we need to add our HUD components to it
				GameObject mainCanvas = (GameObject) Instantiate(Resources.Load ("Canvas"), new Vector3(), Quaternion.identity);
				
				// Set up our Event System (this would happen naturally by adding a Canvas through the UI)
				GameObject eventSystem = new GameObject("EventSystem");
				eventSystem.AddComponent<EventSystem>();
				eventSystem.AddComponent<StandaloneInputModule>();
				eventSystem.AddComponent<TouchInputModule>();
				
				pauseButton = GameObject.Find ("PauseButton").GetComponent<Button>();
				pauseButton.onClick.AddListener(() => onPauseButtonClick());
				
				voilaButton = GameObject.Find ("VoilaButton").GetComponent<Button>();
				voilaButton.onClick.AddListener(() => onVoilaButtonClick());
			}
			
			return _instance;
		}
	}

	private static void onPauseButtonClick() {
		Debug.Log ("Pressed Pause Button!");
		voilaButton.gameObject.SetActive(!voilaButton.gameObject.activeSelf);		
	}
	
	private static void onVoilaButtonClick() {
		Debug.Log ("Pressed Voila Button!");
	}
}
