using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    public float fallSpeed = 3f;
    void Start()
    {
        StartCoroutine(FallRoutine());
    }

    IEnumerator FallRoutine()
    {
        while (true)
        {
            Vector3 movement = new Vector3(0f, -fallSpeed * Time.deltaTime, 0f);
            transform.Translate(movement);

            if (transform.position.y < -5f)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = 5f;
                transform.position = newPosition;
            }

            yield return null;
        }
    }
}
