using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoverBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;


    private void Start()
    {
        stoveCounter.OnProgressChanged += StoverCounter_OnProgressChanged;
        Hide();
    }

    private void StoverCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

}
