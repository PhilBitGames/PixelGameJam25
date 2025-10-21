using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    public event Action<Target> OnDestroyed;
}