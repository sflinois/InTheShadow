using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {
    
    public PuzzleObject[] puzzleObjects;
    public GameObject menuPanel;
    public GameObject menuButton;
    public RectTransform menuProgressBar;
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
            if (!menuPanel.activeSelf)
            {
                int solved = 0;
                int i = 0;
                float progress = 0f;
                for (i = 0; i < puzzleObjects.Length; i++)
                {
                    if (puzzleObjects[i].isSolved())
                        solved += 1;
                    progress += puzzleObjects[i].getProgress(pDifficulty);
                }
                progress /= i;
                menuProgressBar.localScale = new Vector3(progress, 1f, 1f);
                if (i == solved)
                {
                    isSolved = true;
                    menuPanel.SetActive(true);
                    menuButton.SetActive(true);
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
	}

    private void handleMouse()
    {
        float drag;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
        if (menuPanel.activeSelf)
            return;
        
        // mode selection
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift) && pDifficulty >= 3)
                selectMode = 3;
            else if (Input.GetKey(KeyCode.LeftControl) && pDifficulty >= 2)
                selectMode = 2;
            else if (pDifficulty >= 1)
                selectMode = 1;
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
            else if (selectMode == 3)
            {
                drag = Input.GetAxis("Mouse Y");
                if (drag > 0.05f || drag < -0.05f)
                    puzzleObjects[puzzleIndex].transpose(drag);
            }
        }
    }
}
