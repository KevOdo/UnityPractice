using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform ThirdPersonTransform;
    public Transform FirstPersonTransform;
    public Camera FirstPersonCam;
    public Camera ThirdPersonCam;
    public Transform ActiveCam;
    public float speed = 2f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    
    void Start()
    {
        ActiveCam = ThirdPersonTransform;
        ThirdPersonCam.enabled = true;
        FirstPersonCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.speed = 6f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            this.speed = 2f;
        }
        if(Input.GetKeyDown("c"))
        {
            ThirdPersonCam.enabled = !ThirdPersonCam.enabled;
            FirstPersonCam.enabled = !FirstPersonCam.enabled;
            Debug.Log(ThirdPersonCam.enabled);
            if(ThirdPersonCam.enabled){
                ActiveCam = ThirdPersonTransform;
            } else {
                ActiveCam = FirstPersonTransform;
            }
        }

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + ActiveCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
