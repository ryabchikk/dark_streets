using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;


public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button serverbtn;

    [SerializeField] private Button clientbtn;
    [SerializeField] private Button hostbtn;
    
    private void Awake(){
        serverbtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });

        clientbtn.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            "127.0.0.1",  
            (ushort)12345,
             "0.0.0.0"
        );
            NetworkManager.Singleton.StartClient();
        });

        hostbtn.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            "127.0.0.1",  
            (ushort)12345,
             "0.0.0.0"
        );
            NetworkManager.Singleton.StartHost();
        });

    }
}
