using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footStepTimer;
    private float footSteptimerMax = 0.1f;

    private void Start() {
        player = GetComponent<Player>();
    }


    private void Update() {
       footStepTimer -= Time.deltaTime;

       if(footStepTimer <= 0) {
        footStepTimer = footSteptimerMax;

        if(player.IsWalking()) {
            float volume = 1f;
            SoundManager.Instance.PlayFootStepsSound(transform.position, volume);
        }
       }
    }
}
