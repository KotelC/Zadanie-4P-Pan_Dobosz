namespace TicTacToe
{
    internal class Program
    {
        //Pokazuje na planszy  które numery są wolne . Jeżeli zostaną zajętę wyświetlają X badź O w zależności od gracza który wykonał ruch na dane pole
        private static void ustawienie_planszy(string[,] tablica, int a, int b, int r = 0)
        {
            string p;
            for (int i = a; i < b; i++)
            {
                p = i.ToString();
                if (tablica[r, i - a] != null)
                    p = tablica[r, i - a];
                Console.Write("|  {" + p + "}  ");
            }
            Console.WriteLine("|");
        }

        //Wyświetlanie planszy
        private static void plansza(string[,] tablica)
        {
            Console.WriteLine("+-------+-------+-------+");
            Console.WriteLine("|       |       |       |");
            ustawienie_planszy(tablica, 1, 4, 0);
            Console.WriteLine("|       |       |       |");
            Console.WriteLine("+-------+-------+-------+");
            Console.WriteLine("|       |       |       |");
            ustawienie_planszy(tablica, 4, 7, 1);
            Console.WriteLine("|       |       |       |");
            Console.WriteLine("+-------+-------+-------+");
            Console.WriteLine("|       |       |       |");
            ustawienie_planszy(tablica, 7, 10, 2);
            Console.WriteLine("|       |       |       |");
            Console.WriteLine("+-------+-------+-------+");
        }

        //Wprowadzenie ruchu na planszę
        private static void ruch_gracza(ref string[,] tablica)
        {
            while (true)
            {
                //Pobieramy pole na które został wykonany ruch przez gracza
                int r = int.Parse(Console.ReadLine());

                //Otrzymujemy numer pola w rzędzię poprzez  obliczenie (r - 1) / 3
                //Otzrymujemy numer pola w kolumnie poprzez obliczenie (r - 1) mod 3 
                if (tablica[(r - 1) / 3, (r - 1) % 3] == null)
                {
                    tablica[(r - 1) / 3, (r - 1) % 3] = "O";
                    break;
                }

                //Jeżeli wybramy pole już zajęte zostajemy zmuszeni do wykonania ruchu ponownie
                Console.WriteLine("Podałeś zajęte już pole, spróbuj ponownie");
            }
        }

        //Tworzenie listy  w zakeresie  (int, int) 
        private static List<(int, int)> miejsca(string[,] tablica)
        {
            //Wyświetlanie listy (f) wolnych pól na planszy
            List<(int, int)> f = new List<(int, int)>();

            //Pętla szukająca wolnych pól na planszy (Są to pola które nie posiadają  'O' bądź'X'
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tablica[i, j] == null) //Jeżeli wyszukane pole jest wolne dodajemy je do listy f
                        f.Add((i, j));
            return f;
        }

        private static bool Wygrana(string[,] tablica, string znak = "X")
        {
            //Sprawdzanie czy są trzy znaky tego samego gracza w rzędzie
            List<string> rzad;
            //Sprawdzanie czy są trzy znaky tego samego gracza w kolumnie
            List<string> kolumna;

            //Sprawdzanie czy są 3 znaki powtarzające się po przekątnej  jeżeli są one takiego samego znaku to ten znak wygrywa
            if (tablica[0, 0] == znak && tablica[1, 1] == znak && tablica[2, 2] == znak ||
                tablica[0, 2] == znak && tablica[1, 1] == znak && tablica[2, 0] == znak)
            {
                Console.WriteLine("ZWYCIESTWO " + znak);
                return true;
            }

            //Sprawdzanie rzędów oraz kolumn
            for (int i = 0; i < 3; i++)
            {
                rzad = new List<string>();
                kolumna = new List<string>();

                // Jeżeli znajdziemy znak O bądź X dodajemy go do listy zajętych pól
                for (int j = 0; j < 3; j++)
                {
                    if (tablica[i, j] == znak)
                        rzad.Add(znak); //Sprawdzanie rzędów
                    if (tablica[j, i] == znak)
                        kolumna.Add(znak); //Sprawdzanie Kolumn
                }

                //Jeżeli są trzy znaki w tej samej kolumnie bądź rzędzie  to ten znak wygrywa
                if (rzad.Count == 3 || kolumna.Count == 3)
                {
                    Console.WriteLine("ZWYCIESTWO " + znak);
                    return true;
                }
            }
            return false;
        }

        private static void ruch_komputer(ref string[,] board)
        {
            Random random = new Random();
            //Otrzymujemy miejsca wolnych pól
            var f = miejsca(board);

            //Komputer wybiera środek mapy jako pierwszy ruch całej gry

            if (f.Contains((1, 1)))
                board[1, 1] = "X";
            else
            {
                //Komputer wybiera losowe dostępne miejsce aby wykonać swój ruch . int p losuję liczbę która jest wolna na tablicy f
                int p = random.Next(0, f.Count);
                board[f[p].Item1, f[p].Item2] = "X";
            }
        }

        static void Main(string[] args)
        {
            string[,] br = new string[3, 3];

            //Główna pętla gry: Ruch komputera -> Wyświetlenie tablicy-> Sprawdzenie czy komputer wygrał -> Ruch gracza -> Wyświetlenie tablicy -> Sprawdzenie czy gracz wygrał ->  powtórzenie
            while (true)
            {
                ruch_komputer(ref br);
                plansza(br);
                if (Wygrana(br))
                    break;
                ruch_gracza(ref br);
                plansza(br);
                if (Wygrana(br, "O"))
                    break;
            }
        }
    }
}