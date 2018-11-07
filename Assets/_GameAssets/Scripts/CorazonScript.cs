using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonScript : MonoBehaviour {
    public int salud = 50;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            collision.gameObject.GetComponent<MarcianoScript>().RecibirSalud(salud);
            Destroy(gameObject);
        }
    }
}
