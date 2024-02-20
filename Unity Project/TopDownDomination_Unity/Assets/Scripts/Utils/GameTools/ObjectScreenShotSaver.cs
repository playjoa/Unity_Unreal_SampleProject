using System.Collections;
using UnityEngine;

namespace Utils.GameTools
{
    public class ObjectScreenShotSaver : MonoBehaviour
    {
        [Header("File Settings")] 
        [SerializeField] private string directoryLocation = "C:/Users/MyUser/Desktop/";
        [SerializeField] private string folderName = "SkinPhotos";
        [SerializeField] private bool useGameObjectName;
        [SerializeField] private string prefixFileName = "img_";

        [Header("Screenshot Settings")] 
        [SerializeField] private float shutterTime = 0.1f;
        [SerializeField] private bool screenShotAtStart = false;
        [SerializeField] private KeyCode screenShotKey = KeyCode.S;
        
        [Header("Objects to Screenshot")] 
        [SerializeField] private GameObject[] objectsToScreenShot;

        private Coroutine _screenShotCoroutine;
        private GameObject _currentObjectToScreenShot;
        private const string TypeOfFile = ".png";

        private WaitForSeconds _waitForShutter;
        private string FolderLocation => directoryLocation + folderName;

        private void OnValidate() => objectsToScreenShot ??= GetChildObjects();

        private void Awake()
        {
            _waitForShutter = new WaitForSeconds(shutterTime);
            DeactivateObjects();
        }

        private void Start()
        {
            if (screenShotAtStart)
                StartCoroutine(PrintAndSaveObjects());
        }

        private GameObject[] GetChildObjects()
        {
            var childObjects = new GameObject[transform.childCount];

            for (var i = 0; i < childObjects.Length; i++)
                childObjects[i] = transform.GetChild(i).gameObject;

            return childObjects;
        }

        private void DeactivateObjects()
        {
            foreach (var g in objectsToScreenShot)
                g.SetActive(false);
        }

        private void Update()
        {
            if (_screenShotCoroutine != null) return;

            if (Input.GetKeyDown(screenShotKey))
            {
                _screenShotCoroutine = StartCoroutine(PrintAndSaveObjects());
            }
        }

        private IEnumerator PrintAndSaveObjects()
        {
            Debug.Log($"Starting Screenshots {gameObject.name}");
            yield return _waitForShutter;

            for (var i = 0; i < objectsToScreenShot.Length; i++)
            {
                _currentObjectToScreenShot = objectsToScreenShot[i];
                _currentObjectToScreenShot.SetActive(true);
                yield return _waitForShutter;
                
                yield return new WaitForEndOfFrame();
                Debug.Log($"Screen shooting {_currentObjectToScreenShot.name}");
                var textureFromGameView = new Texture2D(Screen.width, Screen.height);
                textureFromGameView.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                textureFromGameView.Apply();
                var textureGameViewBytes = textureFromGameView.EncodeToPNG();
                
                Destroy(textureFromGameView);
                SaveScreenShot(textureGameViewBytes, i);
                
                _currentObjectToScreenShot.SetActive(false);
            }

            Debug.Log($"Done Screenshots {gameObject.name}");
            _screenShotCoroutine = null;
        }

        private void SaveScreenShot(byte[] screenShotBytes, int screenShotIndex)
        {
            CreateDirectory();

            var path = System.IO.Path.Combine(FolderLocation, FileName(screenShotIndex));
            System.IO.File.WriteAllBytes(path, screenShotBytes);
        }
        
        private string FileName(int numberFile)
        {
            if (useGameObjectName)
                return _currentObjectToScreenShot.name + TypeOfFile;
            
            return prefixFileName + numberFile + TypeOfFile;
        }

        private void CreateDirectory()
        {
            if (System.IO.Directory.Exists(FolderLocation))
            {
                Debug.Log($"Directory Already exists: {FolderLocation}");
                return;
            }
            
            Debug.Log($"Creating Directory: {FolderLocation}");
            System.IO.Directory.CreateDirectory(FolderLocation);
        }
    }
}