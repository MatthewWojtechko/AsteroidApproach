using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                AudioListener.volume = 0;
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.volume = 1;
            }
        }
    }
}
