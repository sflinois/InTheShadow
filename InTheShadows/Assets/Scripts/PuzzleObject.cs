using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject : MonoBehaviour {

    public int difficulty;
    private Transform objTrans;
    public float TransYResolved;
    private bool solved = false;
    private float speed = 5f;
    private float angleTolerance = 5f;
    private float YTolerance = 0.05f;

    private float solveSpeed = 0.1f;
    private float solveYSpeed = 0.001f;


	private void Start () {
        objTrans = GetComponent<Transform>();
        
        if (difficulty == 1)
            objTrans.eulerAngles = new Vector3(0, Random.Range(50f, 280f), 0);
        else if (difficulty == 2)
            objTrans.eulerAngles = new Vector3(0, Random.Range(50f, 280f), Random.Range(50f, 280f));
        else if (difficulty >= 3)
        {
            objTrans.eulerAngles = new Vector3(0, Random.Range(50f, 280f), Random.Range(50f, 280f));
            objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y + Random.Range(-0.05f, 0.05f), objTrans.position.z);
        }
	}

	private void Update()
	{
        if (solved)
            rotateToSolved();
	}

    public void rotate(float rotx, float roty, float rotz)
    {
        if (solved)
            return;
        objTrans.eulerAngles = new Vector3(objTrans.eulerAngles.x + (rotx * speed),
                                           objTrans.eulerAngles.y + (roty * speed),
                                           objTrans.eulerAngles.z + (rotz * speed));
    }

    public void transpose(float transposey)
    {
        if (solved)
            return;
        if (transposey > 0 && objTrans.position.y - TransYResolved < 1)
            objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y + (transposey * 0.006f * speed), objTrans.position.z);
        if (transposey < 0 && objTrans.position.y - TransYResolved > -1)
            objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y + (transposey * 0.006f * speed), objTrans.position.z);
    }

    public bool isSolved()
    {
        Debug.Log(objTrans.position.y);
        Debug.Log(TransYResolved);
        if ((objTrans.eulerAngles.x + angleTolerance) % 360 > 0
            && (objTrans.eulerAngles.x + angleTolerance) % 360 < (angleTolerance * 2)
            && (objTrans.eulerAngles.y + angleTolerance % 360) > 0
            && (objTrans.eulerAngles.y + angleTolerance) % 360 < (angleTolerance * 2)
            && (objTrans.eulerAngles.z + angleTolerance) % 360 > 0
            && (objTrans.eulerAngles.z + angleTolerance) % 360 < (angleTolerance * 2))
        {
            if (objTrans.position.y - TransYResolved < YTolerance
                && objTrans.position.y - TransYResolved > -YTolerance)
            {
                solved = true;
                return true;
            }
        }
        return false;
    }

    public void rotateToSolved()
    {
        float eulerX = objTrans.eulerAngles.x;
        float eulerY = objTrans.eulerAngles.y;
        float eulerZ = objTrans.eulerAngles.z;
        float posX = objTrans.position.x;
        float posY = objTrans.position.y;
        float posZ = objTrans.position.z;

        if ((objTrans.eulerAngles.x + angleTolerance) % 360 < angleTolerance - solveSpeed)
            eulerX += solveSpeed;
        else if ((objTrans.eulerAngles.x + angleTolerance) % 360 > angleTolerance + solveSpeed)
            eulerX -= solveSpeed;
        else
            eulerX = 0;

        if ((objTrans.eulerAngles.y + angleTolerance) % 360 < angleTolerance - solveSpeed)
            eulerY += solveSpeed;
        else if ((objTrans.eulerAngles.y + angleTolerance) % 360 > angleTolerance + solveSpeed)
        {
            eulerY -= solveSpeed;
            Debug.Log(eulerY);
        }
        else
            eulerY = 0;

        if ((objTrans.eulerAngles.z + angleTolerance) % 360 < angleTolerance - solveSpeed)
            eulerZ += solveSpeed;
        else if ((objTrans.eulerAngles.z + angleTolerance) % 360 > angleTolerance + solveSpeed)
            eulerZ -= solveSpeed;
        else
            eulerZ = 0;
        objTrans.eulerAngles = new Vector3(eulerX, eulerY, eulerZ);

        if (posY + solveYSpeed < TransYResolved)
            posY += solveYSpeed;
        else if (posY - solveYSpeed > TransYResolved)
            posY -= solveYSpeed;
        else
            posY = TransYResolved;
        objTrans.position = new Vector3(posX, posY, posZ);
    }
}
