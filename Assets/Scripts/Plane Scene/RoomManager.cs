using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Player player;

    public List<Room> rooms;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Room room in rooms)
        {
            if (!room.inRoom)
            {
                room.DisableWalls();
            }
            else
            {
                room.EnableWalls();
            }
        }
    }
    public void AddRoom(Room roomToAdd)
    {
        rooms.Add(roomToAdd);
        roomToAdd.roomID = rooms.Count;
    }
}
