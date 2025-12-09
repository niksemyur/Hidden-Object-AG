using UnityEngine;
using Zenject;
using DG.Tweening;
using HiddenObject.Configs;
using HiddenObject.Signals.Item;

namespace HiddenObject.Item
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] ItemData _itemData;
        [SerializeField] private float _fadeDuration = 0.5f;

        [Inject] private readonly SignalBus _signalBus;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        public string GetId() => _itemData.Id;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        public void FindItem()
        {
            if (_collider != null)
                _collider.enabled = false;

            _spriteRenderer.DOFade(0f, _fadeDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }
    }
}