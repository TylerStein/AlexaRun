using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexaRun.ScriptableObjects;

namespace AlexaRun.Behaviours
{
    public class CameraFollowBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private Camera camera = null;
        [SerializeField] private CameraFollowSettings cameraFollowSettings = null;
        [SerializeField] private float targetSize = 10f;

        private Vector3 targetPosition = Vector3.zero;

        public void SetTarget(Transform target) {
            this.target = target;
        }

        public void SetOrthographicSize(float size) {
            targetSize = size;
        }

        // Start is called before the first frame update
        void Start() {
            if (camera == null) camera = Camera.main;
            if (target == null) target = transform;
        }

        // Update is called once per frame
        void Update() {
            targetPosition.Set(target.position.x, target.position.y, transform.position.z);
            float horizontalMove = (targetPosition.x - camera.transform.position.x) * Time.deltaTime * cameraFollowSettings.HorizontalSpeed;
            float verticalMode = (targetPosition.y - camera.transform.position.y) * Time.deltaTime * cameraFollowSettings.VerticalSpeed;
            float zoomMove = (targetSize - camera.orthographicSize) * Time.deltaTime * cameraFollowSettings.ZoomSpeed;

            camera.transform.Translate(new Vector3(horizontalMove, verticalMode, 0));
            camera.orthographicSize += zoomMove;
        }
    }
}