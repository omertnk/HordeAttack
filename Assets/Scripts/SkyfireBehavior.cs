using UnityEngine;

using System.Collections;



public class SkyfireBehavior : MonoBehaviour

{

    [Header("Settings")]

    public float damage = 50f;

    public float explosionRadius = 5f;

    public float warningDuration = 1.0f;



    [Header("Fall Animation")]

    public float fallDuration = 4f;

    public float dropHeight = 40f;

    public float spawnDistance = 0.1f;

    public float meteorScale = 4f;





    [Header("Visual References")]

    public GameObject warningCircle;    

    public GameObject meteorVisual;    

    public GameObject explosionEffect;



    void Start()

    {


        ProjectileBehavior proj = GetComponent<ProjectileBehavior>();

        if (proj != null) Destroy(proj);

        Vector3 targetPos = transform.position + (transform.forward * spawnDistance);

        targetPos.y = 9f; 

        transform.position = targetPos;

        if (meteorVisual != null)

            meteorVisual.transform.localScale = Vector3.one * meteorScale;

        StartCoroutine(ExecuteSkyfire());

    }



    IEnumerator ExecuteSkyfire()

    {


        if (warningCircle != null) warningCircle.SetActive(true);

        if (meteorVisual != null) meteorVisual.SetActive(false); 


        yield return new WaitForSeconds(warningDuration);


        if (warningCircle != null) warningCircle.SetActive(false); 

        if (meteorVisual != null)

        {

            meteorVisual.SetActive(true); 

            Vector3 groundPos = meteorVisual.transform.position;

            Vector3 skyPos = groundPos + (Vector3.up * 20f); 

            float fallTime = 0.2f; 

            float timer = 0;



            while (timer < fallTime)

            {

                meteorVisual.transform.position = Vector3.Lerp(skyPos, groundPos, timer / fallTime);

                timer += Time.deltaTime;

                yield return null;

            }

            meteorVisual.transform.position = groundPos; 

        }


        Explode();

    }



    void Explode()

    {


        if (explosionEffect != null)

            Instantiate(explosionEffect, transform.position, Quaternion.identity);


        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);



        foreach (var hitCollider in hitColliders)

        {

            if (hitCollider.CompareTag("Enemy"))

            {
          

                HealthBar hpScript = hitCollider.GetComponent<HealthBar>();

                if (hpScript == null) hpScript = hitCollider.GetComponentInChildren<HealthBar>();



                if (hpScript != null)

                {

                    hpScript.TakeDamage(damage);

                }

                Debug.Log("Hit Enemy: " + hitCollider.name);

            }

        }

        Destroy(gameObject, 0.5f);

    }

    void OnDrawGizmosSelected()

    {

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, explosionRadius);

    }

}