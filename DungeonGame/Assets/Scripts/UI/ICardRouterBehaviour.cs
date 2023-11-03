using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public interface ICardRouterBehaviour
    {
        void OnClick(CardRouter ctx, Card card);
    }
}