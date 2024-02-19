namespace Gameplay.Entity.Base.Utils
{
    public static class LayerUtils
    {
        public const int UILayerMask = 1 << 5;
        public const int EntityLayerMask = 1 << 6;
        public const int GroundLayerMask = 1 << 7;
        public const int InteractableLayerMask = 1 << 8;
        public const int ProjectileLayerMask = 1 << 9;
        
        public const int UILayerIndex = 5;
        public const int EntityLayerIndex = 6;
        public const int GroundLayerIndex = 7;
        public const int InteractableLayerIndex = 8;
        public const int ProjectileLayerIndex = 9;
    }
}