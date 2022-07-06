using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Ghost : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Button button;
    [Header("Settings")]
    [SerializeField] private float speed;
    public RectTransform Rect { get; private set; }

    public bool IsActive => gameObject.activeSelf;

    [Inject] private ICounter _counter;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        button.onClick.AddListener(() =>
        {
            SetActive(false);
            _counter.IncreaseScore();
        });
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if (transform.position.y > Screen.height * 1.1f)
            SetActive(false);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
