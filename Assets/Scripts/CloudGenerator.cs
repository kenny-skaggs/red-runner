using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float minDelay;
    public float maxDelay;

    public Vector2 yRange;
    public Vector2 zRange;

    public GameObject[] cloudPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        ScheduleCloud();
    }

    void ScheduleCloud()
    {
        float delay = Random.Range(minDelay, maxDelay);
        Invoke(nameof(SpawnCloud), delay);
    }

    void SpawnCloud()
    {
        // Make a cloud
        int cloudIndex = Random.Range(0, cloudPrefabs.Length);
        GameObject cloudPrefab = cloudPrefabs[cloudIndex];
        GameObject newCloud = Instantiate(
            cloudPrefab,
            new Vector3(transform.position.x, Random.Range(yRange.x, yRange.y), Random.Range(zRange.x, zRange.y)),
            transform.rotation
        );

        // Set the new cloud to move
        float newCloudSpeed = Random.Range(minSpeed, maxSpeed);
        ConstantMovement movementScript = newCloud.GetComponent<ConstantMovement>();
        movementScript.movement = new Vector2(-newCloudSpeed, 0);


        // Schedule the next cloud
        ScheduleCloud();
    }
}
