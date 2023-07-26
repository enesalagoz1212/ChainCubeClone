using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTime;

    private void OnEnable()
    {
        DOVirtual.DelayedCall(destroyTime, () =>
        {
            Destroy(gameObject);
        });
    }
}
