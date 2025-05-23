using UnityEngine;
using UnityEngine.InputSystem;

public class Ballista : MonoBehaviour
{
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject BallistaArrowPath;

    public GameObject currentProjectilePrefab;
    // Update is called once per frame
    public void Tick(float deltaTime, Vector3 mouseScreenPosition)
    {
        var vector3 = transform.position;
        vector3.x = mouseScreenPosition.x;
        transform.position = vector3;
    
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (currentProjectilePrefab != null)
            {
                Instantiate(currentProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            }
        }
    }
    
    public void SetProjectilePrefab(GameObject projectilePrefab)
    {
        currentProjectilePrefab = projectilePrefab;
    }
    
    public void SetArrowPathVisibility(bool isVisible)
    {
        BallistaArrowPath.SetActive(isVisible);
    }
}
