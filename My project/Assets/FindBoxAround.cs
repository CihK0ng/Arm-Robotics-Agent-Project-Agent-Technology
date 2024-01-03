using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FindBoxAround : MonoBehaviour
{
    public PickAndPlaceTai _PickAndPlaceTai;

    [SerializeField]private GameObject heatPoint;
    [SerializeField] private float radius = 20f; // Bán kính của hình cầu xung quanh
    [SerializeField] private string cubeTag = "pickitem";
    [SerializeField] private List<GameObject> boxPickList = new List<GameObject>();
    int n = 0;
    void GetSeclectObject(GameObject selectobject)
    {
        if(selectobject != null)
        {
            GameObject firstGameObject = boxPickList[n+1];
        }

    }

    void GetDistanceOfBox(GameObject ob1,GameObject ob2)
    {
        float distance = (ob1.transform.position - ob2.transform.position).magnitude;
    }

    int GetObjectIndex(GameObject obj)
    {
        // Sử dụng IndexOf để lấy vị trí của đối tượng trong List
        return boxPickList.IndexOf(obj);
    }

    public void FindCubesAround(Vector3 center)
    {
        // Sử dụng Physics.OverlapSphere để tìm các Collider trong hình cầu
        Collider[] colliders = Physics.OverlapSphere(center, radius);

        // Duyệt qua mảng các collider để thực hiện hành động nào đó
        foreach (Collider collider in colliders)
        {
            // Kiểm tra nếu collider có tag là "Cube" them vao list
            if (collider.gameObject.CompareTag(cubeTag)){

                //Debug.Log("Found cube: " + collider.gameObject.name);
                AddItemToArray(collider.gameObject);
            }
        }
    }


    // them 1 object vao list pick
    public void AddItemToArray(GameObject aobject) => boxPickList.Add(aobject);

    void RemoveItemToArray()
    {
        if (boxPickList.Count > 0)
        {
            // Xóa phần tử cuối cùng (hoặc phần tử tại index cụ thể)
            boxPickList.RemoveAt(boxPickList.Count - 1);
        }
    }
    public List<GameObject> GetList()
    {
        return boxPickList;
    }
}
