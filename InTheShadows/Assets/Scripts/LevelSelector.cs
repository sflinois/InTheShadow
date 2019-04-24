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
		// Debug.Log(PlayerPrefs.GetInt("unlockedLevel"));
		// PlayerPrefs.SetInt("unlockedLevel", 0);
		if (PlayerPrefs.GetInt("unlockedLevel") < level)
		{
			GetComponent<Renderer>().material.color = Color.grey;
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f,0.1f,0.1f));
			is_lock = true;
		}
		else
		{
			GetComponent<Renderer>().material.color = Color.white;
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.3f,0.3f,0.5f));
			is_lock = false;
		}
	}

    public void launchLevel()
    {
        SceneManager.LoadScene("Level" + level, LoadSceneMode.Single);
    }

	public void lockLevel(bool islock)
	{
		if (islock == true)
		{
			is_lock = true;
			GetComponent<Renderer>().material.color = Color.grey;
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f,0.1f,0.1f));
		}
		else if (islock == false)
		{
			is_lock = false;
			GetComponent<Renderer>().material.color = Color.white;
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.3f,0.3f,0.5f));
			gm.addParticle(Instantiate(unlock_particles, transform.position, Quaternion.identity));
		}
	}

	public void simpleLockLevel(bool islock)
	{
		if (islock == true)
		{
			is_lock = true;
			GetComponent<Renderer>().material.color = Color.grey;
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f,0.1f,0.1f));
		}
		else if (islock == false)
		{
			is_lock = false;
			GetComponent<Renderer>().material.color = Color.white;
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.3f,0.3f,0.5f));
		}
	}
}
