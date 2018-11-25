using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	public int Gold  = 0;
	public int Wood  = 0;
	public int Stone = 0;
	
	public GameObject BuildPanel;
	public GameObject ProducePanel;
	public GameObject MarketPanel;

	void Update () {
		if (Input.GetKeyDown("b")) {
			if (!BuildPanel.activeSelf)
				BuildPanel.SetActive(true);
		}
	}
}
