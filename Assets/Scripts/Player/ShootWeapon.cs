using UnityEngine;
using System.Collections;

public enum WeaponType {Laser, Rocket, Melee}

public class ShootWeapon : MonoBehaviour {

	public Rigidbody2D bullet;
	public float speed;
	public WeaponType wep_type;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
			Shoot ();			
	}
	
	void Shoot() {
		switch (wep_type) {
			case WeaponType.Laser:
				ShootLaser();
				return;
			case WeaponType.Rocket:
				ShootRocket();
				return;
			case WeaponType.Melee:
				AttackMelee();
				return;
			default:
				Debug.LogError("Invalid weapon type");
				return;
		}
	}
	
	void ShootLaser() {
	
	}
	
	void ShootRocket() {
		Vector3 shootDirection = Input.mousePosition;
		shootDirection.z = 0.0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;
		
		Rigidbody2D bulletInstance = Instantiate(bullet, this.transform.position, Quaternion.identity) as Rigidbody2D;		
		
		int shootX = (int) shootDirection.x;
		
		bulletInstance.velocity = new Vector2(shootX * speed, shootDirection.y * speed);
		
		Debug.Log (bulletInstance.velocity.ToString());
	}
	
	void AttackMelee() {
	
	}
}
