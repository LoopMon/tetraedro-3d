using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject tetrahedron;
    public GameObject[] vetGameObj = new GameObject[23];
    private GameObject piramideBase;
    private GameObject piramideMeio;
    private GameObject piramideTopo;

    private bool canRotate = false;
    private bool isRotating = false;
    private bool alpha1 = true;
    private bool alpha2 = false;
    private bool alpha3 = false;
    private bool alpha4 = false;

    private Vector3 aux0;
    private Vector3 aux1;
    private Vector3 aux2;
    private Vector3 centroide;

    private float highSpeedRotation = 50f;
    private float lowSpeedRotation = 25f;
    private float faceRotation = 120f;
    private float timeRotation = 0.5f;

    void Start()
    {
        CriarPiramix();
    }

    void Update()
    {
        if (piramideBase != null && canRotate)
        {
            if (alpha1)
            {
                piramideBase.transform.Rotate(Vector3.up * Time.deltaTime * highSpeedRotation);
                piramideMeio.transform.Rotate(Vector3.up * Time.deltaTime * -highSpeedRotation);
                piramideTopo.transform.Rotate(Vector3.up * Time.deltaTime * lowSpeedRotation);
            }

            if (alpha2)
            {
                RotateAroundFaceAxis(piramideBase, piramideTopo, highSpeedRotation);
                RotateAroundFaceAxis(piramideMeio, piramideTopo, -highSpeedRotation);
                RotateAroundFaceAxis(piramideTopo, piramideBase, lowSpeedRotation);
            }

            if (alpha3)
            {
                RotateAroundFaceAxis(piramideBase, piramideTopo, highSpeedRotation);
                RotateAroundFaceAxis(piramideMeio, piramideTopo, -highSpeedRotation);
                RotateAroundFaceAxis(piramideTopo, piramideBase, lowSpeedRotation);
            }

            if (alpha4)
            {
                RotateAroundFaceAxis(piramideBase, piramideTopo, highSpeedRotation);
                RotateAroundFaceAxis(piramideMeio, piramideTopo, -highSpeedRotation);
                RotateAroundFaceAxis(piramideTopo, piramideBase, lowSpeedRotation);
            }
        }

        if (!canRotate && !isRotating)
        {
            if (Input.GetKeyUp(KeyCode.Z))
            {
                StartCoroutine(FullRotateAroundFaceAxis(piramideBase, piramideTopo, faceRotation, timeRotation));
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                StartCoroutine(FullRotateAroundFaceAxis(piramideMeio, piramideTopo, faceRotation, timeRotation));
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                StartCoroutine(FullRotateAroundFaceAxis(piramideTopo, piramideBase, faceRotation, timeRotation));
            }
        }

        if (Input.GetKeyUp(KeyCode.R)) canRotate = !canRotate;

        TrocarEixo();
    }

    void RotateAroundFaceAxis(GameObject piramideBase, GameObject piramideTopo, float rotationSpeed)
    {
        Vector3 axis = (piramideTopo.transform.position - piramideBase.transform.position).normalized;
        piramideBase.transform.RotateAround(piramideBase.transform.position, axis, rotationSpeed * Time.deltaTime);
    }

    IEnumerator FullRotateAroundFaceAxis(GameObject piramideBase, GameObject piramideTopo, float totalAngle, float duration)
    {
        isRotating = true;

        Vector3 axis = (piramideTopo.transform.position - piramideBase.transform.position).normalized;
        float rotatedAngle = 0f;
        float rotationPerFrame;

        while (rotatedAngle < totalAngle)
        {
            // Quanto vamos rotacionar neste frame
            rotationPerFrame = (totalAngle / duration) * Time.deltaTime;

            // Evitar ultrapassar o ângulo total
            float remainingAngle = totalAngle - rotatedAngle;
            float step = Mathf.Min(rotationPerFrame, remainingAngle);

            piramideBase.transform.RotateAround(piramideBase.transform.position, axis, step);
            rotatedAngle += step;

            yield return null;
        }

        isRotating = false;
    }

    Vector3 CalcularCentroideTetra(GameObject tetra)
    {
        createTetra scriptTetra = tetra.GetComponent<createTetra>();
        if (scriptTetra == null)
        {
            Debug.LogWarning("Componente 'createTetra' não encontrado!");
            return tetra.transform.position;
        }

        Vector3[] vertices = scriptTetra.getVectors();
        if (vertices.Length < 4)
        {
            Debug.LogWarning("Tetraedro não possui 4 vértices.");
            return tetra.transform.position;
        }

        Vector3 centroideLocal = (vertices[0] + vertices[1] + vertices[2] + vertices[3]) / 4f;
        return tetra.transform.TransformPoint(centroideLocal);
    }

    void TrocarEixo()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            piramideBase.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideMeio.transform.rotation = Quaternion.Euler(0, 0, 0);
            piramideTopo.transform.rotation = Quaternion.Euler(0, 0, 0);
            for (int i = 0, tam = vetGameObj.Length; i < tam; i++)
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
            for (int i = 0, tam = vetGameObj.Length; i < tam; i++)
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
            for (int i = 0, tam = vetGameObj.Length; i < tam; i++)
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
            for (int i = 0, tam = vetGameObj.Length; i < tam; i++)
            {
                vetGameObj[i].transform.parent = null;
            }
            // Calcular centro da Base
            aux0 = CalcularCentroideTetra(vetGameObj[0]);
            aux1 = CalcularCentroideTetra(vetGameObj[22]);
            aux2 = CalcularCentroideTetra(vetGameObj[2]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideBase.transform.position = centroide;
            // BASE
            vetGameObj[0].transform.parent = piramideBase.transform;
            vetGameObj[1].transform.parent = piramideBase.transform;
            vetGameObj[2].transform.parent = piramideBase.transform;
            vetGameObj[6].transform.parent = piramideBase.transform;
            vetGameObj[7].transform.parent = piramideBase.transform;
            vetGameObj[9].transform.parent = piramideBase.transform;
            vetGameObj[10].transform.parent = piramideBase.transform;
            vetGameObj[11].transform.parent = piramideBase.transform;
            vetGameObj[14].transform.parent = piramideBase.transform;
            vetGameObj[15].transform.parent = piramideBase.transform;
            vetGameObj[16].transform.parent = piramideBase.transform;
            vetGameObj[18].transform.parent = piramideBase.transform;
            vetGameObj[19].transform.parent = piramideBase.transform;
            vetGameObj[20].transform.parent = piramideBase.transform;
            vetGameObj[21].transform.parent = piramideBase.transform;
            vetGameObj[22].transform.parent = piramideBase.transform;
            // Calcular centro do Meio
            aux0 = CalcularCentroideTetra(vetGameObj[3]);
            aux1 = CalcularCentroideTetra(vetGameObj[17]);
            aux2 = CalcularCentroideTetra(vetGameObj[4]);
            centroide = (aux0 + aux1 + aux2) / 3f;
            piramideMeio.transform.position = centroide;
            // Meio
            vetGameObj[3].transform.parent = piramideMeio.transform;
            vetGameObj[4].transform.parent = piramideMeio.transform;
            vetGameObj[8].transform.parent = piramideMeio.transform;
            vetGameObj[12].transform.parent = piramideMeio.transform;
            vetGameObj[13].transform.parent = piramideMeio.transform;
            vetGameObj[17].transform.parent = piramideMeio.transform;

            // Calcular centro do Topo
            aux0 = CalcularCentroideTetra(vetGameObj[5]);
            piramideTopo.transform.position = new Vector3(aux0[0], aux0[1], aux0[2]);
            // TOPO
            vetGameObj[5].transform.parent = piramideTopo.transform;

            alpha1 = false;
            alpha2 = false;
            alpha3 = false;
            alpha4 = true;
        }

    }

    void CriarPiramix()
    {
        for (int i = 0, tam = vetGameObj.Length; i < tam; i++)
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

        piramideBase.transform.Rotate(0, faceRotation, 0);

        vetGameObj[11].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[11].transform.Rotate(37f, 0f, 180f);
        createTetra tetra = vetGameObj[11].GetComponent<createTetra>();
        RotateTetraAroundBase(tetra, faceRotation);
        vetGameObj[11].transform.parent = piramideBase.transform;
        vetGameObj[12].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[12].transform.Rotate(37f, 0f, 180f);
        tetra = vetGameObj[12].GetComponent<createTetra>();
        RotateTetraAroundBase(tetra, faceRotation);
        vetGameObj[12].transform.parent = piramideBase.transform;

        piramideBase.transform.Rotate(0, faceRotation, 0);

        vetGameObj[13].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[13].transform.Rotate(37f, 0f, 180f);
        tetra = vetGameObj[13].GetComponent<createTetra>();
        RotateTetraAroundBase(tetra, 2 * faceRotation);
        vetGameObj[13].transform.parent = piramideBase.transform;
        vetGameObj[14].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[14].transform.Rotate(37f, 0f, 180f);
        tetra = vetGameObj[14].GetComponent<createTetra>();
        RotateTetraAroundBase(tetra, 2 * faceRotation);
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

        piramideMeio.transform.Rotate(0, faceRotation, 0);

        vetGameObj[20].transform.position = new Vector3(2f, 2 * Mathf.Sqrt(0.75f), 2 * (Mathf.Sqrt(0.75f) / 3));
        vetGameObj[20].transform.Rotate(37f, 0f, 180f);
        tetra = vetGameObj[20].GetComponent<createTetra>();
        RotateTetraAroundBase(tetra, faceRotation);
        vetGameObj[20].transform.parent = piramideMeio.transform;

        piramideMeio.transform.Rotate(0, faceRotation, 0);

        vetGameObj[21].transform.position = new Vector3(2f, 2 * Mathf.Sqrt(0.75f), 2 * (Mathf.Sqrt(0.75f) / 3));
        vetGameObj[21].transform.Rotate(37f, 0f, 180f);
        tetra = vetGameObj[21].GetComponent<createTetra>();
        RotateTetraAroundBase(tetra, 2 * faceRotation);
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

    void RotateTetraAroundBase(createTetra tetra, float rotation)
    {
        if (tetra == null) return;

        Vector3[] vertices = tetra.getVectors();
        if (vertices.Length < 4) return;

        // Assumindo que:
        // p0, p1, p2 = base
        // p3 = topo

        Vector3 baseCenter = (vertices[0] + vertices[1] + vertices[2]) / 3f;
        Vector3 topVertex = vertices[3];

        // Como os vértices estão no local space, precisamos transformar para world space
        Transform t = tetra.transform;
        Vector3 worldBaseCenter = t.TransformPoint(baseCenter);
        Vector3 worldTopVertex = t.TransformPoint(topVertex);

        Vector3 axis = (worldTopVertex - worldBaseCenter).normalized;

        t.RotateAround(worldBaseCenter, axis, rotation);
    }
}
