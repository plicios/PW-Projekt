using System.ComponentModel.DataAnnotations;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels
{
    class ProducerViewModel
    {
        public IProducer Producer { get; }

        public ProducerViewModel(IProducer producer)
        {
            Producer = producer;
        }

        public string FilterValue => Name;

        [Required(ErrorMessage = "Nazwa producenta jest wymagana.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa musi byc dłuższa niż 3 i krótsza niż 50 znaków.")]
        public string Name
        {
            get => Producer.Name;
            set
            {
                Producer.Name = value;
                Validate();
            }
        }

        private void Validate()
        {

        }
    }
}
