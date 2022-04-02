using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomRaycasterScript : MonoBehaviour
{
    void Awake(){
        GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
    }
}
