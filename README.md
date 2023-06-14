# DinnerIn.Web 
Är ett projekt som har fokuserat på att bygga en blogg matlagnings webbsida för att demonstrerar ett fullStack-arbete som 
har arbetas i grupp. Som har inkluderat ett fristående frontend och backend webbsystem som är uppbyggt på C# MVC Core med 
HTML, CSS och JavaScript. Vyerna är uppbyggda med Razor som kombinerar HTML med C#.
# Steg-för-steg för att öppna projeket 
*******
Hur du öppnar DinnerIn projektet till Visual Studieo.  
1. Dubbelkolla så att du har Visual Studio nerladdat.
2. På projektets huvudsida i DinnerIn finns det en flik <code>, där hittar du allmän information om projektet, inklusive filer och mappar som ingår. 
3. I det högra hörnet inuti <code> finns det en knapp som står <code>, där kan du välja hur du vill ladda ner projektet. Äntligen
som en zip fil, öppna det via Visual Studio eller clona via github länk.
4. Efter valfri nerladdning ska projekts alla filer visas på Visual Studio.
5. Öppna Package Manager Console genom att gå till Tools, sedan öppna PMC Package Manager Console. Installera Enity Framework paketet och kör kommandot Install-Package Microsoft.EnityFrameworkCore.
Skapa sedan migrationsfilerna för båda databaserna genom kommandona 
  Add-Migration "InitialCreate" -Context DinnerInDbContext
  Add-Migration "InitialCreate" -Context AuthDbContext
Därefter uppdaterar man databasen via kommandot 
  Update-Database -Context DinnerInDbContext
  Update-Database -Context AuthDbContext
6. Om ett felmeddelande kommer up när man skriver in tex ett recept är detta pga av att alla fält MÅSTE vara ifyllda. 

*******
