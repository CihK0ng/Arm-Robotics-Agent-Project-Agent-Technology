using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PickAndPlace : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pickObj;
    public GameObject bodyType;
    bool a = false;

    public void OnPick()
    {
        StartCoroutine(X());
    }

    IEnumerator X()
    {
        while (true)
        {
            pickObj.transform.position = bodyType.transform.position;
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (a == true)
        {
            OnPick();
            Debug.Log("calll in updatea");
        }

    }
}
