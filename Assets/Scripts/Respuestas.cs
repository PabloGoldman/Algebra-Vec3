using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;
using EjerciciosAlgebra;

public class Respuestas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    Vec3 A;
    [SerializeField] 
    Vec3 B;

    public EjerciciosVector3.Ejercicio ejercicio;

    private Vector3 result;

    [SerializeField]

    void Start()
    {
        Vector3Debugger.AddVector(transform.position, A, Color.yellow, "A");
        Vector3Debugger.AddVector(transform.position, B, Color.blue, "B");
        Vector3Debugger.AddVector(transform.position, transform.position + result, Color.green, "result");
        Vector3Debugger.EnableEditorView();
    }
        
    // Update is called once per frame
    void Update()
    {
        Vector3Debugger.UpdatePosition("A", transform.position, A);
        Vector3Debugger.UpdatePosition("B", transform.position, B);
        Vector3Debugger.UpdatePosition("result", transform.position, result + transform.position);

        switch (ejercicio)
        {
            case EjerciciosVector3.Ejercicio.Uno:
                Vector3Debugger.UpdatePosition("result", A + B);
                break;
            case EjerciciosVector3.Ejercicio.Dos:
                Vector3Debugger.UpdatePosition("result", A - B);
                break;
            case EjerciciosVector3.Ejercicio.Tres:
                Vector3Debugger.UpdatePosition("result", Vector3.Scale(A,B));
                break;
            case EjerciciosVector3.Ejercicio.Cuatro:
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
