using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Hero.Piano
{
    public class NoteAnimation : MonoBehaviour
    {
        [SerializeField] private float riseHeight = 3f; // Насколько высоко будет подниматься объект
        [SerializeField] private float swingStrength = 1f; // Амплитуда качания
        [SerializeField] private float duration = 5f; // Продолжительность полной анимации
        [SerializeField] private int rotations = 2;
        private void Start()
        {
            AnimNote();
        }

        private void AnimNote()
        {
            Sequence sequence = DOTween.Sequence();
        
            // Плавное поднятие вверх
            sequence.Append(transform.DOMoveY(transform.position.y + riseHeight, duration).SetEase(Ease.InOutSine));
            // Качание из стороны в сторону
            sequence.Join(transform.DORotate(new Vector3(0, 0, 1 * swingStrength), duration / 4, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine).SetLoops(4, LoopType.Yoyo));
            // Вращение вокруг своей оси
            sequence.Join(transform.DORotate(new Vector3(0, 360 * rotations, 0), duration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
            sequence.Play();
            sequence.OnComplete(() => Destroy(gameObject));
        }
    }
}