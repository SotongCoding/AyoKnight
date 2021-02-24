using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader _instance;
    public Image loadingImages;
    [SerializeField] SceneData loadedMainScene;
    [SerializeField] SceneData loadedSubScene;
    public List<SceneData> scenes = new List<SceneData> ();

    private void Awake () {
        DontDestroyOnLoad (this.transform.parent.gameObject);
        loadedMainScene.sceneID = 0;
        loadedSubScene.sceneID = -1;

        if (_instance == null) {
            _instance = this;
        }
        else if (this != _instance) {
            Destroy (gameObject);
        }

    }
    public static void UnloadScene (int sceneIndex) {
        SceneData target = _instance.GetScene (sceneIndex);

        _instance.StartCoroutine (_instance.UnloadProcess (sceneIndex));
        if (target.category == SceneType.mainScene) {
            _instance.loadedMainScene = new SceneData (-1, SceneType.mainScene);
        }
        else {
            _instance.StartCoroutine (_instance.UnloadProcess (sceneIndex));
            _instance.loadedSubScene = new SceneData (-1, SceneType.subScene);
        }
    }
    public static void LoadScene (int sceneIndex) {
        SceneData target = _instance.GetScene (sceneIndex);

        if (target.category == SceneType.mainScene) {
            if (_instance.loadedMainScene != target) {

                _instance.StartCoroutine (_instance.LoadProsess (sceneIndex));
                if (_instance.loadedMainScene.sceneID != 0) {
                    _instance.StartCoroutine (_instance.UnloadProcess (_instance.loadedMainScene.sceneID));
                }
                _instance.loadedMainScene = target;
            }

        }
        else if (target.category == SceneType.subScene) {
            if (_instance.loadedSubScene.sceneID != -1) {
                if (_instance.loadedSubScene != target) {
                    _instance.StartCoroutine (_instance.LoadProsess (sceneIndex));
                    _instance.loadedSubScene = target;
                }
                else {
                    _instance.StartCoroutine (_instance.UnloadProcess (sceneIndex));
                    _instance.loadedSubScene = new SceneData (-1, SceneType.subScene);
                }
            }
            else {
                _instance.StartCoroutine (_instance.LoadProsess (sceneIndex));
                _instance.loadedSubScene = target;
            }
        }
    }
    public static void LoadScene (string sceneName) {
        SceneData target = _instance.GetScene (sceneName);
        int sceneIndex = (SceneManager.GetSceneByName (sceneName).buildIndex);

        if (target.category == SceneType.mainScene) {
            if (_instance.loadedMainScene != target) {

                _instance.StartCoroutine (_instance.LoadProsess (sceneIndex));
                if (_instance.loadedMainScene.sceneID != 0) {
                    _instance.StartCoroutine (_instance.UnloadProcess (_instance.loadedMainScene.sceneID));
                }
                _instance.loadedMainScene = target;
            }

        }
        else if (target.category == SceneType.subScene) {
            if (_instance.loadedSubScene.sceneID != -1) {
                if (_instance.loadedSubScene != target) {
                    _instance.StartCoroutine (_instance.LoadProsess (sceneIndex));
                    _instance.loadedSubScene = target;
                }
                else {
                    _instance.StartCoroutine (_instance.UnloadProcess (sceneIndex));
                    _instance.loadedSubScene = new SceneData (-1, SceneType.subScene);
                }
            }
            else {
                _instance.StartCoroutine (_instance.LoadProsess (sceneIndex));
                _instance.loadedSubScene = target;
            }
        }

    }
    SceneData GetScene (int sceneIndex) {
        foreach (var item in scenes) {
            if (item.sceneID == sceneIndex) {
                return item;
            }
        }
        return default;
    }
    SceneData GetScene (string sceneName) {
        foreach (var item in scenes) {
            if (item.sceneName == sceneName) {
                return item;
            }
        }
        return default;
    }
    IEnumerator LoadProsess (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex, LoadSceneMode.Additive);
        while (!operation.isDone) {
            yield return null;
        }
    }
    IEnumerator UnloadProcess (int sceneIndex) {
        SceneManager.UnloadSceneAsync (sceneIndex);
        yield return null;
    }

    [Serializable]
    public struct SceneData {
        public int sceneID;
        public string sceneName;
        public SceneType category;

        public SceneData (int sceneID, string sceneName, SceneType category) {
            this.sceneID = sceneID;
            this.sceneName = sceneName;
            this.category = category;
        }
        public SceneData (int sceneID, SceneType scene) {
            this.sceneID = sceneID;
            this.sceneName = "";
            this.category = scene;

        }

        public static bool operator == (SceneData c1, SceneData c2) {
            return c1.Equals (c2);
        }

        public static bool operator != (SceneData c1, SceneData c2) {
            return !c1.Equals (c2);
        }

        public override string ToString () {
            return sceneID + " : " + sceneName + "\n" + category;
        }

        public override bool Equals (object obj) {
            return obj is SceneData data &&
                sceneID == data.sceneID &&
                sceneName == data.sceneName &&
                category == data.category;
        }

        public override int GetHashCode () {
            int hashCode = 383665895;
            hashCode = hashCode * -1521134295 + sceneID.GetHashCode ();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (sceneName);
            hashCode = hashCode * -1521134295 + category.GetHashCode ();
            return hashCode;
        }
    }

    public enum SceneType {
        mainScene,
        subScene
    }
}