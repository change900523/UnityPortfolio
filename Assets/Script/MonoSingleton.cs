using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T obj = FindObjectOfType<T>();
               
                if (obj == null)
                {
                    string goName = typeof(T).ToString();
                    GameObject go = new GameObject(goName);
                    instance = go.AddComponent<T>();
                }
                else
                {
                    instance = obj;
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}