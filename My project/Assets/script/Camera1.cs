using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public float moveSpeed = 10f;
    [SerializeField] private GameObject camera1;
    // Start is called before the first frame update
   
    

    void Update()
    {
        // Lấy giá trị đầu vào từ phím D
        float zInput = Input.GetKey(KeyCode.D) ? 1 : 0;
        // Di chuyển camera theo chiều Z
        Vector3 newPosition = camera1.transform.position + new Vector3(0, 0, zInput * moveSpeed * Time.deltaTime);
        camera1.transform.position = newPosition;

        float zInputa = Input.GetKey(KeyCode.A) ? 1 : 0;
        // Di chuyển camera theo chiều Z
        Vector3 newPositiona = camera1.transform.position - new Vector3(0, 0, zInputa * moveSpeed * Time.deltaTime);
        camera1.transform.position = newPositiona;


    }
}
