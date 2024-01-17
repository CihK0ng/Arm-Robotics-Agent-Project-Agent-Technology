using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTwoArmCollider : MonoBehaviour
{
    public float touchRadius = 0.5f;
    public bool isarmcollider = false;
    [SerializeField] private GameObject _fork;

    
    public void CheckTwoArmCollider()
    {
        // Kiểm tra sự chạm giữa tay robot và các đối tượng khác
        Collider[] colliders = Physics.OverlapSphere(_fork.transform.position, touchRadius);

        foreach (Collider collider in colliders)
        {
            // Kiểm tra xem có phải tay robot khác không
            if (collider.CompareTag("HandRobot") && collider != this.GetComponent<Collider>())
            {
                // Hai tay robot đang chạm nhau
                Debug.Log("Hands touched!");
                isarmcollider = true;
            }
        }
    }
    public bool Getvaluecollider()
    {
        if (isarmcollider == true)
        {
            return true;
        }else return false;
    }
    public void  resetvalue()
    {
        isarmcollider = false;
    }
}
