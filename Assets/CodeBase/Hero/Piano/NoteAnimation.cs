using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Hero.Piano
{
    public class NoteAnimation : MonoBehaviour
    {
        private float riseHeight = 8f; // Насколько высоко будет подниматься объект
        private float swingStrength = 2f; // Амплитуда качания
        private float duration = 4f; // Продолжительность полной анимации
        private int rotations = 2;
        
        private void Start() => 
            AnimNote();

        private void AnimNote()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
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