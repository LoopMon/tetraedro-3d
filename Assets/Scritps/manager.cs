﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    public GameObject tetrahedron; // prefab da camrera
    public GameObject[] vetGameObj = new GameObject[24];
    GameObject piramideBase;
    GameObject piramideMeio;
    GameObject piramideTopo;
    Vector3 m_Center;

    // Use this for initialization
    void Start()
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

        piramideBase = new GameObject("piramideBase");

        // Calcular centro da Base
        Vector3 pos0 = CalcularCentroideTetra(vetGameObj[0]);
        Vector3 pos2 = CalcularCentroideTetra(vetGameObj[2]);
        Vector3 pos5 = CalcularCentroideTetra(vetGameObj[5]);
        Vector3 centroide = (pos0 + pos2 + pos5) / 3f;
        piramideBase.transform.position = centroide;
        m_Center = centroide;

        // Tornar os objetos de índice 0 a 10 filhos de piramideBase
        for (int i = 0; i <= 10; i++)
        {
            vetGameObj[i].transform.parent = piramideBase.transform;
        }

        piramideBase.transform.Rotate(0, 120f, 0);

        vetGameObj[11].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[11].transform.Rotate(37f, 0f, 180f);
        vetGameObj[11].transform.parent = piramideBase.transform;
        PintarTetra(vetGameObj[11], Color.yellow);
        vetGameObj[12].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[12].transform.Rotate(37f, 0f, 180f);
        vetGameObj[12].transform.parent = piramideBase.transform;
        PintarTetra(vetGameObj[12], Color.yellow);

        piramideBase.transform.Rotate(0, 120f, 0);

        vetGameObj[13].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[13].transform.Rotate(37f, 0f, 180f);
        vetGameObj[13].transform.parent = piramideBase.transform;
        vetGameObj[13].GetComponent<createTetra>().Rebuild();
        PintarTetra(vetGameObj[13], Color.red);
        vetGameObj[14].transform.position = new Vector3(2.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[14].transform.Rotate(37f, 0f, 180f);
        vetGameObj[14].transform.parent = piramideBase.transform;
        PintarTetra(vetGameObj[14], Color.red);

        piramideBase.transform.rotation = Quaternion.Euler(0, 0, 0);

        // MEIO
        vetGameObj[15].transform.position = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[16].transform.position = new Vector3(1.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);
        vetGameObj[17].transform.position = new Vector3(1f, Mathf.Sqrt(0.75f), 1.15f);
        vetGameObj[18].transform.position = new Vector3(2f, Mathf.Sqrt(0.75f), 1.15f);
        vetGameObj[18].transform.Rotate(0, 180f, 0);
        vetGameObj[19].transform.position = new Vector3(2f, 2 * Mathf.Sqrt(0.75f), 2 * Mathf.Sqrt(0.75f) / 3);
        vetGameObj[19].transform.Rotate(37f, 0f, 180f);

        piramideMeio = new GameObject("PiramideMeio");
        Vector3 centroTetra0 = CalcularCentroideTetra(vetGameObj[15]);
        Vector3 centroTetra1 = CalcularCentroideTetra(vetGameObj[16]);
        Vector3 centroTetra2 = CalcularCentroideTetra(vetGameObj[17]);
        centroide = (centroTetra0 + centroTetra1 + centroTetra2) / 3f;
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

        piramideTopo = new GameObject("PiramideTopo");
        Vector3 aux = CalcularCentroideTetra(vetGameObj[22]);

        piramideTopo.transform.position = new Vector3(aux[0], aux[1], aux[2]);
        vetGameObj[22].transform.parent = piramideTopo.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (piramideBase != null)
        {
            piramideBase.transform.Rotate(Vector3.up * Time.deltaTime * 50f);
            piramideMeio.transform.Rotate(Vector3.up * Time.deltaTime * -50f);
            piramideTopo.transform.Rotate(Vector3.up * Time.deltaTime * 25f);
        }
    }

    void PintarTetra(GameObject obj, Color cor, int faceIndex = -1)
    {
        MeshFilter mf = obj.GetComponent<MeshFilter>();
        if (mf == null) return;

        Mesh mesh = mf.mesh;

        // Garante que a malha tenha o array de cores
        Color[] cores = new Color[mesh.vertexCount];

        if (faceIndex < 0)
        {
            // Pinta o tetraedro inteiro
            for (int i = 0; i < cores.Length; i++)
            {
                cores[i] = cor;
            }
        }
        else
        {
            // Cada face tem 3 vértices
            int startIndex = faceIndex * 3;

            if (startIndex + 2 >= mesh.vertexCount)
            {
                Debug.LogWarning("Índice de face fora do intervalo!");
                return;
            }

            // Copia as cores antigas (se quiser manter as outras intactas)
            if (mesh.colors != null && mesh.colors.Length == mesh.vertexCount)
                mesh.colors.CopyTo(cores, 0);

            cores[startIndex] = cor;
            cores[startIndex + 1] = cor;
            cores[startIndex + 2] = cor;
        }

        mesh.colors = cores;
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

}
