using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ссылка на объект (игрока)
    public Vector3 offset = new Vector3(0, 5, -7); // Смещение камеры
    public float smoothSpeed = 0.125f; // Плавность

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Опционально: камера всегда смотрит на цель
        // transform.LookAt(target); 
    }
}