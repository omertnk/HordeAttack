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
    }

    // Inspector'dan dolduracağımız liste (Sırasıyla: Bıçak, Balta, Kutsal Su, vb.)
    public List<ActiveAbility> allAbilities = new List<ActiveAbility>();

    void Update()
    {
        // Aktif olan her yeteneği tek tek kontrol et
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
            // Süre doldu, ateş et!
            PerformAttack(ability);
            // Sayacı sıfırla
            ability.currentCooldown = ability.data.cooldownTime;
        }
    }

    void PerformAttack(ActiveAbility ability)
    {
        if (ability.data.projectilePrefab != null)
        {
            // 1. Çıkış noktası FirePoint (sabit)
            Vector3 spawnPosition = (firePoint != null) ? firePoint.position : transform.position;

            // 2. YÖN HESABI (Kamera Yönü ama Düz)
            // Kameranın baktığı yönü al
            Vector3 shootDirection = Camera.main.transform.forward;
            
            // Y eksenini (yukarı/aşağı) sıfırla ki mermi düz gitsin
            shootDirection.y = 0; 
            
            // Normalize et (Y'yi silince vektör kısalır, hızı korumak için boyunu tekrar 1 yapıyoruz)
            shootDirection.Normalize();

            // 3. Rotasyon Hesabı
            Quaternion spawnRotation = Quaternion.LookRotation(shootDirection);

            // 4. Oluştur
            GameObject projectile = Instantiate(ability.data.projectilePrefab, spawnPosition, spawnRotation);
            
            ProjectileBehavior projScript = projectile.GetComponent<ProjectileBehavior>();
            
            // Eğer script Ana Objede (Parent) değilse hata vermesin diye kontrol:
            if (projScript == null) 
                projScript = projectile.GetComponentInChildren<ProjectileBehavior>();

            if (projScript != null)
            {
                projScript.Setup(shootDirection, ability.data.damage, ability.data.speed, ability.data.lifeTime);
            }
        }
    }

    // LEVEL ATLAMA SİSTEMİNİN ÇAĞIRACAĞI FONKSİYON
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
                // Eğer zaten açıksa belki Level'ini arttırırsın (Hasar artışı vs.)
                Debug.Log(allAbilities[index].data.abilityName + " zaten açık, seviyesi artırılıyor...");
            }
        }
    }
}