using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Room : MonoBehaviour
{
    [SerializeField] Material green, red;

    [SerializeField] Player player;

    public List<SetSelfPlane> wallsMeshes = new List<SetSelfPlane>();

    public List<Planes> planesInRoom = new List<Planes>();

    public bool inRoom = true;

    public bool playerLooking = false;

    public int roomID;

    int pointsInsideRoom = 0;

    public void AddPlane(Planes planeToAdd)
    {
        planesInRoom.Add(planeToAdd);
    }

    public void AddMesh(SetSelfPlane meshToAdd)
    {
        wallsMeshes.Add(meshToAdd);
    }

    private void Update()
    {
        Debug.Log(inRoom);

        inRoom = CheckEnabled(); //Chequea si el jugador o alguno de los puntos del frustrum estan en el room
    }

    public bool CheckEnabled()
    {
        pointsInsideRoom = 0;

        CheckPointInRoom(player.transform.position);

        for (int i = 0; i < player.middle.Length; i++)
        {
            CheckPointInRoom(player.middle[i]);
        }

        return pointsInsideRoom > 0;
    }

    public bool CheckPointInRoom(Vec3 pointToSearch)
    {
        int checkedPlanes = 0;

        foreach (Planes plane in planesInRoom)
        {
            if (plane.GetSide(pointToSearch))
            {
                checkedPlanes++;
            }

            if (checkedPlanes == planesInRoom.Count)
            {
                pointsInsideRoom++;
                Debug.Log("Adentro de la habitacion " + roomID);
            }
            //else 
            //{
            //    inRoom = false;
            //}
        }

        return inRoom;
    }

    public void EnableWalls()
    {
        foreach (SetSelfPlane mesh in wallsMeshes)
        {
            //mesh.GetComponent<MeshRenderer>().enabled = true;
            mesh.GetComponent<MeshRenderer>().material = green;
        }
    }

    public void DisableWalls()
    {
        foreach (SetSelfPlane mesh in wallsMeshes)
        {
            //mesh.GetComponent<MeshRenderer>().enabled = false;
            mesh.GetComponent<MeshRenderer>().material = red;
        }
    }
}
