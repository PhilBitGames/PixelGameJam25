using UnityEngine;

public class DeadUnit : MonoBehaviour
{
    public void GetSoulTaken()
    {
        Destroy(gameObject);
    }
}