using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Room : MonoBehaviour
{
    [SerializeField] GameObject player;

    public List<SetSelfPlane> wallsMeshes = new List<SetSelfPlane>();

    public List<Planes> planesInRoom = new List<Planes>();

    public bool inRoom = true;

    public int roomID;

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
        CheckPlayerInRoom();
    }

    void CheckPlayerInRoom()
    {
        int checkedPlanes = 0;
        foreach (Planes plane in planesInRoom)
        {
            if (plane.GetSide(player.transform.position))
            {
                checkedPlanes++;
            }

            if (checkedPlanes == planesInRoom.Count)
            {
                inRoom = true;
                Debug.Log("Adentro de la habitacion " + roomID);
            }
            else
            {
                inRoom = false;
            }
        }
    }

    public void EnableWalls()
    {
        foreach (SetSelfPlane mesh in wallsMeshes)
        {
            mesh.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void DisableWalls()
    {
        foreach (SetSelfPlane mesh in wallsMeshes)
        {
            mesh.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
