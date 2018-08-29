using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gorny.KetchupCatalog.Interfaces;
using System.ComponentModel.DataAnnotations;
using Gorny.KetchupCatalog.Core;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    class KetchupViewModel
    {
        public IKetchup Ketchup { get; }

        public ObservableCollection<IProducer> Producers { get; }

        public KetchupViewModel(IKetchup ketchup, List<IProducer> producers)
        {
            Ketchup = ketchup;
            Producers = new ObservableCollection<IProducer>(producers);
        }

        public string FilterValue => Name + Producer.Name + Type + ManufactureDate;

        [Required(ErrorMessage = "Nazwa ketchupu jest wymagana.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa musi byc dłuższa niż 3 i krótsza niż 50 znaków.")]
        public string Name
        {
            get => Ketchup.Name;
            set
            {
                Ketchup.Name = value;
                Validate();
            }
        }

        [Required(ErrorMessage = "Data produkcji jest wymagana")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Należy podać datę w formacie \"dd-MM-yyyy\"")]
        public string ManufactureDate
        {
            get => Ketchup.ManufactureDate.ToString("dd-MM-yyyy");
            set
            {

                try
                {
                    DateTime date = DateTime.ParseExact(value, "dd-MM-yyyy", null);
                    Ketchup.ManufactureDate = date;
                    Validate();
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
            }
        }

        private void Validate()
        {

        }
    }
}
