using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private float volume = 1f;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnAnyPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnythingPlacedHere += BaseCounter_OnAnythingPlacedHere;
        PlateKitchenObject.OnAnyIngredientAdded += PlateKitchenObject_OnAnyIngredientAdded;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    public void PlayFootStepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footStep, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning[0], Vector3.zero);
    }

    public void PlayeWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void PlateKitchenObject_OnAnyIngredientAdded(object sender, EventArgs e)
    {
        PlateKitchenObject plateKitchenObject = sender as PlateKitchenObject;
        PlaySound(audioClipRefsSO.objectPickUp, plateKitchenObject.transform.position);
    }
    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        Player player = sender as Player;
        Transform playerKitchenObjectFollowTransform = player.GetKitchenObjectFollowTransform();
        PlaySound(audioClipRefsSO.objectPickUp, playerKitchenObjectFollowTransform.position);
    }

    private void BaseCounter_OnAnythingPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.GetKitchenObjectFollowTransform().position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter delilverCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, delilverCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter delilverCounter = DeliveryCounter.Instance;

        PlaySound(audioClipRefsSO.deliveryFail, delilverCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplayer = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume * volumeMultiplayer);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeMultiplayer);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
