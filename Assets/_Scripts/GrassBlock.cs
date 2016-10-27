using UnityEngine;
using System.Collections;

public class GrassBlock : TerrainBlock {

    [SerializeField]
    Sprite spriteWithGrass;

    void Start() {
        if (!CheckBlockAbove()) 
            GetComponent<SpriteRenderer>().sprite = spriteWithGrass;
    }
}