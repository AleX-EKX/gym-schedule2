using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace avtoriz
{
    public partial class application : Form
    {
        //еще не готово,нужно доделать еще,может на рпм)

        public application()
        {
            InitializeComponent();
            usernameField.Text = "Введите имя";
            usernameField.ForeColor = Color.Gray;

            tel.Text = "Введите телефон";
            tel.ForeColor = Color.Gray;

            mail.Text = "Введите e-mail";
            mail.ForeColor = Color.Gray;

        }
        
        private void brat_btn1_Click(object sender, EventArgs e)
        {
            if (usernameField.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            string imya = usernameField.Text;
            int dlimya = 50;

            if (imya.Length > dlimya)
            {
                MessageBox.Show("Имя слишком длинное");
                imya = imya.Remove(dlimya);
                return;
            }

            double result;

            if (double.TryParse(tel.Text, out result))
            {
                if (tel.Text == "Введите телефон")
                {
                    MessageBox.Show("Введите телефон");
                    return;
                }
                string tele = tel.Text;
                int dltel = 20;

                if (tele.Length > dltel)
                {
                    MessageBox.Show("слишком длинный номер телефона");
                    tele = tele.Remove(dltel);
                    return;
                }
            }
            else
            {
                
                MessageBox.Show("Пожалуйста введите номер");
            }

            try
            {
                string eemail = mail.Text;
                var addr = new System.Net.Mail.MailAddress(eemail);

                if (addr.Address == eemail)
                {
                    
                    
                }
            }
            catch
            {
                MessageBox.Show("Неверный адрес");
                return;
            }


            if (mail.Text == "Введите e-mail")
            {
                MessageBox.Show("Введите e-mail");
                return;
            }
            string email = mail.Text;
            int dlm = 60;

            if (email.Length > dlm)
            {
                MessageBox.Show("email слишком длинный");
                email = email.Remove(dlm);
                return;
            }

            //string userName = usernameField.Text; //имя пользователя
            //string adressTo = mail.Text; //адрес пользователя
            // string messageSubject = tema.Text; //тема
            // string messageText = tekst.Text; //текст
            //  SendMessage(userName, adressTo, messageSubject, messageText);

            if (checkuser())
                return;

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `mail` (`data`,`cena`,`id_bt`,`name`, `phone`, `email`,`kod`) VALUES (@data,@cena,@id_bt,@name, @phone, @email,@kod)", db.GetConnection());

            command.Parameters.Add("@data", MySqlDbType.VarChar).Value = chislo.Text;
            command.Parameters.Add("@cena", MySqlDbType.VarChar).Value = cena.Text;
            command.Parameters.Add("@id_bt", MySqlDbType.VarChar).Value = Program.per_id_bt;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = usernameField.Text;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = tel.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = mail.Text;
            command.Parameters.Add("@kod", MySqlDbType.VarChar).Value = data.vremy;

            try
            {
                db.openConnection();
                command.ExecuteNonQuery();
                this.Close();
                raspisanie rasp = new raspisanie();
                rasp.Show();

                MessageBox.Show("Спасибо за бронь");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.closeConnection();
            }

        }

        public Boolean checkuser()
        {
            DB db = new DB();
            //создаем табл
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `mail` where `data`= @data", db.GetConnection());// выбрать всех пользователь у которых логин такой же как ввел пользователь в регистрации 

            command.Parameters.Add("@data", MySqlDbType.VarChar).Value = chislo.Text;

            // адаптер позволяет нам выбрать данные из бд
            adapter.SelectCommand = command;
            adapter.Fill(table);//данные трансформируем внутрь обекта табл

            if (table.Rows.Count > 0)// если записей больше 0, то будет авторизован
            {
                MessageBox.Show("Упс, уже забронировано");
                return true;//это возврат если сущ пользователь
            }
            else

                return false;//это  если несущ пользователь
        }

        private void usernameField_TextChanged(object sender, EventArgs e)
        {

        }

        private void usernameField_Leave(object sender, EventArgs e)
        {
            if (usernameField.Text == "")//если поле пустое, то напишем введите имя
                usernameField.Text = "Введите имя";
            usernameField.ForeColor = Color.Gray;
        }

        private void usernameField_Enter(object sender, EventArgs e)
        {
            if (usernameField.Text == "Введите имя")//если текст равен внутри формы введите имя, то будет очистка от текста
            {
                usernameField.Text = "";
                usernameField.ForeColor = Color.Black;
            }
        }

        private void tel_Leave(object sender, EventArgs e)
        {
            if (tel.Text == "")//если поле пустое, то напишем введите имя
                tel.Text = "Введите телефон";
            tel.ForeColor = Color.Gray;
        }

        private void tel_Enter(object sender, EventArgs e)
        {
            if (tel.Text == "Введите телефон")//если текст равен внутри формы введите имя, то будет очистка от текста
            {
                tel.Text = "";
                tel.ForeColor = Color.Black;
            }
        }

        private void mail_Leave(object sender, EventArgs e)
        {
            if (mail.Text == "")//если поле пустое, то напишем введите email
                mail.Text = "Введите e-mail";
            mail.ForeColor = Color.Gray;
        }

        private void mail_Enter(object sender, EventArgs e)
        {
            if (mail.Text == "Введите e-mail")//если текст равен внутри формы введите имя, то будет очистка от текста
            {
                mail.Text = "";
                mail.ForeColor = Color.Black;
            }
        }

        private void main_btn_Click(object sender, EventArgs e)
        {

            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` ", db.GetConnection());// выбрать всех пользователь где логин равен тому что ввел пользователь и пароль также
            adapter.SelectCommand = command;
            adapter.Fill(table);//данные трансформируем внутрь обекта табл

            db.openConnection();

            var reader = command.ExecuteReader();

            if (reader.Read())
            {

                Loggeduser.AccessLevel = (AccessLevel)Convert.ToInt32(reader["user_permission"]);

                if (Loggeduser.AccessLevel == AccessLevel.Granted)
                {

                    raspisanie rasp = new raspisanie();
                    rasp.Show();
                    //rasp.edit.Visible = true;
                    rasp.clear.Visible = true;
                   this.Hide();

                }
                else
                {
                    raspisanie rasp = new raspisanie();
                    rasp.Show();
                    //rasp.edit.Visible = false;
                    rasp.clear.Visible = false;
                    this.Hide();
                }

            }

            db.closeConnection();
        }

        private void materialFlatButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MainForm MainForm = new MainForm();
            MainForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exit_MouseLeave(object sender, EventArgs e)
        {
            exit.ForeColor = Color.White;//при убирании мыши меняет цвет на белый
        }
        private void exit_MouseEnter(object sender, EventArgs e)
        {
            exit.ForeColor = Color.Red;//при наведение меняет цвет на красный
        }
        Point lastPoint;
        private void application_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
                //код для перемещения окна
            }
        }

        private void application_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);//код для перемещения
        }
    }
}
