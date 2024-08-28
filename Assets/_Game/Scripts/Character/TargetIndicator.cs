using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : GameUnit
{ 
    public Transform Target;
    public TextMeshProUGUI textName;


    [SerializeField] private Image Icon;

    [SerializeField] private TextMeshProUGUI textLevel;
     
    [SerializeField] private Image arrow; 
    private RectTransform rect; 
    [SerializeField] private GameObject follow;

    private float Offset = 2f;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        float minX = rect.rect.width / 2 + Offset;
        float maxX = Screen.width - minX;

        float minY = rect.rect.height / 2 + Offset;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(Target.transform.position);
        Vector2 direction = (Vector2)pos - new Vector2(Screen.width / 2, Screen.height / 2);
        float angle = Vector2.Angle(direction, Vector3.right);
        if (direction.y <= 0) angle = 360 - angle;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        if (pos.x == maxX || pos.y == maxY || pos.x == minX || pos.y == minY)
        {
            textName.gameObject.SetActive(false);
            arrow.gameObject.SetActive(true);
            follow.transform.eulerAngles = Vector3.forward * angle;
        }
        else
        {
            textName.gameObject.SetActive(true);
            arrow.gameObject.SetActive(false);
            follow.transform.eulerAngles = Vector3.zero;
        }

        this.TF.position = pos;
    }

    public void SetLevel(int level)
    {
        textLevel.text = level.ToString();
    }
    public override void OnInit()
    {
        Color color = new Color(Random.value, Random.value, Random.value,1);
        Icon.color = color;
        textName.color = color;

    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
