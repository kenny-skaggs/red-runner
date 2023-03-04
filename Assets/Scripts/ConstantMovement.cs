using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement.x * Time.deltaTime, movement.y * Time.deltaTime, 0);
    }
}
