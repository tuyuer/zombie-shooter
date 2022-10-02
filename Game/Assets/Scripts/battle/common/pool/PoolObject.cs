using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{

    public float lifeTime = 3.0f;
    public delegate void OnObjectDestroyed(PoolObject poolObj);
    public event OnObjectDestroyed onObjectDestroyed;

    private float elapsed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > lifeTime)
        {
            DestroySelf();
        }
    }

    public void Reset()
    {
        elapsed = 0;
        gameObject.SetActive(true);
    }

    public virtual void DestroySelf()
    {
        if (onObjectDestroyed != null)
        {
            onObjectDestroyed(this);
        }
    }
}
