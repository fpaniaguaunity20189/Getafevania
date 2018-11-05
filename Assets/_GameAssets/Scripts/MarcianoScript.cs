using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarcianoScript : MonoBehaviour {
    [SerializeField] LayerMask floorLayer;
    [SerializeField] Transform posPies;
    [SerializeField] Text txtPuntuacion;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 1;
    int vidasMaximas = 3;
    [SerializeField] int vidas;
    [SerializeField] int puntos = 0;
    [SerializeField] float radioOverlap = 0.1f;
    Animator playerAnimator;
    Rigidbody2D rb2D;
    bool saltando = false;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        txtPuntuacion.text = "Score:" + puntos.ToString();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            saltando = true;
        }
    }

    void FixedUpdate() {
        float xPos = Input.GetAxis("Horizontal");
        float ySpeedActual = rb2D.velocity.y;

        if (Mathf.Abs(xPos) > 0.01f) {
            playerAnimator.SetBool("andando", true);
        } else {
            playerAnimator.SetBool("andando", false);
        }

        if (saltando) {
            saltando = false;
            if (EstaEnElSuelo()) {
                rb2D.velocity = new Vector2(xPos * speed, jumpForce);
            } else {
                rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            }
        } else if (Mathf.Abs(xPos) > 0.01f) {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
        }
    }

    public void IncrementarPuntuacion(int puntosAIncrementar) {
        puntos = puntos + puntosAIncrementar;
        txtPuntuacion.text = "Score:" + puntos.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Moneda")) {
            IncrementarPuntuacion(1);
            Destroy(collision.gameObject);
        }
    }

    private bool EstaEnElSuelo() {
        bool enSuelo = false;
        Collider2D colider = Physics2D.OverlapCircle(posPies.position, radioOverlap, floorLayer);
        if (colider != null) {
            enSuelo = true;
        }
        return enSuelo;
    }

    /*
    //Version basada en TAG y utilizando OverlapCircleAll
    private bool EstaEnElSuelo() {
    bool enSuelo = false;
    Collider2D[] cols = Physics2D.OverlapCircleAll(posPies.position, radioOverlap);
    for (int i = 0; i < cols.Length; i++) {
    if (cols[i].gameObject.tag == "Suelo") {
    enSuelo = true;
    break;
    }
    }
    return enSuelo;
    }
    */
}
