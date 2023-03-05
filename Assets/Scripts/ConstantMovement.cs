using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public Vector2 movement;
    public float rotation;

    // Update is called once per frame
    void Update()
    {
        if (movement.x != 0 || movement.y != 0) {
            transform.Translate(movement.x * Time.deltaTime, movement.y * Time.deltaTime, 0);
        }

        if (rotation != 0) {
            transform.Rotate(
                new Vector3(0, 0, rotation * Time.deltaTime)
            );
        }
    }
}
