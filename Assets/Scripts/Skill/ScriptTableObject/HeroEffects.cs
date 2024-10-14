using UnityEngine;
[CreateAssetMenu(fileName = "HeroEffects", menuName = "ScriptableObjects/HeroEffects")]
public class HeroEffects : ScriptableObject
{
    public ParticleSystem shootEffect;
    public ParticleSystem returnHomeEffect;
    public ParticleSystem Skill1;
    public ParticleSystem Skill2;
    public ParticleSystem Skill3;
    public GameObject bulletPrefab;
    public GameObject healthBar;
}
