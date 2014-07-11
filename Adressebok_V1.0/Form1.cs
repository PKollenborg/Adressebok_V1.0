using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Xml;


namespace Adressebok_V1._0
{
    public partial class Adressebok : Form
    {
        public Adressebok()
        {
            InitializeComponent();
        }

        bool textboxmail = false;
        bool textboxpassword = false;
        string Host = "";
        int Port = 0;
        List<Person> People = new List<Person>();
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(path + "\\Adressebok - Peder"))
                Directory.CreateDirectory(path + "\\Adressebok - Peder");
            if (!File.Exists(path + "\\Adressebok - Peder\\settings.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter(path + "\\Adressebok - Peder\\settings.xml", Encoding.UTF8);
                xW.WriteStartElement("People");
                xW.WriteEndElement();
                xW.Close();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\Adressebok - Peder\\settings.xml");
            foreach (XmlNode xNode in xDoc.SelectNodes("People/Person"))
            {
                Person p = new Person();
                p.Fornavn = xNode.SelectSingleNode("Fornavn").InnerText;
                p.Etternavn = xNode.SelectSingleNode("Etternavn").InnerText;
                p.Adresse = xNode.SelectSingleNode("Adresse").InnerText;
                p.Emailadresse = xNode.SelectSingleNode("Emailadresse").InnerText;
                p.Telefonnummer = xNode.SelectSingleNode("Telefonnummer").InnerText;
                p.Mobilnummer = xNode.SelectSingleNode("Mobilnummer").InnerText;
                p.ekstrainfo = xNode.SelectSingleNode("Ekstrainfo").InnerText;
                p.fødselsdato = DateTime.FromFileTime(Convert.ToInt64(xNode.SelectSingleNode("Fødselsdato").InnerText));
                People.Add(p);
                listView1.Items.Add(p.Fornavn);

            }
        }

        class Person
        {
            public string Fornavn
            {
                get;
                set;
            }
            public string Etternavn
            {
                get;
                set;
            }
            public string Adresse
            {
                get;
                set;
            }
             public string Emailadresse
            {
                get;
                set;
            }

            public string Telefonnummer
            {
                get;
                set;
            }
            public string Mobilnummer
            {
                get;
                set;
            }

            public string ekstrainfo
            {
                get;
                set;
            }

            public DateTime fødselsdato
            {
                get;
                set;

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Email koden  
            // VENT 30 SEK!   Tar en liten stund før man får meldingen!
            richTextBox2.Text=(textBox7.Text);

            var fromAddress = new MailAddress(textBox6.Text, richTextBox1.Text);
            // Avsender mailadressen står i TextBox6, Avsender navn i richTextBox1
            var toAddress = new MailAddress(richTextBox2.Text, richTextBox3.Text);
            // Mottaker mailadressen står I richTextBox2 , Mottaker navn I richTextbox3
            string fromPassword = (textBox8.Text); //passord avsenders mailkonto HUSK OG ENDRE TILBAKE TIL XXXXX
            string subject = (richTextBox4.Text); //Emne kan også skriver her ved og sette det  mellom " "
            string body = (richTextBox5.Text);  // selve meldingen også skrive her ved og sette den mellom " "

            var smtp = new SmtpClient
                       {
                           Host = Host,
                           Port = Port,
                           EnableSsl = true,
                           DeliveryMethod = SmtpDeliveryMethod.Network,
                           UseDefaultCredentials = false,
                           Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                       };
            using (var message = new MailMessage(fromAddress, toAddress)
                                 {
                                     Subject = subject,
                                     Body = body
                                 })
            {
                {
                    smtp.Send(message);
                    SendEmail.Enabled = false;
                    CompleteMailVerification.Enabled= true;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }




        

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Fornavn = textBox1.Text;
            p.Etternavn = textBox2.Text;
            p.Adresse = textBox3.Text;
            p.Emailadresse = textBox7.Text;
            p.Telefonnummer = textBox4.Text;
            p.Mobilnummer = textBox5.Text;
            p.fødselsdato = dateTimePicker1.Value;
            p.ekstrainfo = richTextBox6.Text;
            People.Add(p);
            listView1.Items.Add(p.Fornavn + " " + p.Etternavn);


            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            richTextBox6.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelPassord.Visible = true;
            PanelPassord.Enabled = true;
            PanelMain.Visible = false;
            PanelMain.Enabled = false;
            label8.Text = "Hei! Har du Husket å velge E-post levrandøren din?" + Environment.NewLine +
                "Hvis ikke klikk på Settings/Email  øverst til venstre" + Environment.NewLine +    " også velger du der :)";

            this.Size = new System.Drawing.Size(299, 319); ;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textboxmail = false;
            textboxpassword = false;
            CompleteMailVerification.Enabled = false;
            avsendermaillabel.Text = (textBox6.Text);
            richTextBox2.Text = (textBox7.Text);
            richTextBox3.Text = (textBox1.Text +" " + textBox2.Text);
          


            panelMail.Visible = true;
            panelMail.Enabled = true;
            PanelPassord.Visible = false;
            PanelPassord.Visible = false;
            PanelMain.Visible = false;
            PanelMain.Enabled = false;
            this.Size = new System.Drawing.Size(882, 478); ;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }


        private void BackAdressebook_Click(object sender, EventArgs e)
        {
            PanelMain.Enabled = true;
            PanelMain.Visible = true;

            panelMail.Enabled = false;
            panelMail.Visible = false;
            PanelPassord.Enabled = false;
            PanelPassord.Visible = false;
            CompleteMailVerification.Enabled= true;

            this.Size = new System.Drawing.Size(557, 496); ;
        }

        private void richTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void PanelPassord_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return; // ikke fjern  kræsjer uten.
            textBox1.Text = People[listView1.SelectedItems[0].Index].Fornavn;
            textBox2.Text = People[listView1.SelectedItems[0].Index].Etternavn;
            textBox3.Text = People[listView1.SelectedItems[0].Index].Adresse;
            textBox4.Text = People[listView1.SelectedItems[0].Index].Telefonnummer;
            textBox5.Text = People[listView1.SelectedItems[0].Index].Mobilnummer;
            textBox6.Text = People[listView1.SelectedItems[0].Index].ekstrainfo;
            textBox7.Text = People[listView1.SelectedItems[0].Index].Emailadresse;
            dateTimePicker1.Value = People[listView1.SelectedItems[0].Index].fødselsdato;
  }

        private void textchange()
        {
            if (textboxmail && textboxpassword)
                CompleteMailVerification.Enabled = true;

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textboxmail = true;
            textchange();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textboxpassword = true;
            textchange();
        }
        private void sMTPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hotmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Host = "smtp.live.com";    //  Oppsett for  Hotmail
            Port = 587;
        }


        private void gmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Host = "smtp.gmail.com"; // oppsett for Gmail
            Port = 587;
        }



