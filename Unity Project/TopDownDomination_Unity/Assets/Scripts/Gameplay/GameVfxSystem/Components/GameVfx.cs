using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameVfxSystem.Controller;
using Gameplay.GameVfxSystem.Data;
using UnityEngine;

namespace Gameplay.GameVfxSystem.Components
{
    public class GameVfx : MonoBehaviour
    {
        public string VfxId => _currentData.Id;
        public bool Active => gameObject.activeInHierarchy;

        private static GameVfxController VfxController => GameController.ME.GameVfxController;
        
        private IGameEntity _owner;
        private VfxData _currentData;
        
        private Vector3 _followOffSet = Vector3.zero;
        
        private bool _alive = false;
        private float _timeLeft;

        public void Initiate(IGameEntity owner, VfxData data, float vfxOverrideDuration = -1)
        {
            _owner = owner;
            _currentData = data;
            
            _timeLeft = vfxOverrideDuration > 0 ? vfxOverrideDuration : data.VfxDuration;
            _followOffSet = data.StartPositionOffSet;
            
            gameObject.SetActive(true);
            _alive = true;
        }

        public void ForceEndVFX()
        {
            _alive = false;
            ReturnVfx();
        }

        public void DisableVfx()
        {
            _alive = false;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_alive) return;

            _timeLeft -= Time.deltaTime;
            ProcessVfxType();

            if (!(_timeLeft <= 0)) return;
            
            ReturnVfx();
            _alive = false;
        }

        private void ProcessVfxType()
        {
            if (_currentData.Type == VfxType.FixedInWorldSpace) return;
            if (_owner == null) return;
            
            transform.position = _owner.EntityTransform.position + _followOffSet;
        }

        private void ReturnVfx()
        {
            VfxController.ReturnVfx(this);
        }
    }
}