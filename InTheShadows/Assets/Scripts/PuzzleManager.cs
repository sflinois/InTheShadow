using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {
    
    public PuzzleObject[] puzzleObjects;
    public Canvas menuCanvas;
	public GameManager gm;
    public int pDifficulty;
	public int pLevel;
    private int puzzleIndex = -1;
    private int selectMode = 0;
    private bool isSolved = false;

    void Update()
    {
        if (!isSolved)
        {
            if (!menuCanvas.enabled)
            {
                isSolved = true;
                menuCanvas.enabled = true;
                for (int i = 0; i < puzzleObjects.Length; i++)
                {
                    if (!puzzleObjects[i].isSolved())
                    {
                        isSolved = false;
                        menuCanvas.enabled = false;
                        break;
                    }
                }
				if (isSolved && PlayerPrefs.GetInt("isTest") == 0)
				{
					if (PlayerPrefs.GetInt("tounlockLevel") < pLevel + 1)
						PlayerPrefs.SetInt("tounlockLevel", pLevel + 1);
				}
            }
            handleMouse();
        }
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Start puzzleManager");
	}

    private void handleMouse()
    {
        float drag;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Keydown escape");
            menuCanvas.enabled = !menuCanvas.enabled;
        }
        if (menuCanvas.enabled)
            return;
        
        // mode selection
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.O) && pDifficulty >= 3)
                selectMode = 3;
            else if (Input.GetKey(KeyCode.P) && pDifficulty >= 2)
                selectMode = 2;
            else if (pDifficulty >= 1)
                selectMode = 1;
        }

        if (selectMode > 0)
        {
            PuzzleObject tmp;
            int tmpIndex = 0;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                tmp = hit.transform.gameObject.GetComponent<PuzzleObject>(); 
                if (tmp)
                {
                    for (int i = 0; i < puzzleObjects.Length; i++)
                    {
                        if (puzzleObjects[i] == tmp)
                            break;
                        tmpIndex++;
                    }
                    puzzleIndex = tmpIndex < puzzleObjects.Length ? tmpIndex : -1;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            puzzleIndex = -1;
            selectMode = 0;
        }
        if (puzzleIndex >= 0 && selectMode > 0)
        {
            if (selectMode == 1)
            {
                drag = Input.GetAxis("Mouse X");
                if (drag > 0.05f || drag < -0.05f)
                    puzzleObjects[puzzleIndex].rotate(0, drag, 0);
            }
            else if (selectMode == 2)
            {
                drag = Input.GetAxis("Mouse Y");
                if (drag > 0.05f || drag < -0.05f)
                    puzzleObjects[puzzleIndex].rotate(0, 0, drag);
            }
            else if (selectMode == 1)
            {
//                drag = Input.GetAxis("Mouse X");
//                if (drag > 0.05f || drag < -0.05f)
//                    puzzleObjects[puzzleIndex].rotate(0, drag, 0);
            }
        }
    }
}
