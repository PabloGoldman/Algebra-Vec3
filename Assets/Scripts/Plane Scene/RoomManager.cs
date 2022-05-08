using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class RoomManager : MonoBehaviour
{
    public List<Room> rooms;

    int cameraRoomNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Room room in rooms)
        {
            if (!room.inRoom)
            {
                room.gameObject.SetActive(false);
            }
            else
            {
                room.gameObject.SetActive(true);
            }
        }
    }
    public void AddRoom(Room roomToAdd)
    {

    }
}
