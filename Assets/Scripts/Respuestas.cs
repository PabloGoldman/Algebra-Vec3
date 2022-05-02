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

    public Vector3 a;
    public Vector3 b;

    float t;


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
            case EjerciciosVector3.Ejercicio.Cinco: // Lerp
                t += Time.deltaTime;

                if (t > 1)
                {
                    t = 0;
                }
                VectorDebugger.UpdatePosition("Vec", Vec3.Lerp(a, b, t));
                break;
            case EjerciciosVector3.Ejercicio.Seis:
                VectorDebugger.UpdatePosition("Vec", Vec3.Max(a, b));
                break;
            case EjerciciosVector3.Ejercicio.Siete:
                VectorDebugger.UpdatePosition("Vec", Vec3.Project(a, b));
                break;
            case EjerciciosVector3.Ejercicio.Ocho:
                float num = Vector3.Distance(a, b);
                Vector3 vector3 = a + b;
                Vector3 normalized = ((Vector3)vector3).normalized;

                VectorDebugger.UpdatePosition("Vec", num * normalized);
                break;
            case EjerciciosVector3.Ejercicio.Nueve:
                VectorDebugger.UpdatePosition("Vec", Vec3.Reflect(a, ((Vector3)b.normalized)));
                break;
            case EjerciciosVector3.Ejercicio.Diez:
                t += Time.deltaTime;
                VectorDebugger.UpdatePosition("Vec", Vec3.LerpUnclamped(a, b, t));
                break;
            default:
                break;
        }
    }
}
