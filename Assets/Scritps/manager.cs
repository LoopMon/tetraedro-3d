using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour {

    public GameObject tetrahedron; // prefab da camrera
    public GameObject[] vetGameObj = new GameObject[24];
    GameObject pai;
    Vector3 m_Center;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 24; i++) {
            if (i == 0) {
                vetGameObj[i] = Instantiate(tetrahedron, new Vector3(0, 0, 0), Quaternion.identity); // tetraedro base
            }
            else { 
                vetGameObj[i] = Instantiate(
                    tetrahedron, 
                    new Vector3(vetGameObj[i-1].transform.position.x + 1, 0, 0), 
                    vetGameObj[i-1].transform.rotation //i-1 posicao anterior
                );
            }
        }

        // BASE
        vetGameObj[3].transform.position = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[4].transform.position = new Vector3(1.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[5].transform.position = new Vector3(1f, 0, 2*Mathf.Sqrt(0.75f));

        // MEIO
        vetGameObj[6].transform.position = new Vector3(0.5f, Mathf.Sqrt(0.75f), 0.29f);
        vetGameObj[7].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), 0.29f);
        vetGameObj[8].transform.position = new Vector3(1f, Mathf.Sqrt(0.75f), 1.15f);

        // TOPO
        vetGameObj[9].transform.position = new Vector3(1f, 2*Mathf.Sqrt(0.75f), 2*0.29f);

        // BASE MEIOS
        vetGameObj[10].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), 0.29f);
        vetGameObj[10].transform.Rotate(37f, 0f, 180f);
        vetGameObj[11].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), 0.29f);
        vetGameObj[11].transform.Rotate(37f, 0f, 180f);






        // importante para manipular os seus filhos
        //pai = new GameObject(); 
        //pai.transform.position = new Vector3(0,1,0); //pivo
        //pai.transform.position = new Vector3(0, 1, 0); //pivo
        //vetGameObj[3].transform.parent = pai.transform;
        //vetGameObj[3].transform.bounds
    }

	// Update is called once per frame
	void Update () {

	}
}
