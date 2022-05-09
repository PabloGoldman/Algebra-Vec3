using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam;
    const uint maxVertexPerPlane = 4;
    int resolutionGrid = 10;

    Vector3[] frustumCornerFar = new Vector3[maxVertexPerPlane];
    Vector3[] frustumCornerNear = new Vector3[maxVertexPerPlane];

    Vec3 leftMiddlePosFar;
    Vec3 leftMiddlePosNear;
    Vec3 rigthMiddlePosFar;
    Vec3 rigthMiddlePosNear;

    Vec3[] intermediatePointsFar;
    Vec3[] intermediatePointsNear;

    public Vec3[] middle; //Puntos medios en el negro del coso

    private void Start()
    {
        cam = Camera.main;

        intermediatePointsFar = new Vec3[resolutionGrid];
        intermediatePointsNear = new Vec3[resolutionGrid];
        middle = new Vec3[resolutionGrid];
    }

    private void Update()
    {
        CalculateEndsOfFrustum();
        BinarySearchInRays();
    }

    private void CalculateEndsOfFrustum() //Se calcula y se dibujan los planos del frustrum
    {
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCornerFar);  //Obtengo el Frustrum lejano
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCornerNear); //Obtengo el Frustrum cercano

        for (int i = 0; i < maxVertexPerPlane; i++)
        {
            frustumCornerFar[i] = FromLocalToWorld(frustumCornerFar[i], cam.transform);
            frustumCornerNear[i] = FromLocalToWorld(frustumCornerNear[i], cam.transform);

            //ORDEN DE LOS VERTICES: 0 abajo izq, 1 arriba izq, 2 arriba der, 3 abajo der
        }

        leftMiddlePosFar = CalculateTheMiddle(frustumCornerFar[1], frustumCornerFar[0]);
        leftMiddlePosNear = CalculateTheMiddle(frustumCornerNear[1], frustumCornerNear[0]);
        rigthMiddlePosFar = CalculateTheMiddle(frustumCornerFar[2], frustumCornerFar[3]);
        rigthMiddlePosNear = CalculateTheMiddle(frustumCornerNear[2], frustumCornerNear[3]);

        intermediatePointsFar = CalculateGrid(leftMiddlePosFar, rigthMiddlePosFar);
        intermediatePointsNear = CalculateGrid(leftMiddlePosNear, rigthMiddlePosNear);
    }

    private Vec3[] CalculateGrid(Vec3 leftMiddlePos, Vec3 rigthMiddlePos)
    {
        List<Vec3> gridPoints = new List<Vec3>();

        for (int i = 0; i < resolutionGrid; i++)
        {
            gridPoints.Add(Vector3.Lerp(leftMiddlePos, rigthMiddlePos, (float)i / resolutionGrid)); //Creo otra interpolacion lineal desde los puntos laterales del frustrum guardando en una lista los puntos intermedios de la grilla
        }

        return gridPoints.ToArray(); //transformo la lista a Array
    }

    Vec3 CalculateTheMiddle(Vec3 lhs, Vec3 rhs)
    {
        return new Vec3((lhs.x + rhs.x) / 2, (lhs.y + rhs.y) / 2, (lhs.z + rhs.z) / 2);
    }

    private Vector3 FromLocalToWorld(Vector3 point, Transform transformRef) //Recibe un punto y tansform de un objeto
    {
        Vector3 result = Vector3.zero;

        result = new Vector3(point.x * transformRef.localScale.x, point.y * transformRef.localScale.y, point.z * transformRef.localScale.z); //Multiplica el punto por la escala

        result = transformRef.localRotation * result; //Luego multiplica el resutado por la rotacion

        return result + transformRef.position; //El resutado le sumamos la posicion del objeto y retornamos las coordenadas en globales
    }

    void BinarySearchInRays()
    {
        for (int i = 0; i < resolutionGrid; i++)
        {
            middle[i] = CalculateTheMiddle(intermediatePointsNear[i], intermediatePointsFar[i]);
        }

        for (int i = 0; i < resolutionGrid; i++)
        {
            middle[i] = CalculateTheMiddle(middle[i], intermediatePointsFar[i]);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < maxVertexPerPlane; i++)
        {
            Gizmos.DrawSphere(frustumCornerFar[i], .1f);
            Gizmos.DrawSphere(frustumCornerNear[i], .1f);
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < resolutionGrid; i++)
        {
            Gizmos.DrawSphere(intermediatePointsFar[i], .1f);
            Gizmos.DrawSphere(intermediatePointsNear[i], .05f);
        }

        Gizmos.color = Color.blue;

        for (int i = 0; i < resolutionGrid; i++)
        {
            Gizmos.DrawLine(intermediatePointsNear[i], intermediatePointsFar[i]);
        }

        Gizmos.color = Color.black;

        for (int i = 0; i < resolutionGrid; i++)
        {
            Gizmos.DrawSphere(middle[i], .2f);
        }
    }
}
