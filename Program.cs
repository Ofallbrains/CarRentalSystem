using CarRentalSystem.Views;
namespace CarRentalSystem;
internal static class Program { [STAThread] static void Main(){ ApplicationConfiguration.Initialize(); Application.Run(new MainForm()); } }