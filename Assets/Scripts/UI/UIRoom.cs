using System;
using System.Linq;
using System.Net.Mime;
using System.Net.WebSockets;
using Photon.Pun;
using Scene;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIRoom : MonoBehaviour
    {
        [SerializeField] private GameObject objectContents;
        [SerializeField] private GameObject objectChatItem;

        [SerializeField] private Text textPlayerList;
        
        [SerializeField] private InputField inputMessage;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return) && !inputMessage.isFocused)
                inputMessage.ActivateInputField();
        }

        public void OnEndEditMessage()
        {
            if(Input.GetKeyDown(KeyCode.Return))
                OnClickSend();
        }

        public void OnClickSend()
        {
            SendChat(inputMessage.text);
            inputMessage.text = string.Empty;
        }

        private void SendChat(string message)
        {
            message = $"{PhotonNetwork.LocalPlayer.NickName}:{message}";

            SceneRoom.Instance.SendChat(message);

            AddChat(message);
        }

        public void UpdatePlayer()
        {
            var curPlayers = PhotonNetwork.PlayerList.Aggregate("--접속 유저--\n\n", (current, player) => current + (player.NickName + "\n"));

            textPlayerList.text = curPlayers;
        }

        public void AddChat(string message)
        {
            if (objectChatItem == null)
                return;

            if (objectContents == null)
                return;

            var obj = Instantiate(objectChatItem, objectContents.transform);

            obj.GetComponent<Text>().text = message;
            objectContents.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }
}