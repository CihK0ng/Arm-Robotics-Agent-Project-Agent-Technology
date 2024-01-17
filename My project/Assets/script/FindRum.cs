using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FindRum : MonoBehaviour
{
    [SerializeField]  private float radius = 20f;
    [SerializeField] private GameObject fixedObject;
    private string rumtarget = "Rum";
    private Collider RumObject; 

    // Start is called before the first frame update
   
    public void FindRumAound()
    {

         Collider[] cylinders = Physics.OverlapSphere(fixedObject.transform.position, radius);
        foreach (Collider c in cylinders)
        {
            //Debug.Log(c.name + c.transform.localPosition);
            if (c.name == rumtarget)
            {
                RumObject = c;
            }
            
        }

    }

    public string Getgameobj()
    {
        if (RumObject != null && RumObject.gameObject != null)
        {
            return RumObject.gameObject.name;
        }
        else
        {
            Debug.LogError("RumObject or RumObject.gameObject is null.");
            return "Undefined";
        }
    }



}
