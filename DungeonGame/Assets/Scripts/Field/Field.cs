using UnityEngine;
using DG.Tweening;
using LevelLogic;

namespace GameBoard
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Line[] _lines;
        [SerializeField] private Color _fogColor;
        [SerializeField] private Color _targetFogColor;
        [SerializeField] private float _fogTime;
        [SerializeField] private float _defogTime;
        [SerializeField] private HeroPlacer _heroPlacer;

        private bool _isLocked = true;

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
        }

        private void OnDisable()
        {
            Level.Instance.CurrentStateMachine.OnGameStarted -= Relock;
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

        private void AnimateColor(Cell target, float animationTime, Color endColor, Color targetEndColor)
        {
            if (_isLocked || _heroPlacer.IsSelected() == false)
                return;

            var row = GetRow(target);
            var line = _lines[target.LineNumber].Cells;

            for (int i = 0; i < row.Length; i++)
            {
                AnimateCell(row[i], endColor, animationTime);
            }

            for (int i = 0; i < line.Length; i++)
            {
                AnimateCell(line[i], endColor, animationTime);
            }

            AnimateCell(target, endColor, animationTime);
        }

        public void Relock()
        {
            _isLocked = !_isLocked;
        }

        public void Fog(Cell target)
        {
            AnimateColor(target, _fogTime, _fogColor, _targetFogColor);
        }

        public void Defog(Cell target)
        {
            AnimateColor(target, _defogTime, Color.white, Color.white);
        }
    }
}
