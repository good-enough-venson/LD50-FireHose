using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class RoomScript : MonoBehaviour
{
    public ApartmentManager.Room data;

    public ParticleSystem fire;
    public ParticleSystem smoke;

    public MeshRenderer[] burntMeshes;

    public Color unburntColor, burntColor;
    public AnimationCurve colorChangeCurve;

    public void UpdateVisuals()
    {
        if (data.fireState == ApartmentManager.FireState.Fire) {
            if (!fire.isPlaying) fire.Play();
        } else if(fire.isPlaying) fire.Stop();

        if (data.fireState == ApartmentManager.FireState.Smoke) {
            if (!smoke.isPlaying) smoke.Play();
        } else if(smoke.isPlaying) smoke.Stop();

        foreach(var m in burntMeshes){
            m.material.color = Vector4.Lerp(
                unburntColor, burntColor, 
                colorChangeCurve.Evaluate(data.burn)
            );
        }
    }
}
