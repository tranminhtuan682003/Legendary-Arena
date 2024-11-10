using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour
{
    public bool usePortrait;

    void Awake()
    {
        if (usePortrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }
}
