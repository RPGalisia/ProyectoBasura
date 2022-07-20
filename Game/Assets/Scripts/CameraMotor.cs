using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform target;    
    

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().position = new Vector3(target.position.x, target.position.y, -10);
    }
}
