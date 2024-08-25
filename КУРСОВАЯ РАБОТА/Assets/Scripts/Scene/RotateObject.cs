using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 70f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}