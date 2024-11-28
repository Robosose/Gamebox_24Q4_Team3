using Patterns.Singleton;

namespace Settings
{
    public class GammaMixer : Singleton<GammaMixer>
    {
        private float _gamma;

        public float Gamma => _gamma;

        public void SetGammaValue(float value)
        {
            _gamma = value;
        }
    }
}