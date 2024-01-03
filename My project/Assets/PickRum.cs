using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRum : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;

    [SerializeField] private GameObject _center;
    [SerializeField] private GameObject _arm;
   
    [SerializeField] private GameObject _fork;
    [SerializeField] private GameObject _rum;
    [SerializeField] private GameObject _endpoint;
    [SerializeField] private GameObject _defaultheight;


    [SerializeField] private FindBoxAround _findboxaround;
    [SerializeField] private FindRum _findrum;
    private GameObject getob;

    IEnumerator StartTransfer()
    {
        _findrum.FindRumAound(); 
        getob = _findrum.Getgameobj();
        if(getob != null)
        {
            yield return StartCoroutine(MoveAndPick(_rum,_fork,_arm, _endpoint));
            yield return StartCoroutine(MoveAndPlace(_rum, _fork, _arm, _endpoint));

        }
    }
    IEnumerator MoveAfterOtherArm()
    {
        // Đợi cho đến khi cánh tay kia mang đồ đi
        yield return new WaitUntil(() => otherArm.GetComponent<OtherArmScript>().IsItemTaken());

        // Sau khi cánh tay kia đã mang đồ đi, thì cánh tay này mới chuyển động
        Debug.Log("Other arm has taken the item. This arm is moving now.");

        // Thực hiện chuyển động của cánh tay này, ví dụ:
        // transform.Translate(Vector3.right * 2.0f);

        // Đợi thêm một khoảng thời gian nếu cần
        yield return new WaitForSeconds(moveDelay);

        // Thực hiện các hành động khác sau khi chuyển động
        Debug.Log("Arm movement completed.");
    }
    IEnumerator MoveAndPick(GameObject __rum, GameObject __forkpoint, GameObject __arm, GameObject __endpoint)
    {
        //  thong so  khoang cach Y height
        float placeHeightDistance = __rum.transform.position.y - 0;
        float boxheighDistance = (__rum.transform.position.y - 0);
        float forkheighDistance = (__forkpoint.transform.position.y - 0);

        // Step 2: move xa gan atribute Khoang cach OXZ
        float boxDistance = (__rum.transform.position - _center.transform.position).magnitude;
        float forkDistance = (__forkpoint.transform.position - _center.transform.position).magnitude;
        float endDistance = (__endpoint.transform.position - _center.transform.position).magnitude;

        // step 3 rotate atribute XY 
        Vector3 boxDirection = Vector3.Normalize(__rum.transform.position - _center.transform.position);
        Vector3 forkDirection = Vector3.Normalize(__forkpoint.transform.position - _center.transform.position);
        Vector3 endDirection = Vector3.Normalize(__endpoint.transform.position - _center.transform.position);
        Debug.Log("3");
        Quaternion toRotation = Quaternion.LookRotation(boxDirection, Vector3.up);

        boxDirection = new Vector3(boxDirection.x, 0, boxDirection.z);
        boxDirection = boxDirection.normalized;

        forkDirection = new Vector3(forkDirection.x, 0, forkDirection.z);
        forkDirection = forkDirection.normalized;

        Debug.Log("2");
        // step 1 di chuyen chieu cao 
        yield return StartCoroutine(MoveHeight1(forkheighDistance, placeHeightDistance, __rum));

        // step 2 di chuyen xa gan 

        yield return StartCoroutine(MoveAround1(boxDistance, forkDistance, __arm));

        // Step 3: rotate quay vong
        yield return StartCoroutine(MoveRotate1(boxDirection, forkDirection, boxDirection));

        // Step 3: setParent
        yield return StartCoroutine(SetParent(__rum));
    }

    IEnumerator MoveAndPlace(GameObject __rum, GameObject __forkpoint, GameObject __arm, GameObject __endpoint)
    {

        float defaultDistance = (_defaultheight.transform.position.y - 0);
        float forkheighDistance = (__forkpoint.transform.position.y - 0);
        float placeHeightDistance = __endpoint.transform.position.y - 0;

        float forkDistance = (__forkpoint.transform.position - _center.transform.position).magnitude;

        Vector3 boxDirection = Vector3.Normalize(__rum.transform.position - _center.transform.position);
        boxDirection = new Vector3(boxDirection.x, 0, boxDirection.z);
        boxDirection = boxDirection.normalized;

        Vector3 forkDirection = Vector3.Normalize(__forkpoint.transform.position - _center.transform.position);
        forkDirection = new Vector3(forkDirection.x, 0, forkDirection.z);
        forkDirection = forkDirection.normalized;

        Vector3 endDirection = Vector3.Normalize(__endpoint.transform.position - _center.transform.position);
        endDirection = new Vector3(endDirection.x, 0, endDirection.z);
        endDirection = endDirection.normalized;

        forkDistance = (__forkpoint.transform.position - _center.transform.position).magnitude;
        float endDistance = (__endpoint.transform.position - _center.transform.position).magnitude;


        yield return StartCoroutine(MoveHeightDefault(forkheighDistance, defaultDistance, _defaultheight));

        // step 2 rotate 
        yield return StartCoroutine(MoveRotate2(endDirection, forkDirection, boxDirection));

        // step 3 di chuyen xa gan 
        yield return StartCoroutine(MoveAround2(endDistance, forkDistance, __arm));

        // step 1 di chuyen chieu cao
        yield return StartCoroutine(MoveHeight2(forkheighDistance, placeHeightDistance, __endpoint));

        // huy  
        yield return StartCoroutine(DeleteParent(__rum));
        yield return new WaitForSeconds(0.01f);
    }

    // Di chuyen 
    IEnumerator MoveAround1(float _endDistance, float _forkDistance, GameObject __arm)
    {
        while (Mathf.Abs(_endDistance - _forkDistance) > 0.05f)
        {
            //Debug.Log("aa");
            if (_endDistance > _forkDistance)
            {
                __arm.transform.localPosition += new Vector3(0, 0, 0.04f);
                _forkDistance = (_fork.transform.position - _center.transform.position).magnitude;
            }

            if (_endDistance < _forkDistance)
            {
                __arm.transform.localPosition -= new Vector3(0, 0, 0.04f);
                _forkDistance = (_fork.transform.position - _center.transform.position).magnitude;
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    IEnumerator MoveRotate1(Vector3 _endDirection, Vector3 _forkDirection, Vector3 _boxDirection)
    {

        Quaternion toRotation = Quaternion.LookRotation(_boxDirection, Vector3.up);
        while ((Vector3.Angle(_endDirection, _forkDirection) >= 0.5f))
        {
            _center.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);

            _forkDirection = Vector3.Normalize(_fork.transform.position - _center.transform.position);
            _forkDirection = new Vector3(_forkDirection.x, 0, _forkDirection.z);
            _forkDirection = _forkDirection.normalized;
            yield return new WaitForSeconds(0.01f);
        }
        _center.transform.forward = _endDirection;
        Debug.Log("v");
        yield return null;
    }

    IEnumerator MoveHeight1(float _forkheighDistance, float _placeHeightDistance, GameObject __endpoint)
    {
        while (Mathf.Abs(_forkheighDistance - _placeHeightDistance) > 0.5f)
        {
            if (_placeHeightDistance > _forkheighDistance)
            {
                _arm.transform.localPosition += new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (__endpoint.transform.position.y - 0);
                _forkheighDistance = (_fork.transform.position.y - 0);
            }

            if (_placeHeightDistance < _forkheighDistance)
            {
                _arm.transform.localPosition -= new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (__endpoint.transform.position.y - 0);
                _forkheighDistance = (_fork.transform.position.y - 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
    /// function parent
    IEnumerator SetParent(GameObject __rum)
    {
        if (_fork != null)
        {
            yield return new WaitForSeconds(1f);
            __rum.transform.parent = _fork.transform;
            Debug.Log(__rum.transform.localScale);
            __rum.transform.localScale = new Vector3(
                    1f / _fork.transform.lossyScale.x,
                    1f / _fork.transform.lossyScale.y,
                    1f / _fork.transform.lossyScale.z
                    );

        }
        else
        {

            Debug.LogError("_forkPoint is null. Make sure it's properly initialized.");
            yield return null;
        }
    }

    IEnumerator DeleteParent(GameObject __rum)
    {
        if (__rum.transform.IsChildOf(_fork.transform) == true)
        {
            yield return new WaitForSeconds(0.02f);
            __rum.transform.parent = null;
            __rum.transform.localScale = Vector3.one;
            __rum.transform.localRotation = Quaternion.identity;
        }
        else yield return "no parent";
    }

    IEnumerator MoveHeightDefault(float _forkheighDistance, float _placeHeightDistance, GameObject __endpoint)
    {
        while (Mathf.Abs(_forkheighDistance - _placeHeightDistance) > 0.5f)
        {
            if (_placeHeightDistance > _forkheighDistance)
            {
                _arm.transform.localPosition += new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (__endpoint.transform.position.y - 0);
                _forkheighDistance = (_fork.transform.transform.position.y - 0);
            }

            if (_placeHeightDistance < _forkheighDistance)
            {
                _arm.transform.localPosition -= new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (__endpoint.transform.position.y - 0);
                _forkheighDistance = (_fork.transform.transform.position.y - 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
    IEnumerator MoveHeight2(float _forkheighDistance, float _placeHeightDistance, GameObject __endpoint)
    {
        while (Mathf.Abs(_forkheighDistance - _placeHeightDistance) > 0.5f)
        {
            if (_placeHeightDistance > _forkheighDistance)
            {
                _arm.transform.localPosition += new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (__endpoint.transform.position.y - 0);
                _forkheighDistance = (_fork.transform.transform.position.y - 0);
            }

            if (_placeHeightDistance < _forkheighDistance)
            {
                _arm.transform.localPosition -= new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (__endpoint.transform.position.y - 0);
                _forkheighDistance = (_fork.transform.transform.position.y - 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    IEnumerator MoveRotate2(Vector3 _endDirection, Vector3 _forkDirection, Vector3 _boxDirection)
    {

        Quaternion toRotation = Quaternion.LookRotation(_endDirection, Vector3.up);
        while ((Vector3.Angle(_endDirection, _forkDirection) >= 0.5f))
        {
            _center.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);

            _forkDirection = Vector3.Normalize(_fork.transform.position - _center.transform.position);
            _forkDirection = new Vector3(_forkDirection.x, 0, _forkDirection.z);
            _forkDirection = _forkDirection.normalized;

            yield return new WaitForSeconds(0.02f);
        }
        _center.transform.forward = _endDirection;
        Debug.Log("v");
        yield return null;
    }
    IEnumerator MoveAround2(float _endDistance, float _forkDistance, GameObject __arm)
    {
        while (Mathf.Abs(_endDistance - _forkDistance) > 0.05f)
        {
            //Debug.Log("aa");
            if (_endDistance > _forkDistance)
            {
                __arm.transform.localPosition += new Vector3(0, 0, 0.04f);
                _forkDistance = (_fork.transform.position - _center.transform.position).magnitude;
            }

            if (_endDistance < _forkDistance)
            {
                __arm.transform.localPosition -= new Vector3(0, 0, 0.04f);
                _forkDistance = (_fork.transform.position - _center.transform.position).magnitude;
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }



    // quay OXZ
    IEnumerator Rotate(GameObject center, Vector3 targetDirection, Vector3 forkDirection, float speed)
    {

        Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        while ((Vector3.Angle(targetDirection, forkDirection) > 0.5f))
        {
            center.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);
            yield return new WaitForSeconds(0.02f);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(StartTransfer());

        }
    }
}
