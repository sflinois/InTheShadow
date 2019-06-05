using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public int level;
	public bool is_lock;
	public GameObject unlock_particles;
	public GameManager gm;

	// Use this for initialization
	void Start () {
		// PlayerPrefs.SetInt("unlockedLevel", 0);
		if (PlayerPrefs.GetInt("unlockedLevel") < level)
		{
			simpleLockLevel(true);
		}
		else
		{
			simpleLockLevel(false);
		}
	}

    public void launchLevel()
    {
        SceneManager.LoadScene("Level" + level, LoadSceneMode.Single);
    }

	public void lockLevel(bool islock)
	{
		simpleLockLevel(islock);
		if (islock == false)
			gm.addParticle(Instantiate(unlock_particles, transform.position, Quaternion.identity));
	}

	public void simpleLockLevel(bool islock)
	{
		GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
		if (islock == true)
		{
			is_lock = true;
			GetComponent<Renderer>().material.color = Color.grey;
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f,0.1f,0.1f));
		}
		else if (islock == false)
		{
			is_lock = false;
			GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
			if (PlayerPrefs.GetInt("unlockedLevel") == level || PlayerPrefs.GetInt("isTest") == 1)
			{
				GetComponent<Renderer>().material.color = Color.white;
				GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.3f,0.3f,0.5f));
			}
			else
			{
				GetComponent<Renderer>().material.color = new Color(0.95f, 1f, 0.95f);
				GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.3f,0.4f,0.4f));
			}
		}
	}
}
