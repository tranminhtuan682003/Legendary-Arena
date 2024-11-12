using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransitionToPickHero : MonoBehaviour
{
    private Animator animator;
    private float animationTime = 2f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAnimationAndLoadScene("PickHero"));
    }

    private IEnumerator PlayAnimationAndLoadScene(string targetScreen)
    {
        // Kích hoạt animation và đợi cho nó chạy một thời gian nhất định
        animator.SetTrigger("Run");
        yield return new WaitForSeconds(animationTime);
        SelectGameManager.Instance.ReadyScreenState(true);

        yield return new WaitForSeconds(10f);

        UIManager.Instance.isPickHeroScreen = true;
        UIManager.Instance.ShowScreen(targetScreen);
    }
}
