using Unity.VisualScripting;
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
    [SerializeField]
    float verticalSensitivity = 1;
    [SerializeField]
    float horizontalSensitivity = 1;
    [SerializeField]
    InventoryManager inventoryManager;
  
    
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = headCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, 3))
            {
                ItemPickable item = hitInfo.collider.gameObject.GetComponent<ItemPickable>();

                if(item != null)
                {
                    inventoryManager.ItemPicked(hitInfo.collider.gameObject);
                }
            }
        }

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

        xAxis -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        xAxis = Mathf.Clamp(xAxis, -80, 80);
        headCam.transform.localRotation = Quaternion.Euler(xAxis, 0, 0);
        headCam.transform.localPosition = new Vector3(headCam.transform.localPosition.x, cameraStartPos + (Wave(bobMagnitude, bobSpeed) * Mathf.Abs(Input.GetAxis("Vertical"))), headCam.transform.localPosition.z);
    }
    float Wave(float mag, float freq)
    {
        return (mag * Mathf.Sin(Time.time * freq));
    }

    
}
