using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace konyves
{
    class Program
    {
        struct Adat
        {
            public string könyv_cime;
            public string könyv_irója;
            public int kiadás_éve;
            public int oldalszám;
        }
        static void Main(string[] args)
        {
            Adat[] adatok = new Adat[1000];
            StreamReader olvas = new StreamReader(@"C:\Users\Rendszergazda\Downloads\konyvek.txt");
            int n = 0;
            //int k_irok = 0;
            while (!olvas.EndOfStream)
            {
                string sor = olvas.ReadLine();
                string[] db = sor.Split(';');
                adatok[n].könyv_cime = db[0];
                adatok[n].könyv_irója = db[1];
                adatok[n].kiadás_éve = int.Parse(db[2]);
                adatok[n].oldalszám = int.Parse(db[3]);
                //string[] irok = adatok[n].könyv_irója.Split('-');
                //k_irok = k_irok + irok.Length;
                n++;
            }
            olvas.Close();

            //1.feladat: 1, Hány 2019-es könyv van a file-ban?
            Console.WriteLine("1.feladat: {0} db könyv van a file-ban!", n);

            //2.feladat és 3.feladat: Hány író van a file-ban?
            //Console.WriteLine("A fájlban {0} író van.",k_irok);
            int max = 0;
            int csere = 0;
            for (int i = 0; i < n - 1; i++)
            {
                string[] db = adatok[i].könyv_irója.Split('-');
                for (int k = 0; k < db.Length; k++)
                {
                    for (int j = 1; j < n; j++)
                    {
                        string[] k_db = adatok[j].könyv_irója.Split('-');
                        for (int m = 0; m < k_db.Length; m++)
                        {
                            if (db[k] == k_db[m])
                            {
                                max++;
                                csere = j + 1;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("A fájlban {0} író van", max);
            Console.WriteLine(adatok[csere].könyv_irója);
            //4.feladat: Kérd be egy író nevét
            Console.Write("kérem adja meg az író nevét: ");
            string iro_neve = Console.ReadLine();

            //5.feladat: A bekért író összes művének adatait listázd ki 
            //(azokat is, amit mással írt) 
            for (int i = 0; i < n; i++)
            {
                string[] db = adatok[i].könyv_irója.Split('-');
                if (db.Length > 1)
                {
                    for (int j = 0; j < db.Length; j++)
                    {
                        if (db[j] == iro_neve)
                        {
                            Console.WriteLine(adatok[i].könyv_cime);
                        }
                    }
                }
                else
                {
                    if (adatok[i].könyv_irója == iro_neve)
                    {
                        Console.WriteLine(adatok[i].könyv_cime);
                    }
                }
            }

            //6.feladat
            StreamWriter ir = new StreamWriter(@"C:\Users\Rendszergazda\Downloads\irok.txt");

            for (int i = 0; i < n; i++)
            {
                string[] db = adatok[i].könyv_irója.Split('-');
                if (db.Length > 1)
                {
                    for (int j = 0; j < db.Length; j++)
                    {
                        ir.WriteLine(j + 1 + ". " + db[j]);
                    }
                    ir.WriteLine("A könyv címe: " + adatok[i].könyv_cime);
                }
                else
                {
                    ir.WriteLine(db.Length + ". " + db[0]);
                    ir.WriteLine("A könyv címe: " + adatok[i].könyv_cime);
                }
            }
            ir.Close();


            //7.feladat: Van-e olyan könyv, amit legalább 3 író jegyez?
            int szerzo = 0;
            for (int i = 0; i < n; i++)
            {
                string[] db = adatok[i].könyv_irója.Split('-');
                if (db.Length >= 3)
                {
                    Console.WriteLine("7.feladat: Van ilyen könyv!");
                    szerzo++;
                }

            }
            Console.WriteLine("{0}db könyv van az adatbázisban, amelynek legalább 3 szerzője van!", szerzo);

            //8.feladat

            for (int i = 2012; i < 2020; i++)
            {
                int ev = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == adatok[j].kiadás_éve)
                    {
                        ev++;
                    }
                }
                if (ev == 0)
                {
                    Console.WriteLine(i + ": nem jelent meg könyv!");
                }
                else
                {
                    Console.WriteLine(i + ": {0} könyv jelent meg!", ev);
                }

            }

            //9.feladat
            int maxx = 0;
            int cseree = 0;
            for (int i = 0; i < adatok[i].oldalszám; i++)
            {
                if (maxx < adatok[i].oldalszám)
                {
                    maxx = adatok[i].oldalszám;
                    cseree = i;
                }
            }
            Console.WriteLine("A legnagyobb oldalszámú könyv címe: {0} oldalszáma: {1}", adatok[cseree].könyv_cime, adatok[cseree].oldalszám);



            Console.ReadKey();
        }
    }
}