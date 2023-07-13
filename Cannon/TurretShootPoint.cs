using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootPoint : MonoBehaviour
{
    public GameObject objectHit;
    public bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 200, Color.white);

    }

    public void raycast()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit, Mathf.Infinity))
        {
            if (rayHit.collider.tag == "Asteroid")
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayHit.distance, Color.yellow);
                objectHit = rayHit.collider.gameObject;
                hasHit = true;
            }
        }
    }


}
