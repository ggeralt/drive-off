Ova web aplikacija služi kako bi privatne osobe mogle staviti svoje vozilo na oglas i nuditi ga u najam.
Sastoji se od tri dijela:
1. ASP.NET Web Api: back-end aplikacije koji komunicira sa SQL bazom podataka putem Entity Framework-a te podatke šalje na front-end
2. ASP.NET MVC: front-end aplikacije odnosno klijent koji komunicira sa Api-em i dohvaća podatke sa njega kako bi ih prikazao na View.
3. ASP.NET WPF: front-end aplikacije koji je korišten isključivo od strane administrtora aplikacije gdje administratora također komunicirajući sa Api-em ima mogućnost brisanja drugih oglasa i komentara.
