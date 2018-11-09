using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoscaScript : MonoBehaviour {
    Slider slider;
    public Transform limiteDerecho;
    public Transform limiteIzquierdo;
    bool haciaDerecha = false;
    private void Start() {
        transform.position = limiteDerecho.position;
        slider = GetComponentInChildren<Slider>();
        QuitarVida(50);
    }
    void Update () {
        if (haciaDerecha == true) {
            transform.Translate(Vector2.right * Time.deltaTime);
            if (transform.position.x > limiteDerecho.position.x) {
                haciaDerecha = false;
                CambiarOrientacion();
            }
        } else {
            transform.Translate(Vector2.left* Time.deltaTime);
            if (transform.position.x < limiteIzquierdo.position.x) {
                haciaDerecha = true;
                CambiarOrientacion();
            }
        }
	}
    private void CambiarOrientacion () {
        if (haciaDerecha) {
            transform.localScale = new Vector2(-1, 1);
        } else {
            transform.localScale = new Vector2(1, 1);
        }
    }
    private void QuitarVida(int vida) {
        slider.value = slider.value - vida;
    }
}
