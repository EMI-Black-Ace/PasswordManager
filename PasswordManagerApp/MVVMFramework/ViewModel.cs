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

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call this method in a derived viewmodel to raise the PropertyChanged event
        /// </summary>
        /// <param name="value">the new value being assigned to the property</param>
        /// <param name="caller">USE DEFAULT -- the name of the property being changed</param>
        protected void SetProperty(object value, [CallerMemberName] string caller = null)
        {
            if (!propertyDict.ContainsKey(caller))
            {
                propertyDict.Add(caller, this.GetType().GetProperty(caller));
            }
            if(value != propertyDict[caller].GetValue(this))
            {
                propertyDict[caller].SetValue(value, this);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }

        /// <summary>
        /// Call this method in a derived viewmodel to raise the PropertyChanged event
        /// ONLY on a property where SetProperty cannot be called within the property itself
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertySelector">Selects the property to raise the event for.
        /// Example: () => this.Name</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            if(propertySelector is MemberExpression member 
            && member.Member.MemberType == MemberTypes.Property)
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
