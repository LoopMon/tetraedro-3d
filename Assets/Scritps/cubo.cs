using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // Rotação no eixo X e Y local com velocidade controlada
        transform.Rotate(Vector3.right * 15 * Time.deltaTime);
        transform.Rotate(Vector3.up * 15 * Time.deltaTime, Space.World);

        // Rotação ao redor de um ponto específico
        Vector3 point = new Vector3(0, 1, 0); // Definir ponto de rotação
        transform.RotateAround(point, Vector3.up, 15 * Time.deltaTime);
    }
}
