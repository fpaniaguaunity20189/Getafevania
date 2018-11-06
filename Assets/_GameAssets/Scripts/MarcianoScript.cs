using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarcianoScript : MonoBehaviour {

    enum EstadoPlayer { Pausa, AndandoDer, AndandoIzq, Saltando, Sufriendo };
    EstadoPlayer estado = EstadoPlayer.Pausa;

    [SerializeField] LayerMask floorLayer;
    [SerializeField] Transform posPies;
    [SerializeField] Text txtPuntuacion;
    [SerializeField] Text txtSalud;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 1;

    int vidasMaximas = 4;
    [SerializeField] int vidas;
    int saludMaxima = 100;
    [SerializeField] int salud;


    [SerializeField] int puntos = 0;
    [SerializeField] float radioOverlap = 0.1f;
    Animator playerAnimator;
    Rigidbody2D rb2D;

    public float fuerzaImpactoX = 4;
    public float fuerzaImpactoY = 4;

    private void Awake() {
        vidas = vidasMaximas;
        salud = saludMaxima;
    }

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        txtPuntuacion.text = "Score:" + puntos.ToString();
        txtSalud.text = "Health:" + salud.ToString();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            estado = EstadoPlayer.Saltando;
        }
        if (estado==EstadoPlayer.Sufriendo && EstaEnElSuelo()) {
            estado = EstadoPlayer.Pausa;
        }
    }

    void FixedUpdate() {
        float xPos = Input.GetAxis("Horizontal");
        float ySpeedActual = rb2D.velocity.y;
        
        if (estado==EstadoPlayer.Sufriendo) {
            return;
        }

        if (Mathf.Abs(xPos) > 0.01f) {
            playerAnimator.SetBool("andando", true);
        } else {
            playerAnimator.SetBool("andando", false);
        }

        if (estado == EstadoPlayer.Saltando) {
            estado = EstadoPlayer.Pausa;
            if (EstaEnElSuelo()) {
                rb2D.velocity = new Vector2(xPos * speed, jumpForce);
            } else {
                rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            }
        } else if (xPos > 0.01f) {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            if (estado == EstadoPlayer.AndandoIzq) {
                transform.localScale = new Vector2(1, 1);
            }
            estado = EstadoPlayer.AndandoDer;
        } else if (xPos < -0.01f) {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            if (estado == EstadoPlayer.AndandoDer) {
                transform.localScale = new Vector2(-1, 1);
            }
            estado = EstadoPlayer.AndandoIzq;
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

    public void RecibirDanyo(int danyo) {
        salud = salud - danyo;
        if (salud <= 0) {
            vidas--;
            salud = saludMaxima;
        }
        if (estado == EstadoPlayer.AndandoDer) {
            GetComponent<Rigidbody2D>().AddRelativeForce(
            new Vector2(-fuerzaImpactoX, fuerzaImpactoY), ForceMode2D.Impulse);
            estado = EstadoPlayer.Sufriendo;
        } else if (estado == EstadoPlayer.AndandoIzq) {
            GetComponent<Rigidbody2D>().AddRelativeForce(
            new Vector2(fuerzaImpactoX, fuerzaImpactoY), ForceMode2D.Impulse);
            estado = EstadoPlayer.Sufriendo;
        }
        txtSalud.text = "Health:" + salud.ToString();
    }

    public int GetVidas() {
        return this.vidas;
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
