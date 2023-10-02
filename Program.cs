using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace project
{
    public class Program
    {
        public static string userName { get; private set; }// programa ne norejo niekaip korektiskai veikti tai pasiule padaryti toki varianta

        public static bool Login(string userName, ref Dictionary<string, int> userScore)// metodas kuris patikrina ar toks user yra jei ne tada pridieda
        {
            if (!userScore.ContainsKey(userName))// patikrina useri
            {
                userScore.Add(userName, 0);//prideda useri ir priskeria 0 tasku
            }
            Console.WriteLine(userName + " Jus sėkmingai prisijungėte.");
            return true;//grazina i sistema kad juseris prijunges
        }
        public static void ShowParticipants(Dictionary<string, int> userScore)
        {
            Console.Clear();
            Console.WriteLine("Dalyviai:");
            foreach (var participant in userScore)
            {
                Console.WriteLine($"{participant.Key} - {participant.Value} taškai");
            }
            Console.WriteLine("\nSpauskite Enter, kad grįžtumėte į meniu.");
            Console.ReadLine();
        }
        public static void ShowResults(Dictionary<string, int> userScore)
        {
            Console.Clear();
            Console.WriteLine("Rezultatai:");

            // Rikiuojame vartotojus pagal taškų skaičių mažėjimo tvarka
            var sortedUsers = userScore.OrderByDescending(pair => pair.Value).ToList();

            Console.WriteLine("TOP 10 dalyvių:");

            for (int i = 0; i < Math.Min(10, sortedUsers.Count); i++)
            {
                string username = sortedUsers[i].Key;
                int points = sortedUsers[i].Value;

                // Pridedame žvaigždutę prie TOP 3 vartotojų
                string star = i < 3 ? "*" : "";

                Console.WriteLine($"{i + 1}. {username} - {points} taškai {star}");
            }

            Console.WriteLine("\nSpauskite Enter, kad grįžtumėte į meniu.");
            Console.ReadLine();
        }
        public static void Game(Dictionary<string, int> userScore) // zaidimo logika
        {
            Console.Clear();
            Random random = new Random();
            Dictionary<int, string> questions = new Dictionary<int, string>() //klausimai
            {
                {1, "Kuriais metais krikštatėvis buvo išleistas pirmą kartą?"},
                {2, "Kuris aktorius pelnė geriausią aktoriaus Oskarą už filmus „Filadelfija“ (1993) ir „Forrest Gump“ (1994)?" },
                {3, "Kuris aktorius pateikė balsą personažui Nemo 2003 m. Filme „Rasti Nemo“?" },
                {4, "Kaip vadinasi 2015 m. Filmas apie pasienietį 1820-ųjų prekybos kailiais \n ekspedicijoje ir jo kovą už išlikimą, kai mane nugvelbė meška?" },
                {5, "Kaip buvo vadinamas pirmasis Adele įrašas?" },
                {6, "Kokia 1960-ųjų amerikiečių pop grupė sukūrė „bangos garsą“?" },
                {7, "Monetų meistras lordas Petyras Baelišas taip pat buvo žinomas kokiu vardu?" },
                {8, "Kaip vadinamas pirmasis epizodas?(Sostu karai)" },
                {9, "Kas buvo atsakingas už Nakties karaliaus sukūrimą?(Sostu kakrai)" },
                {10, "Koks yra sidabro cheminis simbolis?" },
                {11, "Koks buvo pirmasis Bondo filmas, 1962-aisiais pasiekęs ekranus, kai Seanas Connery vaidino 007?" },
                {12, "Kiek Bondo filmų pasirodė Rogeris Moore'as kaip 007?" },
                {13, "Kuris Bondo filmas buvo išleistas 2006 m." }
            };
            Dictionary<int, string> correctAnswer = new Dictionary<int, string>() // tesingi atsakimai
            {
                {1, "1972" },
                {2, "Tom Hanks" },
                {3,"Aleksandras Gouldas" },
                {4,"Wielebny" },
                {5,"Gimtojo miesto šlovė" },
                {6, "Beach Boys" },
                {7, "Mažasis pirštas" },
                {8, "Žiema ateina" },
                {9,"Miško vaikai" },
                {10, "Ag" },
                {11, "Dr" },
                {12,"Septyni" },
                {13,"Casino Royale" }
            };
            Dictionary<int, string> Answers = new Dictionary<int, string>() // atskimu variacija
            {
                {1, "1960, 1976, 1980, 1972" },
                {2, "Robbie Williams, Robert De Niro, Tom Hanks, Robbie Lawler"},
                {3,"Tomas Paciuza, Algirdas Rimkus, Aleksandras Gouldas, Gedeminas Skirma" },
                {4, "Yesterday, Wielebny, Tomorow, Next Day" },
                {5,"Gimtojo miesto šlovė, Gimtasis krastas, Mano NAMAI, Tevyne" },
                {6,"Beach Boys, Linkin Park, Brooklyn Boys, Buffalo Springfield" },
                {7, "Mažasis pirštas, Pasalunas, Ziurke, Bailys" },
                {8, "Pirmasis sniegas, Žiema ateina, Speigas, Puga"},
                {9, "Miško vaikai, Drakonai, Zmones, Dievas"},
                {10, "Au, Ag, Zn, Pb"},
                {11, "Dr, Td, Kd, Ab" },
                {12, "Vienas, Penki, Septyni, Keturi" },
                {13, "Golden eye, Casino Royale, Skyfall, Spectre" }
            };
            Dictionary<int, int> points = new Dictionary<int, int>()
            {
                {1, 300},
                {2, 300},
                {3, 200},
                {4,100},
                {5,100},
                {6,200},
                {7,250},
                {8,100},
                {9, 200},
                {10,100},
                {11,300},
                {12,250},
                {13,100}
            };

            bool continuePlaying = true;

            do
            {
                Console.Clear();
                int randomKey = random.Next(1, 13);
                Console.WriteLine(questions[randomKey]);
                Console.WriteLine(Answers[randomKey]);
                var input = Console.ReadLine();

                if (input.ToLower() == "q")
                {
                    continuePlaying = false;
                    Console.WriteLine(userName + " Jūs baigėte žaidimą. Viso gero \n Tam, kad pradėti žaisti " +
                        " jums teks vėl įrašyti Vardą ir Pavardę. ");
                    break;
                }

                if (correctAnswer[randomKey] == input)
                {
                    Console.WriteLine("Sveikinu, jūs atsakėte teisingai!!!");
                    if (userScore.ContainsKey(userName))
                    {
                        userScore[userName] += points[randomKey];
                        foreach (var point in userScore)
                        {
                            Console.WriteLine($"{point.Key} {point.Value}  tasku");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Jūs atsakėte neteisingai.");
                    break;
                }

                Console.WriteLine("Ar norite tęsti žaidimą? (Įveskite 'q' jei norite baigti)");
                var continueInput = Console.ReadLine();
                if (continueInput.ToLower() == "q")
                {
                    continuePlaying = false;
                    Console.WriteLine(userName + " Jūs baigėte žaidimą. Viso gero \n Tam, kad pradėti žaisti " +
                        " jums teks vėl įrašyti Vardą ir Pavardę. ");
                }
            } while (continuePlaying);
        }
        public static void GameTwo(Dictionary<string, int> userScore) // zaidimo logika
        {
            Console.Clear();
            Random random = new Random();
            Dictionary<int, string> questions = new Dictionary<int, string>() //klausimai
            {
                {1, "Kuriais metais krikštatėvis buvo išleistas pirmą kartą?"},
                {2, "Kuris aktorius pelnė geriausią aktoriaus Oskarą už filmus „Filadelfija“ (1993) ir „Forrest Gump“ (1994)?" },
                {3, "Kiek savarankiškų komizionų padarė Alfredas Hitchcockas savo filmuose 1927–1976 metais?" },
                {4, "Kuris 1982 m. Filmas buvo labai gerbėjų sutiktas dėl meilės tarp jauno, tėvo neturinčio priemiesčio berniuko ir pasiklydusio," +
                " geranoriško bei namuose gyvenančio svečio iš kitos planetos vaizdavimo?" },
                {5, "Kuri aktorė vaidino Mary Poppins 1964 m. Filme „Mary Poppins“?" },
                {6, "Kuriame 1963 m. Klasikiniame filme pasirodė Charlesas Bronsonas?" },
                {7, "Kuriame 1995 m. Filme Sandra Bullock vaidino personažą Angelą Bennett - imtynes ​​Ernestą Hemingway, „Tinklą“ ar „28 dienas“?" },
                {8, "Kuri Naujosios Zelandijos režisierė režisavo šiuos filmus - „Iškirpime“ (2003), „Vandens dienoraštis“ (2006) ir „Ryški žvaigždė“ (2009)?" },
                {9, "Kuris aktorius pateikė balsą personažui Nemo 2003 m. Filme „Rasti Nemo“?" },
                {10, "Kuris kalinys, pavadintas „smurtingiausiu kaliniu Didžiojoje Britanijoje“, buvo 2009 m. Filmo tema?" },
                {11, "Kuris 2008 m. Filmas, kuriame vaidina Chritianas Bale'as, turi šią citatą: „Aš tikiu, kad tai, kas tavęs neužmuš, o tiesiog padaro tave ... keistuoliu“." },
                {12, "Amerikiečių aktorė, vaidinusi Tokijo požemio boso O-Ren Ishii vaidmenį filme „KillBill I I & II“" },
                {13, "Kuriame filme Hugh Jackmanas išgarsėjo kaip konkurentas burtininkas iš personažo, kurį vaidina Christianas Bale'as?" }
            };
            Dictionary<int, string> correctAnswer = new Dictionary<int, string>() // tesingi atsakimai
            {
                {1, "1972" },
                {2, "Tom Hanks" },
                {3,"37" },
                {4,"IR Nežemiškas" },
                {5,"Julie Andrews" },
                {6, "Great Escape" },
                {7, "Grynasis" },
                {8, "Jane Campion" },
                {9,"Aleksandras Gouldas" },
                {10, "Charlesas Bronsonas" },
                {11, "Tamsos riteris" },
                {12,"Lucy Liu" },
                {13,"Prestige" }
            };
            Dictionary<int, string> Answers = new Dictionary<int, string>() // atskimu variacija
            {
                {1, "1960, 1976, 1980, 1972" },
                {2, "Robbie Williams, Robert De Niro, Tom Hanks, Robbie Lawler"},
                {3,"37, 33, 39, 30" },
                {4, "IR Nežemiškas, Nezemiskas, Is Toli, Svetimas" },
                {5,"Julie Andrews, Julie Julie, Julia Roberts, Julie Bergan" },
                {6,"Great Escape, Escape from Tarkov, Escape Game, Escape Room" },
                {7, "Grynasis, Grynasis Pelnas, Greitis, Gamtos jėgos" },
                {8, "Jane Campion, Jane Fonda, Jane Birkin, Jane Austen"},
                {9, "Tomas Paciuza, Algirdas Rimkus, Aleksandras Gouldas, Gedeminas Skirma"},
                {10, "Charlesas Bronsonas, Martin Sheen, Emilio Estevez, Ramon Estevez"},
                {11, "Tamsos riteris, Tamsos Baikeris, Tamsos Mokykla, Tamsos Vaikis" },
                {12, "Lucy Liu, Lucy Hale, Lucy Boynton, Lucy Hawking" },
                {13, "Prestige, Prestige Garden, Prestige Mode, Prestige Point" }
            };
            Dictionary<int, int> points = new Dictionary<int, int>()
            {
                {1,300},
                {2,300},
                {3,200},
                {4,100},
                {5,100},
                {6,200},
                {7,250},
                {8,100},
                {9,200},
                {10,100},
                {11,300},
                {12,250},
                {13,100}
            };

            bool continuePlaying = true;

            do
            {
                Console.Clear();
                int randomKey = random.Next(1, 13);
                Console.WriteLine(questions[randomKey]);
                Console.WriteLine(Answers[randomKey]);
                var input = Console.ReadLine();

                if (input.ToLower() == "q")
                {
                    continuePlaying = false;
                    Console.WriteLine(userName + " Jūs baigėte žaidimą. Viso gero \n Tam, kad pradėti žaisti " +
                        " jums teks vėl įrašyti Vardą ir Pavardę. ");
                    break;
                }

                if (correctAnswer[randomKey] == input)
                {
                    Console.WriteLine("Sveikinu, jūs atsakėte teisingai!!!");
                    if (userScore.ContainsKey(userName))
                    {
                        userScore[userName] += points[randomKey];
                        foreach (var point in userScore)
                        {
                            Console.WriteLine($"{point.Key} {point.Value}  tasku");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Jūs atsakėte neteisingai.");
                    break;
                }

                Console.WriteLine("Ar norite tęsti žaidimą? (Įveskite 'q' jei norite baigti)");
                var continueInput = Console.ReadLine();
                if (continueInput.ToLower() == "q")
                {
                    continuePlaying = false;
                    Console.WriteLine(userName + " Jūs baigėte žaidimą. Viso gero \n Tam, kad pradėti žaisti " +
                        " jums teks vėl įrašyti Vardą ir Pavardę. ");
                }
            } while (continuePlaying);
        }
        public static void GameThree(Dictionary<string, int> userScore) // zaidimo logika
        {
            Console.Clear();
            Random random = new Random();
            Dictionary<int, string> questions = new Dictionary<int, string>() //klausimai
            {
                {1, "Kokia 1960-ųjų amerikiečių pop grupė sukūrė „bangos garsą“?"},
                {2, "Kokiais metais „The Beatles“ pirmą kartą išvyko į JAV?" },
                {3, "Kas buvo pagrindinė dainininkė kartu su aštuntojo dešimtmečio pop grupe „Slade“?" },
                {4, "Kaip buvo vadinamas pirmasis Adele įrašas?" },
                {5, "Future Nostalgia“ su singlu „Don't Start Now“ yra antrasis studijinis albumas, iš kurio anglų dainininkės?" },
                {6, "Koks yra grupės, kurioje yra šie nariai: Johnas Diakonas, Brianas May, Freddie Mercury, Rogeris Tayloras, vardas?" },
                {7, "Kuris dainininkas, be kita ko, buvo žinomas kaip „Popo karalius“ ir „Pirštinė“?" },
                {8, "Kuriai amerikiečių popmuzikos žvaigždei buvo sėkmingi 2015 m. Singlai „Sorry“ ir „Love Yourself“?" },
                {9, "Kurios alternatyvios roko grupės 1991 m. Hitas buvo „prarasti savo religiją“?" },
                {10, "Kokios dainos tekstas yra toks: „Staiga nesu pusė žmogaus, kuris buvau anksčiau. / Ant manęs kabo šešėlis. “?" }
            };
            Dictionary<int, string> correctAnswer = new Dictionary<int, string>() // tesingi atsakimai
            {
                {1, "Beach Boys" },
                {2, "1964" },
                {3,"Noddy" },
                {4,"Gimtojo miesto šlovė" },
                {5,"Dua Lipa" },
                {6, "Karalienė" },
                {7, "Michaelas Jacksonas" },
                {8, "Justinas Bieberis" },
                {9,"REM" },
                {10, "vakar" }
            };
            Dictionary<int, string> Answers = new Dictionary<int, string>() // atskimu variacija
            {
                {1, "Beach Boys, Linkin Park, Brooklyn Boys, Buffalo Springfield" },
                {2, "1960, 1976, 1964, 1972"},
                {3, "Noddy, Ruana Shawl, Nicole Kidman, Katie Holmes" },
                {4, "Gimtojo miesto šlovė, Gimtasis krastas, Mano NAMAI, Tevyne" },
                {5, "Dua Lipa, Rihanna, Beyonce, Eddel" },
                {6, "Karalienė, Princas, Karalius, Lordas" },
                {7, "Michaelas Jacksonas, Freddie Mercury, Johnny Bravo, Dexter Cartoon" },
                {8, "Justinas Bieberis, Eminem, Enigma, Scooter"},
                {9, "REM, TEM, TNT, D&B"},
                {10, "vakar, rytoj, siandien, poryt"}
            };
            Dictionary<int, int> points = new Dictionary<int, int>()
            {
                {1,300},
                {2,300},
                {3,200},
                {4,100},
                {5,100},
                {6,200},
                {7,250},
                {8,100},
                {9,200},
                {10,100},
            };

            bool continuePlaying = true;

            do
            {
                Console.Clear();
                int randomKey = random.Next(1, 10);
                Console.WriteLine(questions[randomKey]);
                Console.WriteLine(Answers[randomKey]);
                var input = Console.ReadLine();

                if (input.ToLower() == "q")
                {
                    continuePlaying = false;
                    Console.WriteLine(userName + " Jūs baigėte žaidimą. Viso gero \n Tam, kad pradėti žaisti " +
                        " jums teks vėl įrašyti Vardą ir Pavardę. ");
                    break;
                }

                if (correctAnswer[randomKey] == input)
                {
                    Console.WriteLine("Sveikinu, jūs atsakėte teisingai!!!");
                    if (userScore.ContainsKey(userName))
                    {
                        userScore[userName] += points[randomKey];
                        foreach (var point in userScore)
                        {
                            Console.WriteLine($"{point.Key} {point.Value}  tasku");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Jūs atsakėte neteisingai.");
                    break;
                }

                Console.WriteLine("Ar norite tęsti žaidimą? (Įveskite 'q' jei norite baigti)");
                var continueInput = Console.ReadLine();
                if (continueInput.ToLower() == "q")
                {
                    continuePlaying = false;
                    Console.WriteLine(userName + " Jūs baigėte žaidimą. Viso gero \n Tam, kad pradėti žaisti " +
                        " jums teks vėl įrašyti Vardą ir Pavardę. ");
                }
            } while (continuePlaying);
        }

        public static void Main()
        {
            Console.WriteLine("****************************************************");
            Console.WriteLine("*      !!!!!SVEIKI ATVYKE I PROTO MUSI!!!!!        *");
            Console.WriteLine("****************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Taisykles:");
            Console.WriteLine("Zaidimas yra paprastas, Jums bus uzduotas klausimas, turesite pasirinkti viena atsakima is ketriu.\n" +
                "Jei atsakysite neteisingai, zaidimas bus uzbaigtas, sugrisite atgal i meniu langa.\nTam kad isiti i meniu langa reikia ivesti 'q'.\n" +
                "SEKMES!!!! ");

            var userScore = new Dictionary<string, int>();
            Console.Write("Iveskite vartotojo Varda ir Pavarde: ");
            userName = Console.ReadLine();
            bool isLoggedIn = Login(userName, ref userScore);
            bool continuePlaying = true;


            do
            {
                Console.Clear();
                Console.WriteLine(userName + " Sveikinam jus sekmingai prisijunget.");
                Console.WriteLine("Pasirinkite veiksmą:");
                Console.WriteLine("1. Pradėti žaidimą mix");
                Console.WriteLine("2. Pradėti žaidimą filmai");
                Console.WriteLine("3. Pradėti žaidimą muzika");
                Console.WriteLine("3. Peržiūrėti dalyvius");
                Console.WriteLine("5. Peržiūrėti rezultatus");
                Console.WriteLine("6. Baigti");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Game(userScore);
                        break;
                    case "2":
                        GameTwo(userScore);
                        break;
                    case "3":
                        GameThree(userScore);
                        break;
                    case "4":
                        ShowParticipants(userScore);
                        break;
                    case "5":
                        ShowResults(userScore);
                        break;
                    case "6":
                        continuePlaying = false;
                        Console.WriteLine(userName + " Jūs baigėte žaidimą. Viso gero \n Tam, kad pradėti žaisti " +
                            " jums teks vėl įrašyti Vardą ir Pavardę. ");
                        break;
                    default:
                        Console.WriteLine("Neteisingas pasirinkimas. Pasirinkite dar kartą.");
                        break;
                }
            } while (continuePlaying);



        }
    }
}