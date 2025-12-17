using UnityEngine;
using System.Collections.Generic;

public class AbilityController : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint; 
        
    [System.Serializable]
    public class ActiveAbility
    {
        public AbilityData data;
        public float currentCooldown;
        public bool isUnlocked;
        public bool isCameraFront;
        public bool isCharacterFront;
    }
    
    public List<ActiveAbility> allAbilities = new List<ActiveAbility>();

    void Update()
    {
        foreach (var ability in allAbilities)
        {
            if (ability.isUnlocked)
            {
                HandleAbilityCooldown(ability);
            }
        }
    }

    void HandleAbilityCooldown(ActiveAbility ability)
    {
        ability.currentCooldown -= Time.deltaTime;

        if (ability.currentCooldown <= 0)
        {
            PerformAttack(ability);
            ability.currentCooldown = ability.data.cooldownTime;
        }
    }

    void PerformAttack(ActiveAbility ability)
    {
        if (ability.data.projectilePrefab != null)
        {
            Vector3 spawnPosition = (firePoint != null) ? firePoint.position : transform.position;
            Vector3 shootDirection = Vector3.zero;
            if (ability.isCameraFront)
            {
                shootDirection = Camera.main.transform.forward;
            }

            if (ability.isCharacterFront)
            {
                shootDirection = transform.forward;
            }

            shootDirection.y = 0; 
            
            shootDirection.Normalize();
            
            Quaternion spawnRotation = Quaternion.LookRotation(shootDirection);
            
            GameObject projectile = Instantiate(ability.data.projectilePrefab, spawnPosition, spawnRotation);
            
            ProjectileBehavior projScript = projectile.GetComponent<ProjectileBehavior>();
            
          
            if (projScript == null) 
                projScript = projectile.GetComponentInChildren<ProjectileBehavior>();

            if (projScript != null)
            {
                projScript.Setup(shootDirection, ability.data.damage, ability.data.speed, ability.data.lifeTime);
            }
        }
    }
    
    public void UnlockAbility(int index)
    {
        if (index < allAbilities.Count)
        {
            if (!allAbilities[index].isUnlocked)
            {
                allAbilities[index].isUnlocked = true;
                Debug.Log(allAbilities[index].data.abilityName + " Açıldı!");
            }
            else
            {
                Debug.Log(allAbilities[index].data.abilityName + " zaten açık, seviyesi artırılıyor...");
            }
        }
    }
}