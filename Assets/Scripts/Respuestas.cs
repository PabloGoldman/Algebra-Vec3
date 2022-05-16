using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;
using EjerciciosAlgebra;

public class Respuestas : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] int exercise;

    public Color vectorColor = Color.red;

    public Vector3 a;
    public Vector3 b;

    float t;

    void Start()
    {
        VectorDebugger.EnableCoordinates();
        VectorDebugger.EnableEditorView();
        VectorDebugger.AddVector(Vector3.zero, vectorColor, "Vec");
        VectorDebugger.AddVector(Vector3.zero, Color.white, "a");
        VectorDebugger.AddVector(Vector3.zero, Color.black, "b");
    }
       
    // Update is called once per frame
    void Update()
    {
        VectorDebugger.UpdateColor("Vec", vectorColor);
        VectorDebugger.UpdatePosition("a", a);
        VectorDebugger.UpdatePosition("b", b);

        switch (exercise)
        {
            case 1:
                VectorDebugger.UpdatePosition("Vec", (a + b));
                break;
            case 2:
                VectorDebugger.UpdatePosition("Vec", a - b);
                break;
            case 3:
                VectorDebugger.UpdatePosition("Vec", Vector3.Scale(a, b));
                break;
            case 4:
                VectorDebugger.UpdatePosition("Vec", Vec3.Cross(a, b));
                break;
            case 5: // Lerp
                t += Time.deltaTime;

                if (t > 1)
                {
                    t = 0;
                }

                VectorDebugger.UpdatePosition("Vec", Vec3.Lerp(a, b, t));
                break;
            case 6:
                VectorDebugger.UpdatePosition("Vec", Vec3.Max(a, b));
                break;
            case 7:
                VectorDebugger.UpdatePosition("Vec", Vec3.Project(a, b));
                break;
            case 8:
                float num = Vector3.Distance(a, b);
                Vector3 vector3 = a + b;
                Vector3 normalized = ((Vector3)vector3).normalized;

                VectorDebugger.UpdatePosition("Vec", num * normalized);
                break;
            case 9:
                VectorDebugger.UpdatePosition("Vec", Vec3.Reflect(a, ((Vector3)b.normalized)));
                break;
            case 10:
                t += Time.deltaTime;
                VectorDebugger.UpdatePosition("Vec", Vec3.LerpUnclamped(a, b, t));
                break;
            default:
                break;
        }
    }
}
