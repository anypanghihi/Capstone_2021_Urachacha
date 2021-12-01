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

        public GameObject MissionDisplay;
        public Text missionText;

        public GameObject PlayerCountDisplay;
        public Text playerCountText;

        public GameObject ReadyCountDisplay;
        public Text readyCountText;

        public void UpdateMissionText(string curMission)
        {
            missionText.text = "라운드 목표 : \n" + curMission;
        }

        public void UpdatePlayerCountText(int curPlayer, int maxPlayer)
        {
            playerCountText.text = "인원 : \n" + curPlayer + " / " + maxPlayer;
        }

        public void UpdateCountDown(string count)
        {
            readyCountText.text = count;
        }
    }
}
