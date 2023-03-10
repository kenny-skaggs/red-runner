using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGenerator : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float minDelay;
    public float maxDelay;

    public Vector2 yRange;
    public Vector2 zRange;
    public Vector2 xRange;

    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        SchedulePrefab();
    }

    void SchedulePrefab()
    {
        float delay = Random.Range(minDelay, maxDelay);
        Invoke(nameof(SpawnPrefab), delay);
    }

    void SpawnPrefab()
    {
        // Make a cloud
        int prefabIndex = Random.Range(0, prefabs.Length);
        GameObject prefab = prefabs[prefabIndex];
        Vector3 position = new Vector3(
            Random.Range(xRange.x, xRange.y),
            Random.Range(yRange.x, yRange.y), 
            Random.Range(zRange.x, zRange.y)
        );
        Instantiate(prefab, position, transform.rotation);


        // Schedule the next prefab
        SchedulePrefab();
    }
}
