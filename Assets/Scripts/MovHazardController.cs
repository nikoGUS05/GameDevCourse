using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovHazardController : MonoBehaviour
{
    public int moveVel = 6;

    Transform startPosition;

    public Transform[] spawnPosMovHazard;
    public Transform endPosMovHazard;

    public GameObject MovHazardPrefab;

    GameObject spawnedMovHazard;
       

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x != GameManager.instance.endPosMovHazard.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (endPosMovHazard.position.x, transform.position.y, transform.position.z), Time.deltaTime * moveVel);
        }

        int MovHazard = Random.Range(0, 100);
        int chances = Random.Range(0, 10);
        if (chances > 0)
        {
            spawnedMovHazard = Instantiate(MovHazardPrefab, new Vector3(spawnPosMovHazard[MovHazard].position.x, spawnPosMovHazard[MovHazard].position.y, spawnPosMovHazard[MovHazard].position.z), Quaternion.identity, transform);
        }
    }

}
