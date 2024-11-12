using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneManager : MonoBehaviour
{
    private Animator animator;
    private float animationTime = 0.5f;
    private float transitionTime = 5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAnimationAndLoadScene("Lounge"));
    }

    private IEnumerator PlayAnimationAndLoadScene(string targetScene)
    {
        // Kích hoạt animation và đợi cho nó chạy một thời gian nhất định
        animator.SetTrigger("Run");
        yield return new WaitForSeconds(animationTime);

        // Bắt đầu tải Scene bất đồng bộ nhưng không kích hoạt ngay
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false;

        // Đợi thêm thời gian cần thiết trước khi kích hoạt chuyển cảnh
        yield return new WaitForSeconds(transitionTime - animationTime);

        // Kích hoạt chuyển Scene
        asyncLoad.allowSceneActivation = true;
    }
}
