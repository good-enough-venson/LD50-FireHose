using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject resiPrefab;
    public int columns = 4;
    public int rows = 5;
    public Vector3 roomSize = new Vector3(6f, 3.8f, 4f);
    public Room[] roomDatae = { };

    public enum FireState { None, Smoke, Fire, Burnt }

    public enum Modifier { None, Water, Ladder, Argon, Stim }

    [System.Serializable]
    public class Room
    {
        public string roomName;
        public RoomScript roomScript;
        public Vector2Int gridPos;
        public FireState fireState = FireState.None;
        public Modifier modifier = Modifier.None;
        public List<GameObject> residents;

        public float burn { get; private set; }

        public void IncrementBurn(float amount)
        {
            if(amount > 0f && modifier == Modifier.Argon) return; 
            if(amount > 0f && modifier == Modifier.Stim) amount *= 2;

            burn += amount;
            burn = Mathf.Max(Mathf.Min(burn, 1f), 0f);
            
            fireState = burn < 0.1f ? FireState.None : 
                burn < 0.3f ? FireState.Smoke : 
                burn < 1f ? FireState.Fire : 
                FireState.Burnt;

            if(roomScript) roomScript.UpdateVisuals();
        }
    }

    private void Awake()
    {
        roomDatae = new Room[columns * rows];
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 p = new Vector3(
                    transform.position.x + roomSize.x * x,
                    transform.position.y + roomSize.y * y,
                    transform.position.z
                );

                var r = Instantiate<GameObject> (
                    roomPrefab, p, transform.rotation, transform
                ).GetComponent<RoomScript>();

                roomDatae[y*columns + x] = new Room {
                    roomName = string.Format("Room {0}0{1}", y, x),
                    roomScript = r,
                    gridPos = new Vector2Int(x, y),
                    residents = new List<GameObject>()
                };
                
                r.data = roomDatae[y*columns + x];
            }
        }
    }

    public Room GetRoom(int x, int y) {
        if (x < 0 || y < 0 || x >= columns || y >= rows) return null;
        else if (y*columns + x >= roomDatae.Length) return null;

        var r = roomDatae[y*columns + x];
        Debug.Log(string.Format("[{0},{1}] {2}: {3}", x, y, r.roomName, r.burn));
        return roomDatae[y*columns + x];
    }
}
