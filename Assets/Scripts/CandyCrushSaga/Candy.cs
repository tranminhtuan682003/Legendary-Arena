using UnityEngine;

public class Candy : MonoBehaviour
{
    public CandyType candyType;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private GridManager gridManager;
    private float minSwipeDistance = 0.5f; // Khoảng cách tối thiểu để nhận diện vuốt

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void OnMouseDown()
    {
        startTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        endTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;

        if (swipeDirection.magnitude >= minSwipeDistance)
        {
            swipeDirection.Normalize();

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                // Vuốt ngang
                if (swipeDirection.x > 0)
                {
                    gridManager.SwapCandies(this.gameObject, Vector2.right); // Vuốt sang phải
                }
                else
                {
                    gridManager.SwapCandies(this.gameObject, Vector2.left); // Vuốt sang trái
                }
            }
            else
            {
                // Vuốt dọc
                if (swipeDirection.y > 0)
                {
                    gridManager.SwapCandies(this.gameObject, Vector2.up); // Vuốt lên
                }
                else
                {
                    gridManager.SwapCandies(this.gameObject, Vector2.down); // Vuốt xuống
                }
            }
        }
    }
}
