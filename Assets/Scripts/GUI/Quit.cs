using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Quit : MonoBehaviour {

	private static Quit _instance;	
	private static GameManager _gm;
	
	private static GameObject quitPanel;
	private static Button confirmButton;
	private static Button cancelButton;

	/*
	* Find/Create/Return our one and only Quit Menu object
	* for the game
	**/
	public static Quit Instance {
		get {
			// if we do not have an instance already, lets look to see
			// if one has already been created for us
			if (_instance == null) {
				_instance = Object.FindObjectOfType<Quit>();
			}
			
			// If we still dont have an instance, one must not be created
			// so lets create our own and prevent it from being deleted
			// when the level changes
			if (_instance == null)
			{
				_gm = GameManager.Instance;
				_instance = _gm.gameObject.AddComponent<Quit> ();

				// Lets find our canvas that we must add ourselves to
				GameObject guiCanvas = GameObject.Find ("_GUI");
				
				// Now that we have created the Base object we need to add our HUD components to it
				quitPanel = (GameObject) Instantiate(Resources.Load ("GUI/QuitPanel"), new Vector3(), Quaternion.identity);
				quitPanel.transform.SetParent(guiCanvas.transform);
				quitPanel.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				
				// Lets hook up the buttons!
				confirmButton = GameObject.Find ("ConfirmQuit").GetComponent<Button>();
				confirmButton.onClick.AddListener(() => onConfirmClick());
				
				cancelButton = GameObject.Find ("CancelQuit").GetComponent<Button>();
				cancelButton.onClick.AddListener(() => onCancelClick());
			}
			
			return _instance;
		}
	}
	
	public static void displayQuit(bool aEnable) {
		quitPanel.SetActive( aEnable );
	}
	
	private static void onConfirmClick() {
		Debug.Log("Quit:onConfirmQuit = Setting Game State: " + GameState.MainMenu);
		_gm.SetGameState(GameState.MainMenu);
	}
	
	private static void onCancelClick() {
		Debug.Log ("Canceled Quit!");
		displayQuit(false);
	}
}
