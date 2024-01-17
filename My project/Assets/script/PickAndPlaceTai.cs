using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class PickAndPlaceTai : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;
    [SerializeField] private Transform _centrePoint;
    [SerializeField] private Transform _forkPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private GameObject _box;
    [SerializeField] private GameObject _arm;
    [SerializeField] private GameObject _ar;
    [SerializeField] private GameObject _forkob;
    [SerializeField] private GameObject _endBox;
    [SerializeField] private GameObject _defaultheight;
    private bool pickedrum = false;

    [SerializeField] private EndPointControll _addEndPoint;
    [SerializeField] private FindBoxAround _findboxaround;
    [SerializeField] private PickRum _pickrum;
    [SerializeField] private FindRum _findRum;
    [SerializeField] private IsTwoArmCollider _istwoarmcollider;
    


    private List<GameObject> receivedList;
    

    IEnumerator StartTransfer()
    {
        _findboxaround.FindCubesAround(_ar.transform.position);
        receivedList = _findboxaround.GetList();
        for(int i = 0; i <= 5; i++)
        {
            yield return StartCoroutine(Reset());

        }

        
        yield return new WaitForSeconds(0.01f);
    }
    IEnumerator Reset()
    {
        foreach (GameObject go in receivedList)
        {

            _findRum.FindRumAound();
            if (_findRum.Getgameobj() == "Rum")
            {
                if (pickedrum == false)
                {
                    yield return StartCoroutine(_pickrum.Pickrumprocees());
                    pickedrum = true;
                }
            }

            yield return StartCoroutine(MoveAndPick(go, _forkob, _arm, _endPoint));
            yield return StartCoroutine(MoveAndPlace(go, _forkob, _arm, _endPoint));
        }
    }
    IEnumerator MoveHeightDefault(float _forkheighDistance, float _placeHeightDistance, GameObject _endPoint)
    {
        while (Mathf.Abs(_forkheighDistance - _placeHeightDistance) > 0.1f)
        {
            if (_placeHeightDistance > _forkheighDistance)
            {
                _arm.transform.localPosition += new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (_endPoint.transform.position.y - 0);
                _forkheighDistance = (_forkPoint.transform.position.y - 0);
            }

            if (_placeHeightDistance < _forkheighDistance)
            {
                _arm.transform.localPosition -= new Vector3(0, 0.03f, 0);
                _placeHeightDistance = (_endPoint.transform.position.y - 0);
                _forkheighDistance = (_forkPoint.position.y - 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    // Phương thức để nhận danh sách từ bên ngoài
    public void SetList(List<GameObject> list)
    {
        receivedList = new List<GameObject>();
        // Gán danh sách nhận được từ bên ngoài cho danh sách nội bộ của ReceiverClass
        receivedList = list;

        // Bây giờ bạn có thể sử dụng receivedList như bình thường
        Debug.Log("Received List Count: " + receivedList.Count);
    }

    IEnumerator MoveAndPick( GameObject __box, GameObject __forkpoint, GameObject  __arm, Transform __endpoint)
    {
        //  thong so  khoang cach Y height
        float placeHeightDistance = __box.transform.position.y - 0;
        float heightDistance = Mathf.Abs(__box.transform.position.y - __box.transform.position.y);
        float boxheighDistance = (__box.transform.position.y - 0);
        float forkheighDistance = (__forkpoint.transform.position.y - 0);

        // Step 2: move xa gan atribute Khoang cach OXZ
        float boxDistance = (__box.transform.position - _centrePoint.position).magnitude;
        float forkDistance = (__forkpoint.transform.position - _centrePoint.position).magnitude;
        float endDistance = (__endpoint.position - _centrePoint.position).magnitude;

        // step 3 rotate atribute XY 
        Vector3 boxDirection = Vector3.Normalize(__box.transform.position - _centrePoint.position);
        Vector3 forkDirection = Vector3.Normalize(__forkpoint.transform.position - _centrePoint.position);
        Vector3 endDirection = Vector3.Normalize(__endpoint.position - _centrePoint.position);
        Debug.Log("3");
        Quaternion toRotation = Quaternion.LookRotation(boxDirection, Vector3.up);

        boxDirection = new Vector3(boxDirection.x, 0, boxDirection.z);
        boxDirection = boxDirection.normalized;

        forkDirection = new Vector3(forkDirection.x, 0, forkDirection.z);
        forkDirection = forkDirection.normalized;

        Debug.Log("2");
        // step 1 di chuyen chieu cao 
        yield return StartCoroutine(MoveHeight1(forkheighDistance, placeHeightDistance, __box.transform));

        // step 2 di chuyen xa gan 

        yield return StartCoroutine(MoveAround1(boxDistance, forkDistance, __arm));

        // Step 3: rotate quay vong
        yield return StartCoroutine(MoveRotate1(boxDirection, forkDirection, boxDirection));

        // Step 3: setParent
        yield return StartCoroutine(SetParent(__box));
    }

    IEnumerator MoveAndPlace(GameObject __box, GameObject __forkpoint, GameObject __arm, Transform __endpoint)
    {
        float defaultDistance = (_defaultheight.transform.position.y - 0);
        float forkheighDistance = (__forkpoint.transform.position.y - 0);
        float placeHeightDistance = __endpoint.position.y - 0;
        float forkDistance = (__forkpoint.transform.position - _centrePoint.position).magnitude;

        Vector3 boxDirection = Vector3.Normalize(__box.transform.position - _centrePoint.position);
        boxDirection = new Vector3(boxDirection.x, 0, boxDirection.z);
        boxDirection = boxDirection.normalized;

        Vector3 forkDirection = Vector3.Normalize(__forkpoint.transform.position - _centrePoint.position);
        forkDirection = new Vector3(forkDirection.x, 0, forkDirection.z);
        forkDirection = forkDirection.normalized;

        Vector3 endDirection = Vector3.Normalize(__endpoint.position - _centrePoint.position);
        endDirection = new Vector3(endDirection.x, 0, endDirection.z);
        endDirection = endDirection.normalized;

        forkDistance = (__forkpoint.transform.position - _centrePoint.position).magnitude;
        float endDistance = (__endpoint.position - _centrePoint.position).magnitude;

        yield return StartCoroutine(MoveHeightDefault(forkheighDistance, defaultDistance, _defaultheight));
        
        // step 3 di chuyen xa gan 
        yield return StartCoroutine(MoveAround2(endDistance, forkDistance, __arm));

        // step 2 rotate
        yield return StartCoroutine(MoveRotate2(endDirection, forkDirection, boxDirection));
        
        // step 1 di chuyen chieu cao
        yield return StartCoroutine(MoveHeight2(forkheighDistance, placeHeightDistance, __endpoint));

        // huy  
        yield return StartCoroutine(DeleteParent(__box));
        __endpoint.transform.position += new Vector3(0, 1, 0);
        yield return new WaitForSeconds(0.01f);
        
    }

    IEnumerator MoveAround1(float _endDistance, float _forkDistance, GameObject __arm)
    {
        while (Mathf.Abs(_endDistance - _forkDistance) >= 0.05f)
        {
            //Debug.Log("aa");
            if (_endDistance > _forkDistance)
            {
                __arm.transform.localPosition += new Vector3(0, 0, 0.04f);
                _forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
            }

            if (_endDistance < _forkDistance)
            {
                __arm.transform.localPosition -= new Vector3(0, 0, 0.04f);
                _forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    IEnumerator MoveRotate1(Vector3 _endDirection, Vector3 _forkDirection, Vector3 _boxDirection)
    {

        Quaternion toRotation = Quaternion.LookRotation(_boxDirection, Vector3.up);
        while ((Vector3.Angle(_endDirection, _forkDirection) >= 0.2f))
        {
            _ar.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);

            _forkDirection = Vector3.Normalize(_forkPoint.position - _centrePoint.position);
            _forkDirection = new Vector3(_forkDirection.x, 0, _forkDirection.z);
            _forkDirection = _forkDirection.normalized;
            yield return new WaitForSeconds(0.02f);
        }
        _ar.transform.forward = _endDirection;
        Debug.Log("v");
        yield return null;
    }

    IEnumerator MoveHeight1(float _forkheighDistance, float _placeHeightDistance, Transform __endpoint)
    {
         while (Mathf.Abs(_forkheighDistance - _placeHeightDistance) >= 0.1f)
        {
            if (_placeHeightDistance > _forkheighDistance)
            {
                _arm.transform.localPosition += new Vector3(0, 0.04f, 0);
                _placeHeightDistance = (__endpoint.position.y - 0);
                _forkheighDistance = (_forkPoint.position.y - 0);
            }

            if (_placeHeightDistance < _forkheighDistance)
            {
                _arm.transform.localPosition -= new Vector3(0, 0.04f, 0);
                _placeHeightDistance = (__endpoint.position.y - 0);
                _forkheighDistance = (_forkPoint.position.y - 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
    /// function parent
    IEnumerator SetParent(GameObject _box)
    {
        if(_forkPoint != null)
{
            yield return new WaitForSeconds(1f);
            _box.transform.parent = _forkPoint;
            Debug.Log(_box.transform.localScale);
            _box.transform.localScale = new Vector3(
                    1f / _forkPoint.lossyScale.x,
                    1f / _forkPoint.lossyScale.y,
                    1f / _forkPoint.lossyScale.z
                    );
            
        }
        else
        {
            
            Debug.LogError("_forkPoint is null. Make sure it's properly initialized.");
            yield return null;
        }
    }

    IEnumerator DeleteParent(GameObject __box)
    {
        if (__box.transform.IsChildOf(_forkPoint) == true)
        {
            yield return new WaitForSeconds(0.02f);
            __box.transform.parent = null;
            __box.transform.localScale = Vector3.one;
            __box.transform.localRotation = Quaternion.identity;
        }
        else yield return "no parent";
    }

    IEnumerator MoveHeight2(float _forkheighDistance, float _placeHeightDistance, Transform __endpoint)
    {
        while (Mathf.Abs(_forkheighDistance - _placeHeightDistance) >= 0.1f)
        {
            if (_placeHeightDistance > _forkheighDistance)
            {
                _arm.transform.localPosition += new Vector3(0, 0.04f, 0);
                _placeHeightDistance = (__endpoint.position.y - 0);
                _forkheighDistance = (_forkPoint.transform.position.y - 0);
            }

            if (_placeHeightDistance < _forkheighDistance)
            {
                _arm.transform.localPosition -= new Vector3(0, 0.04f, 0);
                _placeHeightDistance = (__endpoint.position.y - 0);
                _forkheighDistance = (_forkPoint.transform.position.y - 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    IEnumerator MoveRotate2(Vector3 _endDirection, Vector3 _forkDirection, Vector3 _boxDirection)
    {

        Quaternion toRotation = Quaternion.LookRotation(_endDirection, Vector3.up);
        while ((Vector3.Angle(_endDirection, _forkDirection) >= 0.2f))
        {
            _ar.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);

            _forkDirection = Vector3.Normalize(_forkPoint.position - _centrePoint.position);
            _forkDirection = new Vector3(_forkDirection.x, 0, _forkDirection.z);
            _forkDirection = _forkDirection.normalized;

            yield return new WaitForSeconds(0.02f);
        }
        _ar.transform.forward = _endDirection;
        Debug.Log("v");
        yield return null;
    }
    IEnumerator MoveAround2(float _endDistance, float _forkDistance, GameObject __arm)
    {
        while (Mathf.Abs(_endDistance - _forkDistance) >= 0.05f)
        {
            //Debug.Log("aa");
            if (_endDistance > _forkDistance)
            {
                __arm.transform.localPosition += new Vector3(0, 0, 0.04f);
                _forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
            }

            if (_endDistance < _forkDistance)
            {
                __arm.transform.localPosition -= new Vector3(0, 0, 0.04f);
                _forkDistance = (_forkPoint.position - _centrePoint.position).magnitude;
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
  

    // lock
    IEnumerator Rotate(GameObject ar, Vector3 targetDirection, Vector3 forkDirection , float speed)
    {

        Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        while ((Vector3.Angle(targetDirection, forkDirection) > 0.5f))
        {
            ar.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _speed);
            yield return new WaitForSeconds(0.02f);
        }  

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(StartTransfer());

        }
    }
}
