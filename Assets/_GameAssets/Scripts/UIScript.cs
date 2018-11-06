using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public Image prefabImagenVida;
    public GameObject panelVidas;
    public MarcianoScript ms;
    private int numeroVidas;
    //Image nuevaImage;
    Image[] imagenesVida;
	void Start () {
        numeroVidas = ms.GetVidas();
        imagenesVida = new Image[numeroVidas];

        for (int i = 0; i < imagenesVida.Length; i++) {
            imagenesVida[i] = Instantiate(prefabImagenVida, panelVidas.transform);
        }
	}

}
