using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript 
{
    /// <summary>
    ///  // step: khoang cach chieu cao 
/*
    float heightDistance = Mathf.Abs(_box.transform.position.y - _box.transform.position.y);
    float boxheighDistance = (_box.transform.position.y - 0);
    float forkheighDistance = (_forkPoint.transform.position.y - 0);
    float placeHeightDistance = _endPoint.position.y - 0;

    var originalScale = _box.transform.localScale;

        

        while ((forkheighDistance - boxheighDistance) > 1f)
        {
            if (boxheighDistance > forkheighDistance)
            {
                _arm.transform.localPosition += new Vector3(0, 0.02f, 0);
    boxheighDistance = (_box.transform.position.y - 0);
                forkheighDistance = (_forkPoint.transform.position.y - 0);
            }

if (boxheighDistance < forkheighDistance)
{
    _arm.transform.localPosition -= new Vector3(0, 0.02f, 0);
    boxheighDistance = (_box.transform.position.y - 0);
    forkheighDistance = (_forkPoint.transform.position.y - 0);
}
yield return new WaitForSeconds(0.02f);
        }

        // Step 1: move
        float boxDistance = (_box.transform.position - _centrePoint.position).magnitude;
float forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;

while (Mathf.Abs(boxDistance - forkDistance) > 0.2f)
{
    if (boxDistance > forkDistance)
    {
        _arm.transform.localPosition += new Vector3(0, 0, 0.04f);
        boxDistance = (_box.transform.position - _centrePoint.position).magnitude;
        forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
    }

    if (boxDistance < forkDistance)
    {
        _arm.transform.localPosition -= new Vector3(0, 0, 0.04f);
        boxDistance = (_box.transform.position - _centrePoint.position).magnitude;
        forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
    }
    yield return new WaitForSeconds(0.02f);
}

// Step 2: rotate
Vector3 boxDirection = Vector3.Normalize(_box.transform.position - _centrePoint.position);
boxDirection = new Vector3(boxDirection.x, 0, boxDirection.z);
boxDirection = boxDirection.normalized;
Vector3 forkDirection = Vector3.Normalize(_forkPoint.position - _centrePoint.position);
forkDirection = new Vector3(forkDirection.x, 0, forkDirection.z);
forkDirection = forkDirection.normalized;

Quaternion toRotation = Quaternion.LookRotation(boxDirection, Vector3.up);
while ((Vector3.Angle(boxDirection, forkDirection) >= 4f))
{
    //Debug.Log((Vector3.Angle(boxDirection, forkDirection)));
    _ar.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);

    forkDirection = Vector3.Normalize(_forkPoint.position - _centrePoint.position);
    forkDirection = new Vector3(forkDirection.x, 0, forkDirection.z);
    forkDirection = forkDirection.normalized;

    yield return new WaitForSeconds(0.02f);
}
_ar.transform.forward = boxDirection;

// Step 3: setParent
yield return new WaitForSeconds(1f);


_box.transform.parent = _asd.transform;
Debug.Log(_box.transform.localScale);
_box.transform.localScale = new Vector3(
        1f / transform.lossyScale.x,
        1f / transform.lossyScale.y,
        1f / transform.lossyScale.z
        );


// step 


while (Mathf.Abs(forkheighDistance - placeHeightDistance) > 1f)
{
    if (placeHeightDistance > forkheighDistance)
    {
        _arm.transform.localPosition += new Vector3(0, 0.03f, 0);
        placeHeightDistance = (_endPoint.position.y - 0);
        forkheighDistance = (_forkPoint.transform.position.y - 0);
    }

    if (placeHeightDistance < forkheighDistance)
    {
        _arm.transform.localPosition -= new Vector3(0, 0.03f, 0);
        placeHeightDistance = (_endPoint.position.y - 0);
        forkheighDistance = (_forkPoint.transform.position.y - 0);
    }
    yield return new WaitForSeconds(0.02f);
}


// Sptep 4: rotate
Vector3 endDirection = Vector3.Normalize(_endPoint.position - _centrePoint.position);
endDirection = new Vector3(endDirection.x, 0, endDirection.z);
endDirection = endDirection.normalized;

toRotation = Quaternion.LookRotation(endDirection, Vector3.up);
while ((Vector3.Angle(endDirection, forkDirection) >= 0.4f))
{
    _ar.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);

    forkDirection = Vector3.Normalize(_forkPoint.position - _centrePoint.position);
    forkDirection = new Vector3(forkDirection.x, 0, forkDirection.z);
    forkDirection = forkDirection.normalized;

    yield return new WaitForSeconds(0.02f);
}
_ar.transform.forward = endDirection;

// Step 5: move
forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
float endDistance = (_endPoint.position - _centrePoint.position).magnitude;

while (Mathf.Abs(endDistance - forkDistance) > 0.2f)
{
    //Debug.Log("aa");
    if (endDistance > forkDistance)
    {
        _arm.transform.localPosition += new Vector3(0, 0, 0.04f);
        forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
    }

    if (endDistance < forkDistance)
    {
        _arm.transform.localPosition -= new Vector3(0, 0, 0.04f);
        forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
    }
    yield return new WaitForSeconds(0.02f);
}

//
yield return new WaitForSeconds(0.02f);
_box.transform.SetParent(null);
_box.transform.localScale = originalScale;
_box.transform.localRotation = Quaternion.identity;


// tang endpoint len 1 don vi 
if (Mathf.Abs(endDistance - boxDistance) < 0.3f)
{
    _endPoint.position += new Vector3(_endPoint.position.x, 0.5f, _endPoint.position.z);
}
    }
*/
/// </summary>
   
}
