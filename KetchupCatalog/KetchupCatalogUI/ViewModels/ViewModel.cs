using Gorny.KetchupCatalog.KetchupCatalogUI.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    public class ViewModel : INotifyDataErrorInfo, INotifyPropertyChanged
    {

        protected void Validate()
        {
            ValidationContext validationContext = new ValidationContext(this, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            _errors.Clear();

            IEnumerable<IGrouping<string, ValidationResult>> namesResultsList = from result in validationResults
                                                                                from names in result.MemberNames
                                                                                group result by names into list
                                                                                select list;

            foreach (IGrouping<string, ValidationResult> validationResult in namesResultsList)
            {
                List<string> messages = validationResult.Select(result => result.ErrorMessage).ToList();
                _errors.Add(validationResult.Key, messages);
                RaiseErrorChanged(validationResult.Key);
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        protected Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool HasErrors => _errors.Count > 0;


        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            return null;
        }

        protected void RaiseErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
