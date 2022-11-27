using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    void Update()
    {
        transform.position = transform.parent.position; 
    }
}
