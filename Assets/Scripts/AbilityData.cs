using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability System/Ability")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public Sprite icon;

    [Header("Savaş Ayarları")]
    public GameObject projectilePrefab;
    public float cooldownTime = 3f;
    public float speed = 10f;
    public float lifeTime = 5f;
    public float damage = 10f;

  
}

