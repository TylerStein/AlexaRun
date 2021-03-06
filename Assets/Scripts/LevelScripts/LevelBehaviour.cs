﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AlexaRun.Behaviours;
using AlexaRun.Behaviours.Player;
using AlexaRun.Enums;
using AlexaRun.Interfaces;
using AlexaRun.Structures;

namespace AlexaRun.Level
{
    public class LevelBehaviour : MonoBehaviour, IBehaviourStateObservable
    {
        [SerializeField] private List<RoomBehaviour> roomBehaviours = new List<RoomBehaviour>();
        [SerializeField] private PlayerBehaviour playerBehaviour = null;
        [SerializeField] private ScoreController scoreBehaviour = null;
        [SerializeField] private UnityEvent onStateChange = new UnityEvent();
        [SerializeField] [ReadOnly] private EBehaviourState levelState = EBehaviourState.OK;
        [SerializeField] private SoundEffectBehaviour inGameAudioBehaviour = null;

        [SerializeField] private string levelName = "level";

        [SerializeField] public PauseBehaviour pauseBehaviour = null;
        [SerializeField] public GameOverBehaviour gameOverBehaviour = null;
        [SerializeField] public LevelBoundaries boundaries;
        public LevelBoundaries Boundaries { get { return boundaries; } }

        [SerializeField] private GameObject objectHighlight = null;
        public GameObject ObjectHighlight { get { return objectHighlight; } }

        private static LevelBehaviour levelBehaviourInstance = null;
        public static LevelBehaviour Instance() {
            return levelBehaviourInstance;
        }

        private void Awake() {
            LevelBehaviour[] behaviours = FindObjectsOfType<LevelBehaviour>();
            if (behaviours.Length > 1) throw new UnityException("Cannot have more than one LevelBehaviour per scene!");
            levelBehaviourInstance = this;

            if (playerBehaviour == null) {
                playerBehaviour = FindObjectOfType<PlayerBehaviour>();

            }

            for (int i = 0; i < roomBehaviours.Count; i++) {
                roomBehaviours[i].SubscribeToStateChange(OnRoomStateChange);
            }
            scoreBehaviour.SetRoomScoreModifier(roomBehaviours.Count);

            inGameAudioBehaviour.PlaySound();
        }

        private void OnRoomStateChange() {
            int failCount = 0;
            for (int i = 0; i < roomBehaviours.Count; i++) {
                if (roomBehaviours[i].GetBehaviourState() == EBehaviourState.FAILED) failCount++;
            }
            scoreBehaviour.SetRoomScoreModifier(roomBehaviours.Count - failCount);

            if (failCount == roomBehaviours.Count) {
                // All rooms failed
                levelState = EBehaviourState.FAILED;
                inGameAudioBehaviour.StopSound();
                gameOverBehaviour.SetGameOver(playerBehaviour);
                scoreBehaviour.StoreScore(levelName);
                playerBehaviour.SetGameOver();
                onStateChange.Invoke();
            }

        }

        public void AddScore(int amount) {
            scoreBehaviour.IncrementScore(amount);
        }

        public void SubscribeToStateChange(UnityAction listener) {
            onStateChange.AddListener(listener);
        }

        public void UnsubscribeFromStateChange(UnityAction listener) {
            onStateChange.RemoveListener(listener);
        }

        public EBehaviourState GetBehaviourState() {
            return levelState;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;

            Vector3 top = new Vector3(Boundaries.minX, 20, 0);
            Vector3 bottom = new Vector3(Boundaries.minX, -20, 0);

            Gizmos.DrawLine(top, bottom);

            top.Set(Boundaries.maxX, 20, 0);
            bottom.Set(Boundaries.maxX, -20, 0);

            Gizmos.DrawLine(top, bottom);

        }
    }
}
