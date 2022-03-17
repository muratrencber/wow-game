using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

	public float movementSpeed = 10f;
	[SerializeField] float _initialJumpForce, _holdJumpForce, _holdTime;

	public Transform cam;
	public groundchecker gc;
	private Animator anim;

	public static float highestY;
	

	Rigidbody2D rb;

	float movement = 0f;
	bool jumping = true;
	float jumpTimer;

	
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Time.timeScale = 1;
		anim = GetComponent<Animator>();
		highestY = transform.position.y;
	}
	
	
	void Update () 
    {
		if(transform.position.y > highestY)
			highestY = transform.position.y;

        if(Input.GetKeyDown(KeyCode.Space) && gc.grounded)
		{
			jumpTimer = 0;
			rb.AddForce(Vector2.up * _initialJumpForce);
            jumping = true;
			gc.grounded=false;
			anim.SetTrigger("jump");
        }
		else if(Input.GetKey(KeyCode.Space) && jumping)
		{
			jumpTimer += Time.deltaTime;
			if(jumpTimer > _holdTime)
				jumping = false;
			else
				rb.AddForce(Vector2.up * _holdJumpForce * Time.deltaTime);
		}
		else
			jumping = false;
		if(gc.grounded)
			jumping = false;
		movement = Input.GetAxis("Horizontal") * movementSpeed;
		
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

		if(transform.position.y < 200)
		{
            GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            List<GameObject> checkpointsList = checkpoints.ToList();
            checkpointsList.Sort((a,b) => b.transform.position.y.CompareTo(a.transform.position.y));
            foreach(GameObject checkPoint in checkpointsList)
            {
                if(checkPoint.transform.position.y < Player.highestY)
                {
					Vector2 velo = rb.velocity;
					velo.y = 0;
					rb.velocity = velo;
					Vector3 pos = checkPoint.transform.position;
					pos.z = transform.position.z;
                    transform.position = pos;
                    return;
                }
            }
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


