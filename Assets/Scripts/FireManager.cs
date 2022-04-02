using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public ApartmentManager apartment;

    public int numRoomsBurning = 1;
    public float fireSpreadSpeedUpwards = 4f;
    public float fireSpreadSpeedSideways = 2f;
    public float fireSpreadSpeedDownwards = 1f;
    public float fireSpreadSpeedMultiplier = 0.1f;

    public List<ApartmentManager.Room> burningRooms;

    // Start is called before the first frame update
    void Start()
    {
        burningRooms = new List<ApartmentManager.Room>(apartment.roomDatae.Length);
        
        for (int c = 0; c < Mathf.Min(numRoomsBurning, burningRooms.Capacity); c++) {
            ApartmentManager.Room room;
            do {
                room = apartment.roomDatae[Random.Range(0, apartment.roomDatae.Length)];
            } while(burningRooms.Contains(room));


            room.IncrementBurn(0.51f);
            burningRooms.Add(room);
        }
    }

    void Update()
    {
        foreach (var room in burningRooms)
        {
            Debug.Log(room.roomName + " is burning. " + room.burn.ToString());

            var r = apartment.GetRoom(room.gridPos.x - 1, room.gridPos.y);
            if (r != null) r.IncrementBurn(room.burn * fireSpreadSpeedSideways * fireSpreadSpeedMultiplier * Time.deltaTime);
            r = apartment.GetRoom(room.gridPos.x + 1, room.gridPos.y);
            if (r != null) r.IncrementBurn(room.burn * fireSpreadSpeedSideways * fireSpreadSpeedMultiplier * Time.deltaTime);
            r = apartment.GetRoom(room.gridPos.x, room.gridPos.y + 1);
            if (r != null) r.IncrementBurn(room.burn * fireSpreadSpeedUpwards * fireSpreadSpeedMultiplier * Time.deltaTime);
            r = apartment.GetRoom(room.gridPos.x, room.gridPos.y - 1);
            if (r != null) r.IncrementBurn(room.burn * fireSpreadSpeedDownwards * fireSpreadSpeedMultiplier * Time.deltaTime);
            room.IncrementBurn(0.3f * fireSpreadSpeedDownwards * fireSpreadSpeedMultiplier * Time.deltaTime);
        }

        foreach (var room in apartment.roomDatae) {
            if (room.fireState == ApartmentManager.FireState.Fire) {
                if (!burningRooms.Contains(room)) burningRooms.Add(room);
            }
            // else if (room.fireState == ApartmentManager.FireState.Smoke)
            //     room.IncrementBurn(-0.01f * fireSpreadSpeedDownwards * fireSpreadSpeedMultiplier * Time.deltaTime);
        }

        for (int i = burningRooms.Count - 1; i >= 0 ; i--) {
            if (burningRooms[i].fireState != ApartmentManager.FireState.Fire)
                burningRooms.RemoveAt(i); 
        }
    }
}
