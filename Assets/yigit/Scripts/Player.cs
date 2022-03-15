using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

	public float movementSpeed = 10f;

    
		

	public Transform cam;
	public groundchecker gc;
	private Animator anim;
	

	Rigidbody2D rb;

	float movement = 0f;

	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Time.timeScale = 1;
		anim = GetComponent<Animator>();
		cam=Camera.main.transform;
	}
	
	
	void Update () 
    {
		        

        if(Input.GetKeyDown(KeyCode.Space) && gc.grounded){
            Vector2 velocity = rb.velocity;
				velocity.y = 9;
				rb.velocity = velocity;
				gc.grounded=false;
			anim.SetTrigger("jump");
        }
		movement = Input.GetAxis("Horizontal") * movementSpeed;
        {
            
        }
		{
			if(cam.position.y > transform.position.y + 7f){
				Time.timeScale = 0;
				
			}
		}
		
		if(Input.GetAxisRaw("Horizontal")==1){
			transform.eulerAngles=new Vector3(0,0,0);
		}
			if(Input.GetAxisRaw("Horizontal")==-1){
			transform.eulerAngles=new Vector3(0,180,0);
			
		}

		if(Mathf.Abs( Input.GetAxisRaw("Horizontal"))>0){
		anim.SetBool("running", true );

		}
		else{
		anim.SetBool("running", false );

		}
	}

	void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;

		/*if (this.transform.position.x > 4f)
        {
            transform.position = new Vector3(-4f, transform.position.y, transform.position.z);
        }
        if (this.transform.position.x < -4f)
        {
            transform.position = new Vector3(4f, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.A)  ||Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        if (Input.GetKeyDown(KeyCode.D)  ||Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
	}*/
	}

	
}


