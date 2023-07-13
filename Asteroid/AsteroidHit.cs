using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AsteroidHit : MonoBehaviour
{
    public int health;
    public bool isBig;
    public int numSmall;
    public AsteroidMovement moveScript;
    public AsteroidSetMaster master;
    public ParticleSystem particles;
    public bool fullySpawned;
    public Material material;


    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        if (!isBig)
        {
            material.color = Color.yellow;
        }
    }

        // Update is called once per frame
        void Update()
    {
        if (fullySpawned)   // prevents error from yet initialized variables
        {
            if (health < 1)
            {
                if (isBig)
                {
                    master.spawnSmalls();
                    master.decreaseBigAmnt();
                    // SFX
                }
                else
                {
                    master.decreaseSmallAmnt();
                    // small explosion
                    // SFX
                }
                GameObject.Destroy(this.gameObject);//
            }
        }
    }

    public void hit()
    {
        if (health > 1)
            master.normalHitSFX.Play();
        else
            master.finalHitSFX.Play();

        health--;
        moveScript.startRetreat();

        if (!isBig)
            master.playSmallExplosionPart(transform.position);
        else
        {
            particles.Play();
            if (health == 1)
            {
                material.color = Color.red;
            }
        }
    }

    public void shieldCollision()
    {
        // particle effect
        // sfx
        master.playSheildExplosionPart(transform.position);

        master.decreaseBigAmnt();
        for (int i = 0; i < numSmall; i++)
        {
            master.decreaseSmallAmnt();
        }

        GameObject.Destroy(this.gameObject);
    }
}
