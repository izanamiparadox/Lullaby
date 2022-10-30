using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptPali : MonoBehaviour
{
    public float camRotY;
    public Transform player;

    public void Update()
    {
        transform.rotation = Quaternion.Euler(0f, camRotY, 0f);
        transform.position = player.transform.position;
    }

}
