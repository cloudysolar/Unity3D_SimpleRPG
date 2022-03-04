using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    public Transform target;

    [Range(0f, 10f)]
    public float dist = 10.0f;

    [Range(0f, 10f)]
    public float height = 5.0f;

    [Range(0f, 10f)]
    public float rotationSpeed = 5.0f;

    private void LateUpdate() {
        float angle = Mathf.LerpAngle(transform.eulerAngles.y, target.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);

        transform.position = target.position - (rotation * Vector3.forward * dist) + (Vector3.up * height);
        transform.LookAt(target);
    }
}
