using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   //Variables

	//Player Variables
	public float maxHealth;
	public float health;

	public float movementSpeed;
	Animation anime;

	public float attackTimer;
	private float currentAttackTimer;

	private bool moving;
	private bool attacking;
	private bool followingEnemy;

	private float damage;
	public float minDamage;
	public float maxDamage;

	private bool attacked;

	//PMR Variables
	public GameObject playerMovePoints;
	private Transform pmr;

	
	private bool triggeringPMR ;

	//Enemy Variables
	private bool triggeringEnemy;
	private GameObject attackingEnemy;

	Plane playerPlane;


	void Start(){

		pmr = Instantiate(playerMovePoints.transform, this.transform.position, Quaternion.identity);
		pmr.GetComponent<BoxCollider>().enabled = false;
		anime = GetComponent<Animation>();
		currentAttackTimer=attackTimer;
		health=maxHealth;

		playerPlane = new Plane(Vector3.up, transform.position);
	}
   //Functions
	void Update(){
		//Player Movement
		Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		float hitDistance = 0.0f;

		if(playerPlane.Raycast(ray, out hitDistance)){
			Vector3 mousePosition = ray.GetPoint(hitDistance);

			if(Input.GetMouseButtonDown(0)){
				moving=true;
				triggeringPMR = false;
				pmr.transform.position = mousePosition;
				pmr.GetComponent<BoxCollider>().enabled = true;

				if(Physics.Raycast(ray, out hit)){

					if(hit.collider.tag == "Enemy"){
						attackingEnemy = hit.collider.gameObject;
						followingEnemy=true;

					}else{
					attackingEnemy=null;
					followingEnemy=false;
				}
				}

				
			}
		}

		if(moving)
			Move();
		else{
			if(attacking){
				Attack();
			}else{
				Idle();
			}
		}
		

		if(triggeringPMR){
			moving =false;
		}


		if(triggeringEnemy){
			Attack();
		}

		if(attacked){
			currentAttackTimer -= 1 * Time.deltaTime;
		}

		if(currentAttackTimer <=0){
			currentAttackTimer=attackTimer;
			attacked=false;
		}
	}


	public void Attack(){
		if(!attacked){
			damage = Random.Range(minDamage, maxDamage);
			attacked = true;
		}
		if(attackingEnemy){
			transform.LookAt(attackingEnemy.transform);
			attackingEnemy.GetComponent<Enemy>().aggro = true;
		}
		anime.CrossFade("attack");

	}

	public void Idle(){
		anime.CrossFade("idle");
	}


	public void Move(){
		
		if(followingEnemy){
			if(!triggeringEnemy){
					transform.position = Vector3.MoveTowards(transform.position, attackingEnemy.transform.position, movementSpeed);
					this.transform.LookAt(attackingEnemy.transform);
					}
			}else{
				transform.position = Vector3.MoveTowards(transform.position, pmr.transform.position, movementSpeed);
				this.transform.LookAt(pmr.transform);
			}
		
		anime.CrossFade("walk");
	}


	void OnTriggerEnter(Collider other){

		if(other.tag == "PMR"){
			triggeringPMR = true;
		}

		if(other.tag == "Enemy"){
			triggeringEnemy = true;
			
		}
	}

	void OnTriggerExit(Collider other){

		if(other.tag == "PMR"){
			triggeringPMR = false;
		}

		if(other.tag == "Enemy"){
			triggeringEnemy = false;
			
		}
	}

	
	}

