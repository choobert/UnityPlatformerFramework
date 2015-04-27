using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	private Animator anim;
	private bool isFading = false;

	void Awake () {
		anim = GetComponent<Animator>();	
	}
	
	public IEnumerator FadeToClear() {
		isFading = true;
		
		anim.SetTrigger("FadeIn");
		
		while (isFading) {
			yield return null;
		}
	}
	
	public IEnumerator FadeToDark() {
		isFading = true;
		
		anim.SetTrigger("FadeOut");
		
		while (isFading) {
			yield return null;
		}
	}
	
	void AnimationComplete() {
		isFading = false;
	}
}
