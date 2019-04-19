using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void loadLevel(int num)
    {
        SceneManager.LoadScene("Level" + num, LoadSceneMode.Single);
    }
}
