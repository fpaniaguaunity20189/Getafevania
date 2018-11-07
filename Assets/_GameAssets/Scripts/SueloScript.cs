using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            Vector2 position = GameController.GetPosition();
            collision.gameObject.transform.position = position;
        }
    }
}
