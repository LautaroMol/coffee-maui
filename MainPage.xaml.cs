using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace coffee_maui
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string _iconoUno = "&#xE807;";
        private string _iconoDos = "&#xE806;";
        private string _textoCompleto = "El café capuchino​, del italiano capuccino, es una bebida con café y leche nacida en Italia, preparada con café expreso y crema de leche conseguida con vapor para darle cremosidad. Se compone de leche, café espresso y, en ocasiones se agrega cacao en polvo o canela por encima según el gusto del consumidor.";
        private string _textoCorto = "El café capuchino​...";

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Inicializa las propiedades
            IconoDerecho = _iconoUno; // Cambié el nombre a IconoDerecho
            TextoCapuchino = _textoCorto;
            LeerMasTexto = " Leer más";

            // Inicializa los comandos
            CambiarIconoCommand = new Command(CambiarIcono);
            LeerMasCommand = new Command(LeerMas);
        }

        // Propiedad para el icono (derecho)
        private string _iconoDerecho;
        public string IconoDerecho
        {
            get => _iconoDerecho;
            set
            {
                _iconoDerecho = value;
                OnPropertyChanged(nameof(IconoDerecho)); // Notifica el cambio
            }
        }

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

        // Comando para cambiar el icono
        public ICommand CambiarIconoCommand { get; }

        // Comando para expandir el texto de la descripción
        public ICommand LeerMasCommand { get; }

        // Método que alterna el icono
        private void CambiarIcono()
        {
            IconoDerecho = (IconoDerecho == _iconoUno) ? _iconoDos : _iconoUno;
        }

        // Método para mostrar más texto
        private void LeerMas()
        {
            Console.WriteLine("Leer más tocado");

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
        //
        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
