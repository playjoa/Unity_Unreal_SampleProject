using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityGraphics
{
    public class EntityGraphicsController : BaseEntityComponent
    {
        [Header("Graphics Holders")]
        [SerializeField] protected Transform entityGraphicsHolder;

        public Animator EntityAnimator { get; private set; }
        public Transform EntityGraphicsHolder => entityGraphicsHolder;
        
        private GameObject _entityGraphicsView;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            SetUpGraphicView();
        }

        public void ToggleGraphics(bool value)
        {
            entityGraphicsHolder.gameObject.SetActive(value);
        }

        protected virtual void SetUpGraphicView()
        {
            if (Owner.EntityData.EntityGraphicView == null) return;

            _entityGraphicsView = Instantiate(Owner.EntityData.EntityGraphicView, entityGraphicsHolder);
            if (_entityGraphicsView.TryGetComponent<Animator>(out var entityAnimator))
            {
                EntityAnimator = entityAnimator;
            }
        }
        
        private void SetUpScales(EntityData entityData)
        {
            if (Owner.EntityTransform != null)
            {
                Owner.EntityTransform.localScale = entityData.EntityScale;
            }
        }
    }
}