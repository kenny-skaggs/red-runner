using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public bool canMoveLeft;

    // Update is called once per frame
    void LateUpdate()
    {
        float newX = player.transform.position.x + offset;

        if (transform.position.x < newX || canMoveLeft) {
            transform.position = new Vector3(
                player.transform.position.x + offset,
                transform.position.y,
                transform.position.z
            );
        }
    }
}
