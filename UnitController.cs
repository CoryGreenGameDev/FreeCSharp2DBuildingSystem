using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {
	public SpriteRenderer   PartySprite;
	public CameraController cController;
	public Manager          manager;
	public Vector2          center;

	public Sprite hpEmpty;
	public Sprite hpHalf;
	public Sprite hpAlmostFull;
	public Sprite hpFull;

	public GameObject camera;
	public GameObject HPBar;
	public GameObject BuildPanel;
	public GameObject CantPlace;
	public GameObject notEnoughMat;
	public GameObject Capital;
	public GameObject Barracks;
	public GameObject House;

	public int curHp;
	public int randomInt = 0;
	public int buildID   = 0;
	public int uID       = 1;
	public int hp        = 5;

	public float tileSize = 1f;
	public float radius   = 5.0f;
	public float speed    = 5f;

	public bool UnitSelected = false;
	public bool isCitizen    = false;
	public bool isEnemy      = false;
	public bool flipOnX      = false;

	Vector3 pos;
	Transform tr;

	void Start() {
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		cController = camera.GetComponent<CameraController>();
		curHp = hp;
		HPBar.GetComponent<SpriteRenderer>().sprite = hpFull;
		pos = transform.position;
		tr = transform;
		PartySprite = PartySprite.GetComponent<SpriteRenderer>();
	}

	void Update() {
		randomInt = Random.Range(0, 201);

		if (isEnemy || isCitizen) {
			/* shitty random ai movement patrol thing i shitted out */
			if(randomInt == 1 && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.down))
				pos += Vector3.down;

			if(randomInt == 2 && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.up))
				pos += Vector3.up;

			if(randomInt == 3 && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.left))
				pos += Vector3.left;

			if(randomInt == 4 && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.right))
				pos += Vector3.right;

			transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    	} else {
			if (Input.GetKey(KeyCode.Alpha1) && cController.player != null)
				cController.unitSelected = 0;

			if (Input.GetKey(KeyCode.Alpha2) && cController.unit1 != null)
				cController.unitSelected = 1;

			if (Input.GetKey(KeyCode.Alpha3) && cController.unit2 != null)
				cController.unitSelected = 2;

			if (Input.GetKey(KeyCode.Alpha4) && cController.unit3 != null)
				cController.unitSelected = 3;

			if (Input.GetKey(KeyCode.Alpha5) && cController.unit4 != null)
				cController.unitSelected = 4;

			if (Input.GetKey(KeyCode.Alpha6) && cController.unit5 != null)
				cController.unitSelected = 5; 

			if (Input.GetKey(KeyCode.Alpha7) && cController.unit6 != null)
				cController.unitSelected = 6;

			if (Input.GetKey(KeyCode.Alpha8) && cController.unit7 != null)
				cController.unitSelected = 7;

			if (Input.GetKey(KeyCode.Alpha9) && cController.unit8 != null)
				cController.unitSelected = 8;

			if (cController.unitSelected != uID) {
				/* if nothing? lolwut */
			} else if (cController.unitSelected == uID) {
				if (Input.GetKeyDown("e"))
					cController.unitSelected = 0;

				if (Input.GetKey(KeyCode.D) && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.right)) {
					flipOnX = true;
					flipX();
					pos += Vector3.right;
				}

				if (Input.GetKey(KeyCode.A) && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.left)) {
					flipOnX = false;
					flipX();
					pos += Vector3.left;
				}

				if (Input.GetKey(KeyCode.W) && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.up))
					pos += Vector3.up;

				if (Input.GetKey(KeyCode.S) && tr.position == pos && !Physics2D.OverlapPoint(pos + Vector3.down))
					pos += Vector3.down;

				transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * speed);
			}
		}

		if (curHp == hp)
			HPBar.GetComponent<SpriteRenderer>().sprite = hpFull;

		if (curHp == hp - 1)
			HPBar.GetComponent<SpriteRenderer>().sprite = hpAlmostFull; 

		if (curHp == hp - 2)
			HPBar.GetComponent<SpriteRenderer>().sprite = hpAlmostFull; 

		if (curHp == hp - 3)
			HPBar.GetComponent<SpriteRenderer>().sprite = hpHalf; 

		if (curHp == hp - 4)
			HPBar.GetComponent<SpriteRenderer>().sprite = hpHalf; 

		if (curHp == 0)
			HPBar.GetComponent<SpriteRenderer>().sprite = hpEmpty;

		void flipX() {
			PartySprite.flipX = flipOnX;
		}

		void OnMouseDown() {
			Debug.Log(center = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position); 

			if(!isEnemy)
				cController.unitSelected = uID;
		}

		/* -- BUILD DATA --
		 * buildID, 0 = Capital  | Cost: 1 Gold, 2 Stone
		 * buildID, 1 = House    | Cost: 1 Gold, 2 Wood
		 * buildID, 2 = Barracks | Cost: 3 Gold, 1 Stone, 1 Stone
		 */

		public void Build(int buildID) {
			if (buildID == 0 && manager.Stone >= 2 && manager.Gold >= 1) {
				BuildPanel.SetActive(false);
				StartCoroutine("PlaceBuilding");
			} else if (manager.Stone < 2 || manager.Gold < 1) {
				notEnoughMat.SetActive(true);
			}

			if (buildID == 1 && manager.Wood >= 2 && manager.Gold >= 1) {
				BuildPanel.SetActive(false);
				StartCoroutine("PlaceBuilding");
			} else if (manager.Wood < 2 || manager.Gold < 1) {
				notEnoughMat.SetActive(true);
			}

			if (buildID == 2 && manager.Wood >= 1 && manager.Gold >= 3 && manager.Stone >= 1) {
				BuildPanel.SetActive(false);
				StartCoroutine("PlaceBuilding");	
			} else if (manager.Wood < 1 || manager.Gold < 3 && manager.Stone < 1) {
				notEnoughMat.SetActive(true);
			}
		}

		public void BuildSet0() {
			buildID = 0;
		}
		public void BuildSet1() {
			buildID = 1;
		}
		public void BuildSet2() {
			buildID = 2;
		}

		/* Building System v2.0 by Cory Green (Unity2D Prefab Raycast Instantiation in a simple Coroutine) */
		IEnumerator PlaceBuilding() {
			while (true) {
				/* collider circle define */
				Collider2D[] hitColliders = null;

				/* mouse cast */
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

				if (Input.GetButtonDown("Fire1") && !BuildPanel.activeSelf && hit.collider == null && buildID == 0) {
					center = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
					hitColliders = Physics2D.OverlapCircleAll(center, radius, 1);

					if (hitColliders.Length == 1){
						Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y),10.0f));
						p.x = Mathf.Round(p.x/tileSize) * tileSize + 0.5f;
						p.y = Mathf.Round(p.y/tileSize) * tileSize + 0.5f;
						Instantiate(Capital,new Vector3(p.x,p.y, 0.0f),Quaternion.identity);
						//Debug.Log(p);
						manager.Stone -= 2;
						manager.Gold--;
						StopCoroutine("PlaceBuilding");
					} else if (hitColliders.Length > 1) {
						CantPlace.GetComponent<Animation>().Play(); 
						StopCoroutine("PlaceBuilding");
					}
				}

				if (Input.GetButtonDown("Fire1") && !BuildPanel.activeSelf && hit.collider == null && buildID == 1) {
					center = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
					hitColliders = Physics2D.OverlapCircleAll(center, radius, 1);

					if (hitColliders.Length == 1){
						Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y),10.0f));
						p.x = Mathf.Round(p.x/tileSize) * tileSize;
						p.y = Mathf.Round(p.y/tileSize) * tileSize - 0.375f;
						Instantiate(House,new Vector3(p.x,p.y, 0.0f),Quaternion.identity);
						Debug.Log("The Center pos is: " + center);
						//Debug.Log(p);
						manager.Wood -= 2;
						manager.Gold--;
						StopCoroutine("PlaceBuilding");
					} else if (hitColliders.Length > 1) {
						CantPlace.GetComponent<Animation>().Play(); 
						StopCoroutine("PlaceBuilding");
					}
				}

				/* NEEDS UPDATING! old single building format! */
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
		}
	}
}
