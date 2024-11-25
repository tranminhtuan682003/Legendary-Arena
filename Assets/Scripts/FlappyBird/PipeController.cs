using UnityEngine;

public class PipeController : MonoBehaviour
{
    private float speed = 1f;
    private bool hasPassed = false;

    private void OnEnable()
    {
        // Cập nhật tốc độ mỗi khi Pipe được kích hoạt lại
        speed = FlappyBirdGameManager.Instance.GetCurrentPipeSpeed();
        hasPassed = false; // Đặt lại trạng thái để tính điểm cho lần tiếp theo
    }

    void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FlappyBird") && !hasPassed)
        {
            hasPassed = true;
            SoundFlappyManager.Instance.PlayPointSound();
            FlappyBirdGameManager.Instance.SetScore(1);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
