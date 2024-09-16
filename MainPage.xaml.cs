using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace coffee_maui
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string _textoCompleto = "El café capuchino​, del italiano capuccino, es una bebida con café y leche nacida en Italia, preparada con café expreso y crema de leche conseguida con vapor para darle cremosidad. Se compone de leche, café espresso y, en ocasiones se agrega cacao en polvo o canela por encima según el gusto del consumidor.";
        private string _textoCorto = "El café capuchino​...";
        private string _precio = "2000 $";
        private SizeOption _selectedSize;

        // Lista para almacenar los pedidos
        public ObservableCollection<OrderItem> OrderItems { get; } = new ObservableCollection<OrderItem>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this; // Asegúrate de que el BindingContext esté correctamente configurado

            // Inicializa las propiedades y comandos
            TextoCapuchino = "El café capuchino​...";
            LeerMasTexto = " Leer más";

            LeerMasCommand = new Command(LeerMas);
            AddToOrderCommand = new Command(AddToOrder);
            BuyCommand = new Command(ShowOrderSummary);
            ClearOrderCommand = new Command(ClearOrder); 

            // Configura la lista de opciones de tamaño
            SizeOptions = new ObservableCollection<SizeOption>
    {
        new SizeOption { Size = "Chico" },
        new SizeOption { Size = "Mediano" },
        new SizeOption { Size = "Grande" }
    };

            // Selecciona el tamaño predeterminado
            SelectedSize = SizeOptions.First();
        }


        public ObservableCollection<SizeOption> SizeOptions { get; }

        // Propiedad para el texto de la descripción
        private string _textoCapuchino;
        public string TextoCapuchino
        {
            get => _textoCapuchino;
            set
            {
                _textoCapuchino = value;
                OnPropertyChanged(nameof(TextoCapuchino)); // Notifica el cambio
            }
        }

        // Propiedad para el texto "Leer más"
        private string _leerMasTexto;
        public string LeerMasTexto
        {
            get => _leerMasTexto;
            set
            {
                _leerMasTexto = value;
                OnPropertyChanged(nameof(LeerMasTexto)); // Notifica el cambio
            }
        }

        // Propiedad para el precio
        public string Precio
        {
            get => _precio;
            set
            {
                _precio = value;
                OnPropertyChanged(nameof(Precio));
            }
        }

        // Propiedad para el tamaño seleccionado
        public SizeOption SelectedSize
        {
            get => _selectedSize;
            set
            {
                _selectedSize = value;
                UpdatePrice();
                OnPropertyChanged(nameof(SelectedSize));

                // Actualiza la selección de los RadioButtons
                foreach (var sizeOption in SizeOptions)
                {
                    sizeOption.IsSelected = sizeOption == _selectedSize;
                }
            }
        }

        public ICommand LeerMasCommand { get; }
        public ICommand AddToOrderCommand { get; }
        public ICommand BuyCommand { get; }
        public ICommand ClearOrderCommand { get; }

        private void LeerMas()
        {
            if (TextoCapuchino == _textoCorto)
            {
                TextoCapuchino = _textoCompleto;
                LeerMasTexto = ""; // Oculta "Leer más"
            }
            else
            {
                TextoCapuchino = _textoCorto;
                LeerMasTexto = "... Leer más";
            }

            OnPropertyChanged(nameof(TextoCapuchino));
            OnPropertyChanged(nameof(LeerMasTexto));
        }

        private void UpdatePrice()
        {
            if (SelectedSize != null)
            {
                switch (SelectedSize.Size)
                {
                    case "Chico":
                        Precio = "1500 $";
                        break;
                    case "Mediano":
                        Precio = "2000 $";
                        break;
                    case "Grande":
                        Precio = "2500 $";
                        break;
                }
            }
        }

        // Método para manejar el cambio de selección del RadioButton
        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton && e.Value)
            {
                var selectedSize = SizeOptions.FirstOrDefault(option => option.Size == (string)radioButton.Content);
                if (selectedSize != null)
                {
                    SelectedSize = selectedSize;
                }
            }
        }

        // Agregar al pedido
        private void AddToOrder()
        {
            if (SelectedSize != null)
            {
                OrderItems.Add(new OrderItem
                {
                    Size = SelectedSize.Size,
                    Price = Precio
                });
                OnPropertyChanged(nameof(CoffeeCount)); // Actualiza el contador
            }
        }

        // Mostrar resumen del pedido
        private async void ShowOrderSummary()
        {
            var totalAmount = OrderItems.Count;
            var totalPrice = OrderItems.Sum(item => decimal.Parse(item.Price.Replace(" $", "")));

            var orderSummary = $"Total cafés: {totalAmount}\n";

            var groupedBySize = OrderItems.GroupBy(item => item.Size);
            foreach (var group in groupedBySize)
            {
                var size = group.Key;
                var count = group.Count();
                var price = group.First().Price;
                orderSummary += $"{size}: {count} cafés - {price}\n";
            }
            orderSummary += $"Total a pagar: {totalPrice} $";

            await DisplayAlert("Resumen del pedido", orderSummary, "OK");
        }

        // Limpiar el pedido
        private void ClearOrder()
        {
            OrderItems.Clear();
            OnPropertyChanged(nameof(CoffeeCount)); 
        }


        // Contador de cafés en el pedido
        public int CoffeeCount => OrderItems.Count;

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SizeOption : INotifyPropertyChanged
    {
        private bool _isSelected;

        public string Size { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OrderItem
    {
        public string Size { get; set; }
        public string Price { get; set; }
    }
}
