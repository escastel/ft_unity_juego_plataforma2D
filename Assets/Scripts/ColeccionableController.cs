using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColeccionableController : MonoBehaviour
{
    public TMP_Text textoNumeroPuntos;
    private int puntos;
    private int totalColeccionables;
    void Start()
    {
        puntos = 0;
        textoNumeroPuntos.text = puntos.ToString();
        totalColeccionables = transform.childCount;
    }

    private void Update()
    {
        if (puntos >= totalColeccionables)
        {
            SceneManager.LoadScene("Victoria");
        }
    }

    public void SumaPuntos()
    {
        puntos++;
        textoNumeroPuntos.text = puntos.ToString();
    }
}
