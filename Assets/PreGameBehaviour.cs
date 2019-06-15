using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexaRun.Behaviours;

namespace AlexaRun.Level
{
    public class PreGameBehaviour : MonoBehaviour
    {
        [SerializeField] private PauseBehaviour pauseBehaviour = null;
        [SerializeField] private GameObject preGameMenuRoot = null;
        [SerializeField] private InputController inputController = null;

        [SerializeField] [ReadOnly] private bool isActive = true;

        // Start is called before the first frame update
        void Start() {
            isActive = true;
            preGameMenuRoot.SetActive(true);
            pauseBehaviour.Pause(false);
        }

        // Update is called once per frame
        void Update() {
            if (isActive) {
                if (inputController.AltInteract || inputController.Interact || inputController.Pause) {
                    preGameMenuRoot.SetActive(false);
                    pauseBehaviour.UnPause();
                    isActive = false;
                }
            }
        }
    }
}