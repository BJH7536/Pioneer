using UnityEngine;
using UnityEngine.UI;

public class MainPagePanel : LobbyPanelBase
{
    [Header("MainPagePanel Vars")] 
    [SerializeField] private Button singlePlayBtn;
    [SerializeField] private Button multiPlayBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button quitBtn;

    [SerializeField] private GameObject mainMenuBg;
    
    // VRUIP에서 UI OnOff는 제공
    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        
        singlePlayBtn.onClick.AddListener(OnClickSinglePlay);
        multiPlayBtn.onClick.AddListener(OnClickMultiPlay);
        settingBtn.onClick.AddListener(OnClickSetting);
        quitBtn.onClick.AddListener(OnClickQuit);
    }
    private void OnClickSinglePlay()
    {
        base.ClosePanel();
        mainMenuBg.SetActive(false);
        lobbyUIManager.ShowPanel(LobbyPanelType.SinglePlayPanel);
    }
    
    private void OnClickMultiPlay()
    {
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.MultiPlayPanel);
    }
    
    private void OnClickSetting()
    {
        base.ClosePanel();
        lobbyUIManager.ShowPanel(LobbyPanelType.SettingsPanel);
    }
    
    private void OnClickQuit()
    {
        base.ClosePanel();
        // 게임 종료
    }
}
