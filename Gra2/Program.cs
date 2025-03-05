using System;
using System.Collections.Generic; //do używania stack<>
using System.Threading.Tasks;

namespace Gra
{
    //główna klasa
    public class Rozgrywka
    {
        public delegate void PobierzImieDelegate(string imie);
        public event PobierzImieDelegate pobierzImieDelegate;

        protected int lvl, dmg, zycie, dmg_w, zycie_w, zycie_y, dmg_y; //dostepnosc zmiennych jest ustawiona na protected, zamiast public by byly dostepne tylko w klasie bazowej i pochodnej
        protected string imie, ekwipunek, lokalizacja;

        public Stack<string> LokalizacjeStos = new Stack<string>(); //tutaj będę przechowywać dane o aktualnej lokalizacji użytkownika, dzięki temu będę ją wyswietlać na ekranie

        //---------------HERMETYZACJA - GETTERY I SETTERY-----------------
        protected int Lvl
        {
            get { return lvl; }
            set { lvl = value; }
        }
        protected int Dmg
        {
            get { return dmg; }
            set { dmg = value; }
        }
        protected int Zycie
        {
            get { return zycie; }
            set { zycie = value; }
        }
        protected int Dmg_w
        {
            get { return dmg_w; }
            set { dmg_w = value; }
        }
        protected int Zycie_w
        {
            get { return zycie_w; }
            set { zycie_w = value; }
        }
        protected int Zycie_y
        {
            get { return zycie_y; }
            set { zycie_y = value; }
        }
        protected int Dmg_y
        {
            get { return dmg_y; }
            set { dmg_y = value; }
        }
        protected string Lokalizacja
        {
            get { return lokalizacja; }
            set { lokalizacja = value; }
        }
        protected string Ekwipunek
        {

            get { return ekwipunek; }
            set { ekwipunek = value; }
        }
        public Rozgrywka() //konstruktor klasy bazowej
        {
            Lvl = 1;
            Dmg = 10;
            Zycie = 7;
            Dmg_w = 14;
            Zycie_w = 8;
            Zycie_y = 30;
            Dmg_y = 24;
            LokalizacjeStos.Push("Senna Kotlina - START"); //dodaje na stos wartość startową
            Lokalizacja = LokalizacjeStos.Peek(); //pobiera wartość ze szczytu stosu, by wyświetlić jak najbardziej aktualną wartość
            Ekwipunek = "Twój plecak jest pusty";
        }
        public void PobierzImie() //pobieranie od użytkownika imienia za pomoca delegata oraz obsługiwania wydarzenia do tego
        {
            var imieWprowadzone = false;

            while (!imieWprowadzone)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Wpisz swój nick: ");
                Console.ResetColor();
                string wprowadzoneImie = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(wprowadzoneImie))
                {
                    this.imie = wprowadzoneImie;
                    Console.Clear();
                    pobierzImieDelegate?.Invoke(this.imie);
                    imieWprowadzone = true;
                    Wybor1();
                }
                else //by użytkownik nie mógł wprowadzić pustego pola, jako nick
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nie wpisałeś poprawnie swojego nicku! Spróbuj ponownie.");
                    Console.ResetColor();
                }
            }
        }
        //---------------METODY WYWOŁUJĄCE STATYSTYKI-----------------
        //--GRACZ--
        public virtual void Statystyki(int lvl, int zycie, int dmg, string lokalizacja, string ekwipunek)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.Write("                            ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("STATYSTYKI GRACZA - " + imie);
            Console.ResetColor();
            string statystyki = $" \n Twój poziom gry: {lvl} \n Twoje punkty życia: {zycie} \n Twoje punkty walki: {dmg} \n Twoja aktualna lokalizacja: {lokalizacja} \n Zawartość Twojego plecaka: {ekwipunek}"; //łańcuch interpolowany
            Console.WriteLine(statystyki);
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("");
        }
        //--WILKOŁAK--
        protected void Wilkolak(int dmg_w, int zycie_w)
        {
            this.dmg_w = dmg_w;
            this.zycie_w = zycie_w;
        }
        protected void Statystyki_w(int zycie_w, int dmg_w)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.Write("                            ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("STATYSTYKI WILKOŁAKA");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            string statystyki_w = $" \n Punkty życia Wilkołaka: {zycie_w} \n Punkty walki Wilkołaka: {dmg_w}";
            Console.WriteLine(statystyki_w);
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.ResetColor();
        }
        //--YETI--
        protected void Yeti(int dmg_y, int zycie_y)
        {
            this.dmg_y = dmg_y;
            this.zycie_y = zycie_y;
        }
        protected void Statystyki_y(int zycie_y, int dmg_y)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.Write("                            ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("STATYSTYKI YETI'EGO");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string statystyki_y = $" \n Punkty życia Yeti'ego: {zycie_y} \n Punkty walki Yeti'ego: {dmg_y}";
            Console.WriteLine(statystyki_y);
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.ResetColor();
        }
        //------------WYBÓR PIERWSZEJ LOKALIZACJI------------
        protected void Wybor1()
        {
            Console.WriteLine("Stajesz przed swoim pierwszym poważnym wyborem. \nPrzed Tobą znajdują się trzy ścieżki, zadecyduj, którą chcesz podążać najpierw");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Wybierz odpowiednią cyfrę na klawiaturze: ");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write(" 1 ");
            Console.ResetColor();
            Console.Write(" -jeśli chcesz podążać mroczną leśną ścieżką");
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(" 2 ");
            Console.ResetColor();
            Console.Write(" -jeśli bliskie Ci wspinaczki górskie");
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write(" 3 ");
            Console.ResetColor();
            Console.Write(" -jeśli uspokaja Cię szum fal");
            Console.WriteLine();

            var dobrywybor1 = false; //var - domyślna zmienna
            while (!dobrywybor1)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();
                if (Char.IsDigit(keyInfo.KeyChar))
                {
                    double wybor1 = double.Parse(keyInfo.KeyChar.ToString());
                    if (wybor1 == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Wybrałeś mroczną leśną ścieżkę. Przed Tobą pierwsze wyzwanie! " + this.imie);
                        Lokalizacje lokalizacje = new Lokalizacje(); // tworzenie obiektu klasy Lokalizacje
                        lokalizacje.ObslugaPobierzImie(imie);
                        lokalizacje.Las(); // wywołanie metody Las na obiekcie lokalizacje
                        dobrywybor1 = true;
                    }
                    else if (wybor1 == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Wybrałeś wspinaczki górskie. Przed Tobą pierwsze wyzwanie! " + this.imie);
                        Lokalizacje lokalizacje = new Lokalizacje();
                        lokalizacje.ObslugaPobierzImie(imie);
                        lokalizacje.Gory();
                        dobrywybor1 = true;
                    }
                    else if (wybor1 == 3)
                    {
                        Console.Clear();
                        Console.WriteLine("Wybrałeś szum fal. Przed Tobą pierwsze wyzwanie! " + this.imie);
                        Lokalizacje lokalizacje = new Lokalizacje();
                        lokalizacje.ObslugaPobierzImie(imie);
                        lokalizacje.Morze();
                        dobrywybor1 = true;
                    }
                    else
                    {
                        Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                        wybor1 = Convert.ToDouble(Console.ReadLine());
                    }
                }
                else
                    Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
            }
        }
        //-----------DIALOGI----------
        protected void Rozmowa() //dla przejrzystości kodu, przerwa przed dialogiem
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("                                      ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Rozmowa");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
        }
        protected void Dialog(string postac, string kwestia)
        {
            ConsoleColor kolor;
            if (postac == "Kowal")
            {
                kolor = ConsoleColor.Blue;
            }
            else if (postac == this.imie)
            {
                kolor = ConsoleColor.DarkYellow;
            }
            else if (postac == "Rybak")
            {
                kolor = ConsoleColor.Cyan;
            }
            else if (postac == "Mężczyzna")
            {
                kolor = ConsoleColor.DarkMagenta;
            }
            else if (postac == "Podróżnik 1")
            {
                kolor = ConsoleColor.DarkBlue;
            }
            else if (postac == "Podróżnik 2")
            {
                kolor = ConsoleColor.DarkGreen;
            }
            else if (postac == "Starzec")
            {
                kolor = ConsoleColor.DarkGreen;
            }
            else
            {
                kolor = ConsoleColor.DarkYellow;
            }
            Console.ForegroundColor = kolor;
            Console.Write(postac + ": ");
            Console.ResetColor();
            Console.WriteLine(kwestia);
        }
    }
    public class Lokalizacje : Rozgrywka //dziedziczenie klas, tworzymy klasę pochodną Lokalizacje
    {
        int licznik_lokalizacji = 0;
        public string imie_lokalizacje;

        public delegate void PobierzImieLokalizacjeDelegate(string imie);
        public event PobierzImieLokalizacjeDelegate pobierzImieLokalizacjeDelegate;
        public void ObslugaPobierzImie(string imie) //obslugiwanie delegata - pobieranie imienia od uzytkownika
        {
            this.imie_lokalizacje = imie;
            pobierzImieLokalizacjeDelegate?.Invoke(imie_lokalizacje);
        }
        public override void Statystyki(int lvl, int zycie, int dmg, string lokalizacja, string ekwipunek)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.Write("                            ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("STATYSTYKI GRACZA - " + imie_lokalizacje);
            Console.ResetColor();
            string statystyki = $" \n Twój poziom gry: {lvl} \n Twoje punkty życia: {zycie} \n Twoje punkty walki: {dmg} \n Twoja aktualna lokalizacja: {lokalizacja} \n Zawartość Twojego plecaka: {ekwipunek}";
            Console.WriteLine(statystyki);
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("");
        }
        public Lokalizacje() : base() //konstruktor klasy pochodnej
        {
            Lvl = 1;
            Dmg = 7;
            Zycie = 20;
            Dmg_w = 4;
            Zycie_w = 24;
            Zycie_y = 24;
            dmg_y = 24;
            Ekwipunek = "Twój plecak jest pusty";
        }
        //------------WYJŚCIE Z GRY------------
        protected void WyjsciezGry()
        {
            if (licznik_lokalizacji == 10)
            {
                Console.WriteLine("\nBył_ś już w każdej lokalizacji " + imie_lokalizacje);
                Console.WriteLine("Dziękujemy za grę! Mamy nadzieję, że Ci się podobało! Oto Twoje końcowe statystyki {0}: ", imie_lokalizacje);
                Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Wciśnij ENTER aby zwycięsko zakończyć grę!");
                Console.ResetColor();
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                var grawtrakcie = true;
                Console.WriteLine("Pora na kolejną przygodę! Czy jesteś gotowy?");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Naciśnij dowolny przycisk, by kontynuować, \nlub naciśnij ESC, jeśli masz dosyć przygód na dziś \ni chcesz zakończyć grę w tym miejscu, w którym aktualnie jesteś");
                Console.ResetColor();
                while (grawtrakcie)
                {
                    ConsoleKeyInfo wyjscie = Console.ReadKey(true);
                    if (wyjscie.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Czy jesteś pewny, że chcesz zakończyć grę? Naciśnij ENTER, jeśli to Twoja ostateczna decyzja lub dowolny klawisz, by pozostać w grze");
                        Console.ResetColor();
                        ConsoleKeyInfo wyjscie_ostateczne = Console.ReadKey(true);
                        if (wyjscie_ostateczne.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Dziękujemy za grę!");
                            Console.ResetColor();
                            Console.WriteLine("Mamy nadzieję, że Ci się podobało! Wracaj do nas czym prędzej! \n Oto Twoje końcowe statystyki {0}: ", imie_lokalizacje);
                            Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek); //za pomoca get i set odwołujemy się do własności jak do zwykłego publicznego pola 
                            grawtrakcie = false;
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cieszymy się, że zostajesz z nami, teraz możesz wybrać swoją kolejną lokalizację");
                            WyborLokalizacji();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        WyborLokalizacji();
                    }
                }
            }
        }
        //--------------------------WYBOR KOLEJNEJ LOKALIZACJI--------------------------
        public void WyborLokalizacji()
        {
            Console.Write("Twoja ostatnia lokalizacja: ");
            if (Lokalizacja == "las")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Lokalizacja);
                Console.ResetColor();
                Console.WriteLine();
            }
            else if (Lokalizacja == "gory")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Lokalizacja);
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(Lokalizacja);
                Console.ResetColor();
                Console.WriteLine();
            }
            var dobrywybor2 = false;
            switch (licznik_lokalizacji)
            {
                case 1:
                    {
                        Console.WriteLine("Byłeś tylko w lesie.");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Po wybraniu następnej lokalizacji wciśnij ENTER");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" 2 ");
                        Console.ResetColor();
                        Console.Write(" -jeśli bliskie Ci wspinaczki górskie");
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(" 3 ");
                        Console.ResetColor();
                        Console.Write(" -jeśli uspokaja Cię szum fal");
                        Console.WriteLine();

                        while (!dobrywybor2)
                        {
                            double wybor2;
                            if (double.TryParse(Console.ReadLine(), out wybor2)) //sprawdzamy, czy to co wpisał użytkownik można przekonwertować do double, jesli nie to wyświetlamy błąd
                            {
                                Console.WriteLine();
                                if (wybor2 == 2)
                                {
                                    Console.WriteLine("Wybrałeś wspinaczki górskie. Przed Tobą koeljne wyzwanie! " + imie_lokalizacje);
                                    Gory();
                                    dobrywybor2 = true;
                                }
                                else if (wybor2 == 3)
                                {
                                    Console.WriteLine("Wybrałeś szum fal. Przed Tobą kolejne wyzwanie! " + imie_lokalizacje);
                                    Morze();
                                    dobrywybor2 = true;
                                }
                                else
                                {
                                    Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                                Console.Clear();
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Byłeś tylko nad morzem.");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Po wybraniu następnej lokalizacji wciśnij ENTER");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" 1 ");
                        Console.ResetColor();
                        Console.Write(" -jeśli chcesz podążać mroczną leśną ścieżką");
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" 2 ");
                        Console.ResetColor();
                        Console.Write(" -jeśli bliskie Ci wspinaczki górskie");
                        Console.WriteLine();
                        while (!dobrywybor2)
                        {
                            double wybor2;
                            if (double.TryParse(Console.ReadLine(), out wybor2))
                            {
                                Console.WriteLine();
                                if (wybor2 == 1)
                                {
                                    Console.WriteLine("Wybrałeś mroczną ścieżkę. Przed Tobą kolejne wyzwanie! " + imie_lokalizacje);
                                    Las();
                                    dobrywybor2 = true;
                                }
                                else if (wybor2 == 2)
                                {
                                    Console.WriteLine("Wybrałeś górskie wspinaczki. Przed Tobą kolejne wyzwanie! " + imie_lokalizacje);
                                    Gory();
                                    dobrywybor2 = true;
                                }
                                else
                                {
                                    Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Byłeś nad morzem i w lesie. Pozostają Ci tylko góry, {0}!", imie_lokalizacje);
                        Gory();
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("Byłeś tylko w górach.");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Po wybraniu następnej lokalizacji wciśnij ENTER");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" 1 ");
                        Console.ResetColor();
                        Console.Write(" -jeśli chcesz podążać mroczną leśną ścieżką");
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(" 3 ");
                        Console.ResetColor();
                        Console.Write(" -jeśli uspokaja Cię szum fal");
                        Console.WriteLine();
                        while (!dobrywybor2)
                        {
                            double wybor2;
                            if (double.TryParse(Console.ReadLine(), out wybor2))
                            {
                                Console.WriteLine();
                                if (wybor2 == 1)
                                {
                                    Console.WriteLine("Wybrałeś mroczną ścieżkę. Przed Tobą kolejne wyzwanie! " + imie_lokalizacje);
                                    Las();
                                    dobrywybor2 = true;
                                }
                                else if (wybor2 == 3)
                                {
                                    Console.WriteLine("Wybrałeś szum fal. Przed Tobą kolejne wyzwanie! " + imie_lokalizacje);
                                    Morze();
                                    dobrywybor2 = true;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                                Console.ResetColor();
                            }
                        }
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("Byłeś w lesie i w górach. Pozostaje Ci tylko morze, {0}!", imie_lokalizacje);
                        Morze();
                        break;
                    }
                case 9:
                    {
                        Console.WriteLine("Byłeś nad morzem i w górach. Pozostają Ci tylko las, {0}!", imie_lokalizacje);
                        Las();
                        break;
                    }
            }
        }
        //--------------------------LAS--------------------------
        public void Las()
        {
            Console.Clear();
            LokalizacjeStos.Push("las"); //dodaję nową lokalizację na szczyt stosu
            Lokalizacja = LokalizacjeStos.Peek();
            Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
            licznik_lokalizacji++;

            Console.Write("                                        ");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("LAS");
            Console.ResetColor();
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Witaj w mrocznym lesie, gdzie ciemność i tajemnica splatają się w gęstej koronie drzew.\nTo miejsce pełne legend i opowieści, które od dawna przerażają serca śmiałków.\nKażdy krok może prowadzić do nieznanych przepaści, a złowrogie dźwięki i szepty wokół sprawiają, że włosy stają dęba.");
            Console.WriteLine("\nLas jest obfity w leśne przysmaki! \nNa wejściu znajdujesz ciekawą roślinę - tojad.");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Czy chcesz podnieść tojad?");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Napisz");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" TAK");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" jeśli chcesz zabrać ze sobą tojad");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("W przeciwnym wypadku napisz ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("NIE");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\nUWAGA!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" Stajesz przed swoim pierwszym wyborem w mrodznym lesie. \nPamiętaj, że każda decyzja ma ogromne znaczenie!");
            Console.ResetColor();


            string wybor2;
            var dobrywybor2 = false;
            while (!dobrywybor2)
            {
                wybor2 = Console.ReadLine().ToLower();
                Console.WriteLine();
                if (wybor2 == "tak")
                {
                    Console.Clear();
                    Console.WriteLine("Postanowił_ś podnieść tajemniczną roślinę  " + imie_lokalizacje);
                    if (Ekwipunek.Contains("pusty"))
                        Ekwipunek = "tojad ";
                    else
                        Ekwipunek += "tojad ";
                    Console.WriteLine("Zawartość Twojego plecaka: " + Ekwipunek);
                    dobrywybor2 = true;
                }
                else if (wybor2 == "nie")
                {
                    Console.Clear();
                    Console.WriteLine("Postanowił_ś nie zabierać ze sobą tojadu!" + imie_lokalizacje);
                    Console.WriteLine("Zawartość Twojego plecaka: " + Ekwipunek);
                    dobrywybor2 = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dokonałeś złego wyboru! Wpisz TAK lub NIE, by dokonać wyboru");
                    Console.ResetColor();
                }
            }
            Rozmowa();
            Console.WriteLine("\nPodczas przechadzki po lesie zauważasz małą chatkę, a obok niej nieznajomą postać.\nPostanawiasz podejść bliżej i się przywitać.\n");
            Dialog("Kowal", "Witaj w mrocznym lesie, podróżniku! W czym mogę Ci pomoć?\n ");
            Dialog(imie_lokalizacje, "Witaj! Wędruję przez las, szukając przygód i tajemnic. Czy masz jakieś porady dla mnie?\n");
            Dialog("Kowal", "Nazywam się Edward i jestem lokalnym kowalem.\nJako kuźnik jestem pochłonięty sztuką kowalstwa oraz tworzeniem unikatowych oraz fukcjonalnych przedmiotów. \nMyślę, że to może Cię zainteresować. Mam dla Ciebie szybką zagadkę.\nJeśli ją rozwiążesz, może wytworzę dla Ciebie przydatną broń. \nZainteresowany?");

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nWybór:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Jeżeli jesteś zainteresowany wciśnij ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("DOWOLNY PRZYCISK");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(", aby usłyszeć zagadkę kowala.");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Jeżeli nie chcesz rozwiązywać zagadki wciśnij ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ESC");
            Console.ResetColor();
            Console.WriteLine();

            ConsoleKeyInfo zagadka = Console.ReadKey(true);
            if (zagadka.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                WalkazWilkiem();
            }
            else
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("ZAGADKA OD KOWALA");
                Console.ResetColor();
                Console.WriteLine("Co ma korzenie, ale nie rośnie? \nMoże być gorące lub zimne. \nJest wykorzystywane przez wielu ludzi. \nCo to jest?");
                Console.WriteLine("1 - Woda   \n2 - Drzewo   \n3 - Elektryczność   \n4 - Kawa");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Wybierz odpowiedź, wpisując jedną z podanych powyżej cyfr");
                Console.ResetColor();

                //Zagadka
                double wybor3;
                var dobrywybor3 = false;

                while (!dobrywybor3)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    Console.WriteLine();
                    if (Char.IsDigit(keyInfo.KeyChar))
                    {
                        wybor3 = double.Parse(keyInfo.KeyChar.ToString());
                        if (wybor3 == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine("Super {0}, odpowiedział_ś poprawnie na zagadkę. \nWchodzisz na nowy level!", imie_lokalizacje);
                            Console.WriteLine("Elektryczność ma swoje korzenie w zjawisku elektrycznym, ale sama w sobie nie rośnie. \nMoże być zarówno gorąca, jak i zimna, w zależności od zastosowania. \nJest szeroko wykorzystywana przez wielu ludzi w różnych dziedzinach życia");
                            Console.ResetColor();
                            if (Ekwipunek.Contains("tojad"))
                            {
                                Ekwipunek = Ekwipunek.Replace("tojad", "");
                                if (String.IsNullOrWhiteSpace(Ekwipunek))
                                {
                                    Ekwipunek = "Twój plecak jest pusty";
                                }
                                Console.WriteLine("Ponadto w swoim ekwipunku posiadał_ś tojad, \ndzięki temu Kowal mógł wytworzyć śmiercionośną broń!");
                                if (Ekwipunek.Contains("pusty"))
                                    Ekwipunek = "włócznia ";
                                else
                                    Ekwipunek += "włócznia ";
                            }
                            dobrywybor3 = true;
                        }
                        else if (wybor3 == 1 | wybor3 == 2 | wybor3 == 4)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Clear();
                            Console.WriteLine("Niestety {0}, odpowiedział_ś źle, musimy Ci odebrać 5 punktów walki", imie_lokalizacje);
                            Console.WriteLine("Poprawna odpowiedź to - elektryczność");
                            Console.WriteLine("Elektryczność ma swoje korzenie w zjawisku elektrycznym, ale sama w sobie nie rośnie. \nMoże być zarówno gorąca, jak i zimna, w zależności od zastosowania. \nJest szeroko wykorzystywana przez wielu ludzi w różnych dziedzinach życia");
                            Console.ResetColor();
                            Dmg -= 5;
                            Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                            Console.WriteLine("Pomimo to, nie poddawaj się!! Musimy ruszać dalej!");
                            dobrywybor3 = true;
                        }
                        else if (wybor3 != 1 | wybor3 != 2 | wybor3 != 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Dokonałeś złego wyboru! Wybierz liczbę od 1 do 4");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Dokonałeś złego wyboru! Wybierz liczbę od 1 do 4");
                        Console.ResetColor();
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Wcisnij dowolny przycisk aby przejść dalej");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                RozmowaWilk();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Wcisnij dowolny przycisk, aby stanąć do Walki z wilkolakiem");
                Console.ResetColor();

                Console.ReadKey();
                Console.Clear();
                WalkazWilkiem();
            }
        }
        //Rozmowa przed walką
        public void RozmowaWilk()
        {

            Rozmowa();
            Console.WriteLine("Żegnasz się z kowalem i wyruszasz w dalszą drogę.\nNie zdajesz sobie sprawy, że w zakamarkach lasu czyha na Ciebie niebezpieczeństwo...");
            Dialog(imie_lokalizacje, "Edward! Muszę się już żegnać. Nadszedł czas, aby kontynuować moją podróż\n");
            Dialog("Kowal", "Och, to smutne wieści. Jednak rozumiem, że podróżnicy mają swoje własne cele i tęsknią za dalekimi miejscami.\nDo zobaczenia i pamiętaj, że zawsze będziesz mile widziany w moim domu!");
            Dialog(imie_lokalizacje, "Jeszcze raz dziękuje za miłą rozmowę! Do zobaczenia!\n");
            Console.WriteLine("Wyruszyłeś dalej w przygodę.\nPodczas swojej wędrówki w pewnym momęcie poczuł_ś niepokój. \nNagle zauważył_ś świecące oczy oraz ostre jak brzytwa kły, wyłaniające się zza zarośli!");
            Console.WriteLine("Zamarł_ś na chwilę, czując, jak adrenalina przepływa przez Twoje żyły.\nGdy spojrzał_ś w żarzące się oczy wilkołaka, wiedział_ś, że musisz działać szybko.");
            Console.WriteLine("Niestety nie było żadnej drogi ucieczki, więc musiał_ś stanąć do walki z bestią\n");
        }
        //Walka z wilkołakiem
        public void WalkazWilkiem()
        {

            if (Ekwipunek.Contains("włócznia"))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Wciśnij SPACJĘ, aby rzucić włócznią w wilkołaka");
                Console.ResetColor();
                ConsoleKeyInfo klawisz = Console.ReadKey(true);
                if (klawisz.Key == ConsoleKey.Spacebar)
                {
                    double szansaTrafienia = new Random().NextDouble();
                    if (szansaTrafienia <= 2)
                    {
                        Console.Clear();
                        Zycie_w = 0;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Udało Ci się pokonać wilkołaka, w nagrodzę dostajesz 4 punkty życia i wchodzisz poziom wyżej. Gratulacje {0}", imie_lokalizacje);
                        Console.WriteLine("Niestety podczas walki włócznia się złamała.");
                        Zycie += 4;
                        Lvl++;
                        Console.ResetColor();
                        Ekwipunek = Ekwipunek.Replace("włócznia", "");
                        if (String.IsNullOrEmpty(Ekwipunek))
                        {
                            Ekwipunek = "Twój plecak jest pusty";
                        }
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        Statystyki_w(Zycie_w, Dmg_w);
                        WyjsciezGry();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Niestety trafiłeś włócznią w drzewo, jedyne co Ci pozostało to walka ręczna");
                        Console.ResetColor();
                        Ekwipunek = Ekwipunek.Replace("włócznia", "");
                        if (String.IsNullOrEmpty(Ekwipunek))
                        {
                            Ekwipunek = "Twój plecak jest pusty";
                        }
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        WalkazWilkiem();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Niestety trafiłeś włócznią w drzewo, jedyne co Ci pozostało to walka ręczna");
                    Console.ResetColor();
                    Ekwipunek = Ekwipunek.Replace("włócznia", "");
                    if (String.IsNullOrEmpty(Ekwipunek))
                    {
                        Ekwipunek = "Twój plecak jest pusty";
                    }
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    WalkazWilkiem();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Musisz stoczyć walkę z wilkołakiem, klikaj SPACJĘ, by zadawać mu ciosy");
                Console.ResetColor();

                Random random = new Random();
                Dmg_w = random.Next(5, 7);
                while (Zycie_w > 0 && Zycie > 0 && Dmg > 0 && Dmg_w > 0)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        if (Zycie_w > 0 && Zycie > 0 && Dmg > 0 && Dmg_w > 0)
                        {
                            Zycie_w -= Dmg;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("Zadajesz obrażenia wilkołakowi! Pozostałe punkty życia wilkołaka: " + Zycie_w);
                            Console.ResetColor();
                        }

                        if (Zycie_w > 0 && Zycie > 0 && Dmg > 0 && Dmg_w > 0)
                        {
                            Zycie -= Dmg_w;
                            Dmg_w = Dmg_w - 1;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Wilkołak zadał Ci obrażenia! Twoje pozostałe punkty życia: " + Zycie);
                            Console.ResetColor();
                        }

                    }
                    else if (Zycie_w > 0 && Zycie > 0 && Dmg > 0 && Dmg_w > 0)
                    {
                        Zycie -= Dmg_w;
                        Dmg_w = Dmg_w - 1;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nie trafiłeś, ale za to Wilkołak tak. Twoje pozostałe punkty życia: " + Zycie);
                        Console.ResetColor();

                    }

                }
                if (Zycie_w <= 0 && (Zycie > 0 && Dmg >= 0))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Wilkołak stracił wszystkie punkty życia! Dzięki temu wchodzisz na wyższy poziom ");
                    Console.ResetColor();
                    Zycie_w = 0;
                    Lvl++;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Oto statystyki Twoje, {0} oraz Twojego przeciwnika: ", imie_lokalizacje);
                    Console.ResetColor();
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    Statystyki_w(Zycie_w, Dmg_w);
                    Console.WriteLine("W nagrode dostajesz: ");
                    WyjsciezGry();
                }
                else if ((Zycie_w > 0 && Dmg_w >= 0) && Zycie <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wilkołak Cię pokonał! Oto statystyki Twoje, {0} oraz Twojego przeciwnika: ", imie_lokalizacje);
                    Console.ResetColor();
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    Statystyki_w(Zycie_w, Dmg_w);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("GAME OVER");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Wciśnij dowolny przycisk, aby opuścić grę");
                    Console.ResetColor();
                    Console.ReadKey();
                    Environment.Exit(0);

                }
                else if ((Zycie_w >= 0 && Zycie >= 0) || (Dmg <= 0 && Dmg_w <= 0))
                {
                    Lvl--;
                    if (lvl <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nie udało Ci się go pokonać, spadasz na niższy lvl.");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Oto statystyki końcowe Twoje {0} oraz Twojego przeciwnika: ", imie_lokalizacje);
                        Console.ResetColor();
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        Statystyki_w(Zycie_w, Dmg_w);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Niestety twój poziom spadł do 0");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("GAME OVER\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Naciśnij ENTER aby wyjść z gry");
                        Console.ResetColor();
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Nie udało Ci się go pokonać, spadasz na niższy lvl. Ważne, że uszedłeś z życiem! Ruszaj w dalszą podróż!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Oto statystyki Twoje {0} oraz Twojego przeciwnika: ", imie_lokalizacje);
                        Console.ResetColor();
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        Statystyki_w(Zycie_w, Dmg_w);
                        WyjsciezGry();
                    }

                }

            }
        }
        //--------------------------Morze--------------------------
        public void Morze()
        {
            Console.Clear();
            LokalizacjeStos.Push("morze");
            Lokalizacja = LokalizacjeStos.Peek();
            Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
            licznik_lokalizacji += 2;

            Console.Write("                                       ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Morze");
            Console.ResetColor();
            Console.Write("");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Witaj w spokojnym miasteczku przy oceanie!\nWyobraź sobie, że ruszasz w niezwykłą podróż przy kojących dźwiękach oceanu, który skrywa w sobie\ntajemnice i niewyjaśnione zagadki. \nTo nie jest typowy wypad na leżakowanie i relaks - to podróż pełna napięcia, tajemniczości i niebezpieczeństw.\n ");
            Console.WriteLine("Na wejściu do plaży spotykasz rybaka");
            Console.WriteLine("Rybak był wysokim, starszym mężczyzną o pogodnej twarzy. Jego oczy promieniały mądrością i doświadczeniem. \nBujna siwa broda oraz zapuszczone włosy były porozrzucane przez morską bryzę.\nNa plaży poza Wami nie ma nikogo. Decydujesz się podejść i zapytać o powód tego dziwnego zjawiska...");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nWcisnij");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" DOWOLNY");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" przycisk aby podejść do rybaka");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            Rozmowa();
            Dialog(imie_lokalizacje, "Przepraszam, ale zastanawiam się, dlaczego ta plaża jest tak pusta. Czy wiesz, dlaczego tutaj nie ma nikogo?");
            Dialog("Rybak", "Och, witaj! Tak, ta plaża jest rzeczywiście opustoszała z powodu pewnej historii.\n Mówi się, że w tych wodach poluje rekin, spragniony ludzkiej krwi...");
            Dialog(imie_lokalizacje, "Rekin? Naprawdę? To brzmi strasznie. Czy znasz jakieś szczegóły?");
            Dialog("Rybak", "Otóż... Wiele lat temu jeden z rybaków doświadczył przerażającego spotkania z ogromnym rekinem w tych wodach.\n Wielu ludzi od tamtego czasu unika tej plaży z obawy przed kolejnym atakiem.");
            Dialog(imie_lokalizacje, "Czy od tamtego incydentu, który miał miejsce kilka lat temu, doszło do kolejnych ataków bestii?");
            Dialog("Rybak", "Nie, od tamtego czasu nie odnotowano żadnych kolejnych ataków, lecz większość mieszkańców jest świadoma tej\n historii i ryzyka, które się z nią wiąże.\n Czasem jednak słychać plotki o przecinającej wodę płetwie grzbietowej rekina...");
            Dialog(imie_lokalizacje, "Rozumiem obawy miejscowych, jednak chciał_bym zobaczyć to miejsce na własne oczy. \nWydaje mi się fascynujące.");
            Dialog("Rybak", "Twoja ciekawość jest uzasadniona, Podróżniku, ale nie mogę przestać Cię upominać.\n Bezpieczeństwo jest najważniejsze. Jeśli zdecydujesz się udać na plażę, proszę, bądź nadzwyczaj ostrożny.\n Mam nadzieję, że jeszcze się kiedyś spotkamy.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Czy chcesz się rozejrzeć i zobaczyć czemu plaże są puste oraz czy plotki, które słyszałeś są słuszne? \nRuszasz na plażę sprawdzić, czy jest się czego bać?");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Naciśnij");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" ENTER");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(", aby wybrać się na plażę");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Naciśnij");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ESC");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(", aby zrezygnować ze spaceru się na plażę");
            Console.ResetColor();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\nUWAGA!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" Stajesz przed swoim pierwszym wyborem nad morzem! \nPamiętaj, że każda decyzja ma ogromne znaczenie!");
            Console.ResetColor();

            var dobrywybor3 = false;
            while (!dobrywybor3)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    LowcaRekina();
                    dobrywybor3 = true;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Lvl--;
                    if (lvl <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Postanowiłeś zrezygnować z wejścia na plażę z powodu obaw związanych z obecnością rekinów. Twój lęk przed tymi morskimi\ndrapieżnikami był na tyle silny, że postanowił_ś unikać wodnych kąpieli i przechadzek po plaży.");
                        Console.WriteLine("Niestety poddałeś się za szybko. Twój poziom spada o 1.\n");
                        Console.WriteLine("Oto Twoje statystyki końcowe {0}: ", imie_lokalizacje);
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Niestety twój poziom spadł do 0.");
                        Console.WriteLine("GAME OVER");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("\nNaciśnij ENTER aby wyjść z gry");
                        Console.ResetColor();
                        Console.ReadLine();
                        Environment.Exit(0);


                        dobrywybor3 = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Postanowiłeś zrezygnować z wejścia na plażę z powodu obaw związanych z obecnością rekinów. Twój lęk przed tymi morskimi\ndrapieżnikami był na tyle silny, że postanowił_ś unikać wodnych kąpieli i przechadzek po plaży.");
                        Console.WriteLine("Niestety poddałeś się za szybko. Twój poziom spada o 1.\n");
                        Console.WriteLine("Oto Twoje statystyki {0}: ", imie_lokalizacje);
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        dobrywybor3 = true;
                        WyjsciezGry();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dokonałeś złego wyboru! Aby dokonać wyboru kliknij ENTER lub ESC");
                    Console.ResetColor();
                }
            }
        }
        public void LowcaRekina()
        {
            Console.WriteLine("Podczas spaceru po plaży, napotkałeś pewnego mężczyznę, wydawał się być zdenerowany i pełen determinacji.");
            Console.WriteLine("Mężczyzna miał przy sobie trzy wiadra rybich wnętrzności, które miały służyć za przynętę. \nWłaśnie zauważyłeś, że miał wsiadać na swoją małą łódkę. Postanawiasz podejść i porozmawiać");
            Rozmowa();
            Dialog(imie_lokalizacje, "Cześć! Co robisz na plaży? Nie boisz się rekina?");
            Dialog("Mężczyzna", "Hej, przyjacielu! Mam pewną misję tutaj. Planuję rozrzucić przynętę dla rekina i stawić mu czoła.\n Chcę pokonać tę bestię raz na zawsze!");
            Dialog(imie_lokalizacje, "To brzmi naprawdę odważnie. Rozumiem, że jesteś zdeterminowany, ale polowanie na rekina \n to niebezpieczne przedsięwzięcie. Czy naprawdę jesteś pewien, że możesz sobie poradzić w pojedynkę?");
            Dialog("Mężczyzna", "Mój dziadek był rybakiem i spędzał wiele czasu na tych wodach. \nNiestety to właśnie on był ofiarą ataku rekina kilka lat temu.");
            Dialog(imie_lokalizacje, "Przykro mi słyszeć o tym, że to właśnie twój dziadek był ofiarą");
            Dialog("Mężczyzna", "Dziś mija piąta rocznica jego śmierci. To wydarzenie mną wstrząsnęło i zmotywowało do podjęcia działań.");
            Dialog(imie_lokalizacje, "Rozumiem, że twoja determinacja wynika z pragnienia ochrony innych przed tym samym cierpieniem.\n Chciałbym dołączyć do twej misji!");
            Dialog("Mężczyzna", "Naprawdę doceniam Twoją chęć pomocy. To dla mnie ważne, aby mieć wsparcie innych, którzy rozumieją powagę\n sytuacji. Wsiadajmy na łódkę!\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Naciśnij");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" SPACJE");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(", aby wsiąść na łódkę.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Teraz już nie ma odwrotu!.");
            Console.ResetColor();

            int j = 1;
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    Console.Clear();
                    WalkazRekinem();
                    break;
                }
                else if (j == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Musisz wcisnąć SPACJĘ! To nie jest SPACJA! ");
                    Console.ResetColor();
                    j++;
                }
                else if (j == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Musisz wcisnąć SPACJĘ! Robisz sobię żarty?!");
                    Console.ResetColor();
                    j++;
                }
                else if (j == 3)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Musisz wcisnąć SPACJĘ! Twój towarzysz zaczyna się denerwować!");
                    Console.ResetColor();
                    j++;
                }
                else if (j == 4)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Musisz wcisnąć SPACJĘ! To twoja ostatnia szansa!!");
                    Console.ResetColor();
                    j++;
                }
                else if (j == 5)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Niestety czas się skończył a twój kompan popłynoł sam. Prawdopodobnie bez twojej pomocy umrzę.\nMasz go na sumieniu oraz kończysz grę\n");
                    Console.ResetColor();
                    Console.WriteLine("Oto Twoje statystyki końcowe {0}: ", imie_lokalizacje);
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("GAME OVER");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nNaciśnij ENTER aby wyjść z gry");
                    Console.ResetColor();
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }
        //Walka z rekinem
        public void WalkazRekinem()
        {
            Console.Clear();
            Console.WriteLine("Wchodzisz na pokład i widzisz dużą skrzynię, w której jest broń na rekina. \nTwój kompan ze stresu zapomniał do niej hasła. Rozwiąż zagadkę, aby ją otworzyć");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nZAGADKA");
            Console.ResetColor();
            Console.WriteLine(" Co może być gorące lub zimne,\n Jest wykorzystywane przez wielu ludzi.\n Może być mocne lub słabe, \n I potrafi oświetlić ciemność.\n ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Co to jest?\n");
            Console.ResetColor();
            Console.WriteLine(" 1 - Światło\n 2 - Wiatr\n 3 - Ognisko\n 4 - Rower\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Wybierz odpowiedź, wpisując jedną z powyższych odpowiedzi - nie korzystaj z polskich znaków, następnie naciśnij");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" ENTER");
            Console.ResetColor();
            Console.WriteLine();
            //Zagadka
            string zagadka3 = Console.ReadLine().ToLower();
            int licznik3 = 3;
            for (int i = 0; i <= 3; i++)
            {
                if (zagadka3 == "swiatlo")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Super, otworzyłeś skrzynię, w środku znalazłeś harpun! \nPonadto wchodzisz na wyższy poziom!");
                    Lvl++;
                    Console.ResetColor();
                    if (Ekwipunek.Contains("pusty"))
                        Ekwipunek = "harpun";
                    else
                        Ekwipunek += " harpun";
                    Lvl++;
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("\nWcisnij");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" DOWOLNY");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(" przycisk aby podejść do rybaka");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    i = 3;
                }
                else
                {
                    licznik3--;
                    if (licznik3 == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Niestety {0}, Twoje próby się skończyły, musimy Ci odebrać 5 punktów walki", imie_lokalizacje);
                        Dmg -= 5;
                        if (dmg < 0) dmg = 0;
                        Console.WriteLine("Pomimo, że nie otworzyłeś skrzyni, to znalazłeś obok pistolet z małą zawartością naboi w magazynku");
                        if (Ekwipunek.Contains("pusty"))
                            Ekwipunek = "pistolet";
                        else
                            Ekwipunek += " pistolet";
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("\nWcisnij");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" DOWOLNY");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(" przycisk aby kontynuować przygodę!");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("To nie jest dobra odpowiedz! Twoje pozostałe próby: {0}", licznik3);
                    Console.ResetColor();
                    zagadka3 = Convert.ToString(Console.ReadLine());
                }
            }

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Przez kilka dobrych godzin...]");
            Console.ResetColor();
            Console.WriteLine("Razem z tajemniczym mężczyzną niezłomnie płynęliście na łodzi, dążąc jak najdalej w poszukiwaniu rekina.\nZ każdą mijaną minutą, odległość między nimi, a brzegiem coraz bardziej się powiększała.");
            Console.WriteLine("Choć zmęczenie i niewielkie złudzenia zaczynały dawać się we znaki, podróżnik i mężczyzna utrzymywali wysokie\nmorale i nadzieję na znalezienie rekina. \nIch wspólne dążenie ku rozwiązaniu zagadki i ochronie innych osób przed\nniebezpieczeństwem było ich natchnieniem, pomagając im przetrwać trudności płynące z długiej podróży.");
            Rozmowa();
            Dialog("Mężczyzna", "Wypłynęliśmy na odpowiednią odległość od brzegu, gdzie woda jest głębsza. Teraz musimy spróbować rzucić \n przynętę i przyciągnąć uwagę rekina.");
            Dialog(imie_lokalizacje, "Jasne. Bierzmy się do roboty i rozprawmy się z bestią!\n\n ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Wciśnij");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" DOWOLNY");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" przycisk, aby przejść dalej");
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
            int przyneta = 0;
            int rzut;

            for (int i = 0; i <= 10; i++)
            {
                Console.Write("Naciśnij");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" SPACJĘ");
                Console.ResetColor();
                Console.WriteLine(", by wyrzucić trochę rybich wnętrzności w morze\n");

                ConsoleKeyInfo klawisz = Console.ReadKey(true);
                if (klawisz.Key == ConsoleKey.Spacebar)
                {

                    Random random = new Random();
                    przyneta = random.Next(3, 7);
                    if (przyneta < 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nNa horyzoncie nadal nie pojawia się płetwa grzbietowa rekina, rzucaj przynętę dalej");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\nKątem oka zauważyłeś rekina pływającego w pobliżu Twojej łodzi. Rzucaj dalej, może podpłynie bliżej");
                        Console.ResetColor();
                        i += 3;
                        continue;
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nie nacisnąłeś spacji. Spróbuj ponownie");
                    Console.ResetColor();
                    i--;
                }
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();
            if (Ekwipunek.Contains("harpun"))
            {
                Random losowy = new Random();
                rzut = losowy.Next(0, 2);
                if (rzut == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Z pełnym skupieniem i determinacją rzucasz harpun w kierunku rekina, jednak twoje ręce drżą, a strzał nie jest trafny.\nNiestety, harpun nie trafia celu...");
                    Console.WriteLine("Nagle, z przerażającą siłą, rekin błyskawicznie nadpływa w Waszym kierunku, dopadając Was zaskakująco szybko.");
                    Zycie = 0;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nNiestety rekin Was dopadł i na tym etapie kończy się Twoja gra.");
                    Console.ResetColor();
                    Ekwipunek = Ekwipunek.Replace("harpun", "");
                    if (String.IsNullOrWhiteSpace(Ekwipunek))
                    {
                        Ekwipunek = "Twój plecak jest pusty";
                    }
                    Console.WriteLine("\nOto twoje statystyki końcowe: ");
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("GAME OVER");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Naciśnij ENTER aby wyjść z gry");
                    Console.ResetColor();
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Udało Ci się perfekcyjnie rzucić harpunem w kierunku rekina. Harpun trafił celnie.");
                    Console.WriteLine("Ponadto zdołał_ś wyciągnąć z ryby swoją broń. \nHarpun pozostaje w Twoim posiadaniu, jako nagroda za odwagę!");
                    Console.WriteLine("Brawo! Udało Ci się pokonać rekina!");
                    Console.ResetColor();
                    Console.WriteLine("W nagrodę otrzymujesz: 5 punktów życia oraz Twój poziom wzrasta o jeden! Gratulacje {0}", imie_lokalizacje);
                    Ekwipunek = Ekwipunek.Replace("harpun", "");
                    if (String.IsNullOrWhiteSpace(Ekwipunek))
                    {
                        Ekwipunek = "Twój plecak jest pusty";
                    }
                    Zycie += 5;
                    Lvl++;
                    Console.WriteLine("Oto Twoje statystyki: ");
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    WyjsciezGry();
                }
            }
            else if (Ekwipunek.Contains("pistolet"))
            {
                Console.Clear();
                Console.WriteLine("Niestety skończyła Wam się przynęta. Nie użyłeś pistoletu, więc mozesz go zachowac na potem.");
                Console.WriteLine("Oto Twoje statystyki: ");
                Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                WyjsciezGry();
            }
        }
        //--------------------------GÓRY--------------------------
        public void Gory()
        {
            Console.Clear();
            LokalizacjeStos.Push("gory");
            Lokalizacja = LokalizacjeStos.Peek();
            Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
            licznik_lokalizacji += 7;

            Console.Write("                               ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("GÓRY");
            Console.ResetColor();
            Console.Write("");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Podróż w góry jest zawsze pełna ekscytujących i niezapomnianych chwil. Dla niektórych podróżników, \nGóry to miejsce wyzwań, mogą tu sprawdzić swoje umiejętności, pokonać własne granice.\nDla innych góry to również miejsce tajemnic oraz krążących od pokoleń legend.");
            Console.Write("Po wyczerpującej wędrówce, czujesz jak Twoje siły opadają. \nPragniesz napełnić swój żołądek, by zyskać energię do dalszego eksplorowania gór.\n Zauważasz pobliską roślinność, skąpaną soczystymi owocami. \nWiedząc, że niektóre z nich mogą okazać się jadalne, z determinacją zbliżasz się do krzaczków, \ngotowy na zbieranie pożywienia...");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nPrzed Tobą znajdują się trzy różne dary Ziemi, są to:");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" 1 - wilczełyko, \n 2 - borówka czernica, \n 3 - pokrzyk wilcza jagoda");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\nUWAGA!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" Stajesz przed swoim pierwszym wyborem w górach! \nPamiętaj, że każda decyzja ma ogromne znaczenie!");
            Console.ResetColor();

            double wybor4;
            var dobrywybor4 = false;
            while (!dobrywybor4)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();
                if (Char.IsDigit(keyInfo.KeyChar))
                {
                    wybor4 = double.Parse(keyInfo.KeyChar.ToString());
                    if (wybor4 == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Wybrał_ś wilczełyko " + imie_lokalizacje);
                        if (Ekwipunek.Contains("pusty"))
                            Ekwipunek = "wilczełyko ";
                        else
                            Ekwipunek += "wilczełyko, "; //trujące
                        Console.WriteLine("Zawartość Twojego plecaka: " + Ekwipunek);
                        dobrywybor4 = true;
                    }
                    else if (wybor4 == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Wybrał_ś borówkę czernicę " + imie_lokalizacje);
                        if (Ekwipunek.Contains("pusty"))
                            Ekwipunek = "borówka czernica ";
                        else
                            Ekwipunek += "borówka czernica "; //jadalna
                        Console.WriteLine("Zawartość Twojego plecaka: " + Ekwipunek);
                        dobrywybor4 = true;
                    }
                    else if (wybor4 == 3)
                    {
                        Console.Clear();
                        Console.WriteLine("Wybrał_ś wilczą jagodę " + imie_lokalizacje);
                        if (Ekwipunek.Contains("pusty"))
                            Ekwipunek = "wilcza jagoda ";
                        else
                            Ekwipunek += "wilcza jagoda "; //trująca
                        Console.WriteLine("Zawartość Twojego plecaka: " + Ekwipunek);
                        dobrywybor4 = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dokonałeś złego wyboru! Wybierz poprawną liczbę, aby przejść dalej:");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("Postanawiasz zachować swoją zdobycz na później, jeszcze nie czujesz aż tak doskwierającego głodu!");
            Console.WriteLine();
            Console.WriteLine("Wędrując dalej, udało Ci się podsłuchać rozmowy dwóch wędrowców. \nWspominają o legendarnej bestii, siejącej postrach wśród tutejszej ludności. \nIch historie są pełne opisów ogromnego, owłosionego stworzenia, które rzekomo przemierza góry wśród mglistych szczytów.");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("                                       ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Rozmowa");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Dialog("Podróżnik 1", "Nie do wiary, że legendarny Yeti znajduje się tak blisko nas! Od zawsze myślałem, że to tylko bajki\n dla dzieciaków");
            Dialog("Podróżnik 2", "To prawda, to brzmi niesamowicie, ale jednocześnie również niesamowicie przerażająco... Czy słyszałeś coś\n więcej na ten temat?");
            Dialog("Podróżnik 1", "Tak, przysłuchałem się rozmowie starców u podnóża gór. \nOpowiadali o potężnym stworzeniu, przypominającym ogromnego człowieka, pokrytego gęstym futrem. \nTwierdzą, że Yeti przemierza te góry, zwłaszcza w okolicach jaskiń, które służą mu za kryjówkę.");
            Dialog("Podróżnik 2", "Ależ to niesamowite! \nCzy są jakieś dowody na istnienie stwora w tych okolicach?");
            Dialog("Podróżnik 1", "Niestety, nikt nie posiada żadnych fotografii ani fizycznych dowodów. Wszystko wiadomo jedynie z opowieści przekazywanych ustnie. \nJednak starcy wydawali się być przekonani o tym, co widzieli.");

            Console.WriteLine("Nie ukrywasz, że ta rozmowa nieco Cię przeraziła.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Czy mimo to chcesz kontynuować podróż, biorąc pod uwagę ryzyko spotkania potwora?");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray; //tak
            Console.Write("Naciśnij");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" ENTER");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(", aby kontynuować przygodę");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray; //nie
            Console.Write("Naciśnij");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ESC");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(", aby zrezygnować");
            Console.ResetColor();
            Console.WriteLine();
            var dobrywybor5 = false;
            while (!dobrywybor5)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine("Jednak nie tak łatwo Cię przestraszyć! Ruszajmy dalej w poszukiwaniach bestii!");
                    Starzec();
                    dobrywybor5 = true;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Po głębszych rozważaniach, doszedłeś do wniosku, że dla swojego bezpieczeństwa najlepszą decyzją będzie zrezygnowanie z dalszej podróży. \n Tutejsi muszą sobie sami poradzić z potworem...");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Niestety uciekając potknąłeś się i na Twojej nodze pojawiła się rana. Tracisz 3 punkty życia\n");
                    Console.ResetColor();
                    Zycie -= 3;
                    Console.WriteLine("Oto Twoje statystyki {0}: ", imie_lokalizacje);
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    dobrywybor5 = true;
                    WyjsciezGry();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dokonałeś złego wyboru! Aby dokonać poprawnego wyboru kliknij ENTER lub ESC");
                    Console.ResetColor();
                }
            }
        }
        public void Starzec()
        {
            Console.WriteLine();
            Console.WriteLine("Podczas swojej wędrówki niespodziewanie napotykasz starca, który wygląda na wyczerpanego i głodnego. \nJego oczy pełne były błagalnej prośby o pomoc, a spomiędzy spierzchniętych warg niemal wyrywał się krzyk...");
            Console.WriteLine("Siwe włosy opadały mu na ramiona, a broda była tak długa, że sięgała aż do piersi. Wyglądał, jakby był częścią tych gór.");

            Rozmowa();
            Dialog(imie_lokalizacje, "Przepraszam, czy mogę ci jakoś pomóc? Widzę, że jesteś wyczerpany.");
            Dialog("Starzec", "Jak cudownie spotykać ludzi. Błagam, czy mógłbyś podzieilić się ze mną jedzeniem? \nOd kilku dni błądzę po tych górach bez jakeigokolwiek pożywienia. Będę niesamowicie wdzięczny za Twoją pomoc.");

            Console.WriteLine("\nPrzypominasz sobie owoce, które zebrałeś przy wejściu na szlak. \nNadal jednak nie jesteś pewn_, czy te rośliny na pewno nie są szkodliwe dla ludzi.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Czy postanawiasz podjąć ryzyko, ofiarowując starcowi jedyne pożywienie, jakie posiadasz?");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Wpisz");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" TAK");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(", jeśli chcesz nakarmić starca");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Wpisz");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" NIE");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(", aby zachować zapasy dla siebie");
            Console.ResetColor();

            Console.WriteLine();

            string wybor6;
            var dobrywybor6 = false;
            while (!dobrywybor6)
            {
                wybor6 = Console.ReadLine().ToLower();
                Console.WriteLine();
                if (wybor6 == "tak")
                {
                    Console.Clear();
                    Console.WriteLine("Postanowił_ś nakarmić starca " + imie_lokalizacje);
                    if (Ekwipunek.Contains("czernica"))
                    {
                        Console.Clear();
                        Console.WriteLine("Na szczęście zebrana przez Ciebie roślina okazała się jadalna! \nStarzec poczuł się lepiej, nabierając zdrowych rumieńców na twarzy.");
                        Console.WriteLine("Twój poziom wzrasta o jeden oraz dostajesz cztery punkty życia.\nPonadto czujesz satysfakcję z pomocy potrzebującym! Brawo {0}!", imie_lokalizacje);

                        Zycie += 4;
                        Lvl++;
                        Ekwipunek = Ekwipunek.Replace("borówka czernica ", "");
                        if (String.IsNullOrWhiteSpace(Ekwipunek))
                        {
                            Ekwipunek = "Twój plecak jest pusty";
                        }
                        Console.WriteLine("Starzec jest Ci bardzo wdzięczny i w zamian dostajesz od niego starą harmonijkę, która podobno może uspokoić nawet najstraszniejszą bestię...");
                        if (Ekwipunek.Contains("pusty"))
                            Ekwipunek = "harmonijka";
                        else
                            Ekwipunek += "harmonijka";
                        Console.WriteLine("\n" +
                            "Zawartość Twojego plecaka: " + Ekwipunek);
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("\nWcisnij");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" DOWOLNY");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(" przycisk aby kontynuować przygodę");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        dobrywybor6 = true;
                        WalkazYeti();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Zebrana przez Ciebie roślina okazała się trująca! Starzec nie wini Cię za to, jednak mogłeś bardziej uważać! " + imie_lokalizacje);
                        Console.WriteLine("Przez to, że trzymałeś tą roślinę w ręku zaczęła Ci ona drętwieć co utrudni ewentualną walkę. Tracisz 2 punkty żcia.", imie_lokalizacje);

                        Zycie -= 2;
                        if (Ekwipunek.Contains("wilczełyko"))
                        {
                            Ekwipunek = Ekwipunek.Replace("wilczełyko ", "");
                            if (String.IsNullOrWhiteSpace(Ekwipunek))
                            {
                                Ekwipunek = "Twój plecak jest pusty";
                            }
                        }
                        if (Ekwipunek.Contains("jagoda"))
                        {
                            Ekwipunek = Ekwipunek.Replace("wilcza jagoda ", "");
                            if (String.IsNullOrWhiteSpace(Ekwipunek))
                            {
                                Ekwipunek = "Twój plecak jest pusty";
                            }
                        }
                        Console.WriteLine("\nZawartość Twojego plecaka: " + Ekwipunek);
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("\nWcisnij");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" DOWOLNY");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(" przycisk aby kontynuować przygodę");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        dobrywybor6 = true;
                        WalkazYeti();
                    }
                }
                else if (wybor6 == "nie")
                {
                    Console.Clear();
                    Console.WriteLine("Kierując się egoizmem lub nadmierną ostrożnością, postanowił_ś nie dzielić się ze starcem swoimi zdobyczami" + imie_lokalizacje);
                    Console.WriteLine("Zawartość Twojego plecaka: " + Ekwipunek);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("\nWcisnij");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" DOWOLNY");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(" przycisk aby kontynuować przygodę");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    dobrywybor6 = true;
                    WalkazYeti();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dokonałeś złego wyboru! Wpisz TAK lub NIE, by dokonać wyboru");
                    Console.ResetColor();
                }
            }
        }
        public void WalkazYeti()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n[Po kilku długich godzinach wędrówki...]");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Twoje mięśnie były napięte, a serce biło mocniej pod wpływem wysiłku. Wiedziałeś, że jeszcze przed Tobą znajdują się trudne szlaki, ale niezłomna determinacja napędzała Cię do dalszej wędrówki\n");
            Console.WriteLine("Podczas wspinaczki Twoje spojrzenie padło na tajemniczą jaskinię, która wznosiła się przed Tobą w majestatycznym blasku. \nJej ciemne otwory i skaliste kształty wydawały się zapraszać do wnętrza, skrywając niezliczone sekrety...");
            Console.WriteLine("Zatrzymał_ś się na chwilę, przyciągnięt_ tajemniczością jaskini, jednocześnie rozważając potencjalne niebezpieczeństwo. \nYeti wciąż tkwił w Twoich myślach. Uczucie ciekawości, wraz z chęcią eksploracji przejmowały nad Tobą kontrolę!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nCzy ośmielasz się wejść do środka jaskini?");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Naciśnij");
            Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" ENTER");
            Console.ResetColor();
            Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(", jeśli chcesz wejść do jaskini lub");
            Console.ResetColor();
            Console.ResetColor(); Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" DOWOLNY PRZYCISK");
            Console.ResetColor();
            Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(", jeśli rezygnujesz...");
            Console.ResetColor();

            ConsoleKeyInfo przycisk = Console.ReadKey(true);
            if (przycisk.Key == ConsoleKey.Enter)
            {
                if (Ekwipunek.Contains("harmonijka"))
                {
                    Console.Clear();
                    Console.WriteLine("Kiedy przekroczyłeś próg jaskini, Twoje oczy napotkały zaskakujący widok.\nW głębi stał Yeti – olbrzymi stwór. \nTwoje serce mocno zabiło w piersiach, a adrenalina przepływała przez Twoje żyły.");
                    Console.WriteLine("Nagle przypomniałeś sobie, że w Twoim plecaku znajduje się harmonijka – mały podarek od wcześniej napotkanego starca. \n Bez wahania sięgnąłeś po nią oraz zacząłeś grać delikatną melodię, która odbijała się echem po ścianach jaskini.");
                    Console.WriteLine("Zaskoczony Yeti zatrzymał się, patrząc na Ciebie z ciekawością. Jego groźny wyraz twarzy stonował się, a jego wzrok\n nabierał łagodności.");
                    Console.WriteLine("Stopniowo, na skutek hipnotyzujących dźwięków harmonijki, Yeti zaczął się uspokajać. Jego gigantyczne oczy stawały się\n ciężkie, a cuchnący oddech cichnął, zamieniając się w delikatnie pochrapywanie.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Wciśnij");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" DOWOLNY");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(" przycisk, aby wymknąć się z jaskini");
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Po cichu wychodzisz z jaskini, przemykając obok bestii. \n Nie wiesz, kiedy Yeti wybudzi się ze snu, ale masz nadzieję, że nieprędko. \nUdało Ci się pokonać potwora bez walki, samym sprytem!");
                    Console.ResetColor();
                    Zycie_y = 0;
                    Ekwipunek = Ekwipunek.Replace("harmonijka", "");
                    if (String.IsNullOrWhiteSpace(Ekwipunek))
                    {
                        Ekwipunek = "Twój plecak jest pusty";
                    }
                    Statystyki_y(Zycie_y, Dmg_y);
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    WyjsciezGry();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Kiedy przekroczyłeś próg jaskini, Twoje oczy napotkały zaskakujący widok.\nW głębi stał Yeti – olbrzymi stwór. \nTwoje serce mocno zabiło w piersiach, a adrenalina przepływała przez Twoje żyły.");
                    Console.WriteLine("Nie posiadasz żadnej potencjalnej broni, jednak u wejścia jaskini zauważasz stos kamieni. \n Musisz bronić się jedynym, co Ci pozostało. Do walki!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Liczba kamieni jest równa liczbie Twoich punktów walki. Spróbuj pokonać bestię!");
                    Console.ResetColor();
                    int silarzutu = 0;
                    int kamienie = Dmg;
                    for (int i = kamienie; i > 0 && Zycie_y > 0; i--) //randomowo odejmuje liczbe pkt zycia yetiemu, powiedzmy, ze zalezy od sily rzutu
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("Naciśnij");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" SPACJĘ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(", aby rzucić kamieniem");
                        Console.ResetColor();
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Spacebar)
                        {
                            Random random = new Random();
                            silarzutu = random.Next(2, 7);
                            Zycie_y = Zycie_y - silarzutu;

                            if (silarzutu < 4)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Zycie_y = (Zycie_y < 0) ? 0 : Zycie_y;
                                Console.WriteLine("\nTo był słaby rzut, postaraj się bardziej. Siła rzutu: {0} Punkty życia Yetiego: {1}", silarzutu, Zycie_y); //by nie wyświetlała się informacja, że życie yetiego jest ujemne tylko równe 0
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Zycie_y = (Zycie_y < 0) ? 0 : Zycie_y;
                                Console.WriteLine("\nŚwietny rzut, oby tak dalej! Siła rzutu: {0} Punkty życia Yetiego: {1}", silarzutu, Zycie_y);
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nNie trafiłeś. Spróbuj ponownie...");
                            Console.ResetColor();
                            // odtwarzanie iteracji pętli, by gracz mógł naciśnąć ponownie spację

                        }
                        kamienie--;
                    }
                    if (Zycie_y > 0 && kamienie == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Niestety nie dałeś rady pokonać yetiego - giniesz");
                        Console.ResetColor();
                        Zycie = 0;
                        Statystyki_y(Zycie_y, Dmg_y);
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("GAME OVER");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("\nNaciśnij ENTER aby wyjść z gry");
                        Console.ResetColor();
                        Console.ReadLine();
                        Environment.Exit(0);

                    }
                    else if (Zycie_y == 0 && kamienie == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Okazałeś się godnym przeciwnikiem dla Yeti'ego! Niestety nie wystarczająco dobrym," +
                            " by z nim wygrać... \n Twoja broń się skończyła. Postanawiasz wziąć nogi za pas i uciekasz," +
                            " korzystając z rozkojarzonego potwora, który został przed chwilą trafiony kamieniem.");


                        Statystyki_y(Zycie_y, Dmg_y);
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        WyjsciezGry();
                    }
                    else if (Zycie_y == 0 && kamienie > 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\"Udało się! Pokonałeś zmorę mieszkańców gór, odchodzisz z miasteczka w chwale!" +
                            " W nagrodzę dostajesz piętnaście punktów walki oraz wchodzisz na wyższy poziom!\"");
                        Console.ResetColor();
                        Statystyki_y(Zycie_y, Dmg_y);
                        Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                        WyjsciezGry();

                    }
                }
            }
            else
            {
                Lvl--;
                if (lvl <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Niestety nie ma innej drogi niż jaskinia. Musisz zawrócić, szukając przygód w innym miejscu...\nPozostawiasz lokalnych w niebezpieczeństwie z stale czyhającym na nich potworem.");
                    Console.WriteLine("Twój poziom spada o jeden");
                    Console.WriteLine("Ponadto padasz ofiarą rabusiów, którzy przetrząsają Twój plecak oraz kieszenie, zabierając wszystko, co tylko mogli!");

                    Ekwipunek = "Twój plecak jest pusty!";


                    Ekwipunek = "Twój plecak jest pusty!";

                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.ResetColor();
                    Console.WriteLine("Oto twoje statystyki końcowe: ");
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Niestety twój poziom spadł do 0");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("GAME OVER\n");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Naciśnij ENTER aby wyjść z gry");
                    Console.ResetColor();
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Niestety nie ma innej drogi niż jaskinia. Musisz zawrócić, szukając przygód w innym miejscu... \nPozostawiasz lokalnych w niebezpieczeństwie z stale czyhającym na nich potworem.");
                    Console.WriteLine("Twój poziom spada o jeden");
                    Console.WriteLine("Ponadto padasz ofiarą rabusiów, którzy przetrząsają Twój plecak oraz kieszenie, zabierając wszystko, co tylko mogli!");
                    Console.ResetColor();
                    Ekwipunek = "Twój plecak jest pusty!";


                    Ekwipunek = "Twój plecak jest pusty!";
                    Statystyki(Lvl, Zycie, Dmg, Lokalizacja, Ekwipunek);
                    WyjsciezGry();
                }
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                Console.BackgroundColor = ConsoleColor.Red;

                Console.WriteLine("PRZYGODY W SENNEJ KOTLINIE");
                Console.ResetColor();
                Console.WriteLine("Witam w Sennej Kotlinie");
                Console.WriteLine("Dzisiaj stoczysz walkę dobra ze złem, próbując uratować mieszkańców Sennej Kotliny, \nna których czyhają przerażające potwory. \nPamiętaj, że każdy wybór ma swoje konsekwencje, a czasu nie można cofnąć. \nSłuchaj swojej intuicji, ale również nie zapominaj o logicznym myśleniu. \nPoza tym baw się dobrze i nie daj się pożreć!");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nNa sam początek podaj nam swój nick,\ndzięki temu będziemy wiedzieć, jak się do Ciebie zwracać przez całą grę!");
                Console.ResetColor();

                Rozgrywka ustawienia = new Rozgrywka();
                Lokalizacje lokalizacje = new Lokalizacje();

                ustawienia.pobierzImieDelegate += (imie) =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Ustawiłeś swój nick na: " + imie);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Cześć {0}!", imie);
                    Console.ResetColor();
                    lokalizacje.ObslugaPobierzImie(imie);
                };
                ustawienia.pobierzImieDelegate += lokalizacje.ObslugaPobierzImie;
                ustawienia.PobierzImie();

                Console.ReadKey();
            }
        }
    }
}

