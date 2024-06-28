using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Animals;


    public void PlayButton()
    {
        SceneManager.LoadScene("SelectChar");
    }
    public void ExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void LeftButton()
    {

    }

    void RightButton()
    {

    }

    void ConfirmButton()
    {

    }
}
