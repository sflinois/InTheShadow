using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public int level = 1;
	public Renderer render;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Color color = render.material.GetColor("_EmissionColor");
		if (color.r < 0.5)
		{
			Debug.Log("test");
			render.material.SetColor("_EmissionColor", new EmissionColor(0.6F, 0.6F, 0.6F, 0.6F));
		}
	}

    public void launchLevel()
    {
        SceneManager.LoadScene("Level" + level, LoadSceneMode.Single);
    }

//	public void updateMaterial(Material mat1, Material mat2, Material mat3)
//	{
//		Renderer cur;
//		cur = GetComponent<Renderer>();
//		cur.material = mat2;
//		cur.material.SetColor("_EmissionColor", 0);
//	}
}
