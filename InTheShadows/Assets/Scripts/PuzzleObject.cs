using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject : MonoBehaviour {

    public int difficulty;
    private Transform objTrans;
    public float TransYResolved;
    private bool solved = false;
    private float speed = 5f;
    private float speedCap = 0.2f;
    private float angleTolerance = 5f;
    private float YTolerance = 0.05f;
    private float helpTolerance = 0.3f;

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
            objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y + Random.Range(-1f, 1f), objTrans.position.z);
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
        if (rotx > speedCap || rotx < -speedCap)
            rotx = rotx > 0 ? speedCap : -speedCap;
        if (roty > speedCap || roty < -speedCap)
            roty = roty > 0 ? speedCap : -speedCap;
        if (rotz > speedCap || rotz < -speedCap)
            rotz = rotz > 0 ? speedCap : -speedCap;
        objTrans.eulerAngles = new Vector3(objTrans.eulerAngles.x + (rotx * speed),
                                           objTrans.eulerAngles.y + (roty * speed),
                                           objTrans.eulerAngles.z + (rotz * speed));
    }

    public void transpose(float transposey)
    {
        if (transposey > speedCap + 0.2f || transposey < -speedCap - 0.2f)
            transposey = transposey > 0 ? speedCap + 0.2f : -speedCap - 0.2f;
        if (solved)
            return;
        if (transposey > 0 && objTrans.position.y - TransYResolved < 1)
            objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y + (transposey * 0.006f * speed), objTrans.position.z);
        if (transposey < 0 && objTrans.position.y - TransYResolved > -1)
            objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y + (transposey * 0.006f * speed), objTrans.position.z);
    }

    public bool isSolved()
    {
        //Debug.Log(objTrans.position.y);
        //Debug.Log(TransYResolved);
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
            eulerY -= solveSpeed;
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

    public float getProgress(int difficulty)
    {
        float progress = 0f;
        float tmp;

        if (difficulty >= 1 && (objTrans.eulerAngles.y + angleTolerance) % 360 < 360 * helpTolerance
            || (objTrans.eulerAngles.y + angleTolerance) % 360 > 360 - (360 * helpTolerance))
        {
            tmp = objTrans.eulerAngles.y > 180 ? -1 * (objTrans.eulerAngles.y - 360) : objTrans.eulerAngles.y;
            progress += (1 - (tmp / (360 * helpTolerance))) / difficulty;

        }
        if (difficulty >= 2 && (objTrans.eulerAngles.z + angleTolerance) % 360 < 360 * helpTolerance
             || (objTrans.eulerAngles.z + angleTolerance) % 360 > 360 - (360 * helpTolerance))
        {
            tmp = objTrans.eulerAngles.z > 180 ? -1 * (objTrans.eulerAngles.z - 360) : objTrans.eulerAngles.z;
            progress += (1 - (tmp / (360 * helpTolerance))) / difficulty;

        }
        if (difficulty >= 3 && objTrans.position.y - TransYResolved > -0.8f * helpTolerance
            && objTrans.position.y - TransYResolved < 0.5f * helpTolerance)
        {
            tmp = objTrans.position.y - TransYResolved < 0 ? TransYResolved - objTrans.position.y : objTrans.position.y - TransYResolved;
            progress += (1 - (tmp / (0.8f * helpTolerance))) / difficulty;
        }
        return progress;
    }
}
