using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager: MonoBehaviourPunCallbacks
{
    private readonly Dictionary<string, string> _roomPasswords = new Dictionary<string, string>();
    public NetworkManager instance;

    public NetworkManager Instance
    {
        get
        {
            if (instance != null) return instance;
            
            GameObject obj = new GameObject("NetworkManager");
            instance = obj.AddComponent<NetworkManager>();
            DontDestroyOnLoad(obj);
            return instance;
        }
    }
    
    public void Init()
    {
        PhotonNetwork.ConnectUsingSettings();
        instance = this;
    }

    public override void OnConnectedToMaster()
    {
        JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            if (room.CustomProperties.TryGetValue("password", out var property))
            {
                _roomPasswords[room.Name] = property.ToString();
            }
        }
    }

    public void SetMaxPlayer(int maxPlayers)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.MaxPlayers = (byte)maxPlayers;
        }
    }

    public void SetPassword(string password)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable newProperties = new ExitGames.Client.Photon.Hashtable
            {
                { "password", password }
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(newProperties);
        }
    }

    public void SetPublic()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.CurrentRoom.IsVisible = true;
        }
    }
    
    public void SetPrivate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }

    public void SetSingle()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SetPrivate();
            SetMaxPlayer(1);
            // 멤버 강퇴 코드
        }
    }

    public void SetMulti()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SetPublic();
            SetMaxPlayer(5);
        }
    }

    private static void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    private RoomOptions GetRoomOption(bool isSingle, int userNumber = 5, string password = "")
    {
        if (isSingle)
        {
            return new RoomOptions {
                MaxPlayers = 1,
                IsVisible = false,
                IsOpen = false
            };
        }
        return new RoomOptions {
            MaxPlayers = userNumber,
            IsVisible = true,
            IsOpen = true,
            CustomRoomProperties = new Hashtable
            {
                {"password", password},
            }
        };
    }

    public void CreateSingleRoom(string roomName)
    {
        var roomOptions = GetRoomOption(isSingle: true);
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void CreateMultiRoom(string roomName, string password, int userNumber)
    {
        RoomOptions roomOptions = GetRoomOption(isSingle: false, userNumber, password);

        PhotonNetwork.CreateRoom(roomName, roomOptions: roomOptions);
    }

    public bool TryJoinRoom(string roomName, string inputPassword)
    {
        if (_roomPasswords.ContainsKey(roomName) && _roomPasswords[roomName] == inputPassword)
        {
            PhotonNetwork.JoinRoom(roomName);
            return true;
        }
        Debug.Log("Incorrect password or room does not exist.");
        return false;
    }

    // if Ultimate XR, singleton 
    public void SpawnPlayer(GameObject playerPrefab, Transform centerEyeAnchor)
    {
        var playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        if (!playerInstance.GetComponent<PhotonView>().IsMine) return;
        
        SyncPosition syncScript = playerInstance.GetComponent<SyncPosition>();
        if (syncScript != null)
        {
            syncScript.target = centerEyeAnchor;
        }
    }
}