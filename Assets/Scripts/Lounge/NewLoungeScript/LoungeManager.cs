using UnityEngine;

public class LoungeManager : MonoBehaviour
{
    public TypeGame typeGame;
    public TypeHero typeHero;
    public TypeSupplymentary typeSupplymentary;
    private void OnEnable()
    {
        typeGame = TypeGame.None;
        typeHero = TypeHero.None;
    }
}


