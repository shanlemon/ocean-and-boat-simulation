using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
	
    private Rigidbody rigidbody;

	[SerializeField] private float ForwardForce = 10;
	[SerializeField] private float TurningTorque = 50;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Forward Force
        if (Input.GetKey(KeyCode.W) && !WaterController.current.isGamePaused) 
        {
            GoForward();
        }
    }

    void GoForward()
    {
        rigidbody.AddForce(transform.forward * ForwardForce, ForceMode.Acceleration);

        Vector3 torque = torque = new Vector3(0, TurningTorque * WheelRotator.Instance.angle, 0);
        rigidbody.AddTorque(torque);
    }
}
