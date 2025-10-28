using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadMainMenu", 3);
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
    }
}
