using UnityEngine;
using AlexaRun.Enums;
using AlexaRun.Interfaces;

public class StateAnimationController : MonoBehaviour
{
    [SerializeField] public Animator animator = null;
    [SerializeField] public FailablePointBehaviour linkedPointBehaviour = null;
    [SerializeField] public int initialState = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("state", initialState);
    }

    public void UpdateState() {
        switch (linkedPointBehaviour.GetBehaviourState()) {
            case EBehaviourState.OK:
                animator.SetInteger("state", 0);
                break;
            case EBehaviourState.FAILING:
                animator.SetInteger("state", 1);
                break;
            case EBehaviourState.FAILED:
                animator.SetInteger("state", 2);
                break;
        }
    }
}
