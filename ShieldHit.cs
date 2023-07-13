using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShieldHit : MonoBehaviour
{
    public int maxHealth = 6;
    public int health = 6;
    public Animator healthBar;
    public ParticleSystem diePart;
    public AudioSource normalHitSFX;
    public AudioSource lastHitSFX;
    public Shoot shootScript;
    public ReticleMovement moveScript;

    private List<GameObject> hits;

    void Awake()
    {
        hits = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetInteger("health", health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            if (!hits.Contains(col.gameObject))
            {
                hits.Add(col.gameObject);
                
                AsteroidHit ahScript = col.gameObject.GetComponent<AsteroidHit>();
                if (ahScript.isBig)
                    health -= 2;
                else
                    health--;

                ahScript.shieldCollision();

                healthBar.SetInteger("health", health);


                if (health > 0)
                    normalHitSFX.Play();
                else
                {
                    lastHitSFX.Play();
                    diePart.Play();
                    shootScript.enabled = false;
                    moveScript.enabled = false;
                    StartCoroutine(transitionScene());
                }
            }
        }
    }

    IEnumerator transitionScene()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
    }
}
