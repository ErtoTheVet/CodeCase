using Configuration.Services;

var reader = new ConfigReader(
    "SERVICE-A",
    "mongodb://localhost:27017",
    10000);

//Console.WriteLine(reader.GetValue<string>("SiteName"));
//Console.WriteLine(reader.GetValue<int>("MaxItemCount"));
//Console.WriteLine(reader.GetValue<bool>("IsBasketEnabled"));

while (true)
{
    Console.WriteLine(reader.GetValue<string>("SiteName"));
    Thread.Sleep(3000);
}