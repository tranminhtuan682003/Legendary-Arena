using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotateArea : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Camera Camera;
    private Vector3 initialPosition;
    private float moveSpeed = 0.1f;

    // Giới hạn cho trục X và Z
    private float minX = 0;
    private float maxX = 15f;
    private float minZ = 0;
    private float maxZ = 80;

    void Start()
    {
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        initialPosition = Camera.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIManager.Instance.isDragCamera = true;

        float deltaX = eventData.delta.x * moveSpeed;
        float deltaZ = eventData.delta.y * moveSpeed;

        Vector3 newPosition = Camera.transform.position + new Vector3(deltaX, 0, deltaZ);

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        Camera.transform.position = newPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIManager.Instance.isDragCamera = false;
    }
}
