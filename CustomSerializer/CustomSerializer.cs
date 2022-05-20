// using System.Collections.ObjectModel;
// using System.IO;
// using Autosalon.ViewModels;
// using Newtonsoft.Json;
//
// namespace Autosalon.CustomSerializer;
//
// public static class CustomSerializer
// {
//     public static void Serialize(CustomerViewModel model)
//     {
//         using (StreamWriter sw = new StreamWriter("cars.json", false, System.Text.Encoding.Default))
//         {
//             var json = new JsonSerializer();
//             // json.Serialize(sw, model.Cars);
//         }
//     }
//     
//     public static ObservableCollection<Car>? Deserialize()
//     {
//         ObservableCollection<Car>? cars;
//         if(File.Exists("cars.json") && File.ReadAllText("cars.json") != "")
//         {
//             using (StreamReader sr = File.OpenText("cars.json"))
//             {
//                 var json = new JsonSerializer();
//                 cars = (ObservableCollection<Car>)json.Deserialize(sr, typeof(ObservableCollection<Car>))!;
//             }
//         }
//         else
//         {
//             cars = new ObservableCollection<Car>();
//         }
//
//         return cars;
//     }
// }