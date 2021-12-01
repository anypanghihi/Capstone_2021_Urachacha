using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


namespace Photon.Pun.Urachacha
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

		static public GameManager Instance;

		public float count;

		#endregion

		#region Private Fields

		private GameObject instance;

		private bool counted = true;

        [Tooltip("The prefab to use for representing the player")]
        [SerializeField]
        private GameObject playerPrefab;

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
		{
			Instance = this;

			// in case we started this demo with the wrong scene being active, simply load the menu scene
			if (!PhotonNetwork.IsConnected)
			{
				SceneManager.LoadScene("LobbyScene");

				return;
			}

			if (playerPrefab == null) 
			{ // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
			}
			else 
			{
				if (PlayerManager.LocalPlayerInstance==null)
				{
				    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

					// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,1f,-16f), Quaternion.identity, 0);
				}
				else
				{
					Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
				}
			}
		}

		void Update()
		{
			if (GameObject.FindGameObjectsWithTag("Player").Length == ControlManager.instance.maxPlayerCount && counted)
			{		
				StartCounting();

				//카운트 세고 벽 없애기
				//전체 비추는 카메라 비활성화하기
				//ControlManager 포톤으로 변수 동기화 해야함 or UIManager RPC로 업데이트 해야함 (둘 중 하나인건 Debug 해서 어떤 함수가 작동하는지 알아보고 정하기)
			}

			// "back" button of phone equals "Escape". quit app if that's pressed
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				PuaseApplication();
			}
		}

		void Awake() 
        {
            //DontDestroyOnLoad(gameObject);
        }

        #endregion

		#region Public Methods

		public void PuaseApplication()
		{
			//
		}

		public void StartCounting()
		{
			count -= Time.deltaTime;
			ControlManager.instance.readyCountSecond = string.Format("{0:f0}", count);

			if (count <= 0 && counted)
			{
				ControlManager.instance.readyCountSecond = "모든 플레이어가 접속할 때 까지 잠시만 기다려주세요";
				UIManager.instance.ReadyCountDisplay.SetActive(false);
				counted = false;

				StartRunning();
			}
		}

		public void StartRunning()
		{
			GameObject cam = GameObject.Find("Starting Camera");
			GameObject startingWall = GameObject.Find("Starting Wall");
			
			cam.SetActive(false);
			startingWall.SetActive(false);
		}

		#endregion
    }
}