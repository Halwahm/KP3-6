using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 5f; // Скорость движения камеры
    public float rotationSpeed = 200f; // Скорость вращения камеры

    public float minY = 1f; // Минимальная высота камеры
    public float maxY = 4f; // Максимальная высота камеры

    public float scrollSpeed = 3.5f; // Скорость изменения высоты камеры

    public int maxX; // Максимальная координата X
    public int minX; // Минимальная координата X
    public int maxZ; // Максимальная координата Z
    public int minZ; // Минимальная координата Z

    private void Update()
    {
        // Движение камеры по горизонтали
        float horizontal = Input.GetAxis("Horizontal");
        // Движение камеры по вертикали
        float vertical = Input.GetAxis("Vertical");

        // Передвижение камеры вперед/назад и вправо/влево
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;

        // Рассчитываем новую позицию
        Vector3 newPosition = transform.position + movement * movementSpeed * Time.deltaTime;

        // Проверяем наличие препятствий перед камерой
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement, out hit, movement.magnitude))
        {
            // Если обнаружено столкновение, корректируем новую позицию
            newPosition = hit.point - movement.normalized * 0.1f; // Чтобы избежать застревания камеры в стенах
        }
        else
        {
            // Ограничиваем горизонтальное перемещение в пределах заданных значений
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        }

        // Применяем новую позицию
        transform.position = newPosition;

        // Вращение камеры вокруг вертикальной оси при зажатии правой кнопки мыши
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, mouseX);
        }

        // Поднятие и опускание камеры по вертикальной оси
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
