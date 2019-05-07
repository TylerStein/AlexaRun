using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexaRun.ScriptableObjects;

namespace AlexaRun.Behaviours
{
    public class CameraFollowBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private Transform cameraTransform = null;
        [SerializeField] private CameraFollowSettings cameraFollowSettings = null;

        private Vector3 targetPosition = Vector3.zero;

        // Start is called before the first frame update
        void Start() {
            if (cameraTransform == null) cameraTransform = Camera.main.transform;
            if (target == null) target = transform;
        }

        // Update is called once per frame
        void Update() {
            targetPosition.Set(target.position.x, target.position.y, transform.position.z);
            float horizontalMove = (targetPosition.x - cameraTransform.position.x) * Time.deltaTime * cameraFollowSettings.HorizontalSpeed;
            float verticalMode = (targetPosition.y - cameraTransform.position.y) * Time.deltaTime * cameraFollowSettings.VerticalSpeed;

            cameraTransform.Translate(new Vector3(horizontalMove, verticalMode, 0));
        }
    }
}