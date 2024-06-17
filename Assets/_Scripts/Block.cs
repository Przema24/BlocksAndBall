using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour, IHittable
{
    public delegate void OnBlockHit();
    public static event OnBlockHit onBlockHit;

    public virtual void Hit()
    {
        onBlockHit?.Invoke();
        Destroy(gameObject);
    }
}
