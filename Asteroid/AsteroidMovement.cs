using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Vector3 center;
    //public float xRandomOffset;
    //public float yRandomOffset;
    public float speed;
    public bool retreat;
    public float retreatSpeed;
    public float retreatDuration;
    public float speedupLerpConst;
    public float normRotate;
    public float retreatRotate;
    public float rotateLerpConst;

    private Vector3 destination;
    private bool speedup;
    private float currentSpeed;
    private float currentRotate;

    void Awake()
    {
        //float xRand = Random.Range(0, xRandomOffset);
        //if (Random.Range(0, 2) == 0)
        //    xRand *= -1;

        //float yRand = Random.Range(0, yRandomOffset);
        //if (Random.Range(0, 2) == 0)
        //    yRand *= -1;
    }
    // Start is called before the first frame update
    void Start()
    {
        setRotate(normRotate);
    }

    // Update is called once per frame
    void Update()
    {
        //if (retreat)
        //{
        //    if (!speedup)
        //        currentSpeed = -1 * retreatSpeed;
        //    else
        //    {
        //        currentSpeed = Mathf.Lerp(currentSpeed, speed, speedupLerpConst);
        //        currentRotate = Mathf.Lerp(currentRotate, normRotate, rotateLerpConst);
        //        setRotate(currentRotate);
        //        if (currentRotate > normRotate - 0.1f)
        //            currentRotate = normRotate;
        //        if (currentSpeed > speed - 0.1f)
        //            currentSpeed = speed;

        //        if (currentSpeed == speed && currentRotate == normRotate)
        //            retreat = false;
        //    }
        //}
        //else
        //{
        //    currentSpeed = speed;
        //}

        if (retreat)
            currentSpeed = -1 * retreatSpeed;
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed, speedupLerpConst);

            //currentRotate = Mathf.Lerp(currentRotate, normRotate, rotateLerpConst);
            //setRotate(currentRotate);
            //if (currentRotate > normRotate && currentRotate < normRotate + 0.1f)
            //    currentRotate = normRotate;

            if (currentSpeed > speed - 0.1f)
            {
                currentSpeed = speed;
                setRotate(normRotate);

                
            }
        }
            transform.position = Vector3.MoveTowards(transform.position, center,
                currentSpeed * Time.deltaTime);
    }

    public void startRetreat()
    {
        //speedup = false;
        retreat = true;
        StartCoroutine(waitForSpeedup());
    }

    IEnumerator waitForSpeedup()
    {
        setRotate(retreatRotate);
        yield return new WaitForSeconds(retreatDuration);
        speedup = true;
        retreat = false;

    }

    // Code taken from Unity's RandomRotator script from free asteroid assets.
    private void setRotate(float r)
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * r;
        currentRotate = r;
    }
}
