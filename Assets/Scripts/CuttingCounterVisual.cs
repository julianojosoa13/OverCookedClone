using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update

    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
    }

    private void CuttingCounter_OnProgressChanged(object sender , CuttingCounter.OnProgressChangedEventArgs e) {
        if(e.progressNormalized > 0) {
            animator.SetTrigger(CUT);
        }
    }
}
