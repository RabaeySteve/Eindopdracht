using System;
using System.IO;
using EindopdrachtPG.Domain.Models;
using EindopdrachtPG.Infrastructure.Data.Repositories.KlantenRepository;
using EindopdrachtPG.Infrastructure.DataImport;
using EindopdrachtPG.Infrastructure;
using EindopdrachtPG.Infrastructure.Repositories.KlantenRepository;
using Ado.Data.SqlServer;

class Program {
    
    static void Main(string[] args) {




        // Path naar het tekstbestand met klanten
        var filePath = @"..\..\..\..\Database\Input\klanten.txt";
        var fullPath = Path.GetFullPath(filePath);

        // Controleer of het bestand bestaat
        if (!File.Exists(fullPath)) {
            Console.WriteLine("Het opgegeven bestand bestaat niet.");
            return;
        }

        // Maak een instantie van de DataImporter
        var dataImporter = new DataImporter();
        var klanten = dataImporter.ImportKlantenFromFile(fullPath);



        // Initialiseer de database connectie en klant repository


        // Voeg de klanten toe aan de database via de klantInsert
        foreach (var klant in klanten) {
            klantInsert.NewRecord(klant); // Gebruik de insert-methode van KlantInsert
            Console.WriteLine($"Klant toegevoegd: {klant.Naam}, {klant.Adres}");
        }
    }
}

