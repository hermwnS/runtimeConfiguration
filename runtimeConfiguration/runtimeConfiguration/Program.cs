using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        AppKonfig cfg = new AppKonfig();
        Console.WriteLine("Language (en/id): ");
        string pilihanBahasa = Console.ReadLine();

        string pesan = (pilihanBahasa == "en") ? cfg.konfig.pesanAwal.en : cfg.konfig.pesanAwal.id;
        Console.WriteLine(pesan);

        string jenisBarang = Console.ReadLine();
        double hargaBarang = Convert.ToDouble(Console.ReadLine());

        double totalDiskon = 0;

        if (cfg.konfig.jenisKenaDiskon.Contains(jenisBarang))
        {
            totalDiskon = hargaBarang * cfg.konfig.diskon / 100;
        }
        
        Console.WriteLine(hargaBarang - totalDiskon);
    }
}

public class AppKonfig
{
    public Konfig konfig;
    private const string filepath = "D:\\kuliah\\semester 4\\konstruksi PL\\runtimeConfiguration\\runtimeConfiguration\\runtimeConfiguration\\konfig.json";
    
    public AppKonfig()
    {
        try
        {
            ReadKonfigFile();
        }
        catch
        {
            SetDefault();
            WriteKonfigFile();
        }
    }

    public void ReadKonfigFile()
    {
        string baca = File.ReadAllText(filepath);
        konfig = JsonSerializer.Deserialize<Konfig>(baca);
    }

    public void WriteKonfigFile()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        string tulisan = JsonSerializer.Serialize(konfig,options);
        File.WriteAllText(filepath, tulisan);
    }

    public void SetDefault()
    {
        konfig = new Konfig();
        konfig.pesanAwal = new PesanAwal();
        konfig.pesanAwal.en = "Please enter your item type followed by the item price : ";
        konfig.pesanAwal.id = "Masukkan jenis barang dan harga barangnya : ";

        konfig.diskon = 25.0;
        konfig.jenisKenaDiskon = new List<string>();
        konfig.jenisKenaDiskon.Add("baju");
        konfig.jenisKenaDiskon.Add("celana");
    }
}

public class Konfig
{
    public PesanAwal pesanAwal { get; set; }
    public double diskon { get; set; }
    public List<string> jenisKenaDiskon { get; set; }

    public Konfig() { }
}

public class PesanAwal
{
    public string en { get; set; }
    public string id { get; set;}

    public PesanAwal() { }


}