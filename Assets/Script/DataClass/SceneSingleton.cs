using UnityEngine;

/// <summary>
/// 한 씬 내에서만 singleton을 유지할 객체에게 상속
/// </summary>
/// <typeparam name="T">singleton상태를 유지할 객체</typeparam>
public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {

                instance = (T)FindObjectOfType(typeof(T));
                
                if (instance == null) {
                    GameObject obj = new(typeof(T).Name, typeof(T));
                    instance = obj.AddComponent<T>();
                }
            }
            
            return instance;
        }
    }

    void Awake() {
        if (instance != null) {
            
            if(instance != this) {
                Destroy(gameObject);
            }
            return;
        }
        instance = GetComponent<T>();
    }

}
