using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
   [SerializeField] private StoveCounter stoveCounter;
   private AudioSource audioSource;

   private void Start() {
        audioSource = GetComponent<AudioSource>();
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
   }

   private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
    bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
    if(playSound) {
        audioSource.Play();
    } else {
        audioSource.Stop();
    }
   }
}
