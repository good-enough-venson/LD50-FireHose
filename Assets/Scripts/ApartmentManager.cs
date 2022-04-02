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

    public enum FireState {
        None, Smoke, Fire, Burnt
    }

    [System.Serializable]
    public struct Room
    {
        public GameObject roomObj;
        public Vector2Int roomNum;
        public FireState fireState;
        public float lastFireStateChange;
        public List<GameObject> residents;
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
                    roomPrefab, p, Quaternion.identity, transform
                );

                roomDatae[y * columns + x] = new Room
                {
                    roomObj = r,
                    roomNum = new Vector2Int(x, y),
                    fireState = FireState.None,
                    lastFireStateChange = 0,
                    residents = new List<GameObject>()
                };
            }
        }
    }
}
