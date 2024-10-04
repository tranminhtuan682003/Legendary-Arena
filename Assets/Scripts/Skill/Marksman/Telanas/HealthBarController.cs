using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private Camera cameraPlayer;

    void Start()
    {
        cameraPlayer = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Xác định hướng từ thanh máu đến camera
        Vector3 directionToCamera = cameraPlayer.transform.position - transform.position;

        // Loại bỏ trục Y để thanh máu chỉ quay theo trục X và Z (tránh việc nó bị nghiêng theo trục Y)
        directionToCamera.y = 0;
    }
}
