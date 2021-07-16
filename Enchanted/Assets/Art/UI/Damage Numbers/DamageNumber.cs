using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    private Vector3? _newLoc;

    void Start()
    {
        Animator animator = GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetData(float value, Vector3 worldPoint)
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = value.ToString();
        _newLoc = worldPoint;
    }

    private void LateUpdate()
    {
        if (_newLoc != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(_newLoc.GetValueOrDefault());
            transform.position = screenPos;
            _newLoc = null;
        }
    }
}
