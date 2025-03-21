using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    public float lockOnRange = 100f;
    public Transform currentTarget;
    public PlayerMovement playerMovement;
    public GameObject lockOnIndicatorPrefab;
    private GameObject activeIndicator;

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (playerMovement.lockedToTarget)
            {
                LockOntoCloserEnemy();
            }
            else
            {
                LockOntoClosestEnemy();
            }
        }

        if (playerMovement.lockedToTarget && currentTarget != null)
        {
            LockOntoTarget();
        }
    }

    void LockOntoClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lockOnRange);
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            currentTarget = closestEnemy;
            playerMovement.lockedToTarget = true;
            SpawnIndicator();
        }
        else
        {
            playerMovement.lockedToTarget = false;
            currentTarget = null;
            DestroyIndicator();
        }
    }

    void LockOntoCloserEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lockOnRange);
        Transform closestEnemy = currentTarget;
        float closestDistance = currentTarget != null ? Vector2.Distance(transform.position, currentTarget.position) : float.MaxValue;

        foreach (var col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        if (closestEnemy != currentTarget)
        {
            currentTarget = closestEnemy;
            SpawnIndicator();
        }
        else
        {
            playerMovement.lockedToTarget = false;
            currentTarget = null;
            DestroyIndicator();
        }
    }

    void LockOntoTarget()
    {
        if (currentTarget != null)
        {
            Vector2 directionToTarget = currentTarget.position - transform.position;
            transform.up = directionToTarget.normalized;
        }
    }

    void SpawnIndicator()
    {
        DestroyIndicator();
        if (currentTarget != null && lockOnIndicatorPrefab != null)
        {
            Vector3 indicatorPosition = currentTarget.position;
            indicatorPosition.z = -1;

            activeIndicator = Instantiate(lockOnIndicatorPrefab, indicatorPosition, Quaternion.identity);
            activeIndicator.transform.SetParent(currentTarget);

            ApplyGlowEffect(activeIndicator);
        }
    }

    void ApplyGlowEffect(GameObject indicator)
    {
        SpriteRenderer sr = indicator.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Material glowMaterial = new Material(Shader.Find("Sprites/Default"));
            glowMaterial.SetColor("_Color", Color.white);
            glowMaterial.EnableKeyword("_EMISSION");
            glowMaterial.SetColor("_EmissionColor", Color.white);   

            sr.material = glowMaterial;
        }
    }

    void DestroyIndicator()
    {
        if (activeIndicator != null)
        {
            Destroy(activeIndicator);
        }
    }
}