        private void yahooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Host = "smtp.mail.yahoo.com"; //oppsett for yahoo
            Port = 995;
        }

        private void onlinenoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Host = "smtp.online.no";
            Port = 587; // dobbeltsjekk port 
        }

       


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox6_TextChanged_1(object sender, EventArgs e)
        {

        }



        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            Remove();
        }
        void Remove()
        {
            try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                People.RemoveAt(listView1.SelectedItems[0].Index);

            }
            catch { }


            {

                if (MessageBox.Show("Er du sikker på at du ønsker å slette denne kontakten?", "Adressebok_V1.0-Bekreft sletting",
                      MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                      == DialogResult.Yes)
                {
                    Remove();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox7.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    richTextBox6.Text = "";

                   
                }
            }
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return; // ikke fjern  kræsjer uten.
            People[listView1.SelectedItems[0].Index].Fornavn         = textBox1.Text;
            People[listView1.SelectedItems[0].Index].Etternavn       = textBox2.Text;
            People[listView1.SelectedItems[0].Index].Adresse         = textBox3.Text;
            People[listView1.SelectedItems[0].Index].Telefonnummer   = textBox4.Text;
            People[listView1.SelectedItems[0].Index].Mobilnummer     = textBox5.Text;
            People[listView1.SelectedItems[0].Index].ekstrainfo      = textBox6.Text;
             People[listView1.SelectedItems[0].Index].fødselsdato    =dateTimePicker1.Value;
            listView1.SelectedItems[0].Text                          = textBox1.Text;
            MessageBox.Show("Kontakten er lagret! :) ", "Adressebok_V1.0-LArin",
MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
    }

        private void Adressebok_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load (path + "\\Adressebok - Peder\\settings.xml");
        XmlNode xNode = xDoc.SelectSingleNode ("People");
        xNode.RemoveAll();
        foreach (Person p in People)
        { XmlNode xtop                     = xDoc.CreateElement ("Person");
          XmlNode xFornavn                 = xDoc.CreateElement ( "Fornavn");
          XmlNode xEtternavn               = xDoc.CreateElement ("Etternavn");
          XmlNode xAdresse                 = xDoc.CreateElement ( "Adresse");
          XmlNode xEmailadresse            = xDoc.CreateElement ("Emailadresse");
          XmlNode xTelefonnummer           = xDoc.CreateElement ("Telefonummer");
          XmlNode xMobilnummer             = xDoc.CreateElement ("Mobilnummer");
          XmlNode xFødselsdato             = xDoc.CreateElement("Fødseldato");
          XmlNode xEkstrainfo              = xDoc.CreateElement ("Ekstrainfo");
         xFornavn.InnerText           = p.Fornavn;
         xEtternavn.InnerText         = p.Etternavn;
         xAdresse.InnerText           = p.Adresse;
         xEmailadresse.InnerText      = p.Emailadresse;
         xTelefonnummer.InnerText     = p.Telefonnummer;
         xMobilnummer.InnerText       = p.Mobilnummer;
         xEkstrainfo.InnerText        = p.ekstrainfo;
         xFødselsdato.InnerText = p.fødselsdato.ToFileTime().ToString();
         xtop.AppendChild(xFornavn);
         xtop.AppendChild(xEtternavn);
         xtop.AppendChild(xAdresse);
         xtop.AppendChild(xEmailadresse);
         xtop.AppendChild(xTelefonnummer);
         xtop.AppendChild(xMobilnummer);
         xtop.AppendChild(xFødselsdato);
         xtop.AppendChild(xEkstrainfo);
         xDoc.DocumentElement.AppendChild(xtop);
        }
        xDoc.Save(path + "\\Adressebok - Peder\\settings.xml"); 
        }
    }
}

       
    

