using System;
using UnityEngine;

public class ArtificialBuoyancy : MonoBehaviour
{
    public float BobbingStrength = .1f;
    public float SwayStrength = .1f;

    private Quaternion startRotation;
    private float originalYPos;
    void Start()
    {
        originalYPos = this.transform.position.y;
        startRotation = transform.rotation;
    }

    void LateUpdate()
    {
        BobUpAndDown();
        SwaySideToSide();
    }

    private void BobUpAndDown()
    {
        transform.position = new Vector3(transform.position.x,
                                         originalYPos + ((float)Math.Sin(Time.time) * BobbingStrength),
                                         transform.position.z);
    }

    private void SwaySideToSide()
    {
        float f = Mathf.Sin(Time.time * SwayStrength) * 2f;
        transform.rotation = startRotation * Quaternion.AngleAxis(f, Vector3.back);
    }
}
