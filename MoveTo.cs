using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(g.transform.position.x, g.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(g.transform.position.x, g.transform.position.y, this.transform.position.z);
    }
}
