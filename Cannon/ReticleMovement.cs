using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMovement : MonoBehaviour
{
    public Vector3 startPos;
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;
    public float startSpeed;
    public float speedUpLerpConst;
    public float slowDownLerpConst;
    public float maxSpeed;
    public float rotateXMax;
    public float rotateYMax;
    public AudioSource moveSound;

    private float currentXSpeed;
    private float currentYSpeed;
    private Vector3 movementVector;
    private bool dirRight;
    private bool dirTop;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;

    }

    // Update is called once per frame
    void Update()
    {
        movement();
        this.transform.position += movementVector;
        rotateAim();

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            if (!moveSound.isPlaying)
                moveSound.Play();
        }
        else
            moveSound.Stop();
    }

    void movement()
    {
        float yMove = 0, xMove = 0;

        // Find x movement
        if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            if (currentXSpeed < 0)
                currentXSpeed = 0;
            else if (currentXSpeed < maxSpeed - 0.1f)
                currentXSpeed = Mathf.Lerp(currentXSpeed, maxSpeed, speedUpLerpConst);
            else
                currentXSpeed = maxSpeed;
        }
        else if (Input.GetKey("a"))
        {
            if (currentXSpeed > 0)
                currentXSpeed = 0;
            else if (currentXSpeed > -1 * maxSpeed + 0.1f)
                currentXSpeed = Mathf.Lerp(currentXSpeed, -1 * maxSpeed, speedUpLerpConst);
            else
                currentXSpeed = -1 * maxSpeed;
        }
        else
        {
            if (currentXSpeed < 0.1f && currentXSpeed > -0.1f)
                currentXSpeed = 0;
            else
                currentXSpeed = Mathf.Lerp(currentXSpeed, 0, slowDownLerpConst);
        }

        // Find y movement
        if (Input.GetKey("w") && !Input.GetKey("s"))
        {
            if (currentYSpeed < 0)
                currentYSpeed = 0;
            else if (currentYSpeed < maxSpeed - 0.1f)
                currentYSpeed = Mathf.Lerp(currentYSpeed, maxSpeed, speedUpLerpConst);
            else
                currentYSpeed = maxSpeed;
        }
        else if (Input.GetKey("s"))
        {
            if (currentYSpeed > 0)
                currentYSpeed = 0;
            else if (currentYSpeed > -1 * maxSpeed + 0.1f)
                currentYSpeed = Mathf.Lerp(currentYSpeed, -1 * maxSpeed, speedUpLerpConst);
            else
                currentYSpeed = -1 * maxSpeed;
        }
        else
        {
            if (currentYSpeed < 0.1f && currentYSpeed > -0.1f)
                currentYSpeed = 0;
            else
                currentYSpeed = Mathf.Lerp(currentYSpeed, 0, slowDownLerpConst);
        }

        xMove = currentXSpeed * Time.deltaTime;
        yMove = currentYSpeed * Time.deltaTime;

        xMove = boundMovement(xMove, this.transform.position.x, leftBound, rightBound);
        yMove = boundMovement(yMove, this.transform.position.y, bottomBound, topBound);

        movementVector = new Vector3(xMove, yMove, 0);
    }

    float boundMovement(float movement, float pos, float lowLimit, float hiLimit)
    {
        if (movement + pos > hiLimit)
            return Mathf.Abs(hiLimit - pos);
        else if (movement + pos < lowLimit)
            return -1 * Mathf.Abs(lowLimit - pos);
        else
            return movement;
    }

    // angles the gameobject so that its forward vector is parallel with the proper camera field of view angle
    void rotateAim()
    {
        float yAngle = rotateXMax * (transform.localPosition.x / Mathf.Abs(rightBound));
        float xAngle = -1 * rotateYMax * ((transform.localPosition.y-1) / Mathf.Abs(topBound-1));
        transform.eulerAngles = new Vector3(xAngle, yAngle, 0);
    }
}
