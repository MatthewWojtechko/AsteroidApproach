using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionWithKey : MonoBehaviour
{
    public string nextScene;
    public float seconds;

    bool transTime = false;
    void Awake()
    {
        StartCoroutine(wait());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && transTime)
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(seconds);
        transTime = true;
    }
}
