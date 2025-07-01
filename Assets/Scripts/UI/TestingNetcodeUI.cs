using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;

    [SerializeField] private NetworkManager networkManager;

    private void Start() {
        startHostButton.onClick.AddListener(() => {
            networkManager.StartHost();
            Hide();
        });

        startClientButton.onClick.AddListener(()=> {
            networkManager.StartClient();
            Hide();
        });

        
    }

    private void Hide() {
            gameObject.SetActive(false);
        }

        private void Show() {
            gameObject.SetActive(true);
        }

}
