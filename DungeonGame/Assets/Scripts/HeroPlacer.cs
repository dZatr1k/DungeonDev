using UnityEngine;

namespace Field
{
    public class HeroPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _heroList;
        [SerializeField] private GameObject _typeOfHostedHero;

        private void OnEnable()
        {
            Cell.OnCellClick += PlaceHero;
        }

        private void OnDisable()
        {
            Cell.OnCellClick -= PlaceHero;
        }

        private void PlaceHero(Cell cell)
        {
            if (cell.Hero == null && _typeOfHostedHero != null)
            {
                GameObject hero = Instantiate(_typeOfHostedHero, cell.transform.position, Quaternion.identity, _heroList.transform);
                cell.SetHero(hero);

            }
        }

    }

}
