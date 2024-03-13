using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTypePanel : LobbyPanelBase
{

    [Header("RoomTypePanel Vars")] 
    [SerializeField] private Button cubeBtn;
    [SerializeField] private Button cylinderBtn;
    [SerializeField] private GameObject player;

    private RoomSelectPanel roomTypeAndSize;
    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        
        cubeBtn.onClick.AddListener(OnClickCube);
        cylinderBtn.onClick.AddListener(OnClickCylinder);
    }

    private void OnClickCube()
    {
        roomTypeAndSize.roomType.text = "Cube";
        roomTypeAndSize.roomSize.text = _cubeInfo[1].roomSize;
        player.transform.position = _cubeInfo[1].position;
        
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.RoomSelectPanel);
    }

    private void OnClickCylinder()
    {
        throw new System.NotImplementedException();
    }
}
