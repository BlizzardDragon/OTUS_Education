using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour, IService
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private Vector3 _startPos;
    private Color _startColor;


    private void Start()
    {
        DOTween.Sequence()
            .Append(_rigidbody.DOMoveY(_startPos.y, 0.5f).SetEase(Ease.InQuint))
            .AppendCallback(PlaySound);

        _meshRenderer.material.DOFade(1, 0.5f).SetEase(Ease.OutQuint);
    }

    public void Install()
    {
        _startPos = transform.position;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.MovePosition(_startPos + Vector3.up * 2);

        _startColor = _meshRenderer.material.color;
        Color newColor = _startColor;
        newColor.a = 0;
        _meshRenderer.material.color = newColor;
    }

    private void PlaySound()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        // _audioSource.Play();
    }
}
