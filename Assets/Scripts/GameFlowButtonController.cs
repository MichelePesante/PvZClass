using UnityEngine;
using UnityEngine.UI;
using System;

public class GameFlowButtonController : MonoBehaviour
{
    Animator animator;

    public Button GoNextButton;

    #region API

    public void Setup(Action _goToNext)
    {
        animator = GetComponent<Animator>();
        GoNextButton.onClick.AddListener(_goToNext.Invoke);
    }

    public void GoToNextPhase()
    {
        animator.SetTrigger("GoToNext");
    }

    public void ToggleGoNextButton(bool _value)
    {
        GoNextButton.enabled = _value;
    }

    #endregion

}