using System.ComponentModel.DataAnnotations;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    public class ProducerViewModel : ViewModel
    {
        public IProducer Producer { get; }

        public ProducerViewModel(IProducer producer)
        {
            Producer = producer;
            Validate();
        }

        public string FilterValue => Name;

        [Required(ErrorMessage = "Nazwa producenta jest wymagana.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa musi byc dłuższa niż 5 i krótsza niż 50 znaków.")]
        public string Name
        {
            get => Producer.Name;
            set
            {
                Producer.Name = value;
                Validate();
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
