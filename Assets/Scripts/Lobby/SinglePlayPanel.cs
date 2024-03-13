using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class SinglePlayPanel : LobbyPanelBase
{
    [Header("SinglePlayPanel Vars")] 
    [SerializeField] private Button newRoomBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private GameObject roomSelectPanel; // Rooms Buttons을 담고 있음
    [SerializeField] private TextMeshProUGUI addErrorText; // 빈 정보가 담긴 Button을 누르면 나옴
    
    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        
        newRoomBtn.onClick.AddListener(OnClickNewRoom);
        backBtn.onClick.AddListener(OnClickBack);
        
        // 뷸러온 데이터 buttons에 정보 할당
        for (int i = 0; i < roomDataList.Count; i++)
        {
            string roomName = "Room" + (i + 1);
            Transform roomButtonTransform = roomSelectPanel.transform.Find(roomName);

            if (roomButtonTransform != null)
            {
                TextMeshProUGUI buttonText = roomButtonTransform.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = roomDataList[i].roomName;
                }
                else
                {
                    Debug.LogError("Text component not found on " + roomName);
                }
            }
            else
            {
                Debug.LogError(roomName + " not found in RoomPanel");
            }
        }
    }
    
    private void OnClickNewRoom()
    {
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.KeyboardPanel);
    }

    private void OnClickBack()
    {
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.MainPagePanel);
    }

    // 방 선택 
    public void SelectRoom(Button clickedButton)
    {
        string currentTime= DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
        int roomIndex = 0;
        TextMeshProUGUI buttonText = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
        
        foreach (RoomData room in roomDataList)
        {
            if (room.roomName == buttonText.text)
            {
                currentTime = room.currentTime;
                break;
            }
            roomIndex++;
        }
        
        // currentTime이 초기화 상태면 방이 없어 Error
        if (currentTime == DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss"))
        {
            StartCoroutine(NoRoomError());
            return; 
        }

        // roomName과 currentTime을 다음 씬으로 넘겨주는 부분
        GameObject transfer = GameObject.Find("Information Transfer");

        if (transfer != null)
        {
            DontDestroyOnLoad dontDestroyScript = transfer.GetComponent<DontDestroyOnLoad>();

            if (dontDestroyScript != null)
            {
                dontDestroyScript.roomName = buttonText.text;
                dontDestroyScript.currentTime = currentTime;
                dontDestroyScript.roomIndex = roomIndex;
            }
            else
            {
                Debug.LogError("DontDestroyOnLoad ��ũ��Ʈ�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("'Player' �±װ� ������ GameObject�� ã�� �� �����ϴ�.");
        }

        SceneManager.LoadScene(1);
    }

    IEnumerator<object> NoRoomError()
    {
        addErrorText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        addErrorText.gameObject.SetActive(false);
    }
}
