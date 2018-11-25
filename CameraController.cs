using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public int unitSelected = 0;

	public GameObject unit1;
	public GameObject unit2;
	public GameObject unit3;
	public GameObject unit4;
	public GameObject unit5;
	public GameObject unit6;
	public GameObject unit7;
	public GameObject unit8;
	public GameObject unit9;
	public GameObject unit10;
	public GameObject unit11;
	public GameObject unit12;
	public GameObject unit13;
	public GameObject unit14;
	public GameObject unit15;

	public GameObject player;

	private Vector3 offset;

	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	void LateUpdate () {
		if(unitSelected == 0)
			transform.position = player.transform.position + offset;
			
		if(unitSelected == 1)
			transform.position = unit1.transform.position + offset;

		if(unitSelected == 2)
			transform.position = unit2.transform.position + offset;

		if(unitSelected == 3)
			transform.position = unit3.transform.position + offset;

		if(unitSelected == 4)
			transform.position = unit4.transform.position + offset;

		if(unitSelected == 5)
			transform.position = unit5.transform.position + offset;

		if(unitSelected == 6)
			transform.position = unit6.transform.position + offset;

		if(unitSelected == 7)
			transform.position = unit7.transform.position + offset;

		if(unitSelected == 8)
			transform.position = unit8.transform.position + offset;

		if(unitSelected == 9)
			transform.position = unit9.transform.position + offset;

		if(unitSelected == 10)
			transform.position = unit10.transform.position + offset;

		if(unitSelected == 11)
			transform.position = unit11.transform.position + offset;

		if(unitSelected == 12)
			transform.position = unit12.transform.position + offset;

		if(unitSelected == 13)
			transform.position = unit13.transform.position + offset;

		if(unitSelected == 14)
			transform.position = unit14.transform.position + offset;

		if(unitSelected == 15)
			transform.position = unit15.transform.position + offset;
	}
}
