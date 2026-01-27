using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent (typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    CharacterController cc;
    Vector3 moveDir;
    float moveSpeed;
    float xAxis;
    float bobSpeed;
    float bobMagnitude;
    float cameraStartPos;
    Camera headCam;
    float sprintMultiplier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
        headCam = Camera.main;
        cameraStartPos = headCam.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("MouseX"), 0);
        moveDir = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDir = transform.rotation * moveDir;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintMultiplier = 2;
        }
        else
        {
            sprintMultiplier = 1;
        }
        cc.SimpleMove(moveDir * moveSpeed * sprintMultiplier);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            bobSpeed = 20;
        }
        else
        {
            bobSpeed = 10;
        }
        xAxis -= Input.GetAxis("Mouse Y");
        xAxis = Mathf.Clamp(xAxis, -80, 80);
        headCam.transform.localRotation = Quaternion.Euler(xAxis, 0, 0);
        headCam.transform.localPosition = new Vector3(headCam.transform.localPosition.x, cameraStartPos + (Wave(bobMagnitude, bobSpeed) * Mathf.Abs(Input.GetAxis("Vertical"))), headCam.transform.localPosition.z);
    }
    float Wave(float mag, float freq)
    {
        return (mag * Mathf.Sin(Time.time * freq));
    }
}
