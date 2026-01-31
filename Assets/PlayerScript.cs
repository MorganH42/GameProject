using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent (typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    CharacterController cc;
    Vector3 moveDir;
    [SerializeField]
    float moveSpeed = 10;
    float xAxis;
    [SerializeField]
    float bobSpeed = 10;
    [SerializeField]
    float bobMagnitude = 0.1f;
    float cameraStartPos;
    Camera headCam;
    [SerializeField]
    float sprintMultiplier = 1;
    Ray interactRay;
    RaycastHit interactData;
    [SerializeField]
    GameObject objectInteracted;
    [SerializeField]
    float verticalSensitivity = 1;
    [SerializeField]
    float horizontalSensitivity = 1;
    [SerializeField]
    GameObject itemHeld;
    Rigidbody itemHeldRigidbody;
    [SerializeField]
    GameObject hands;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
        headCam = Camera.main;
        cameraStartPos = headCam.transform.position.y;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        hands = GameObject.Find("hands");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * horizontalSensitivity, 0);
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
        if (Input.GetMouseButtonDown(0))
        {
            if (itemHeld == null)
            {
                interactRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                if (Physics.Raycast(interactRay, out interactData))
                {
                    objectInteracted = interactData.transform.gameObject;
                    if (objectInteracted.tag == "CanHold")
                    {
                        itemHeld = objectInteracted;
                        objectInteracted = null;
                        itemHeldRigidbody = itemHeld.GetComponent<Rigidbody>();
                        itemHeldRigidbody.useGravity = false;
                        itemHeldRigidbody.isKinematic = true;
                        itemHeld.transform.position = hands.transform.position;
                        itemHeld.transform.parent = hands.transform;
                    }

                }
                else
                {
                    objectInteracted = null;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (itemHeld != null)
            {
                Debug.Log("handsfull");
                itemHeldRigidbody.useGravity = true;
                itemHeldRigidbody.isKinematic = false;
                itemHeld.transform.parent = null;
                itemHeld = null;
                itemHeldRigidbody = null;
            }
        }
        xAxis -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        xAxis = Mathf.Clamp(xAxis, -80, 60);
        headCam.transform.localRotation = Quaternion.Euler(xAxis, 0, 0);
        headCam.transform.localPosition = new Vector3(headCam.transform.localPosition.x, cameraStartPos + (Wave(bobMagnitude, bobSpeed) * Mathf.Abs(Input.GetAxis("Vertical"))), headCam.transform.localPosition.z);
    }
    float Wave(float mag, float freq)
    {
        return (mag * Mathf.Sin(Time.time * freq));
    }
}
