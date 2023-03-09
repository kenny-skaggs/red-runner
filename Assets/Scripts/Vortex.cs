using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vortex : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            GameManager.Instance.EnterDungeon(transform.position);
            GameManager.Instance.RemoveFromScene(gameObject);
        }
    }
}
