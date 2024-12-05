using UnityEngine;

public class GameGlobalController : MonoBehaviour
{
    private AsyncOperation sceneLounge;
    private void OnEnable()
    {
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetSceneLoungeAsyncOperation(AsyncOperation sceneLounge)
    {
        this.sceneLounge = sceneLounge;
    }

    public void GoOnSceneLounge()
    {
        sceneLounge.allowSceneActivation = true;
    }
}
