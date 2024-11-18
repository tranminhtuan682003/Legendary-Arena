using System;
public static class KnightEventManager
{
    public static Action<string> OnButtonPressed;

    public static void NotifyButtonPressed(string action)
    {
        OnButtonPressed?.Invoke(action);
    }
}
