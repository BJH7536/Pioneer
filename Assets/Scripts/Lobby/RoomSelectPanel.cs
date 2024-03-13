using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RoomSelectPanel : LobbyPanelBase
{
    [Header("RoomSelectPanel Vars")] 
    [SerializeField] public TextMeshProUGUI roomType;
    [SerializeField] public TextMeshProUGUI roomSize;
    
    [SerializeField] private Button chooseBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button prevMoveBtn;
    [SerializeField] private Button nextMoveBtn;
    [SerializeField] private GameObject player;
    
    private int roomTypeIndex = 1;

    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);

        chooseBtn.onClick.AddListener(OnClickChoose);
        backBtn.onClick.AddListener(OnClickBack);
        prevMoveBtn.onClick.AddListener(() => OnClickMoveRoom(prevMoveBtn));
        nextMoveBtn.onClick.AddListener(() => OnClickMoveRoom(nextMoveBtn));
    }

    private void OnClickChoose()
    {
        //todo DataManager에 PlushRoom 연동
    }

    private void OnClickBack()
    {
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.RoomTypePanel);
        player.transform.position = new Vector3(0, 1, -1.5f);
    }
    
    private void OnClickMoveRoom(Button clickedButton)
    {
        RoomDataClass[] roomInfoArray = null;

        if (roomType.text == "Cube")
        {
            roomInfoArray = _cubeInfo;
        }
        else if (roomType.text == "Cylinder")
        {
            roomInfoArray = _cylinderInfo;
        }

        if (roomInfoArray != null)
        {
            if (clickedButton.gameObject.name == "Icon Prev")
            {
                roomTypeIndex--;
                if (roomTypeIndex < 0)
                {
                    roomTypeIndex = roomInfoArray.Length - 1;
                }
            }
            else if (clickedButton.gameObject.name == "Icon Next")
            {
                roomTypeIndex++;
                if (roomTypeIndex >= roomInfoArray.Length)
                {
                    roomTypeIndex = 0;
                }
            }

            roomSize.text = roomInfoArray[roomTypeIndex].roomSize;
            player.transform.position = roomInfoArray[roomTypeIndex].position;
        }
    }
}
