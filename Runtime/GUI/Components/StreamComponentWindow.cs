using FMETP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

public class StreamComponentWindow : UIWindowDragable
{

    public RawImage streamRenderTexture;

    public UiElement streamPictureInPictureWindow;
    public UiWindow fullscreenStreamWindow;


    public void Awake()
    {

        base.Awake();

        GameManager.onNewSelectedStream += OnStreamSwitched;
        GameManager.onPlayerSwitchScenes += OnStreamSwitched;
        UiManager.OnWindowSwitch += OnWindowSwitch;

    }

    public void ApplySkinData()
    {

        streamRenderTexture.texture = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].baseStreamThumbnail;

        base.ApplySkinData();


        streamRenderTexture.texture = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].baseStreamThumbnail;

    }

    void OnStreamSwitched(int playerID)
    {

        string levelName = "";

        if (playerID == -1)
        {

            UiManager.Instance.GetComponent<GameViewDecoder>().label = (ushort)(6969);
            streamRenderTexture.texture = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].baseStreamThumbnail;
            streamPictureInPictureWindow.GetComponentInChildren<RawImage>().texture = null;

        }
        else
        {

            if(GameManager.Instance.connectedPlayers[playerID].activeSceneIndex == -1)
                levelName = "";
            else
                levelName = GameManager.Instance.clientSkinDataSets[GameManager.Instance.currentClientIndex].levels[GameManager.Instance.connectedPlayers[playerID].activeSceneIndex].title;

            UiManager.Instance.GetComponent<GameViewDecoder>().label = (ushort)(1000 + playerID + 1);

        }

        (windowHeader["playerLevel"] as StringVariable).Value = levelName;
        (fullscreenStreamWindow.windowHeader["playerLevel"] as StringVariable).Value = levelName;

    }

    public void OnWindowSwitch(int index)
    {

        if(GameManager.Instance.currentPlayerId != -1)
            if (UiManager.Instance.mainWindows[index].opensStreamInPiPMode)
                TogglePiPStream(true);
            else
                TogglePiPStream(false);

    }

    void TogglePiPStream(bool setActive)
    {

        streamPictureInPictureWindow.FadeElement(setActive);

    }

}