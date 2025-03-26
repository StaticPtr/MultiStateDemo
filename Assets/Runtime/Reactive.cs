using System;

namespace Runtime
{
    public class Reactive<T> : IDisposable
    {
        public delegate void ValueChangedDelegate(T? oldValue, T? newValue);
        
        public T? Value { get; private set; } = default;

        public event ValueChangedDelegate? OnValueChanged;

        public Reactive(T? initialValue)
        {
            Value = initialValue;
        }
        
        public void ChangeValue(T? newValue)
        {
            T? oldValue = Value;
            Value = newValue;
            
            OnValueChanged?.Invoke(oldValue, Value);
        }

        public void Dispose()
        {
            Value = default;
            OnValueChanged = null;
        }
    }
}