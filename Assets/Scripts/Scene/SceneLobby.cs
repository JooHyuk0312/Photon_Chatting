using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Scene
{
    public class SceneLobby : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UI.UILobby lobby;

        private List<RoomInfo> _roomList = new List<RoomInfo>();

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _roomList = roomList;

            UpdateRoomList();
        }

        private void UpdateRoomList()
        {
            if (PhotonNetwork.IsConnected)
                lobby.SetData(_roomList);
        }
    }
}