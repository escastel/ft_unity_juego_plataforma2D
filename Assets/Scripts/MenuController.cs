using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
