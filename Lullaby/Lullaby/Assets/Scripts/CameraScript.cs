using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float yAngle;
    public float xAngle;

    public Transform player;

    public Vector3 offset;

    // Update is called once per frame

   

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, yAngle, 0);
        transform.rotation = Quaternion.Euler(xAngle, 0, 0);

        transform.position = player.position + offset;
    }
}
