using UnityEngine;
using System.Collections;

public class TerrainBlock : MonoBehaviour {

    float blockDistance = .32f;

    protected bool CheckBlockAbove() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, (transform.position.y + blockDistance)), .02f, 1010);

        switch (cols.Length) {
            case 0:
                return false;

            default:
                return true;
        }
    }

    protected bool CheckBlockBellow() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, (transform.position.y - blockDistance)), .02f, 1010);

        switch (cols.Length) {
            case 0:
                return false;

            default:
                return true;
        }
    }

}