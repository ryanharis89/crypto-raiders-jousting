using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    public RoundHandler roundHandler;
    [SerializeField] private GameObject playerPrefabA; //add prefab in inspector
    [SerializeField] private GameObject playerPrefabB; //add prefab in inspector

    [ServerRpc(RequireOwnership = false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        GameObject newPlayer;
        if (prefabId == 0)
        {
            newPlayer = (GameObject)Instantiate(playerPrefabA, roundHandler.leftPosition, roundHandler.leftPositionRotation);
            roundHandler.InitializePlayerOne(newPlayer);
        }
        else
        {
            newPlayer = (GameObject)Instantiate(playerPrefabB, roundHandler.rightPosition, roundHandler.rightPositionRotation);
            roundHandler.InitializePlayerTwo(newPlayer);
        }
        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 0);
        }
        else
        {
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 1);
        }
    }

}
