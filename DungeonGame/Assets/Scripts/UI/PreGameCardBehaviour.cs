using UnityEngine.UI;
using DG.Tweening;

namespace Card
{
    public class PreGameCardBehaviour : ICardBehaviour
    {
        public void Click(Card ctx)
        {
            ctx.transform.DOMoveY(1, 1);
        }
    }
}