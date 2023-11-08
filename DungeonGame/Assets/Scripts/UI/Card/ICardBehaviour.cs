using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public interface ICardBehaviour
    {
        void OnClick(Card card);
    }
}