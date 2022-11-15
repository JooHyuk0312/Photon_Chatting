using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    [SerializeField]
    InputField input_Nickname;

    [SerializeField]
    Text label_PlayerList;

    [SerializeField]
    InputField input_Message;

    [SerializeField]
    GameObject object_Content;

    [SerializeField]
    GameObject object_ContentText;

    PhotonView photonView;

    string string_userName;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE && !UNITY_ANDROID
        Screen.SetResolution(540, 960, false);
#endif

        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (input_Message.isFocused)
            {

            }
            else
            {
                input_Message.ActivateInputField();
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 5;
        string_userName = input_Nickname.text;
        PhotonNetwork.LocalPlayer.NickName = input_Nickname.text;
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);

        input_Nickname.gameObject.SetActive(false);
    }

    public void OnEndEdit()
    {
        string strMessage = string_userName + " : " + input_Message.text;

        photonView.RPC("RPC_Chat", RpcTarget.All, strMessage);
        input_Message.text = "";
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayer();
        AddMessage($"{input_Nickname.text}님이 방에 참가하였습니다.");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayer();
        AddMessage($"{newPlayer.NickName}님이 입장하였습니다.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayer();
        AddMessage($"{otherPlayer.NickName}님이 퇴장하였습니다.");
    }

    public void Connect()
    {
        AddMessage("연결 중...");

        if (PhotonNetwork.ConnectUsingSettings() == false)
            AddMessage("연결 실패");
        else
            AddMessage("연결 성공");
    }

    void AddMessage(string message)
    {
        if (object_ContentText == null)
            return;

        if (object_Content == null)
            return;

        GameObject obj = Instantiate(object_ContentText, object_Content.transform);

        obj.GetComponent<Text>().text = message;
        object_Content.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    void UpdatePlayer()
    {
        string curPlayers = "--접속 유저--\n\n";

        foreach (var player in PhotonNetwork.PlayerList)
        {
            curPlayers += player.NickName + "\n";
        }

        label_PlayerList.text = curPlayers;
    }

    [PunRPC]
    void RPC_Chat(string message)
    {
        AddMessage(message);
    }
}
