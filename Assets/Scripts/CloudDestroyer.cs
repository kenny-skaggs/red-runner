using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -100) {
            Destroy(gameObject);
        }
    }
}
