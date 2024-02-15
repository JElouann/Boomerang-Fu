using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect _effect; 
    [SerializeField] private VisualEffectAsset _effectAsset;

    private void Start()
    {
        _effect.Play();
    }
}
