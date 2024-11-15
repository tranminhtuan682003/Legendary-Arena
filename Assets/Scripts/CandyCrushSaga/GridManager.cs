using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int width = 8;
    private int height = 8;
    private float candySize = 1.1f;
    private GameObject[,] gridArray;

    private void Start()
    {
        gridArray = new GameObject[width, height];
        StartCoroutine(CreateGrid());
    }

    private IEnumerator CreateGrid()
    {
        yield return new WaitForSeconds(1f);

        float startX = -width / 2 * candySize;
        float startY = -height / 2 * candySize + candySize / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(startX + x * candySize, startY + y * candySize, 0);
                GameCandyManager.instance.GetBackgroundBuble(position, Quaternion.identity);

                GameObject candyInstance = GameCandyManager.instance.GetCandy(position, Quaternion.identity);
                gridArray[x, y] = candyInstance;
            }
        }
        CheckMatches();
    }

    public void CheckMatches()
    {
        List<GameObject> candiesToDestroy = new List<GameObject>();

        // Kiểm tra theo chiều ngang
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width - 2; x++)
            {
                GameObject currentCandy = gridArray[x, y];
                if (currentCandy != null)
                {
                    CandyType type = currentCandy.GetComponent<Candy>().candyType;
                    if (gridArray[x + 1, y] != null && gridArray[x + 2, y] != null)
                    {
                        if (gridArray[x + 1, y].GetComponent<Candy>().candyType == type &&
                            gridArray[x + 2, y].GetComponent<Candy>().candyType == type)
                        {
                            candiesToDestroy.Add(gridArray[x, y]);
                            candiesToDestroy.Add(gridArray[x + 1, y]);
                            candiesToDestroy.Add(gridArray[x + 2, y]);
                        }
                    }
                }
            }
        }

        // Kiểm tra theo chiều dọc
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height - 2; y++)
            {
                GameObject currentCandy = gridArray[x, y];
                if (currentCandy != null)
                {
                    CandyType type = currentCandy.GetComponent<Candy>().candyType;
                    if (gridArray[x, y + 1] != null && gridArray[x, y + 2] != null)
                    {
                        if (gridArray[x, y + 1].GetComponent<Candy>().candyType == type &&
                            gridArray[x, y + 2].GetComponent<Candy>().candyType == type)
                        {
                            candiesToDestroy.Add(gridArray[x, y]);
                            candiesToDestroy.Add(gridArray[x, y + 1]);
                            candiesToDestroy.Add(gridArray[x, y + 2]);
                        }
                    }
                }
            }
        }

        // Phá hủy các viên kẹo trong danh sách sau 0.5 giây
        foreach (var candy in candiesToDestroy)
        {
            StartCoroutine(DestroyCandyWithAnimation(candy));
        }
    }

    private IEnumerator DestroyCandyWithAnimation(GameObject candy)
    {
        Animator animator = candy.GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetTrigger("Disappear");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        yield return new WaitForSeconds(0f); // Chờ thêm 0.5 giây trước khi phá hủy

        Vector2Int pos = FindCandyPosition(candy);
        if (pos.x != -1 && pos.y != -1)
        {
            gridArray[pos.x, pos.y] = null;
        }
        SoundCandyManager.Instance.PlayDestroySound();
        SoundCandyManager.Instance.PlayPointSound();
        candy.SetActive(false);
    }

    private Vector2Int FindCandyPosition(GameObject candy)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridArray[x, y] == candy)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public void SwapCandies(GameObject candy, Vector2 direction)
    {
        Vector2Int candyPosition = FindCandyPosition(candy);
        if (candyPosition == new Vector2Int(-1, -1)) return;

        Vector2Int targetPosition = candyPosition + Vector2Int.RoundToInt(direction);

        if (targetPosition.x >= 0 && targetPosition.x < width && targetPosition.y >= 0 && targetPosition.y < height)
        {
            GameObject targetCandy = gridArray[targetPosition.x, targetPosition.y];
            if (targetCandy != null)
            {
                gridArray[candyPosition.x, candyPosition.y] = targetCandy;
                gridArray[targetPosition.x, targetPosition.y] = candy;

                StartCoroutine(SmoothMove(candy, targetCandy.transform.position));
                StartCoroutine(SmoothMove(targetCandy, candy.transform.position, true));
            }
        }
    }

    private IEnumerator SmoothMove(GameObject candy, Vector3 targetPosition, bool checkAfterMove = false)
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Vector3 startingPosition = candy.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            candy.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }

        candy.transform.position = targetPosition;

        if (checkAfterMove)
        {
            yield return new WaitForSeconds(0.5f); // Chờ thêm 0.5 giây sau khi hoán đổi
            CheckMatches();
        }
    }
}
