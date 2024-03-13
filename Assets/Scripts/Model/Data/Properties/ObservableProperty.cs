using UnityEngine;

namespace PortalGuardian.Model.Data.Properties
{
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] private TPropertyType _value;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;

        public TPropertyType Value
        {
            get => _value;
            set
            {
                if(_value.Equals(value)) return;

                var oldValue = _value;
                _value = value;
                OnChanged?.Invoke(_value, oldValue);
            }
        }
    }
}