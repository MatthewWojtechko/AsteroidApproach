using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject AstSet;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    public int targetNumBigAst;
    public int cooldownNumAst;
    public int minNumSmallSpawn;
    public int maxNumSmallSpawn;
    public int wave;

    public int currNumSmallAsteroids;
    public int currNumBigAst;

    public bool targetCurrentlyReached = false;
    public int doneSpawning = 0; // will equal 0 after done spawning a certain amount

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!targetCurrentlyReached)  // if we need more
        {
            targetCurrentlyReached = true;
            doneSpawning = targetNumBigAst - currNumBigAst;
            for (currNumBigAst = currNumBigAst; currNumBigAst < targetNumBigAst; currNumBigAst++)
            {
                StartCoroutine(waitSpawn());
            }
        }
        else if (doneSpawning == 0 && currNumSmallAsteroids <= cooldownNumAst) // else we don't..... is it time for more yet?
        {
            targetCurrentlyReached = false;
            incrementDifficulty();
        }
    }

    void spawn()
    {
        Instantiate(AstSet, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ)), Quaternion.identity);
        doneSpawning--;
    }

    void incrementDifficulty()
    {
        wave++;

        targetNumBigAst = (int)(wave / 2.0f);
        if (targetNumBigAst < 3)
            targetNumBigAst = 3;
        else if (targetNumBigAst > 8)
            targetNumBigAst = 8;

        if (wave < 8)
            cooldownNumAst = 6;
        else
            cooldownNumAst = 9;


        minNumSmallSpawn = (int)(wave * 0.3f);
        if (minNumSmallSpawn < 1)
            minNumSmallSpawn = 2;
        else if (minNumSmallSpawn > 3)
            minNumSmallSpawn = 4;

        maxNumSmallSpawn = (int)(wave * 0.5f);
        if (maxNumSmallSpawn < 2)
            maxNumSmallSpawn = 2;
        else if (maxNumSmallSpawn > 5)
            maxNumSmallSpawn = 5;
    }

    IEnumerator waitSpawn()
    {
        yield return new WaitForSeconds(Random.Range(1, 2.5f));
        spawn();
    }
}
