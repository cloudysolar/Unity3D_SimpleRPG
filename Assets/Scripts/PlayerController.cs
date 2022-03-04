using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [Range(0f, 10f)]
    public float moveSpeed = 5f;

    [Range(0f, 10f)]
    public float rotationSpeed = 5f;

    [SerializeField]
    float health = 100f;

    Animator ani;
    Rigidbody rig;
    Slider slider;

    private void Start() {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        slider = GameObject.Find("Health Slider").GetComponent<Slider>();
    }

    private void Update() {
        slider.value = health;

        if (health > 0) {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float mouseX = Input.GetAxisRaw("Mouse X");

            int nowAnimation = ani.GetCurrentAnimatorStateInfo(0).fullPathHash;

            transform.Rotate(new Vector3(0f, mouseX, 0f) * rotationSpeed);

            if (h != 0f || v != 0f) {
                ani.SetBool("Sprint", true);

                if (nowAnimation == Animator.StringToHash("Base Layer.Idle") || nowAnimation == Animator.StringToHash("Base Layer.Sprint")) {
                    transform.Translate(new Vector3(h * moveSpeed * Time.deltaTime, 0f, v * moveSpeed * Time.deltaTime));
                }
            }
            else {
                ani.SetBool("Sprint", false);
            }

            if (Input.GetMouseButtonDown(0)) {
                ani.SetBool("Sprint", false);
                ani.SetTrigger("Attack1");
            }

            if (Input.GetMouseButtonDown(1)) {
                ani.SetBool("Sprint", false);
                ani.SetTrigger("Attack2");
            }
        }
        else {
            ani.SetBool("Die", true);
        }

        rig.angularVelocity = Vector3.zero;
    }

    public void getDamaged(float damage) {
        if (health > 0) {
            health -= damage;
            ani.SetTrigger("Damaged");
        }
    }

    public float getHealth() {
        return health;
    }
}
