using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowXZ : MonoBehaviour
{

    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = position;
    }
}
