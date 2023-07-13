using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSetMaster : MonoBehaviour
{
    public GameObject[] asteroidTypes;
    [Header("Big Asteroid")]
    public float bigMinScale;
    public float bigMaxScale;
    [Header("Small Asteroids")]
    public int numSmall;
    public float smallMinScale;
    public float smallMaxScale;
    public float leftmostXStart;
    [Tooltip("Spawning goes from left to right.")]
    public float rightmostXStart;
    [Tooltip("Spawning goes from left to right.")]
    public float smallMinXSpace;
    public float smallMaxXSpace;
    public float smallMinYOffset;
    public float smallMaxYOffset;
    public float smallMinZOffset;
    public float smallMaxZOffset;

    [Header("Particles")]
    public ParticleSystem bigExplosionPart;
    public ParticleSystem smallExplosionPart;
    public ParticleSystem shieldExplosionPart;


    public GameObject bigAsteroid;
    public GameObject[] smallAsteroids;

    public int currNumBig;
    public int currNumSmall;

    public AudioSource finalHitSFX; // play when big asteroid finally explodes
    public AudioSource normalHitSFX;

    private Vector3 bigPosition;

    private Spawner spawnScript;


    void Awake()
    {
        spawnScript = GameObject.FindWithTag("SceneManager").GetComponent<Spawner>();
        spawnBig();
        smallAsteroids = new GameObject[numSmall];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bigAsteroid != null)
            bigPosition = bigAsteroid.transform.position;
    }

    private void spawnBig()
    {
        bigAsteroid = Instantiate(asteroidTypes[Random.Range(0, asteroidTypes.Length)]);
        bigAsteroid.SetActive(false);
        bigAsteroid.transform.localScale = new Vector3(Random.Range(bigMinScale, bigMaxScale),
                                            Random.Range(bigMinScale, bigMaxScale),
                                            Random.Range(bigMinScale, bigMaxScale));
        bigAsteroid.transform.localPosition = this.transform.position;

        AsteroidHit bigScript = bigAsteroid.GetComponent<AsteroidHit>();
        bigScript.master = this;
        bigScript.isBig = true;
        bigScript.health = 5;
        bigScript.numSmall = numSmall;
        bigScript.fullySpawned = true;


        currNumBig++;
        spawnScript.currNumSmallAsteroids += numSmall;
        StartCoroutine(activateWait(bigAsteroid));
    }

    public void spawnSmalls()
    {
        bigExplosionPart.transform.position = bigPosition;
        bigExplosionPart.Play();

        float startX = Random.Range(bigPosition.x + leftmostXStart, bigPosition.x + rightmostXStart);   // where to spawn the next asteroid

        float tempXSpace, tempYOff, tempZOff;   // to tweak the positions of each asteroid so it seems real

        // create proper numSmallber of asteroids
        for (int i = 0; i < numSmall; i++)
        {
            currNumSmall++;

            smallAsteroids[i] = Instantiate(asteroidTypes[Random.Range(0, asteroidTypes.Length)]);
            smallAsteroids[i].SetActive(false);

            smallAsteroids[i].transform.localScale = new Vector3(Random.Range(smallMinScale, smallMaxScale),
                                            Random.Range(smallMinScale, smallMaxScale),
                                            Random.Range(smallMinScale, smallMaxScale));

            tempYOff = Random.Range(smallMinYOffset, smallMaxYOffset);
            tempZOff = Random.Range(smallMinZOffset, smallMaxZOffset);
            
            smallAsteroids[i].transform.position = new Vector3(startX, bigPosition.y + tempYOff, bigPosition.z + tempZOff);

            AsteroidHit smallScript = smallAsteroids[i].GetComponent<AsteroidHit>();
            smallScript.master = this;
            smallScript.isBig = false;
            smallScript.health = 1;
            smallScript.fullySpawned = true;

            StartCoroutine(activateWait(smallAsteroids[i]));


            startX += Random.Range(smallMinXSpace, smallMaxXSpace);
        }
    }

    public void playSmallExplosionPart(Vector3 p)
    {
        smallExplosionPart.transform.position = p;
        smallExplosionPart.Play();
    }

    public void playBigExplosionPart(Vector3 p)
    {
        bigExplosionPart.transform.position = p;
        bigExplosionPart.Play();
    }

    public void playSheildExplosionPart(Vector3 p)
    {
        shieldExplosionPart.transform.position = p;
        shieldExplosionPart.Play();
    }

    public void decreaseSmallAmnt()
    {
        currNumSmall--;
        spawnScript.currNumSmallAsteroids--;

        //if (currNumBig <= 0 && currNumSmall <= 0)
        //    GameObject.Destroy(this.gameObject);
    }

    public void decreaseBigAmnt()
    {
        currNumBig--;
        spawnScript.currNumBigAst--;
    }

    IEnumerator activateWait(GameObject g)
    {
        yield return new WaitForSeconds(0);
        g.SetActive(true);
    }
}
