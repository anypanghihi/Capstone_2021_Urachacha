using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.Urachacha
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields

        [Tooltip("The current Health of our player")]
        public float Gauge = 1f;
        
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion

        #region Private Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField] private GameObject playerUiPrefab;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float gravity;
        [SerializeField] private bool canJump;
        [SerializeField] private bool OnJumpPlatform;

        [SerializeField] private bool isGrounded;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundMask;
        
        private Vector3 moveDirection;
        private float directionY;

        private CharacterController controller;
        private Animator anim;
        private Camera cam;

        Vector3 remotePos;
        Quaternion remoteRot;
        Quaternion remoteCamRot;

        #endregion

        #region MonoBehaviour CallBacks

        public void Awake()
        {
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Start() 
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponentInChildren<Animator>();
            
            ActiveJump();
            
            if (this.playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(this.playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
        }

        public void Update()
        {
            if (photonView.IsMine)
            {
                //Camera.main.gameObject.SetActive(false);
                cam = GetComponentInChildren<Camera>();
                cam.tag = "MainCamera";
                gameObject.layer = LayerMask.NameToLayer("Player");
                gameObject.name = "Player";

                Move();
                UpdateUI();

                if (this.Gauge <= 0f)
                {
                    Gameover();
                }
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                gameObject.name = "Enemy";
                cam = GetComponentInChildren<Camera>();
                cam.enabled = false;

                transform.position = Vector3.Lerp(transform.position, remotePos, 10 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, remoteRot, 10 * Time.deltaTime);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, remoteCamRot, 10 * Time.deltaTime);
            }
        }

        public void UpdateUI()
        {
            if (LocalPlayerInstance != null && UIManager.instance != null)
            {
                UIManager.instance.UpdateMissionText(Urachacha.FIRST_ROUND_MISSION);
                UIManager.instance.UpdatePlayerCountText(ControlManager.instance.finishedPlayerCount, ControlManager.instance.maxPlayerCount);
                UIManager.instance.UpdateCountDown(ControlManager.instance.readyCountSecond);
            }
        }

        public void OnCollisionEnter(Collision other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (other.collider.tag.Equals("Obstacle"))
            {
                Gauge -= 0.01f;
                Debug.Log(Gauge);
            }
        }

        public void OnCollisionStay(Collision other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            // if (other.collider.tag.Equals("Obstacle"))
            // {
            //     Gauge -= 0.0005f;
            //     Debug.Log(Gauge);
            // }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) {
            if (!photonView.IsMine)
            {
                return;
            }

            OnJumpPlatform = false;

            if (hit.collider.tag.Equals("Obstacle"))
            {
                Gauge -= 0.001f;
                Debug.Log(Gauge);
            }
            else if(hit.collider.tag.Equals("JumpPlatform")){
                OnJumpPlatform = true;
            }
            
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
        }

        /// <param name="other">Other.</param>
        public void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
        }

        #if !UNITY_5_4_OR_NEWER
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
    
        #endif

        #endregion

        #region Private Methods
        
        void Move()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (moveDirection != Vector3.zero)
            {   
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            if (isGrounded)
            {
                anim.SetBool("isGrounded", true);

                if(Input.GetKeyDown(KeyCode.Space) && canJump && OnJumpPlatform)
                {
                    canJump = false;
                    jumpSpeed = 15;
                    Jump();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && canJump)
                {
                    canJump = false;
                    jumpSpeed = 4;
                    Jump();           
                }   
            } 

            directionY -= gravity * Time.deltaTime;

            moveDirection.y = directionY;
            controller.Move(moveDirection * Time.deltaTime);
        }

        void Idle()
        {
            moveDirection *= moveSpeed;
            anim.SetFloat("Speed", 0, -0.1f, Time.deltaTime);
        }

        void Run()
        {
            moveDirection *= moveSpeed;
            anim.SetFloat("Speed", 1, -0.1f, Time.deltaTime);
        }

        void Jump()
        {
            directionY = jumpSpeed;
            anim.SetTrigger("isJump");
            Invoke("ActiveJump", 1.3f);
        }

        void ActiveJump()
        {
            canJump = true;
        }

        void Gameover()
        {
            Debug.Log("GameOver");
        }

        #endregion

        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(this.Gauge);
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(cam.transform.rotation);
            }
            else
            {
                this.Gauge = (float)stream.ReceiveNext();
                remotePos = (Vector3)stream.ReceiveNext();
                remoteRot = (Quaternion)stream.ReceiveNext();
                remoteCamRot = (Quaternion)stream.ReceiveNext();
            }
        }

        #endregion
    }
}