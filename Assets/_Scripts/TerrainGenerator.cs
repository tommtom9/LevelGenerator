using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour {
    int gridX = 30;
    [SerializeField]
    int gridY = 100;

    [SerializeField]
    int gridXMin = 40;
    [SerializeField]
    int gridXMax = 200;

    [SerializeField]
    float groundFrequency = 10;
    float randomFrequency;

    [SerializeField]
    float massSpawnFrequency = 10;

    float blockDistance = .32f;

    [SerializeField]
    bool fillHoles = true;

    [SerializeField]
    int fillFrequency = 6;

    [SerializeField]
    GameObject GroundPrefab;

    public delegate void FollowUp(int gridX,int gridY);
    public static event FollowUp followUp;

    void Start() {
        generateLevel();
    }

    void generateLevel() {
        gridX = Random.Range(gridXMin, gridXMax);

        for (int x = 0;x< gridX;x++) {
            Vector3 vectorX = new Vector3(blockDistance * x, 0);
            randomizeNumbers(checkAroundBlock(vectorX));

            if (randomFrequency <= groundFrequency)
                Instantiate(GroundPrefab, vectorX, Quaternion.Euler(Vector3.zero));

            for (int y = 0; y < gridY; y++) {
                Vector3 vectorY = new Vector3(blockDistance * x, blockDistance * y);
                randomizeNumbers(checkAroundBlock(vectorY));

                if (randomFrequency <= groundFrequency)
                    Instantiate(GroundPrefab, vectorY, Quaternion.Euler(Vector3.zero));
            }
        }
        if(fillHoles)
            checkForHoles();
        if(followUp != null)
            followUp(gridX,gridY);
    }

    void checkForHoles() {
        for (int x = 0; x < gridX; x++) {
            Vector3 vectorX = new Vector3(blockDistance * x, 0);
            if(checkAroundBlock(vectorX) >= fillFrequency)
                Instantiate(GroundPrefab, vectorX, Quaternion.Euler(Vector3.zero));

            for (int y = 0; y < gridY; y++) {
                Vector3 vectorY = new Vector3(blockDistance * x, blockDistance * y);
                if (checkAroundBlock(vectorY) >= fillFrequency)
                    Instantiate(GroundPrefab, vectorY, Quaternion.Euler(Vector3.zero));
            }
        }   
    }

    void randomizeNumbers( int nextToBlockModifier ) {
        randomFrequency = Random.Range(0 - nextToBlockModifier * massSpawnFrequency, 100);
    }

    int checkAroundBlock(Vector3 v) {
        Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(v.x,v.y), .32f, 1010);
        return cols.Length;
    }

        bool isBlockOnThisPoint(Vector3 v) {
        RaycastHit2D[] cols = Physics2D.RaycastAll(new Vector2(v.x,v.y), Vector3.forward);
        switch (cols.Length) {
            case 0:
                //Debug.Log("case - " + cols.Length);
                return false;

            default:
                //Debug.Log("default - " + cols.Length);
                return true;

        }
    }
}