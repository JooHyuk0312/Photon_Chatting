using System.Collections.Generic;
using System.Data.Common;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILobby : MonoBehaviour
    {
        [SerializeField] private InputField inputMaxPlayer;
        [SerializeField] private InputField inputRoomName;

        [SerializeField] private GameObject objContent;
        [SerializeField] private UILobbyRoomItem objRoomItem;

        [SerializeField] private List<UILobbyRoomItem> listRoomItem;

        private List<RoomInfo> _roomList;

        public void SetData(List<RoomInfo> roomList)
        {
            _roomList = roomList;
            for (var i = 0; i < roomList.Count; i++)
            {
                if (listRoomItem.Count > i)
                    listRoomItem[i].SetData(_roomList[i]);
                else
                    Instantiate(objRoomItem, objContent.transform).SetData(_roomList[i]);
            }
        }

        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(inputMaxPlayer.text) || string.IsNullOrWhiteSpace(inputRoomName.text))
            {
                Debug.LogError("Create Room Wrong Input");
                return;
            }

            var roomOptions = new RoomOptions();

            if (byte.TryParse(inputMaxPlayer.text, out roomOptions.MaxPlayers) == false)
            {
                Debug.LogError("Create Room Wrong Input");
                return;
            }

            if (_roomList.Find(x => x.Name == inputRoomName.text) != null)
            {
                Debug.LogError("Already Exists Room Name");
                return;
            }

            PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);

            
        }
    }
}