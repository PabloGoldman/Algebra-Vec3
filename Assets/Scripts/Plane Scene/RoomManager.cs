using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class RoomManager : MonoBehaviourSingleton<Room>
{
    [SerializeField] Player player;

    public List<Room> rooms;

    private void Start()
    {
        for (int i = 0; i < rooms.Count; i++) //Setea las ID's de las rooms
        {
            rooms[i].roomID = i;
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            if (i > 0)
            {
                rooms[i].AddAssociatedRoom(rooms[i - 1]);
            }

            if (i < rooms.Count)
            {
                rooms[i].AddAssociatedRoom(rooms[i + 1]);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Room room in rooms)
        {
            if (!room.seeingRoom)
            {
                room.DisableWalls();
            }
            else
            {
                room.EnableWalls();
            }
        }

        foreach (Room room in rooms)
        {
            if (room.CheckPlayerInRoom())
            {
                player.SetInRoom(room);
                Debug.Log("Player room: " + player.inRoom);
            }
        }

        foreach (Room room in rooms)
        {
            for (int i = 0; i < player.middlePoint.Length; i++)
            {
                if (room.CheckPointInRoom(player.middlePoint[i])) //Setea el room del punto 
                {
                    player.SetPointInRoom(i, room);
                    //Debug.Log(player.pointRoom[i]);
                }
            }
        }
    }

    public void AddRoom(Room roomToAdd)
    {
        rooms.Add(roomToAdd);
        roomToAdd.roomID = rooms.Count;
    }
}
