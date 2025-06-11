using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    // Start is called before the first frame update
    private const string STOVE_COUNTER_IS_FLASHING = "isFlashing";
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoverCounter_OnProgressChanged;

        animator.SetBool(STOVE_COUNTER_IS_FLASHING, false);
    }

    private void StoverCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool isFlashing = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(STOVE_COUNTER_IS_FLASHING, isFlashing);
    }

}
