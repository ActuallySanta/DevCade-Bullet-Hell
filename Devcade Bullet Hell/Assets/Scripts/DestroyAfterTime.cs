using System;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (destroyTimer > 0)
        {
            Invoke("DestroyObject", destroyTimer);
        }
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
