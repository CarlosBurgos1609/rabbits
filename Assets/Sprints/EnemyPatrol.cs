using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;

    private GameObject _target;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        UpdateTarget();
        StartCoroutine(PatrolToTarget());
    }

    private void UpdateTarget()
    {
        if (_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }

        if (_target.transform.position.x == minX)
        {
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator PatrolToTarget()
    {
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
        {
            _animator.SetBool("Idle", false);
            Vector2 direction = _target.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector2(_target.transform.position.x, transform.position.y);
        UpdateTarget();

        _animator.SetBool("Idle", true);
        yield return new WaitForSeconds(waitingTime);
        StartCoroutine(PatrolToTarget());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false); // Desactiva al jugador

            if (GameOverManager.Instance != null)
            {
                GameOverManager.Instance.ShowGameOver(); // Muestra Game Over
            }
            else
            {
                Debug.LogError("GameOverManager no está inicializado.");
            }
        }
    }

}
