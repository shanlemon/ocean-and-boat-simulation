using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField] private Vector3 position1;
    [SerializeField] private Vector3 position2;
    [SerializeField] private Vector3 rotation1;
    [SerializeField] private Vector3 rotation2;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = position1;
        transform.localEulerAngles = rotation1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && !WaterController.current.isGamePaused)
        {
            transform.localPosition = (transform.localPosition == position1) ? position2 : position1;
            transform.localEulerAngles = (transform.localPosition == position2) ? rotation2 : rotation1;
        }
    }
}
