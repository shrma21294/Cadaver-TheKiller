using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   //Variable
	bool inventoryEnabled; 

	public GameObject inventory;

	public GameObject playerCamera;

	public static Transform slotHovered;

	public Material selectedSlotMat;

	public Material normalSlotMat;

	//Functions
	void Start(){

	}

	void Update(){
		//Detecting slots
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit)){
			if(hit.transform.tag == "Slot"){
				slotHovered = hit.transform;
				slotHovered.GetComponent<Renderer>().material = selectedSlotMat;			
			}else{
				if(slotHovered){
					slotHovered.GetComponent<Renderer>().material = normalSlotMat;
				}
			}
		}

		//Enabling the Inventory

		if(Input.GetKeyDown(KeyCode.I)){
			inventoryEnabled = !inventoryEnabled;
		}

		if(inventoryEnabled){
			inventory.SetActive(true);
			playerCamera.GetComponent<Camera>().orthographic = true;
		}else{
			inventory.SetActive(false);
			playerCamera.GetComponent<Camera>().orthographic = false;
		}
	}
}
