using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform camera;

    public float speed = 7f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;





        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }

        /* if (Input.GetKey(KeyCode.W))
         {
             transform.position += transform.TransformDirection((Vector3.forward * speed) * Time.deltaTime);
             transform.rotation = Quaternion.Euler(0, 180f + camera.yAngle, 0);
         }

         if (Input.GetKey(KeyCode.S))
         {
             transform.position += transform.TransformDirection((Vector3.back * speed) * Time.deltaTime);
             transform.rotation = Quaternion.Euler(0, -180f + camera.yAngle, 0);
         }

         if (Input.GetKey(KeyCode.A))
         {
             transform.position += transform.TransformDirection((Vector3.left * speed) * Time.deltaTime);
             transform.rotation = Quaternion.Euler(0, 90f + camera.xAngle, 0);
         }

         if (Input.GetKey(KeyCode.D))
         {
             transform.position += transform.TransformDirection((Vector3.right * speed) * Time.deltaTime);
             transform.rotation = Quaternion.Euler(0, -90f + camera.xAngle, 0);
         }
     } */
    }
}
