using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [ExecuteInEditMode]
public class spin_controller : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        if (rotateX)
        {
            transform.Rotate(rotationSpeed * delta, 0, 0);
        }
        if (rotateY)
        {
            transform.Rotate(0, rotationSpeed * delta, 0);
        }
        if (rotateZ)
        {
            transform.Rotate(0, 0, rotationSpeed * delta);
        }
    }
}