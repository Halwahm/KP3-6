using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 5f; 
    public float rotationSpeed = 200f; 

    public float minY = 1f;
    public float maxY = 4f; 

    public float scrollSpeed = 3.5f;

    public int maxX; 
    public int minX; 
    public int maxZ;
    public int minZ;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * vertical + transform.right * horizontal;

        Vector3 newPosition = transform.position + movement * movementSpeed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement, out hit, movement.magnitude))
            newPosition = hit.point - movement.normalized * 0.1f; // „тобы избежать застревани€ камеры в стенах
        else
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        }

        transform.position = newPosition;

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, mouseX);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 scrollDirection = Quaternion.AngleAxis(25f, Vector3.right) * Vector3.up * scroll;
            Vector3 newPositionScroll = transform.position + scrollDirection * scrollSpeed;
            newPositionScroll.y = Mathf.Clamp(newPositionScroll.y, minY, maxY);
            transform.position = newPositionScroll;
        }
    }
}
