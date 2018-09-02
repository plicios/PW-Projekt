using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gorny.KetchupCatalog.Interfaces;
using System.ComponentModel.DataAnnotations;
using Gorny.KetchupCatalog.Core;
using System.Windows;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    public class KetchupViewModel : ViewModel
    {
        public IKetchup Ketchup { get; }

        public ObservableCollection<IProducer> Producers => new ObservableCollection<IProducer>((Application.Current as App)?.DataProvider.Producers ?? new List<IProducer>());

        public KetchupViewModel(IKetchup ketchup)
        {
            Ketchup = ketchup;
            Validate();
        }

        public string FilterValue => Name + Producer.Name + Type + ManufactureDate;

        public void RefreshProducsers()
        {
            OnPropertyChanged(nameof(Producers));
        }

        [Required(ErrorMessage = "Nazwa ketchupu jest wymagana.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa musi byc dłuższa niż 5 i krótsza niż 50 znaków.")]
        public string Name
        {
            get => Ketchup.Name;
            set
            {
                Ketchup.Name = value;
                Validate();
                OnPropertyChanged(nameof(Name));
            }
        }

        [Required(ErrorMessage = "Data produkcji jest wymagana")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Należy podać datę w formacie \"dd-MM-yyyy\"")]
        public string ManufactureDate
        {
            get => Ketchup.ManufactureDate.ToString(Constants.DateFormat);
            set
            {

                try
                {
                    DateTime date = DateTime.ParseExact(value, Constants.DateFormat, null);
                    Ketchup.ManufactureDate = date;
                    Validate();
                    OnPropertyChanged(nameof(ManufactureDate));
                }
                catch (FormatException)
                {

                }
            }
        }

        [Required(ErrorMessage = "Typ ketchupu jest wymagany")]
        public KetchupType Type
        {
            get => Ketchup.Type;
            set
            {
                Ketchup.Type = value;
                Validate();
                OnPropertyChanged(nameof(Type));
            }
        }

        [Required(ErrorMessage = "Producent jest wymagany")]
        public IProducer Producer
        {
            get => Ketchup.Producer;
            set
            {
                Ketchup.Producer = value;
                Validate();
                OnPropertyChanged(nameof(Producer));
            }
        }
    }
}
