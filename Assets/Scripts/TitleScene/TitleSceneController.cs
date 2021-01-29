using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace P1
{
    public class TitleSceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKey)
            {
                LoadingManager.LoadScene("GameScene", LoadSceneMode.Additive);
            }
        }
    }

}
