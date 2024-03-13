using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LobbyPanelBase : MonoBehaviour
{
    [field: SerializeField, Header("LobbyPanelBase Vars")]
    public LobbyPanelType PanelType { get; private set; }

    protected LobbyUIManager lobbyUIManager;
    
    protected List<RoomData> roomDataList = new List<RoomData>();
    private DataManager getRoomData;
    
    protected RoomDataClass[] _cubeInfo;
    protected RoomDataClass[] _cylinderInfo;

    private void Start()
    {
        roomDataList = getRoomData.ReadRoomDataFromFile();
        
        _cubeInfo = new RoomDataClass[]
        {
            new RoomDataClass("Cube", "small", new Vector3(20f, 1f, 0f)),
            new RoomDataClass("Cube", "middle", new Vector3(40f, 1f, 0f)),
            new RoomDataClass("Cube", "large", new Vector3(60f, 1f, 0f))
        };

        _cylinderInfo = new RoomDataClass[]
        {
            new RoomDataClass("Cylinder", "small", new Vector3(20f, 1f, 20f)),
            new RoomDataClass("Cylinder", "middle", new Vector3(40f, 1f, 20f)),
            new RoomDataClass("Cylinder", "large", new Vector3(60f, 1f, 20f))
        };
    }
    

    //Main Menu UI and OVRPlayerController/RoomSelect UI
    public enum LobbyPanelType
    {
        None,
        MainPagePanel,
        SinglePlayPanel,
        KeyboardPanel,
        ReconfirmPanel,
        RoomTypePanel,
        MultiPlayPanel,
        SettingsPanel,
        RoomSelectPanel
    }

    public virtual void InitPanel(LobbyUIManager uiManager)
    {
        lobbyUIManager = uiManager;
    }
    
    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    protected void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
    
}
