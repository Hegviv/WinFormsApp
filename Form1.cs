namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        List<Jatszma> jatszmak = new List<Jatszma>();
        List<Felhasznalo> felhasznalok = new List<Felhasznalo>();
        public Form1()
        {
            InitializeComponent();
            string[] lines = File.ReadAllLines("nyeremeny.txt");
            foreach (var item in lines)
            {
                string[] values = item.Split(';');
                Jatszma jatszma_object = new Jatszma(values[0], values[1], values[2], values[3], values[4]);
                jatszmak.Add(jatszma_object);
            }
            List<string> felhasznalokstr = new List<string>();
            foreach (var item in jatszmak)
            {
                if (!felhasznalokstr.Contains(item.felhasznalo))
                {
                    felhasznalokstr.Add(item.felhasznalo);
                }

            }
            foreach (var item in felhasznalokstr)
            {
                Felhasznalo felhasznalo_object = new Felhasznalo(item, jatszmak);
                felhasznalok.Add(felhasznalo_object);
            }
            int maxtet = int.MinValue;
            Felhasznalo max_ertek_user = felhasznalok[0];
            foreach (var item in felhasznalok)
            {
                if (item.sumtet > maxtet)
                {
                    maxtet = item.sumtet;
                    max_ertek_user =item;
                }
            }
            label2.Text = $"Felhaszn�l�: {max_ertek_user.nev}, T�t�sszege:{maxtet}";

            int vesztes = 0;

            foreach (var item2 in jatszmak)
            {
                if (item2.nyeVve.StartsWith("vesz"))
                {
                    vesztes++;
                }
            }

            label3.Text = $"{vesztes}";

            int min_nyeremeyn = int.MaxValue;

            Jatszma min_ertek = jatszmak[0];

            foreach (var item in jatszmak)
            {
                if (min_nyeremeyn> item.nyeremeny)
                {
                    min_nyeremeyn =item.nyeremeny;
                    min_ertek = item;
                }
            }
            label4.Text = $"{min_ertek.sorszam} {min_ertek.felhasznalo} tet: {min_ertek.tet} szorzo: {min_ertek.szorzo} , nyerem:{min_ertek.nyeremeny}";

            string a_fel = "";
            int adb = 0;
            foreach (var item in felhasznalokstr)
            {
                if (item.StartsWith("a"))
                {
                    adb++;
                    if (adb < 6)
                    {
                        a_fel += item +" ;";
                    }
                }

            }
            label5.Text = $"A:db: {adb}, { a_fel}, ";
            foreach (var item in felhasznalok)

            {
                if (item.nev == "decongen")
                    label6.Text = " Decongen �ssz nyerem�nye: " + item.sumnyer;
            }
            int nyer = 0;
            foreach (var item in jatszmak)
            {
                if (item.nyeVve.StartsWith("ny"))
                {

                    nyer += item.tet;
                }
            }
            label7.Text = "�sszes nyertes t�t �sszege: " + nyer;

            foreach (var item in jatszmak)
            {
                if (item.nyeVve.StartsWith("ny"))
                { 
                dataGridView2.Rows.Add(item.sorszam, item.felhasznalo, item.nyeremeny, item.nyeVve);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in jatszmak)
                if (numericUpDown1.Value == item.sorszam)
                {
                    if (item.nyeVve.StartsWith("ny"))
                    {
                        label1.Text = $"Sorsz�m: {item.sorszam}, N�v: {item.felhasznalo}, T�t: {item.tet}, Szorz�: {item.szorzo}, Nyert-e:{item.nyeVve} Nyerem�ny:{item.nyeremeny}";
                    }
                    else
                    {
                        label1.Text = $"Sorsz�m: {item.sorszam}, N�v: {item.felhasznalo}, T�t: {item.tet}, Szorz�: {item.szorzo}, Nyerte-e: {item.nyeVve}: Nyerem�ny: 0";
                    }
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            foreach (var item in felhasznalok)
            {
                if (textBox2.Text.ToLower()==item.nev)
                {
                    foreach (var item2 in item.order_list)
                    {
                        if (item2.nyeVve.StartsWith("ny"))
                        {
                            dataGridView1.Rows.Add(item2.sorszam, item2.felhasznalo, item2.tet, item2.szorzo, item2.nyeremeny, item2.nyeVve);
                        }
                        else
                        {
                            dataGridView1.Rows.Add(item2.sorszam, item2.felhasznalo, item2.tet, item2.szorzo,"0", item2.nyeVve);
                        }
                    }
                }
            }
        }
    }
}