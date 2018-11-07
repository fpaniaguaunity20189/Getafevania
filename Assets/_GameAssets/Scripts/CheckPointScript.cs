using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            GameController.StorePosition(collision.gameObject.transform.position);
            Destroy(this.gameObject);
        }
    }
}
