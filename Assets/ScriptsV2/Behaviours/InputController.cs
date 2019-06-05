using UnityEngine;
using UnityEngine.Events;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// Normalizes inputs for passing to the Player Behaviour, to be used by a PlayerBehaviour script
    /// </summary>
    public class InputController : MonoBehaviour {
        [SerializeField] private string interactInputName = "Fire1";
        [SerializeField] private string horizontalAxisName = "Horizontal";
        [SerializeField] private string moveUpInputName = "Up";
        [SerializeField] private string moveDownInputName = "Down";
        [SerializeField] private string pauseInputName = "Cancel";
        [SerializeField] private string altInteractInputName = "Jump";

        public bool Interact { get; private set; }
        public float HorizontalMove { get; private set; }
        public bool MoveUp { get; private set; }
        public bool MoveDown { get; private set; }
        public bool Pause { get; private set; }
        public bool AltInteract { get; private set; }

        private void Update() {
            Interact = Input.GetButtonDown(interactInputName);
            HorizontalMove = Input.GetAxis(horizontalAxisName);
            MoveUp = Input.GetButtonDown(moveUpInputName);
            MoveDown = Input.GetButtonDown(moveDownInputName);
            Pause = Input.GetButtonDown(pauseInputName);
            AltInteract = Input.GetButtonDown(altInteractInputName);
        }
    }
}
