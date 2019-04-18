using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	private GameObject cam;
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	public void setMenu(){
		anim.SetBool("isMenu", true);
		anim.SetBool("isLevel", false);
	}

	public void setLevels(){
		anim.SetBool("isMenu", false);
		anim.SetBool("isLevel", true);
	}
}
