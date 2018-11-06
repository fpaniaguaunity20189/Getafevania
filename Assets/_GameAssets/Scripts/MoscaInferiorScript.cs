using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoscaInferiorScript : MonoBehaviour {
    public int danyo = 20;
    private void OnCollisionEnter2D(Collision2D collision) {
        print("ONCOLLISIONENTER2D - MOSCA INFERIOR");
        if (collision.gameObject.name == "Player") {
            collision.gameObject.GetComponent<MarcianoScript>().RecibirDanyo(danyo);
        }
    }
}
