
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingSystem: MonoBehaviour {
  	public Vector2 center;
  	public float radius = 5.0f;
    public GameObject BuildPanel;
  	public GameObject notEnoughMat;
  	public int buildID = 0;
  	public GameObject Capital;
  	public GameObject Barracks;
  	public GameObject House;
    public float tileSize = 1f;
    
  public void Build(int buildID){
  	if(buildID == 0 && manager.Stone >= 2 && manager.Gold >= 1){
  		BuildPanel.SetActive(false);
  		StartCoroutine("PlaceBuilding");
  	} else if(manager.Stone < 2 || manager.Gold < 1){
  		notEnoughMat.SetActive(true);
  	}
  	if(buildID == 1 && manager.Wood >= 2 && manager.Gold >= 1){
  		BuildPanel.SetActive(false);
  		StartCoroutine("PlaceBuilding");
  	} else if(manager.Wood < 2 || manager.Gold < 1){
  		notEnoughMat.SetActive(true);
  	}
  	if(buildID == 2 && manager.Wood >= 1 && manager.Gold >= 3 && manager.Stone >= 1){
  		BuildPanel.SetActive(false);
  		StartCoroutine("PlaceBuilding");	
  	} else if(manager.Wood < 1 || manager.Gold < 3 && manager.Stone < 1){
  		notEnoughMat.SetActive(true);
  	}
    //ALSO SET THIS VIA THE BUTTON (ANOTHER ON CLICK)
    public void BuildSet0(){
  	 buildID = 0;
   }
   public void BuildSet1(){
   	 buildID = 1;
   }
   public void BuildSet2(){
  	buildID = 2;
   }
    
//Building System v2.0 by Cory Green (Unity2D Prefab Raycast Instantiation in a simple Coroutine)
    IEnumerator PlaceBuilding(){
   	while(true){
   	//collider circle define
   	Collider2D[] hitColliders = null;
   	//mouse cast
   	RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
  	 if (Input.GetButtonDown("Fire1") && !BuildPanel.activeSelf && hit.collider == null && buildID == 0) {
  	 		center = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
  	 		hitColliders = Physics2D.OverlapCircleAll(center, radius, 1);
  	 		if(hitColliders.Length == 1){
				Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y),10.0f));
  	 			p.x = Mathf.Round(p.x/tileSize) * tileSize + 0.5f;
  	 			p.y = Mathf.Round(p.y/tileSize) * tileSize + 0.5f;
  	 			Instantiate(Capital,new Vector3(p.x,p.y, 0.0f),Quaternion.identity);
  	 			//Debug.Log(p);
  	 			manager.Stone -= 2;
  	 			manager.Gold--;
  	 			StopCoroutine("PlaceBuilding");
  	 		} else if(hitColliders.Length > 1){
  	 			StopCoroutine("PlaceBuilding");
  	 		}
  	 }
	 if (Input.GetButtonDown("Fire1") && !BuildPanel.activeSelf && hit.collider == null && buildID == 1) {
	 		center = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
  	 		hitColliders = Physics2D.OverlapCircleAll(center, radius, 1);
  	 		if(hitColliders.Length == 1){
  	 			Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y),10.0f));
  	 			p.x = Mathf.Round(p.x/tileSize) * tileSize;
  	 			p.y = Mathf.Round(p.y/tileSize) * tileSize - 0.375f;
  	 			Instantiate(House,new Vector3(p.x,p.y, 0.0f),Quaternion.identity);
  	 			Debug.Log("The Center pos is: " + center);
  	 			//Debug.Log(p);
  	 			manager.Wood -= 2;
  	 			manager.Gold--;
  	 			StopCoroutine("PlaceBuilding");
  	 		} else if(hitColliders.Length > 1){
  	 			StopCoroutine("PlaceBuilding");
  	 		}
  	 }
  	 //NEEDS UPDATING! old single building format! Left if you want only one tile.
	 if (Input.GetButtonDown("Fire1") && !BuildPanel.activeSelf && hit.collider == null && buildID == 2) {
  	 	Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y),10.0f));
  	 	p.x = Mathf.Round(p.x/tileSize) * tileSize + 0.5f;
  	 	p.y = Mathf.Round(p.y/tileSize) * tileSize + 0.5f;
  	 	Instantiate(Barracks,new Vector3(p.x,p.y, 0.0f),Quaternion.identity);
  	 	//Debug.Log(p);
  	 	manager.Stone--;
  	 	manager.Gold -= 3;
  	 	manager.Wood--;
  	 	StopCoroutine("PlaceBuilding");
  	 }
  	 yield return null;
  	}
