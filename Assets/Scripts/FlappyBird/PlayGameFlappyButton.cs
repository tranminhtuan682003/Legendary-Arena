using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGameFlappyButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Handle);
    }

    private void Handle()
    {
        // Gọi các phương thức để chuyển đổi trạng thái UI
        UIFlappyManager.Instance.ChangeStateScreenPlay(true);
        UIFlappyManager.Instance.ChangeStateScreenLoad(false);
        UIFlappyManager.Instance.ChangeStateScreenGameOver(false);
        UIFlappyManager.Instance.CreateFlappyBird();
    }
}
