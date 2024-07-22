using System;

namespace Serfe.MVVM
{
    public class ReactiveProperty<T>
    {
        public event Action<T> OnChange;

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChange?.Invoke(_value);
            }
        }

        public void Invoke()
        {
            OnChange?.Invoke(_value);
        }
    }
}