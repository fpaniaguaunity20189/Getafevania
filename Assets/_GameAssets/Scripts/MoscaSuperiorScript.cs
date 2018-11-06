using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoscaSuperiorScript : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        print("ONCOLLISIONENTER2D - PARTE SUPERIOR MOSCA");
        Destroy(transform.parent.gameObject);
    }
}
