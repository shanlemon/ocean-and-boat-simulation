using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
	
    private Rigidbody rigidbody;
    private WheelRotator wheelRotator;

	[SerializeField] private float ForwardForce = 10;
	[SerializeField] private float TurningTorque = 50;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        wheelRotator = GetComponentInChildren<WheelRotator>();

        if (wheelRotator == null) Debug.LogError("Wheel rotator not found!");
    }

    // Update is called once per frame
    void Update()
    {
        //Forward Force
        if (Input.GetKey(KeyCode.W)) 
        {
            GoForward();
        }
    }

    void GoForward()
    {
        rigidbody.AddForce(transform.forward * ForwardForce, ForceMode.Acceleration);

        Vector3 torque = torque = new Vector3(0, TurningTorque * wheelRotator.angle, 0);
        rigidbody.AddTorque(torque);
    }
}
