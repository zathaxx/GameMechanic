# Spawn Entities from Spawnpoint With Custom Parameters 2D

## Description

This mechanic shows how to spawn prefabs of any type from the location of the corresponding game object, creating a spawnpoint. Each spawnpoint has custom parameters, such as the prefab that should be spawned, the target it will follow, the speed of each entity, the limit on how many entities can spawn, and the minimum range between the spawnpoint and the target in which entities will be allowed to spawn. This could be useful when developing a platformer or any other PvE game where enemies follow the player and prefer to have enemies always spawn in the same locations.

## Implementation

Create a new GameObject in the Unity Editor and attach the SpawnController script to it. Assign both the Entity and Target to prefabs and change all other custom parameters to a value greater than zero. It may be useful to attach a Sprite Renderer to the GameObject so that the spawnpoint can be easily seen during testing.

```cs
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private float lastSpawn;

    public GameObject entity;
    public GameObject target;
    public int entitySpeed;
    public int entityLimit;
    public float spawnRate;
    private float turnSpeed;
    public int spawnRange;


    // Start is called before the first frame update
    void Start()
    {
        turnSpeed = 180f;
        SpawnEntity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawn >= spawnRate && Vector3.Distance (target.transform.position, transform.position) > spawnRange) {
            SpawnEntity();
        }
        MoveEntities();
    }

    void SpawnEntity() {
        if (transform.childCount < entityLimit) {
            GameObject e = Instantiate(entity);
            e.transform.position = transform.position;
            e.transform.parent = transform;
        }                
        lastSpawn = Time.time;
    }

    void MoveEntities() {
        for (int i = 0; i < transform.childCount; i++) {
                Transform currentEntity = transform.GetChild(i);
                Vector3 direction = target.transform.position - currentEntity.position;
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle - 90f);
                currentEntity.rotation = Quaternion.RotateTowards(currentEntity.rotation, targetRotation, turnSpeed * 2 * Time.deltaTime);
                currentEntity.position += currentEntity.up * entitySpeed * Time.deltaTime;
        }
    }


}
```

