using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;
using EjerciciosAlgebra;

public class Respuestas : MonoBehaviour
{
    public EjerciciosVector3.Ejercicio ejercicio;
    public Color vectorColor = Color.red;
    [Space(10f)]
    public Vector3 a;
    public Vector3 b;

    void Start()
    {
        VectorDebugger.EnableCoordinates();
        VectorDebugger.EnableEditorView();
        VectorDebugger.AddVector(Vector3.zero, this.vectorColor, "Vec");
        VectorDebugger.AddVector(Vector3.zero, Color.white, "a");
        VectorDebugger.AddVector(Vector3.zero, Color.black, "b");
    }
       
    // Update is called once per frame
    void Update()
    {
        VectorDebugger.UpdateColor("Vec", this.vectorColor);
        VectorDebugger.UpdatePosition("a", this.a);
        VectorDebugger.UpdatePosition("b", this.b);

        switch (ejercicio)
        {
            case EjerciciosVector3.Ejercicio.Uno:
                VectorDebugger.UpdatePosition("Vec", (a + b));
                break;
            case EjerciciosVector3.Ejercicio.Dos:
                VectorDebugger.UpdatePosition("Vec", a - b);
                break;
            case EjerciciosVector3.Ejercicio.Tres:
                VectorDebugger.UpdatePosition("Vec", Vector3.Scale(this.a, this.b));
                break;
            case EjerciciosVector3.Ejercicio.Cuatro:
                VectorDebugger.UpdatePosition("Vec", Vec3.Cross(a, b));
                break;
            case EjerciciosVector3.Ejercicio.Cinco:
                break;
            case EjerciciosVector3.Ejercicio.Seis:
                break;
            case EjerciciosVector3.Ejercicio.Siete:
                break;
            case EjerciciosVector3.Ejercicio.Ocho:
                break;
            case EjerciciosVector3.Ejercicio.Nueve:
                break;
            case EjerciciosVector3.Ejercicio.Diez:
                break;
            default:
                break;
        }
    }
}
