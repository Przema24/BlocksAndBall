using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IHittable
{
    public virtual void Hit()
    {
        Destroy(gameObject);
    }
}
