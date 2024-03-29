﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variable
	public float maxHealth;
	private float health;
	public float movementSpeed;

	private GameObject player;

	private bool triggeringPlayer;
	public  bool aggro;

	public float attackTimer;
	private float _attackTimer;
	private bool attacked;

	private float damage;
	public float minDamage;
	public float maxDamage;



    //Functions

 	void Start(){
 		player  = GameObject.FindWithTag("Player");
 		_attackTimer = attackTimer;
 		health=maxHealth;
 	}



    void Update(){
    	if(aggro){
    		FollowPlayer();
    	}

    }


    void OnTriggerEnter(Collider other){
    	if(other.tag =="Player"){
    		triggeringPlayer=true;
    	}

    }

     void OnTriggerExit(Collider other){
    	if(other.tag =="Player"){
    		triggeringPlayer=false;
    	}
    }


    public void Attack(){
    	if(!attacked){
    		damage = Random.Range(minDamage, maxDamage);
    		player.GetComponent<Player>().health -= damage;
    		attacked=true;
    		print("Enemy attacked player");
    	}
    }

    public void FollowPlayer(){
    	if(!triggeringPlayer){
    		this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed);
    	}

    	if(_attackTimer <= 0){
    		attacked = false;
    		_attackTimer = attackTimer;
    	}

    	if(attacked)
    		_attackTimer -= 1* Time.deltaTime; 	

    	Attack();
    }
}
