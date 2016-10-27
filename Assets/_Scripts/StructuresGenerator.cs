using UnityEngine;
using System.Collections;

public class StructuresGenerator : MonoBehaviour {

    [SerializeField]
    bool generateAnyStructure = true;

    [SerializeField]
    bool exit = true;

    [SerializeField]
    GameObject exitTeleporter;

    int gridX;
    int gridY;
    float blockDistance = .32f;
    void Start () {
        if (generateAnyStructure)
            TerrainGenerator.followUp += GenerateStructures;
	}

    void GenerateStructures(int GridX, int GridY) {
        gridX = GridX;
        gridY = GridY;

        if (exit)
            GenerateExit();
    }

    void GenerateExit() {
        Vector2 vector = new Vector2((Random.Range(0, gridX) * blockDistance ), (Random.Range(0, gridY) * blockDistance));
        RaycastHit2D[] rayHit = Physics2D.LinecastAll(  vector- new Vector2(blockDistance,0),
                                                        vector + new Vector2(blockDistance,0));
        RaycastHit2D lineHit = Physics2D.Linecast(  new Vector2(vector.x - blockDistance, vector.y + blockDistance),
                                                    new Vector2(vector.x + blockDistance, vector.y + blockDistance));
        if (rayHit.Length >= 4 && lineHit.collider == null) {
            rayHit[0].collider.gameObject.name = "Teleporter Spawn";
            Instantiate(exitTeleporter, vector + new Vector2(0,blockDistance), Quaternion.Euler(0, 0, 0));
        }
        else {
            GenerateExit();
        }
    }
}
