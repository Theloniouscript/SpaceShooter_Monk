using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Singleton for MonoBehaviours
/// </summary>
/// <typeparam name="T"></typeparam>
[DisallowMultipleComponent]

public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Automatically mark object as persistent
    /// </summary>
    [Header("Singleton")]

    [SerializeField] private bool m_DontDestroyOnLoad;
    
    /// <summary>
    /// Singleton Instance. May be null if DoNotDestroyOnLoad flag was not set
    /// </summary>
    public static T Instance { get; private set; }

    #region Unity events
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton : object of type already exists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Instance = this as T;
        if(m_DontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
            
    }

#endregion
}
