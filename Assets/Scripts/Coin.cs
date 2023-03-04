using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        if (other.gameObject.tag == "Player") {
            // Destroy(other.collider.gameObject);
            Debug.Log("hit coin");
        }
    }
}
