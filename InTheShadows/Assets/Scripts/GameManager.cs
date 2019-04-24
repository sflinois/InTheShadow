using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public LevelSelector current = null;
	public LevelSelector[] levels;
	public List<GameObject> particleList;

	// Use this for initialization
	void Start () {
		// PlayerPrefs.SetInt("tounlockeLevel", 1);
		// PlayerPrefs.SetInt("unlockedLevel", 0);
		Debug.Log(PlayerPrefs.GetInt("tounlockLevel"));
		Debug.Log(PlayerPrefs.GetInt("unlockedLevel"));
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
                if (tmp && !tmp.is_lock)
                {
                    current = tmp;
                    Debug.Log("levelLaunched");
                    current.launchLevel();
                }
				else if (tmp)
				{
					//tmp.lockLevel(false);
				}
            }
        }

		if (Input.GetKey(KeyCode.R))
		{
			PlayerPrefs.SetInt("tounlockLevel", 1);
			PlayerPrefs.SetInt("unlockedLevel", 0);
		}

		List<GameObject> tmpParticleList = new List<GameObject>() ;
		foreach(GameObject particle in particleList)
		{
			Light tmpLight = particle.GetComponent<Transform>().GetChild(1).GetComponent<Light>();
			tmpLight.range -= 0.01f;
			if (tmpLight.range <= 0.1f)
			{
				Destroy(particle);
			}
			else
			{
				tmpParticleList.Add(particle);
			}
		}
		particleList = tmpParticleList;
	}
	
	public void updateLevels()
	{
		int to_unlock;

		if (PlayerPrefs.GetInt("tounlockLevel") != 0)
		{
			to_unlock = PlayerPrefs.GetInt("tounlockLevel");
			for(int i = 0; i < levels.Length; i++)
				if (i < to_unlock && levels[i].is_lock)
					levels[i].lockLevel(false);
			PlayerPrefs.SetInt("unlockedLevel", to_unlock);
		}
	}

	public void enableTest(Toggle isTest){
		if (isTest.isOn)
		{
			PlayerPrefs.SetInt("isTest", 1);
			for(int i = 0; i < levels.Length; i++)
				levels[i].simpleLockLevel(false);
		}
		else
		{
			PlayerPrefs.SetInt("isTest", 0);
			for(int i = 0; i < levels.Length; i++)
			{
				if (i < PlayerPrefs.GetInt("unlockedLevel"))
					levels[i].simpleLockLevel(false);
				else
					levels[i].simpleLockLevel(true);
			}
		}
	}

	public void addParticle(GameObject go)
	{
		particleList.Add(go);
	}
}
