using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    List<GameObject> spawnPoints = new List<GameObject>();

    [SerializeField]
    GameObject monsterPrefab;

    [SerializeField]
    GameObject rootObj;

    [SerializeField]
    float spawnCooldown = 10f;
    float nowCooldown = 0f;

    private void Start() {
        
    }

    private void Update() {
        if (spawnPoints.Count > 0) {
            if (monsterPrefab != null) {
                if (nowCooldown >= spawnCooldown) {
                    nowCooldown = 0f;

                    int random = Random.Range(0, spawnPoints.Count);
                    GameObject monster = Instantiate(monsterPrefab, spawnPoints[random].transform.position, Quaternion.identity, rootObj.transform);
                }
                else {
                    nowCooldown += Time.deltaTime;
                }
            }
        }
    }
}
