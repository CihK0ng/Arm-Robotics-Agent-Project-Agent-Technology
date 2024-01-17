using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandRobot"))
        {
            // Chai nước chạm vào tay robot, thực hiện các hành động cần thiết
            Debug.Log("Robot hand touched the water bottle.");
            // Lấy thông tin cần thiết từ chai nước (ví dụ: mức nước, loại nước, ...)
        }
    }
  
}
