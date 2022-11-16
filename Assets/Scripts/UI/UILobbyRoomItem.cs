using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILobbyRoomItem : MonoBehaviour
    {
        [SerializeField] private Text textRoomName;
        [SerializeField] private Text textRoomMember;
        [Space(10)] [SerializeField] private Button buttonJoinRoom;

        private RoomInfo _roomInfo;

        public void SetData(RoomInfo roomInfo)
        {
            _roomInfo = roomInfo;

            textRoomName.text = _roomInfo.Name;
            textRoomMember.text = $"{_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers}";
        }

        public void OnClickJoinRoom()
        {
            PhotonNetwork.JoinRoom(_roomInfo.Name);
        }
    }
}