using System;

namespace TestTask.Infrastructure
{
    public class ReactiveProperty<T>
    {
        public event Action<T> OnValueChange;

        private T _value;


        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChange?.Invoke(_value);
            }
        }
    } 
}
