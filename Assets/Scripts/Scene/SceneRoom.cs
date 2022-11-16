using System;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Scene
{
    public class SceneRoom : MonoBehaviourPunCallbacks
    {
        public static SceneRoom Instance { get; private set; } = null;

        [SerializeField] private UI.UIRoom uiRoom;


        private new PhotonView _photonView;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (photonView == null)
                _photonView = transform.GetOrAddComponent<PhotonView>();
        }

        public override void OnJoinedRoom()
        {
            uiRoom.UpdatePlayer();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            uiRoom.UpdatePlayer();
            uiRoom.AddChat($"{newPlayer.NickName} 님이 입장하였습니다");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            uiRoom.UpdatePlayer();
            uiRoom.AddChat($"{otherPlayer.NickName} 님이 퇴장하였습니다");
        }

        public void SendChat(string message)
        {
            _photonView.RPC(PhotonUtil.RPC_SENDCHAT, RpcTarget.All, message);
        }


        [PunRPC]
        void RPC_Chat(string message)
        {
            uiRoom.AddChat(message);
        }
    }
}