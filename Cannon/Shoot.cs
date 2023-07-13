using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{

    public float cooldown;
    public TurretShootPoint[] shootPoints;
    public ParticleSystem bangPart;
    public int score;
    public int hitPoints;
    public Text scoreText;
    public AudioSource SFX;

    private bool hitAlready = false;
    private bool cooldownOver = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit rayHit;

        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 200, Color.white);

        if (Input.GetKey("return") && cooldownOver)
            shoot();
    }

    void shoot()
    {
        SFX.Play();
        bangPart.Play();
        cooldownOver = false;
        StartCoroutine(cooldownWait());
        foreach (TurretShootPoint S in shootPoints)
        {
            if (!this.hitAlready)
            {
                S.raycast();
                if (S.hasHit)
                {
                    AsteroidHit hitAstScript = S.objectHit.GetComponent<AsteroidHit>();
                    if (hitAstScript != null && hitAstScript.fullySpawned == true)
                    {
                        if (hitAstScript.health > 0)
                        {
                            hitAstScript.hit();
                            S.hasHit = false;
                            score += hitPoints;
                            scoreText.text = "" + score;
                            this.hitAlready = true;

                            Constants.S.score = score;
                        }
                    }
                }
            }
            else
            {
                S.hasHit = false;
            }
        }
        this.hitAlready = false;
    }

    IEnumerator cooldownWait()
    {
        yield return new WaitForSeconds(cooldown);
        cooldownOver = true;
    }
}
