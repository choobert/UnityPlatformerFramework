using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int duration = 1;

	private void Start () {
		StartCoroutine (DestroyAfterSeconds (duration));
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag != GameManager.PLAYER_TAG)
		Destroy (this.gameObject);
	}

	private IEnumerator DestroyAfterSeconds(int sec) {
		yield return new WaitForSeconds (sec);
		Destroy (this.gameObject);
	}	
}
