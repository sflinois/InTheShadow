using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {
    
    public PuzzleObject puzzleObject;
    private bool isMouseDrag = false;
    private bool isSolved = false;

    void Update()
    {
        if (!isSolved)
        {
            handleMouse();
            if (puzzleObject.isSolved())
                isSolved = true;
        }
        else if (isSolved)
        {
            Debug.Log("Solved");
        }
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Start puzzleManager");
	}

    private void handleMouse()
    {
        float drag;
        if (Input.GetMouseButtonDown(0))
            isMouseDrag = true;
        if (Input.GetMouseButtonUp(0))
            isMouseDrag = false;
        if (isMouseDrag)
        {
            drag = Input.GetAxis("Mouse X");
            if (drag > 0.05f || drag < -0.05f)
                puzzleObject.rotate(drag);
        }
    }
}
