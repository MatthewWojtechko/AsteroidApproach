using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Text lastScore;
    public Text highScore;
    public GameObject hsMsg;

    // Start is called before the first frame update
    void Start()
    {
        float hs = PlayerPrefs.GetInt("HighScore", 0);
        lastScore.text = "" + Constants.S.score;
        highScore.text = "" + hs;
        if (Constants.S.score > hs)
        {
            PlayerPrefs.SetInt("HighScore", Constants.S.score);
            hsMsg.SetActive(true);
        }
        else
        {
            hsMsg.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
