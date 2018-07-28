using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PromptArrow : MonoBehaviour
{
    [Tooltip("哪個物件")]
    [SerializeField] CardManager.CardName cardName;
    [Tooltip("物件數量容器")]
    [SerializeField] Text cardText;
    [Tooltip("對應對象")]
    [SerializeField] Transform target;
    [Tooltip("與對象的高度")]
    [SerializeField] float height = .32f;
    Tweener tweener;

    private void Start()
    {
        SetLoopAni();
    }

    void SetLoopAni()
    {
        transform.position = target.localPosition + new Vector3(0, height, 0);
        tweener = transform.DOBlendableLocalMoveBy(new Vector3(0, 0.3f, 0), .7f).SetEase(Ease.OutSine);
        tweener.SetAutoKill(false);
        tweener.SetLoops(-1, LoopType.Yoyo);
    }

    private void LateUpdate()
    {
        cardText.text = CardManager.instance.GetCorrectData(cardName).cardAmount.ToString();
    }
}
