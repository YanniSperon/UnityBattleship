using UnityEngine;

public class Movable : MonoBehaviour
{
    public Transform cameraHolder = null;
    public float moveSpeed = 25.0f;

    private void Update()
    {
        Vector3 pos = cameraHolder.position;
        if (Input.GetKey(KeyCode.W))
        {
            pos += (this.transform.forward * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos -= (this.transform.forward * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos -= (this.transform.right * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos += (this.transform.right * Time.deltaTime * moveSpeed);
        }
        cameraHolder.position = pos;
    }
}
