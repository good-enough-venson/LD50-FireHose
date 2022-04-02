using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody _rb;
    new public Rigidbody rigidbody { 
        get { return _rb == null ? _rb = GetComponent<Rigidbody>() : _rb; }
        set { _rb = value; } 
    }

    public ProjectilePooler pool;

    void OnCollisionEnter(Collision other) {
        var room = other.collider.GetComponent<RoomScript>();
        //if (room) Modifier.ActOn(room);
    }
}
