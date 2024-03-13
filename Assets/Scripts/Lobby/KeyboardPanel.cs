using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardPanel : LobbyPanelBase
{
    [Header("KeyboardPanel Vars")] 
    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TextMeshProUGUI changeNameErrorText;
    [SerializeField] private Button finishBtn;
    [SerializeField] private Button backBtn;

    [SerializeField] private TMP_InputField reconfirmTitle; //Reconfirm Page의 Text
    
    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        inputRoomName.text = "";
        
        finishBtn.onClick.AddListener(OnClickFinish);
        backBtn.onClick.AddListener(OnClickBack);
    }

    private void OnClickFinish()
    {
        foreach (RoomData room in roomDataList)
        {
            // 저장된 방 이름이면 Error 후 초기화
            if (room.roomName == inputRoomName.text)
            {
                StartCoroutine(SameRoomError());
                inputRoomName.text = "";
                return;
            }
        }

        // 이름이 적혀있으면 Page 이동
        if (inputRoomName.text.Length >= 2)
        {
            base.ClosePanel();
            lobbyUIManager.ShowPanel(LobbyPanelType.ReconfirmPanel);
            reconfirmTitle.text = inputRoomName.text;
        }  
    }
    
    IEnumerator SameRoomError()
    {
        changeNameErrorText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        changeNameErrorText.gameObject.SetActive(false);
    }

    private void OnClickBack()
    {
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.SinglePlayPanel);
    }
}
