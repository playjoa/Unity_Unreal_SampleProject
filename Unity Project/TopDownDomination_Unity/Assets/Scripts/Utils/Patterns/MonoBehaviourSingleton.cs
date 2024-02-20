using UnityEngine;

namespace Utils.Patterns
{
    public abstract class MonoBehaviourSingleton<TMonoBehaviour> : MonoBehaviour where TMonoBehaviour : MonoBehaviour
    {
        public static TMonoBehaviour ME { get; private set; }
        
        private void Awake()
        {
            if (ME == null)
            {
                ME = this as TMonoBehaviour;
                DontDestroyOnLoad(gameObject);
                OnAwaken();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnAwaken() { }
    }
}