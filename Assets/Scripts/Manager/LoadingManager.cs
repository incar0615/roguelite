using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace P1
{
    public class LoadingManager : MonoBehaviour
    {
        static string nextSceneName;
        static LoadSceneMode loadSceneMode;

        [SerializeField]
        Image progressBar;

        // Start is called before the first frame update
        void Start()
        {
            if(nextSceneName != null & nextSceneName != "")
            {
                StartCoroutine(LoadScene());
            }
        }

        public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            nextSceneName = sceneName;
            loadSceneMode = mode;
            SceneManager.LoadScene("LoadingScene");
        }

        public static void UnloadLoadingScene()
        {
            SceneManager.UnloadSceneAsync("LoadingScene");
        }

        public void LoadSceneAsync(string sceneName, LoadSceneMode mode)
        {
            SceneManager.LoadSceneAsync(sceneName, mode);
        }

        IEnumerator LoadScene()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName, loadSceneMode);

            op.allowSceneActivation = false;

            float timer = 0.0f;

            while (!op.isDone)
            {
                yield return null;

                timer += Time.deltaTime;
                if (op.progress < 0.9f)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                    if (progressBar.fillAmount >= op.progress)
                    {
                        timer = 0f;
                    }
                }
                else
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                    if (progressBar.fillAmount == 1.0f)
                    {
                        op.allowSceneActivation = true;
                        nextSceneName = "";
                        loadSceneMode = LoadSceneMode.Single;
                        yield break;
                    }
                }
            }
        }
    }

}
