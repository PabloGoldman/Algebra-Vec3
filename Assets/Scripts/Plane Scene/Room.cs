using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Room : MonoBehaviour
{
    public List<Planes> planesInRoom = new List<Planes>();

    public bool inRoom;

    public int actualPlanes = 0; //Para testear que se sumen bien los planos al room

    public int roomID;

    public void AddPlane(Planes planeToAdd)
    {
        planesInRoom.Add(planeToAdd);
        actualPlanes++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(planesInRoom);
        }
    }

    
}
