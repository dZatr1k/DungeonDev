using UnityEngine;
using DG.Tweening;

namespace Card
{
    public class MainGameCardBehaviour : ICardBehaviour
    {
        public void Click(Card ctx)
        {
            ctx.transform.DOMoveX(2, 1);
        }
    }
}