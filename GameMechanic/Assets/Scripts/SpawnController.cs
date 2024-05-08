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