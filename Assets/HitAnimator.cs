using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimator : MonoBehaviour
{
    [SerializeField] AudioClip hitSound;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }

    void HitAnimationDone() {
        Destroy(gameObject);
    }
}
