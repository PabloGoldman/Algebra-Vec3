using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam;
    const uint maxVertexPerPlane = 4;
    int resolutionGrid = 10;

    int maxDivisions = 7; //Divisiones del BST

    Vector3[] frustumCornerFar = new Vector3[maxVertexPerPlane];
    Vector3[] frustumCornerNear = new Vector3[maxVertexPerPlane];

    Vec3 leftMiddlePosFar;
    Vec3 leftMiddlePosNear;
    Vec3 rigthMiddlePosFar;
    Vec3 rigthMiddlePosNear;

    Vec3[] intermediatePointsFar;
    Vec3[] intermediatePointsNear;

    public Room[] pointRoom;   //Es el room del punto negro, lo pongo aca xq no se puede modificar un struct de afuera xd
    public Vec3[] middlePoint;

    public Room inRoom; //Room actual del player

    bool initialized;

    private void Start()
    {
        cam = Camera.main;

        intermediatePointsFar = new Vec3[resolutionGrid];
        intermediatePointsNear = new Vec3[resolutionGrid];

        pointRoom = new Room[resolutionGrid];
        middlePoint = new Vec3[resolutionGrid];


        for (int i = 0; i < resolutionGrid; i++)
        {
            middlePoint[i] = CalculateTheMiddle(intermediatePointsNear[i], intermediatePointsFar[i]);
        }
    }

    private void Update()
    {
        CalculateEndsOfFrustum();
        BinarySearch();
    }

    public void SetInRoom(Room roomIn) //Setea en que habitacion está
    {
        inRoom = roomIn;
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

    public void InitializePoints()
    {
        for (int i = 0; i < resolutionGrid; i++)
        {
            middlePoint[i] = CalculateTheMiddle(intermediatePointsNear[i], intermediatePointsFar[i]);
            initialized = true;
        }
    }

    public void SetPointInRoom(int point, Room roomToAdd) //Setea la habitacion en la que esta el punto
    {
        pointRoom[point] = roomToAdd;
    }

    void BinarySearch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            InitializePoints();

            for (int i = 0; i < resolutionGrid; i++)
            {
                if (pointRoom[i] != this.inRoom || pointRoom[i] == null)
                {
                    middlePoint[i] = CalculateTheMiddle(intermediatePointsNear[i], middlePoint[i]);
                    Debug.Log("restamo");
                }
            }
        }

    }

    public void CalculatePointRooms()
    {
        //Si el middle esta en una habitacion conexa, tiene que ir para adelante
        //Sino, tiene que ir para atras

        for (int i = 0; i < resolutionGrid; i++)
        {
            if (pointRoom[i].associatedRooms.Contains(inRoom) || pointRoom[i].roomID == 5)
            {
                middlePoint[i] = CalculateTheMiddle(intermediatePointsNear[i], middlePoint[i]);
                Debug.Log("restamo");
            }
            else
            {
                middlePoint[i] = CalculateTheMiddle(middlePoint[i], intermediatePointsFar[i]);
                Debug.Log("sumamo");
            }
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
            Gizmos.DrawSphere(middlePoint[i], .2f);
        }
    }
}
