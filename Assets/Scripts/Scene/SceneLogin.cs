using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class SceneLogin : MonoBehaviour
    {
        [SerializeField] private InputField inputNickName;

        public void OnClickJoinButton()
        {
            if (string.IsNullOrEmpty(inputNickName.text))
                return;
            
            PhotonNetwork.LocalPlayer.NickName = inputNickName.text;

            if (PhotonNetwork.ConnectUsingSettings())
            {
                Debug.Log(PhotonNetwork.InLobby);
                
                Debug.Log(PhotonNetwork.CurrentLobby.Name);
                
                // if (PhotonNetwork.JoinLobby()))
                //     StartCoroutine(CoJoinLobby());
                // else
                //     Debug.LogError("접속 실패");
            }
            else
                Debug.LogError("접속 실패");
        }

        private IEnumerator CoJoinLobby()
        {
            yield return new WaitUntil(() => PhotonNetwork.IsConnected);

            SceneManager.LoadScene("Lobby");
        }
    }
}