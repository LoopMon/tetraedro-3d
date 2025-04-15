using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject tetrahedron; // prefab da camera
    public GameObject[] vetGameObj = new GameObject[24];
    private GameObject piramideBase;
    private GameObject piramideMeio;
    private GameObject piramideTopo;

    private bool canRotate = false;
    private bool alpha1 = true;
    private bool alpha2 = false;
    private bool alpha3 = false;
    private bool alpha4 = false;

    private Vector3 aux0;
    private Vector3 aux1;
    private Vector3 aux2;
    private Vector3 centroide;

    // Use this for initialization
    void Start()
    {
        CriarPiramix();
    }

    // Update is called once per frame
    void Update()
    {
        if (piramideBase != null && canRotate)
        {
            if (alpha1)
            {
                piramideBase.transform.Rotate(Vector3.up * Time.deltaTime * 50f);
                piramideMeio.transform.Rotate(Vector3.up * Time.deltaTime * -50f);
                piramideTopo.transform.Rotate(Vector3.up * Time.deltaTime * 25f);
            }

            if (alpha2)
            {
                RotateAroundFaceAxis(piramideBase, piramideTopo, 50f);
                RotateAroundFaceAxis(piramideMeio, piramideTopo, -50f);
                RotateAroundFaceAxis(piramideTopo, piramideBase, 25f);
            }

            if (alpha3)
            {
                RotateAroundFaceAxis(piramideBase, piramideTopo, 50f);
                RotateAroundFaceAxis(piramideMeio, piramideTopo, -50f);
                RotateAroundFaceAxis(piramideTopo, piramideBase, 25f);
            }
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            piramideBase.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideMeio.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideTopo.transform.rotation = Quaternion.Euler(0, 0, 0);
            for (int i = 0; i <= 22; i++)
            {
                vetGameObj[i].transform.parent = null;
            }
            // Calcular centro da Base
            aux0 = CalcularCentroideTetra(vetGameObj[0]);
            aux1 = CalcularCentroideTetra(vetGameObj[2]);
            aux2 = CalcularCentroideTetra(vetGameObj[5]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideBase.transform.position = centroide;
            for (int i = 0; i <= 14; i++)
            {
                vetGameObj[i].transform.parent = piramideBase.transform;
            }
            // Calcular centro do Meio
            aux0 = CalcularCentroideTetra(vetGameObj[15]);
            aux1 = CalcularCentroideTetra(vetGameObj[16]);
            aux2 = CalcularCentroideTetra(vetGameObj[17]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideMeio.transform.position = centroide;
            for (int i = 15; i <= 21; i++)
            {
                vetGameObj[i].transform.parent = piramideMeio.transform;
            }
            // Calcular centro do Topo
            aux0 = CalcularCentroideTetra(vetGameObj[22]);
            piramideTopo.transform.position = new Vector3(aux0[0], aux0[1], aux0[2]);
            vetGameObj[22].transform.parent = piramideTopo.transform;

            alpha1 = true;
            alpha2 = false;
            alpha3 = false;
            alpha4 = false;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            piramideBase.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideMeio.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideTopo.transform.rotation = Quaternion.Euler(0, 0, 0);
            for (int i = 0; i <= 22; i++)
            {
                vetGameObj[i].transform.parent = null;
            }
            // Calcular centro da Base
            aux0 = CalcularCentroideTetra(vetGameObj[0]);
            aux1 = CalcularCentroideTetra(vetGameObj[22]);
            aux2 = CalcularCentroideTetra(vetGameObj[5]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideBase.transform.position = centroide;
            // BASE
            vetGameObj[0].transform.parent = piramideBase.transform;
            vetGameObj[3].transform.parent = piramideBase.transform;
            vetGameObj[5].transform.parent = piramideBase.transform;
            vetGameObj[6].transform.parent = piramideBase.transform;
            vetGameObj[8].transform.parent = piramideBase.transform;
            vetGameObj[9].transform.parent = piramideBase.transform;
            vetGameObj[12].transform.parent = piramideBase.transform;
            vetGameObj[13].transform.parent = piramideBase.transform;
            vetGameObj[14].transform.parent = piramideBase.transform;
            vetGameObj[15].transform.parent = piramideBase.transform;
            vetGameObj[17].transform.parent = piramideBase.transform;
            vetGameObj[18].transform.parent = piramideBase.transform;
            vetGameObj[19].transform.parent = piramideBase.transform;
            vetGameObj[20].transform.parent = piramideBase.transform;
            vetGameObj[21].transform.parent = piramideBase.transform;
            vetGameObj[22].transform.parent = piramideBase.transform;

            // Calcular centro do Meio
            aux0 = CalcularCentroideTetra(vetGameObj[1]);
            aux1 = CalcularCentroideTetra(vetGameObj[16]);
            aux2 = CalcularCentroideTetra(vetGameObj[4]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideMeio.transform.position = centroide;
            // MEIO
            vetGameObj[1].transform.parent = piramideMeio.transform;
            vetGameObj[4].transform.parent = piramideMeio.transform;
            vetGameObj[7].transform.parent = piramideMeio.transform;
            vetGameObj[10].transform.parent = piramideMeio.transform;
            vetGameObj[11].transform.parent = piramideMeio.transform;
            vetGameObj[16].transform.parent = piramideMeio.transform;

            // Calcular centro do Topo
            aux0 = CalcularCentroideTetra(vetGameObj[2]);
            piramideTopo.transform.position = new Vector3(aux0[0], aux0[1], aux0[2]);
            // TOPO
            vetGameObj[2].transform.parent = piramideTopo.transform;

            alpha1 = false;
            alpha2 = true;
            alpha3 = false;
            alpha4 = false;
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            piramideBase.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideMeio.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideTopo.transform.rotation = Quaternion.Euler(0, 0, 0);
            for (int i = 0; i <= 22; i++)
            {
                vetGameObj[i].transform.parent = null;
            }
            // Calcular centro da Base
            aux0 = CalcularCentroideTetra(vetGameObj[2]);
            aux1 = CalcularCentroideTetra(vetGameObj[22]);
            aux2 = CalcularCentroideTetra(vetGameObj[5]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideBase.transform.position = centroide;
            // BASE
            vetGameObj[2].transform.parent = piramideBase.transform;
            vetGameObj[4].transform.parent = piramideBase.transform;
            vetGameObj[5].transform.parent = piramideBase.transform;
            vetGameObj[7].transform.parent = piramideBase.transform;
            vetGameObj[8].transform.parent = piramideBase.transform;
            vetGameObj[10].transform.parent = piramideBase.transform;
            vetGameObj[11].transform.parent = piramideBase.transform;
            vetGameObj[12].transform.parent = piramideBase.transform;
            vetGameObj[13].transform.parent = piramideBase.transform;
            vetGameObj[16].transform.parent = piramideBase.transform;
            vetGameObj[17].transform.parent = piramideBase.transform;
            vetGameObj[18].transform.parent = piramideBase.transform;
            vetGameObj[19].transform.parent = piramideBase.transform;
            vetGameObj[20].transform.parent = piramideBase.transform;
            vetGameObj[21].transform.parent = piramideBase.transform;
            vetGameObj[22].transform.parent = piramideBase.transform;
            // Calcular centro do Meio
            aux0 = CalcularCentroideTetra(vetGameObj[1]);
            aux1 = CalcularCentroideTetra(vetGameObj[15]);
            aux2 = CalcularCentroideTetra(vetGameObj[3]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideMeio.transform.position = centroide;
            // Meio
            vetGameObj[1].transform.parent = piramideMeio.transform;
            vetGameObj[3].transform.parent = piramideMeio.transform;
            vetGameObj[6].transform.parent = piramideMeio.transform;
            vetGameObj[9].transform.parent = piramideMeio.transform;
            vetGameObj[14].transform.parent = piramideMeio.transform;
            vetGameObj[15].transform.parent = piramideMeio.transform;

            // Calcular centro do Topo
            aux0 = CalcularCentroideTetra(vetGameObj[0]);
            piramideTopo.transform.position = new Vector3(aux0[0], aux0[1], aux0[2]);
            // TOPO
            vetGameObj[0].transform.parent = piramideTopo.transform;

            alpha1 = false;
            alpha2 = false;
            alpha3 = true;
            alpha4 = false;
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            piramideBase.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideMeio.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideTopo.transform.rotation = Quaternion.Euler(0, 0, 0);
            for (int i = 0; i <= 22; i++)
            {
                vetGameObj[i].transform.parent = null;
            }
        }

        if (Input.GetKey(KeyCode.R)) canRotate = true;
        if (Input.GetKey(KeyCode.T)) canRotate = false;
    }

    void RotateAroundFaceAxis(GameObject piramideBase, GameObject piramideTopo, float rotationSpeed)
    {
        Vector3 axis = (piramideTopo.transform.position - piramideBase.transform.position).normalized;
        piramideBase.transform.RotateAround(piramideBase.transform.position, axis, rotationSpeed * Time.deltaTime);
    }

    Vector3 CalcularCentroideTetra(GameObject tetra)
    {
        createTetra scriptTetra = tetra.GetComponent<createTetra>();
        if (scriptTetra == null)
        {
            Debug.LogWarning("Componente 'createTetra' não encontrado!");
            return tetra.transform.position;
        }

        // Agora usamos getVectors para pegar os vértices
        Vector3[] vertices = scriptTetra.getVectors();
        if (vertices.Length < 4)
        {
            Debug.LogWarning("Tetraedro não possui 4 vértices.");
            return tetra.transform.position;
        }

        // Calcula o centroide a partir dos vértices
        Vector3 centroideLocal = (vertices[0] + vertices[1] + vertices[2] + vertices[3]) / 4f;
        return tetra.transform.TransformPoint(centroideLocal);
    }

    void CriarPiramix()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i == 0)
            {
                vetGameObj[i] = Instantiate(tetrahedron, new Vector3(0, 0, 0), Quaternion.identity); // tetraedro base
            }
            else
            {
                vetGameObj[i] = Instantiate(
                    tetrahedron,
                    new Vector3(vetGameObj[i - 1].transform.position.x + 1, 0, 0),
                    vetGameObj[i - 1].transform.rotation //i-1 posicao anterior
                );
            }
            vetGameObj[i].name = "Tetraedro_" + i;
        }

        // BASE
        vetGameObj[3].transform.position = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[4].transform.position = new Vector3(1.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[5].transform.position = new Vector3(1f, 0, 2 * Mathf.Sqrt(0.75f));
        vetGameObj[6].transform.position = new Vector3(1.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[6].transform.Rotate(0, 180f, 0);
        vetGameObj[7].transform.position = new Vector3(2.5f, 0, Mathf.Sqrt(0.75f));
        vetGameObj[7].transform.Rotate(0, 180f, 0);
        vetGameObj[8].transform.position = new Vector3(2f, 0, 2 * Mathf.Sqrt(0.75f));
        vetGameObj[8].transform.Rotate(0, 180f, 0);
        // BASE MEIOS
        vetGameObj[9].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[9].transform.Rotate(37f, 0f, 180f);
        vetGameObj[10].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[10].transform.Rotate(37f, 0f, 180f);

        // Calcular centro da Base
        piramideBase = new GameObject("piramideBase");
        aux0 = CalcularCentroideTetra(vetGameObj[0]);
        aux1 = CalcularCentroideTetra(vetGameObj[2]);
        aux2 = CalcularCentroideTetra(vetGameObj[5]);
        centroide = (aux0 + aux1 + aux2) / 3f;
        piramideBase.transform.position = centroide;

        // Tornar os objetos de índice 0 a 10 filhos de piramideBase
        for (int i = 0; i <= 10; i++)
        {
            vetGameObj[i].transform.parent = piramideBase.transform;
        }

        piramideBase.transform.Rotate(0, 120f, 0);

        vetGameObj[11].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[11].transform.Rotate(37f, 0f, 180f);
        vetGameObj[11].transform.parent = piramideBase.transform;
        vetGameObj[12].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[12].transform.Rotate(37f, 0f, 180f);
        vetGameObj[12].transform.parent = piramideBase.transform;

        piramideBase.transform.Rotate(0, 120f, 0);

        vetGameObj[13].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[13].transform.Rotate(37f, 0f, 180f);
        vetGameObj[13].transform.parent = piramideBase.transform;
        vetGameObj[14].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[14].transform.Rotate(37f, 0f, 180f);
        vetGameObj[14].transform.parent = piramideBase.transform;

        piramideBase.transform.rotation = Quaternion.Euler(0, 0, 0);

        // MEIO
        vetGameObj[15].transform.position = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[16].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[17].transform.position = new Vector3(1f, Mathf.Sqrt(0.75f), 1.15f);
        vetGameObj[18].transform.position = new Vector3(2f, Mathf.Sqrt(0.75f), 1.15f);
        vetGameObj[18].transform.Rotate(0, 180f, 0);
        vetGameObj[19].transform.position = new Vector3(2f, 2 * Mathf.Sqrt(0.75f), 2 * Mathf.Sqrt(0.75f) / 3);
        vetGameObj[19].transform.Rotate(37f, 0f, 180f);

        // Calcular centro do Meio
        piramideMeio = new GameObject("PiramideMeio");
        aux0 = CalcularCentroideTetra(vetGameObj[15]);
        aux1 = CalcularCentroideTetra(vetGameObj[16]);
        aux2 = CalcularCentroideTetra(vetGameObj[17]);
        centroide = (aux0 + aux1 + aux2) / 3f;
        piramideMeio.transform.position = centroide;

        for (int i = 15; i <= 19; i++)
        {
            vetGameObj[i].transform.parent = piramideMeio.transform;
        }

        piramideMeio.transform.Rotate(0, 120f, 0);

        vetGameObj[20].transform.position = new Vector3(2f, 2 * Mathf.Sqrt(0.75f), 2 * (Mathf.Sqrt(0.75f) / 3));
        vetGameObj[20].transform.Rotate(37f, 0f, 180f);
        vetGameObj[20].transform.parent = piramideMeio.transform;

        piramideMeio.transform.Rotate(0, 120f, 0);

        vetGameObj[21].transform.position = new Vector3(2f, 2 * Mathf.Sqrt(0.75f), 2 * (Mathf.Sqrt(0.75f) / 3));
        vetGameObj[21].transform.Rotate(37f, 0f, 180f);
        vetGameObj[21].transform.parent = piramideMeio.transform;

        piramideMeio.transform.rotation = Quaternion.Euler(0, 0, 0);

        // TOPO
        vetGameObj[22].transform.position = new Vector3(1f, 2 * Mathf.Sqrt(0.75f), 2 * (Mathf.Sqrt(0.75f) / 3));
        // Calcular centro do Topo
        piramideTopo = new GameObject("PiramideTopo");
        aux0 = CalcularCentroideTetra(vetGameObj[22]);
        piramideTopo.transform.position = new Vector3(aux0[0], aux0[1], aux0[2]);

        vetGameObj[22].transform.parent = piramideTopo.transform;
    }
}
