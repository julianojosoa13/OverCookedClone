using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject particleGameObject;

    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if(e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        stoveOnVisual.SetActive(true);
        particleGameObject.SetActive(true);
    }

    private void Hide() {
        stoveOnVisual.SetActive(false);
        particleGameObject.SetActive(false);
    }


}
