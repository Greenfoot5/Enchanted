using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetData(float value, Vector2 screenPoint)
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = value.ToString();
        transform.position = screenPoint;
    }

    public void SetData(float value, Vector3 worldPoint)
    {
        //Vector2 screenLoc = Camera.main.WorldToScreenPoint(worldPoint);
        //Debug.Log(worldPoint);
        //Debug.Log(screenLoc);
        //SetData(value, screenLoc);
        Debug.Log(Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0)));
    }

    private void Update()
    {
        Debug.Log(Camera.main.WorldToScreenPoint(new Vector3(0,0,0)));
    }
}
