using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1
{
    class GFG : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            // "CompareTo()" method
            return x.CompareTo(y);
        }
    }
    class DecendingComparer<TKey> : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            // "CompareTo()" method
            return y.CompareTo(x);
        }
    }

    class StrategiAlgoritma
    {
        // Data Member
        private string NameFile;
        private string ReadText;
        private int NSimpul;
        private List<string[]> Simpul = new List<string[]>();
        private List<string> Graf = new List<string>();
        private List<string> ListExplore = new List<string>();
        private int degree;

        //  Defaul Contruktor
        public StrategiAlgoritma()
        {
            this.NameFile = "XXXXX";
            this.ReadText = File.ReadAllText(this.NameFile);
            string[] readTextList1 = ReadText.Split('\n');
            this.NSimpul = Convert.ToInt32(readTextList1[0]);
            for (int i = 0; i < GetNSimpul(); i++)
            {
                string[] readTextList2 = readTextList1[i + 1].Split(' ');
                this.Simpul.Add(readTextList2);
                string Text = readTextList2[0];
                if (!IsInAkar(Text)) { this.Graf.Add(Text); }
                Text = readTextList2[1];
                if (!IsInAkar(Text)) { this.Graf.Add(Text); }
                //if(readTextList2[0] == readTextList2[1]) { Console.WriteLine("Sama"); }
            }
        }
        //  User Define Contruktor
        public StrategiAlgoritma(string Na)
        {
            this.NameFile = Na;
            this.ReadText = File.ReadAllText(this.NameFile);
            string[] readTextList1 = ReadText.Split('\n');
            this.NSimpul = Convert.ToInt32(readTextList1[0]);
            for (int i = 0; i < GetNSimpul(); i++)
            {
                string[] readTextList2 = readTextList1[i + 1].Split(' ');
                this.Simpul.Add(readTextList2);
                string Text = readTextList2[0];
                if (!IsInAkar(Text)) { this.Graf.Add(Text); }
                Text = readTextList2[1];
                if (!IsInAkar(Text)) { this.Graf.Add(Text); }
                //if(readTextList2[0] == readTextList2[1]) { Console.WriteLine("Sama");
            }

        }
        // Destruktor
        ~StrategiAlgoritma() { }

        // Fungsi Bolean
        public bool IsInAkar(string S) { return this.Graf.Contains(S); }
        public bool IsAkar(string S, int n = 0)
        {
            string Akar = Convert.ToString(this.Simpul[n][0]);
            if (S == Akar)
            {
                return true;
            }
            else
            {
                if (n + 1 == GetNSimpul())
                {
                    return false;
                }
                else
                {
                    return false || IsAkar(S, n + 1);
                }
            }
        }

        // Fungsi Print
        public void PrintReadFile() { Console.WriteLine(this.ReadText); }
        public void PrintAkar() { foreach (string text in GetGraf()) { Console.WriteLine(text); } }
        public List<string> MyFriend(string MyGraf)
        {
            List<string> ListMyFriend = new List<string>();
            foreach (string[] text in GetSimpul())
            {
                string Akar1 = Convert.ToString(text[0]);
                string Akar2 = Convert.ToString(text[1]);
                if (Akar1 == MyGraf)
                {
                    ListMyFriend.Add(Akar2);
                }
                if (Akar2 == MyGraf)
                {
                    ListMyFriend.Add(Akar1);
                }
            }
            return ListMyFriend;
        }
        public List<string> IrisanFreind(string A, string B)
        {
            List<string> IrisanList = new List<string>();
            List<string> ListA = MyFriend(A);
            List<string> ListB = MyFriend(B);
            if (ListA.Count <= ListB.Count)
            {
                foreach (string text in ListA)
                {
                    if (ListB.Contains(text)) { IrisanList.Add(text); }
                }
            }
            else
            {
                foreach (string text in ListB)
                {
                    if (ListA.Contains(text)) { IrisanList.Add(text); }
                }
            }
            return IrisanList;

        }

        // Getter
        public int GetNSimpul() { return this.NSimpul; }
        public List<string> GetGraf()
        {
            // Mengembalikan list graf dengan urut A - Z
            GFG gg = new GFG();
            List<string> list = this.Graf;
            list.Sort(gg);
            return list;
        }
        public List<string[]> GetSimpul() { return this.Simpul; }


        // Fungsi BFS
        public List<string> FriendRecomBFS(string GrafAkun)
        {
            List<string> TemanA = MyFriend(GrafAkun);
            List<string> RelasiTemanA = new List<string>();
            List<string> result = new List<string>();
            SortedList<string, string> RecomFriends = new SortedList<string, string>(new DecendingComparer<int>());
            foreach (string text in TemanA)
            {
                List<string> temanText = MyFriend(text);
                foreach (string teman in temanText)
                {
                    if (!RelasiTemanA.Contains(teman) && !TemanA.Contains(teman) && teman != GrafAkun) { RelasiTemanA.Add(teman); }
                }
            }

            foreach (string text in RelasiTemanA)
            {

                List<string> irisan = IrisanFreind(GrafAkun, text);
                //Console.Write("{0} memiliki {1} teman yang sama : ",text, irisan.Count);
                string Key = Convert.ToString(irisan.Count);
                Key += " " + text;
                string Val = "";
                foreach (string teman in irisan) { Val += (teman + " "); }
                RecomFriends.Add(Key, Val);
            }
            for (int i = 0; i < RecomFriends.Count; i++)
            {
                string kata = "";
                string[] angka = RecomFriends.Keys[i].Split(' ');
                kata += angka[1] + " memiliki " + angka[0] + " teman yang sama : " + RecomFriends.Values[i];
                Console.Write("{0}\n{1} teman yang sama : ", angka[1], angka[0]);
                Console.WriteLine(RecomFriends.Values[i]);
                Console.WriteLine();
                result.Add(kata);
            }
            return result;
        }
        // BFS EXPLORE
        public string ExploreFriendsBFS(string AkunAsal, string AkunTujuan)
        {

            List<string> ListFriend = new List<string>();
            List<string> ListKujungi = new List<string>();
            string result = "";
            if(AkunAsal == AkunTujuan)
            {
                return "Apakah " + AkunTujuan + " berteman dengan dirinya sendiri?";
            }
            bool Status = false;
            GFG gg = new GFG();
            ListFriend = MyFriend(AkunAsal);
            ListFriend.Sort(gg);
            //Console.WriteLine(ListFriend[0]);

            // Kasus derajat 0
            ListKujungi.Add(AkunAsal);
            if (ListFriend.Contains(AkunTujuan)) { Status = true; ListKujungi.Add(AkunTujuan); }
            
            // pencarian derajat lebih
            while (!ListKujungi.Contains(AkunTujuan) && !Status)
            {
                //Console.WriteLine("while");
                // penanda dikunjungi
                foreach (string text in ListFriend)
                {
                    
                    if (text != AkunTujuan)
                    {
                        ListKujungi.Add(text);
                       
                    }
                    else
                    {
                        ListKujungi.Add(text);
                        break;
                    }
                    //Console.WriteLine(text);
                }
                // Pengecekan apakah dah dikunjungi
                if (ListKujungi.Contains(AkunTujuan))
                {
                    Status = true;
                    break;
                }

                List<string> ListCopy = ListFriend.GetRange(0, ListFriend.Count);
                ListFriend.Clear();
                //Console.WriteLine(ListCopy[0]);
                foreach (string text in ListCopy)
                {
                    List<string> List2 = MyFriend(text);
                    foreach (string text2 in List2)
                    {
                     
                        //Console.WriteLine(text2);
                        if (!ListKujungi.Contains(text2))
                        {
                            ListFriend.Add(text2);
                            //Console.WriteLine("Masuk");
                        }
                    }
                }

                ListFriend.Sort(gg);
                //Console.WriteLine(ListFriend[0]);
                if (!ListFriend.Any()) { Status = true; }

            }
            
            if (ListKujungi.Contains(AkunTujuan))
            {
                //Console.WriteLine("Ada");
                string Awal = AkunAsal;
                string Akhir = AkunTujuan;
                if (string.Compare(AkunAsal, AkunTujuan) > 0)
                {
                    Awal = AkunTujuan;
                    Akhir = AkunAsal;
                }
                ListFriend = MyFriend(Akhir);
                ListFriend.Sort(gg);
                //this.ListExplore.Add(AkunTujuan);

                while (!ListFriend.Contains(Awal))
                {
                    string TambahGraf = ListFriend[0];
                    this.ListExplore.Add(TambahGraf);
                    ListFriend.Clear();
                    ListFriend = MyFriend(TambahGraf);
                    ListFriend.Sort(gg);
                }
                //this.ListExplore.Add(AkunAsal);
                this.ListExplore.Sort(gg);
                if (string.Compare(AkunAsal, AkunTujuan) > 0)
                {
                    this.ListExplore.Reverse();
                }
                this.degree = this.ListExplore.Count;
                if (this.degree == 0)
                {
                    result += " Sudah berteman.  ( ";
                }
                else
                {
                    result += "Derajat pertemanan "+ this.degree + ". (";

                }
                result += AkunAsal + " <---> ";
                Console.WriteLine(this.degree);
                foreach (string text in this.ListExplore)
                {
                    result +=  text + " <---> ";
                    /*
                    if (text != AkunTujuan) { 
                        Console.Write("{0} --- ", text);
                        
                    }
                    else { Console.Write("{0}\n", text); result += text + " )"; } */
                }
                result += AkunTujuan + " )";
            }
            else
            {
                result += "Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.";
                Console.WriteLine("Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
            }
            ListFriend.Clear();
            return result;
        }

        
        //ExploreFriendsDFS
        public void DFS(string akunAsal, string akunTujuan, List<string> listKunjungan)
        {

            listKunjungan.Add(akunAsal);
            List<string> newList = MyFriend(akunAsal);
            foreach (var n in newList)
            {
                if (!listKunjungan.Contains(n) && n != akunTujuan)
                {
                    DFS(n, akunTujuan, listKunjungan);
                }
                else if (n == akunTujuan)
                {
                    listKunjungan.Add(n);
                    break;
                }
            }

        }



        public string ExploreFriendsDFS(string akunAsal, string akunTujuan)
        {
            List<string> listFriend = new List<string>();
            List<string> listKunjungan = new List<string>();
            if (akunAsal == akunTujuan)
            {
                return "Apakah " + akunTujuan + " berteman dengan dirinya sendiri?";
            }
            GFG gg = new GFG();
            string result = "";
            DFS(akunAsal, akunTujuan, listKunjungan);

            if (listKunjungan.Contains(akunTujuan))
            {
                //Console.WriteLine("Ada");
                string awal = akunAsal;
                string akhir = akunTujuan;
                if (string.Compare(akunAsal, akunTujuan) > 0)
                {
                    awal = akunTujuan;
                    akhir = akunAsal;
                }
                listFriend = MyFriend(akhir);
                listFriend.Sort(gg);
                //this.ListExplore.Add(akunTujuan);

                while (!listFriend.Contains(awal))
                {
                    string TambahGraf = listFriend[0];
                    this.ListExplore.Add(TambahGraf);
                    listFriend.Clear();
                    listFriend = MyFriend(TambahGraf);
                    listFriend.Sort(gg);
                }
                //this.ListExplore.Add(akunAsal);
                this.ListExplore.Sort(gg);
                if (string.Compare(akunAsal, akunTujuan) > 0)
                {
                    this.ListExplore.Reverse();
                }
                this.degree = this.ListExplore.Count;
                if (this.degree == 0)
                {
                    result += " Sudah berteman.  ( ";
                }
                else
                {
                    result += "Derajat pertemanan " + this.degree + ". (";

                }

                result += akunAsal + " <---> ";
                foreach (string text in this.ListExplore)
                {
                    result += text + " <---> ";
                    /*if (text != akunTujuan)
                    {
                        Console.Write("{0} --- ", text);
                        result += text + " <---> ";
                    }
                    else { Console.Write("{0}\n", text); result += text + " )"; }*/
                }
                result += akunTujuan + " )";
            }
            else
            {
                //MEET ?
                result += "Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.";
                Console.WriteLine("Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
            }
            listFriend.Clear();
            return result;
        } 
    }
}
