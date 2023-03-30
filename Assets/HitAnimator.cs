using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimator : MonoBehaviour
{
    void HitAnimationDone() {
        Destroy(gameObject);
    }
}
