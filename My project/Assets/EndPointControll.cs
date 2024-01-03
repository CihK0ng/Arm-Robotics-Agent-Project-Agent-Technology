using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EndPointControll : MonoBehaviour
{
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private FindBoxAround _findboxaround;
    [SerializeField] private List<Vector3> listEndPoint = new List<Vector3>();

    public void Start()
    {
        

    }

    public void NextEndPoint(GameObject __endpoint)
    {
       
        if (CheckForCubeAtPosition(__endpoint.transform.position) )
        {
            listEndPoint.Add(__endpoint.transform.position);
            __endpoint.transform.Translate(new Vector3(0f, 1f, 0f) * moveSpeed * Time.deltaTime);

            // Di chuyển tất cả các đối tượng con
            MoveChildren(new Vector3(0f, 1f, 0f) * moveSpeed * Time.deltaTime);
        }
    }
   
    public bool CheckForCubeAtPosition(Vector3 position)
    {
        // kiem tra xem cos cube nao o vi tri endpoint hay chua
        Collider[] colliders = Physics.OverlapBox(position, _endPoint.transform.position);
        return colliders.Length > 0;
    }

    public void MoveChildren(Vector3 translation)
    {
        // Lặp qua tất cả các đối tượng con và di chuyển chúng
        foreach (Transform child in transform)
        {
            child.Translate(translation);
        }
    }
    
    
    
}
