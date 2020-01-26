using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MVVMFramework
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, PropertyInfo> propertyDict 
            = new Dictionary<string, PropertyInfo>();
        private readonly List<string> callStack = new List<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call this method in a derived viewmodel to raise the PropertyChanged event
        /// </summary>
        /// <param name="field">the backing field being assigned to</param>
        /// <param name="value">the new value being assigned to the backing field</param>
        /// <param name="caller">USE DEFAULT -- the name of the property being changed</param>
        protected void SetField<T>(ref T field, T value, [CallerMemberName] string caller = null)
        {
            if(!value.Equals(field))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }

        /// <summary>
        /// Call this method in a derived viewmodel to raise the PropertyChanged event
        /// ONLY on a property where SetProperty cannot be called within the property itself
        /// </summary>
        /// <typeparam name="Tprop"></typeparam>
        /// <param name="propertySelector">Selects the property to raise the event for.
        /// Example: () => Name</param>
        protected void OnPropertyChanged<Tprop>(Expression<Func<Tprop>> propertySelector)
        {
            if (propertySelector.Body is MemberExpression member)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member.Member.Name));
            }
            else
            {
                throw new ArgumentException("Selector expression must only select a property", 
                    nameof(propertySelector));
            }
        }
    }
}
