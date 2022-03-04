using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {
    GameObject target;

    Animator ani;
    Rigidbody rig;
    NavMeshAgent nav;

    [SerializeField]
    float maxHealth = 100f;
    float nowHealth = 100f;

    [SerializeField]
    float maxCooldown = 2f;
    float nowCooldown = 0f;

    private void Start() {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (nowHealth <= 0) {
            if (nav.enabled) {
                rig.isKinematic = true;
                ani.SetBool("Die", true);
                nav.enabled = false;

                StartCoroutine("monsterDead");
            }
            return;
        }
        else {
            if (target != null) {
                nav.isStopped = false;
                nav.speed = 3.5f;
                nav.SetDestination(target.transform.position);
                ani.SetBool("Run", true);

                if (target.GetComponent<PlayerController>().getHealth() > 0) {
                    float dist = Vector3.Distance(target.transform.position, transform.position);
                    
                    if (dist <= 2f) {
                        if (nowCooldown >= maxCooldown) {
                            nowCooldown = 0f;
                            ani.SetTrigger("Attack");
                            StartCoroutine("getPlayerDamaged");
                        }
                        else {
                            nowCooldown += Time.deltaTime;
                        }
                    }
                    else {
                        StopCoroutine("getPlayerDamaged");
                    }
                }
                else {
                    target = null;
                }
            }
            else {
                nav.isStopped = true;
                ani.SetBool("Run", false);
            }

            rig.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && other.name == "Player") {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && other.name == "Player") {
            target = null;
        }
    }

    IEnumerator getPlayerDamaged() {
        yield return new WaitForSeconds(0.5f);
        target.GetComponent<PlayerController>().getDamaged(5f);
    }

    IEnumerator monsterDead() {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    public void getDamaged(float damage) {
        if (nowHealth > 0) {
            nowCooldown = 0f;
            ani.SetTrigger("Damaged");
            nowHealth -= damage;
        }
    }

    public float getMaxHealth() {
        return maxHealth;
    }

    public float getHealth() {
        return nowHealth;
    }
}
