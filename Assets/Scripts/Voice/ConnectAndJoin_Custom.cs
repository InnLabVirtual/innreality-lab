// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectAndJoin.cs" company="Exit Games GmbH">
//   Part of: Photon Voice Utilities for Unity - Copyright (C) 2018 Exit Games GmbH
// </copyright>
// <summary>
// Simple component to call voiceConnection.ConnectUsingSettings() and get into a Voice room easily.
// </summary>
// <remarks>
// Requires a VoiceConnection component attached to the same GameObject.
// </remarks>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;
using Photon.Voice.Unity;


[RequireComponent(typeof(VoiceConnection))]
    public class ConnectAndJoin_Custom : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks
    {
        private VoiceConnection voiceConnection;

        [SerializeField]
        private bool autoConnect = true;

        [SerializeField]
        private bool autoTransmit = true;

        public string m_RoomName { get; set; }

        private RoomOptions roomOptions = new RoomOptions();
        private TypedLobby typedLobby = TypedLobby.Default;

        public bool IsConnected { get { return voiceConnection.Client.IsConnected; } }

        private void Awake()
        {
            voiceConnection = GetComponent<VoiceConnection>();
        }

         void Start()
         {
         }

        private void OnEnable()
        {
            StartCall();
        }

        private void OnDisable()
        {
            FinishCall();
        }

        public void StartCall()
        {
            voiceConnection.Client.AddCallbackTarget(this);
            if (this.autoConnect)
            {
                this.ConnectNow();
            }
        }

        public void FinishCall()
        {
            voiceConnection.Client.RemoveCallbackTarget(this);
        }

        public void ConnectNow()
        {
            Debug.Log("ConnectAndJoin.ConnectNow() will now call: VoiceConnection.ConnectUsingSettings().");
            voiceConnection.ConnectUsingSettings();
        }

        #region MatchmakingCallbacks

        public void OnCreatedRoom()
        {

        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {

        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {

        }

        public void OnJoinedRoom()
        {
            if (voiceConnection.PrimaryRecorder == null)
            {
                voiceConnection.PrimaryRecorder = this.gameObject.AddComponent<Recorder>();
            }
            if (this.autoTransmit)
            {
                voiceConnection.PrimaryRecorder.TransmitEnabled = autoTransmit;
            }
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            if (returnCode == ErrorCode.NoRandomMatchFound)
            {
                voiceConnection.Client.OpCreateRoom(new EnterRoomParams
                {
                    RoomName = m_RoomName,
                    RoomOptions = roomOptions,
                    Lobby = typedLobby
                });
            }
            else
            {
                Debug.LogErrorFormat("OnJoinRandomFailed errorCode={0} errorMessage={1}", returnCode, message);
            }
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogErrorFormat("OnJoinRoomFailed roomName={0} errorCode={1} errorMessage={2}", m_RoomName, returnCode, message);
        }

        public void OnLeftRoom()
        {

        }

        #endregion

        #region ConnectionCallbacks

        public void OnConnected()
        {

        }

        public void OnConnectedToMaster()
        {
                voiceConnection.Client.OpJoinOrCreateRoom(new EnterRoomParams { RoomName = m_RoomName, RoomOptions = roomOptions, Lobby = typedLobby });
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            if (cause == DisconnectCause.None || cause == DisconnectCause.DisconnectByClientLogic)
            {
                return;
            }
            Debug.LogErrorFormat("OnDisconnected cause={0}", cause);
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {

        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {

        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {

        }

        #endregion
    }