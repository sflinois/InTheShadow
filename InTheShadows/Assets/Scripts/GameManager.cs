using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public GameObject puzzleObject;
    public LevelSelector current = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                LevelSelector tmp;
                tmp = hit.transform.gameObject.GetComponent<LevelSelector>(); 
                if (tmp)
                {
                    current = tmp;
                    Debug.Log("levelLaunched");
                    current.launchLevel();
                }
            }
        }
	}
}
