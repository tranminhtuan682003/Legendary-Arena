using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public abstract class SceneTransitionManager : MonoBehaviour
{
    private Animator animator;
    private float transitionDuration;
    private AsyncOperation asyncLoad;
    private string nameScene;
    private string nameAnimation;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        StartCoroutine(TransitionAndLoadScene());
    }

    protected virtual void InitLize(string nameScene, float transitionDuration, string nameAnimation)
    {
        this.nameScene = nameScene;
        this.transitionDuration = transitionDuration;
        this.nameAnimation = nameAnimation;
    }

    private IEnumerator TransitionAndLoadScene()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.2f);
        asyncLoad = SceneManager.LoadSceneAsync(nameScene);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(transitionDuration);

        asyncLoad.allowSceneActivation = true;
    }
}
