using UnityEngine;
using DG.Tweening;
using LevelLogic;
using System.Collections;

namespace GameBoard
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Line[] _lines;
        [SerializeField] private Color _fogColor;
        [SerializeField] private Color _targetFogColor;
        [SerializeField] private float _fogTime;
        [SerializeField] private float _defogTime;
        [SerializeField] private HeroPlacer _heroPlacer;

        private bool _isLocked = true;

        public Transform EndPoint => _endPoint;

        private void OnValidate()
        {
            _heroPlacer ??= FindObjectOfType<HeroPlacer>();
            _lines ??= GetComponentsInChildren<Line>();

            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i].LineNumber = i;
                _lines[i].Validate();
            }
        }

        private void OnEnable()
        {
            Level.Instance.CurrentStateMachine.OnGameStarted += Relock;
            HeroPlacer.OnHeroPlaced += Defog;
        }

        private void OnDisable()
        {
            Level.Instance.CurrentStateMachine.OnGameStarted -= Relock;
            HeroPlacer.OnHeroPlaced -= Defog;
        }

        private Cell[] GetRow(Cell target)
        {
            Cell[] result = new Cell[_lines.Length];

            int index = target.Index;

            for (int i = 0; i < _lines.Length; i++)
            {
                result[i] = _lines[i].GetCell(index);
            }

            return result;
        }

        private void AnimateCell(Cell toAnimate, Color targetColor, float time)
        {
            toAnimate.Renderer.DOKill();
            toAnimate.Renderer.DOColor(targetColor, time);
        }

        private void AnimateCells(Cell[] array, Color endColor, float time)
        {
            for (int i = 0; i < array.Length; i++)
            {
                AnimateCell(array[i], endColor, time);
            }
        }

        private void AnimateColor(Cell target, float animationTime, Color endColor, Color targetEndColor)
        {
            if (_isLocked || _heroPlacer.IsSelected() == false)
                return;

            var row = GetRow(target);
            var line = _lines[target.LineNumber].Cells;

            AnimateCells(row, endColor, animationTime);
            AnimateCells(line, endColor, animationTime);
            AnimateCell(target, targetEndColor, animationTime);
        }

        public void Relock()
        {
            _isLocked = !_isLocked;
        }

        public void Fog(Cell target)
        {
            if (target.Hero == null)
                AnimateColor(target, _fogTime, _fogColor, _targetFogColor);
        }

        public void Defog(Cell target)
        {
            AnimateColor(target, _defogTime, Color.white, Color.white);
        }
    }
}
