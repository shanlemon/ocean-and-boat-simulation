using UnityEngine;

public class WheelRotator : MonoBehaviour {


    public static WheelRotator instance;

	[SerializeField] private float turningSpeed = 1f;

	[SerializeField] private float maxTurnAngle = 360;

	public float angle {private set; get;}

	private void Start() {
		angle = 0;
	}

	// Update is called once per frame
	void Update() {
		TurnShip(Input.GetAxis("Horizontal"));
	}

	private void TurnShip(float input) {
		if ((angle < -maxTurnAngle && input > 0) || (angle > maxTurnAngle && input < 0) || (angle <= maxTurnAngle && angle >= -maxTurnAngle && input != 0)) {
            Vector3 turnAmt = Vector3.right * input * turningSpeed * Time.deltaTime;
			angle += turnAmt.x;
            transform.Rotate(turnAmt, Space.Self);
		}
	}
}
