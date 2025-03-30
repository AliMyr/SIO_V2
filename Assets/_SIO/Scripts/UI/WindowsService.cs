using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowsService : MonoBehaviour
{
    [SerializeField] private Window[] windows;
    private Dictionary<Type, Window> windowsDictionary;

    public void Initialize()
    {
        windowsDictionary = new Dictionary<Type, Window>();
        foreach (var window in windows)
        {
            windowsDictionary[window.GetType()] = window;
            window.Hide(true);
            window.Initialize();
        }

        ShowWindow<MainMenuWindow>(true);
    }

    public T GetWindow<T>() where T : Window => windowsDictionary[typeof(T)] as T;

    public void ShowWindow<T>(bool isImmediately) where T : Window => GetWindow<T>()?.Show(isImmediately);
    public void HideWindow<T>(bool isImmediately) where T : Window => GetWindow<T>()?.Hide(isImmediately);

    public void ShowWindow(Type windowType, bool isImmediately)
    {
        if (windowsDictionary.TryGetValue(windowType, out var window))
            window.Show(isImmediately);
        else
            Debug.LogError($"Window of type {windowType} not found.");
    }
}
