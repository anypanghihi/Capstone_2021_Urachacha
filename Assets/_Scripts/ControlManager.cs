using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Urachacha
{
    public class ControlManager : MonoBehaviour, IPunObservable
    {
        public static ControlManager instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<ControlManager>();
                }

                return m_instance;
            }
        }

        private static ControlManager m_instance;

        public int maxPlayerCount;
        public int finishedPlayerCount;
        public string readyCountSecond;

        void Awake() 
        {
            DontDestroyOnLoad(gameObject);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(this.maxPlayerCount);
                stream.SendNext(this.finishedPlayerCount);
                stream.SendNext(this.readyCountSecond);
                stream.SendNext(GameManager.Instance.count);
            }
            else
            {
                this.maxPlayerCount = (int)stream.ReceiveNext();
                this.finishedPlayerCount = (int)stream.ReceiveNext();
                this.readyCountSecond = (string)stream.ReceiveNext();
                GameManager.Instance.count = (float)stream.ReceiveNext();
            }
        }
    }
}