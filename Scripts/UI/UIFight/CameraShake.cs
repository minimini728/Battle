using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;

    private float ShakeTime;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = new Vector3(0f, 0f, -10f);

        EventDispatcher.instance.AddEventHandler((int)EventEnum.eEventType.ShakeCamera, this.Shake);
    }
    void Update()
    {
        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;

            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;

            transform.position = initialPosition;
        }
    }

    public void Shake(short type)
    {
        this.VibrateForTime(0.3f);
    }
    public void VibrateForTime(float time)
    {
        ShakeTime = time;

    }
}
