using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class CreatePlanes : MonoBehaviour
{
    Planes walls;

    // Start is called before the first frame update
    void Start()
    {
        walls = new Planes(transform.forward, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
