using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject : MonoBehaviour {

    private Transform objTrans;
    private bool solved = false;
    private float speed = 5f;
    private float angleTolerance = 5f;
    private float solveSpeed = 0.1f;

	private void Start () {
        objTrans = GetComponent<Transform>();
        objTrans.eulerAngles.Set(0, 0, 0);
	}

	private void Update()
	{
        if (solved)
            rotateToSolved();
	}

	public void rotate(float rot)
    {
        objTrans.Rotate(objTrans.rotation.x, objTrans.rotation.y + (rot * speed), objTrans.rotation.z, Space.Self);
    }

    public bool isSolved()
    {
        if ((objTrans.eulerAngles.x + angleTolerance) % 360 > 0
            && (objTrans.eulerAngles.x + angleTolerance) % 360 < (angleTolerance * 2)
            && (objTrans.eulerAngles.y + angleTolerance % 360) > 0
            && (objTrans.eulerAngles.y + angleTolerance) % 360 < (angleTolerance * 2)
            && (objTrans.eulerAngles.z + angleTolerance) % 360 > 0
            && (objTrans.eulerAngles.z + angleTolerance) % 360 < (angleTolerance * 2))
        {
            solved = true;
            return true;
        }
        return false;
    }

    public void rotateToSolved()
    {
        float eulerX = objTrans.eulerAngles.x;
        float eulerY = objTrans.eulerAngles.y;
        float eulerZ = objTrans.eulerAngles.z;

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
    }
}
