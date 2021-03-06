﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertices : MonoBehaviour {

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i] = Color.red;
        }
        mesh.colors = colors;
    }
}