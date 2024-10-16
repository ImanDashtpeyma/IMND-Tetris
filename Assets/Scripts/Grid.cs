using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform[,] grid;
    public int width, height;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Transform[width, height];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

