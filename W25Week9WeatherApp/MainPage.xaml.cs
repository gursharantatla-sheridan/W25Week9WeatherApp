using System.Threading.Tasks;

namespace W25Week9WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                stack.Background = Brush.MediumPurple;
            }
        }

        private async void GetWeatherButton_Clicked(object sender, EventArgs e)
        {
            var location = await Geolocation.Default.GetLocationAsync();
            var lat = location.Latitude;
            var lon = location.Longitude;

            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid=adee4d9d26685357054efd2f06359807";

            var weatherData = await WeatherProxy.GetWeatherAsync(url);

            CityLbl.Text = weatherData.name;
            TemperatureLbl.Text = weatherData.main.temp.ToString("F0") + " \u00B0C";
            ConditionsLbl.Text = weatherData.weather[0].description;

            string icon = $"https://openweathermap.org/img/wn/{weatherData.weather[0].icon}@2x.png";
            WeatherImg.Source = ImageSource.FromUri(new Uri(icon));
        }
    }
}
