using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour {
    MonsterController controller;
    GameObject hpBar;

    float maxHealth = 100f;
    float maxWidth = 1.2f;
     
    private void Start() {
        controller = GetComponent<MonsterController>();
        hpBar = gameObject.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    private void Update() {
        if (maxHealth != controller.getHealth()) {
            float nowWidth = maxWidth * (controller.getHealth() / controller.getMaxHealth());

            Vector2 size = hpBar.GetComponent<RectTransform>().sizeDelta;
            hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(nowWidth, size.y);
        }
    }
}
