using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	
	public int movementSpeed = 10;
	public float jumpForce = 10.0f;
	
	private bool jump = false;
	private bool grounded = false;
	
	
	//This method is called when the character collides with a collider (could be a platform).
	void OnCollisionEnter2D(Collision2D hit)
	{
		grounded = true;
		print ("isground");
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if (grounded)
			{
				jump = true;
				grounded = false;
			}
		}
	}
	
	void FixedUpdate()
	{	
		float lHorizontal = Input.GetAxisRaw("Horizontal");
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(lHorizontal * movementSpeed, GetComponent<Rigidbody2D>().velocity.y );
		
		if(jump)
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			jump = false;
		}
	}
}
