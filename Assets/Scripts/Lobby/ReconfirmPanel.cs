using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ReconfirmPanel : LobbyPanelBase
{
    [Header("ReconfirmPanel Vars")] 
    [SerializeField] private Button reconfirmBtn;
    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        reconfirmBtn.onClick.AddListener(OnClickReconfirm);
    }

    private void OnClickReconfirm()
    {
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.RoomTypePanel);
    }
}
