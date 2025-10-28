using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;

    [Header("Movimiento")]
    public float velocidadMovimiento = 5f;
    private float movimientoX;

    [Header("Salto")]
    public float fuerzaSalto = 4;
    private bool estaEnSuelo;
    public Transform compruebaSuelo;
    public float radioEsferaTocaSuelo = 0.1f;
    public LayerMask layerSuelo;

    [Header("Sonido")]
    public AudioSource audioSource;
    public AudioClip getColeccionable;
    public AudioClip sonidoMuerto;
    public AudioClip sonidoCaida;

    [Header("Muerte por Caída")]
    public float alturaMinima = -4f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        estaEnSuelo = Physics2D.OverlapCircle(compruebaSuelo.position, radioEsferaTocaSuelo, layerSuelo);
        rb2d.linearVelocity = new Vector2(movimientoX * velocidadMovimiento, rb2d.linearVelocity.y);

        if (transform.position.y < alturaMinima)
        {
            animator.SetBool("estaHerido", true);
            audioSource.PlayOneShot(sonidoCaida);
            StartCoroutine(ReiniciarNivel());
        }
            if (movimientoX == 0)
            animator.SetBool("estaCorriendo", false);
        if (movimientoX != 0 && estaEnSuelo)
            animator.SetBool("estaCorriendo", true);
        if (estaEnSuelo)
            animator.SetBool("estaSaltando", false);
        else
            animator.SetBool("estaSaltando", true);
    }
    public void OnMove(InputValue inputPlayer)
    {
        movimientoX = inputPlayer.Get<Vector2>().x;
        if (movimientoX != 0 && !animator.GetBool("estaHerido"))
        {
            transform.localScale = new Vector3(Mathf.Sign(movimientoX) * 3.859826f, 4.240974f, 3.5032f);
        }
    }
    public void OnJump(InputValue inputJump)
    {
        if (estaEnSuelo && !animator.GetBool("estaHerido"))
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, fuerzaSalto);
            animator.SetBool("estaSaltando", true);
        }
    }
    IEnumerator ReiniciarNivel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coleccionable"))
        {
            FindObjectOfType<ColeccionableController>().SumaPuntos();
            audioSource.PlayOneShot(getColeccionable);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            animator.SetBool("estaHerido", true);
            audioSource.PlayOneShot(sonidoMuerto);
            StartCoroutine(ReiniciarNivel());
        }
    }
}
