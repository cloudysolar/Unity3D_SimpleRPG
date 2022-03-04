using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public float damage = 10f;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Monster" && other.name == "Body") {
            GameObject monster = other.gameObject.transform.parent.gameObject;
            GameObject player = GameObject.FindWithTag("Player");

            if (player.name == "root") {
                player = player.transform.parent.gameObject;
            }

            MonsterController conteroller = monster.GetComponent<MonsterController>();

            int nowAnimation = player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash;

            if (nowAnimation == Animator.StringToHash("Base Layer.Attack1") || nowAnimation == Animator.StringToHash("Base Layer.Attack2")) {
                conteroller.getDamaged(damage);
            }
        }
    }
}
