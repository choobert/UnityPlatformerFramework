using UnityEngine;
using System.Collections;

public class ShootWeapon : MonoBehaviour {

	public Rigidbody2D bullet;
	public float speed;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
			Shoot ();			
	}
	
	void Shoot() {
		Vector3 shootDirection = Input.mousePosition;
		shootDirection.z = 0.0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;
		
		Rigidbody2D bulletInstance = Instantiate(bullet, this.transform.position, Quaternion.identity) as Rigidbody2D;		
		bulletInstance.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
	}
}
