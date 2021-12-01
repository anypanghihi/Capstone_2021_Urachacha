using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Photon.Pun.Urachacha
{
    public class UIManager : MonoBehaviourPunCallbacks
    {
        public PhotonView PhotonView;
        public static UIManager instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<UIManager>();
                }

                return m_instance;
            }
        }

        private static UIManager m_instance;


        public Text missionText;
        public Text playerCountText;
        public Text countDownText;

        public void UpdateMissionText(string curMission)
        {
            missionText.text = "라운드 목표 : \n" + curMission;
        }

        public void UpdatePlayerCountText(int curPlayer, int maxPlayer)
        {
            playerCountText.text = curPlayer + " / " + maxPlayer;
        }

        public void UpdateCountDown(string count)
        {
            countDownText.text = count;
        }
    }
}
