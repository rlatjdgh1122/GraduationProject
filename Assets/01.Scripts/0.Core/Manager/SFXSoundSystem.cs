using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSoundSystem : MonoBehaviour
{
    private void OnEnable()
    {
        SignalHub.OnIceArrivedEvent += OnGroundArriveSFX;
    }

    private void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= OnGroundArriveSFX;
    }

    private void OnGroundArriveSFX()
    {
        SoundManager.Play2DSound(SoundName.GroundHit);
    }
}
