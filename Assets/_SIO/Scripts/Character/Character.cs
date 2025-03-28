using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public virtual void Initialize()
    {

    }

    private void Start()
    {
        Initialize();
    }
}
