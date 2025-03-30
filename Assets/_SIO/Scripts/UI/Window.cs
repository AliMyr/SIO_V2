using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private string windowName;
    [SerializeField] private Animator windowAnimator;
    [SerializeField] protected string openAnimationName;
    [SerializeField] protected string idleAnimationName;
    [SerializeField] protected string closeAnimationName;
    [SerializeField] protected string hiddenAnimationName;

    public bool IsOpened { get; protected set; }

    private Animator WindowAnimator => windowAnimator ??= GetComponent<Animator>();

    public virtual void Initialize() { }

    public void Show(bool isImmediately)
    {
        OpenStart();
        WindowAnimator.Play(isImmediately ? idleAnimationName : openAnimationName);

        if (isImmediately)
            OpenEnd();
    }

    public void Hide(bool isImmediately)
    {
        CloseStart();
        if (WindowAnimator != null && gameObject.activeInHierarchy)
            WindowAnimator.Play(isImmediately ? hiddenAnimationName : closeAnimationName);

        if (isImmediately)
            CloseEnd();
    }

    protected virtual void OpenStart()
    {
        gameObject.SetActive(true);
        IsOpened = true;
    }

    protected virtual void OpenEnd() { }
    protected virtual void CloseStart() => IsOpened = false;
    protected virtual void CloseEnd() => gameObject.SetActive(false);
}
