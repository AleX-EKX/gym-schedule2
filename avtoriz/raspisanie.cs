using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace avtoriz
{
    public partial class raspisanie : Form
    {
        public raspisanie()
        {
            InitializeComponent();

            DateTime today = DateTime.Now;
            DateTime answer = today.AddDays(6);
            nedelya.Text = today.DayOfWeek.ToString();
            int daysLeft;
            switch (nedelya.Text)
            {
                case "Monday":
                    daysLeft = 6; break;
                case "Tuesday":
                    daysLeft = 5;
                    break;
                case "Wednesday":
                    daysLeft = 4;
                    break;
                case "Thursday":
                    daysLeft = 3;
                    break;
                case "Friday":
                    daysLeft = 2;
                    break;
                case "Saturday":
                    daysLeft = 1;
                    break;
                default:
                    daysLeft = 0;
                    break;
            }
            label17.Text = "Расписание зала до " + today.AddDays(daysLeft).ToString("d");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.Day.ToString("") + "." + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Year.ToString("00") + "\r\n " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
        }

        private void main_btn_Click(object sender, EventArgs e)
        { 
           // string loginuser = log.LogField.Text; //получаем данные от пользователя  
            DB db = new DB();
            //создаем табл
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` where `login`= @ul", db.GetConnection());// выбрать всех пользователь где логин равен тому что ввел пользователь и пароль также

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginForm1.loginuser;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            this.DialogResult = DialogResult.OK;
            loginForm1 log = new loginForm1();
            this.Close();
            MainForm main = new MainForm();
            main.Show();

        }

        private  void clear_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            Clears cle = new Clears();
            cle.Show();
        }

        private void raspisanie_Load(object sender, EventArgs e)
        {
            DB bb = new DB();
            //создаем табл
            DataTable tablee = new DataTable();

            MySqlDataAdapter adapterr = new MySqlDataAdapter();

            MySqlCommand commandd = new MySqlCommand("SELECT * FROM `users` ", bb.GetConnection());// выбрать всех пользователь 
            
            adapterr.SelectCommand = commandd;
            adapterr.Fill(tablee);

            bb.openConnection();

            var readerr = commandd.ExecuteReader();
            
                if (readerr.Read())
                {
                    Loggeduser.AccessLevel = (AccessLevel)Convert.ToInt32(readerr["user_permission"]);

                    if (Loggeduser.AccessLevel == AccessLevel.Granted)//ур доступа
                    {
                       // edit.Visible = true;
                        clear.Visible = false;
                    }
                    else
                    {
                        clear.Visible = true;
                    }

                }
            
            bb.closeConnection();

            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `mail` ", db.GetConnection());// 

            command.Parameters.Add("@kod", MySqlDbType.VarChar).Value = data.vremy;
            adapter.SelectCommand = command;
            adapter.Fill(table);//данные трансформируем внутрь обекта табл

            db.openConnection();

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    data.bron = (bron)Convert.ToInt32(reader["kod"]);

                    if (data.bron == bron.Denied)//ур доступа
                    {
                        this.Show();
                    }

                    else if (data.bron >= bron.ponedelnik16 && data.bron <= bron.ponedelnik22)
                    {
                        int index = (int)(data.bron - (int)bron.ponedelnik16 + 16);
                        Button button = (Button)Controls.Find("pn_btn_" + index, true)[0];
                        button.Enabled = false;
                        button.Text = "Занято";
                        button.BackColor = Color.Red;
                    }

                    else if (data.bron >= bron.vtornik16 && data.bron <= bron.vtornik22)
                    {
                        int index = (int)(data.bron - (int)bron.vtornik16 + 16);
                        Button button = (Button)Controls.Find("vt_btn_" + index, true)[0];
                        button.Enabled = false;
                        button.Text = "Занято";
                        button.BackColor = Color.Red;
                    }
                    else if (data.bron >= bron.creda16 && data.bron <= bron.creda22)
                    {
                        int index = (int)(data.bron - (int)bron.creda16 + 16);
                        Button button = (Button)Controls.Find("cr_btn_" + index, true)[0];
                        button.Enabled = false;
                        button.Text = "Занято";
                        button.BackColor = Color.Red;
                    }
                    else if (data.bron >= bron.chetverg16 && data.bron <= bron.chetverg22)
                    {
                        int index = (int)(data.bron - (int)bron.chetverg16 + 16);
                        Button button = (Button)Controls.Find("cht_btn_" + index, true)[0];
                        button.Enabled = false;
                        button.Text = "Занято";
                        button.BackColor = Color.Red;
                    }
                    else if (data.bron >= bron.pyatnitsa16 && data.bron <= bron.pyatnitsa22)
                    {
                        int index = (int)(data.bron - (int)bron.pyatnitsa16 + 16);
                        Button button = (Button)Controls.Find("pt_btn_" + index, true)[0];
                        button.Enabled = false;
                        button.Text = "Занято";
                        button.BackColor = Color.Red;
                    }
                    else if (data.bron >= bron.subbota8 && data.bron <= bron.subbota22)
                    {
                        int index = (int)(data.bron - (int)bron.subbota8 + 8);
                        Button button = (Button)Controls.Find("sb_btn_" + index, true)[0];
                        button.Enabled = false;
                        button.Text = "Занято";
                        button.BackColor = Color.Red;
                    }
                    else if (data.bron >= bron.voskresene8 && data.bron <= bron.voskresene22)
                    {
                        int index = (int)(data.bron - (int)bron.voskresene8 + 8);
                        Button button = (Button)Controls.Find("vs_btn_" + index, true)[0];
                        button.Enabled = false;
                        button.Text = "Занято";
                        button.BackColor = Color.Red;
                    }
                }
                       
            }
            db.closeConnection();
        }


        Point lastPoint;
        private void raspisanie_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
                //код для перемещения окна
            }
        }

        private void raspisanie_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);//код для перемещения
        }

        private void main_btn_MouseLeave(object sender, EventArgs e)
        {
            //main_btn.ForeColor = Color.White;
        }

        private void main_btn_MouseEnter(object sender, EventArgs e)
        {
            //main_btn.ForeColor = Color.LightSkyBlue;
        }

        private void pn_lab_MouseEnter(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { pn_btn_16, pn_btn_17, pn_btn_18, pn_btn_19, pn_btn_20, pn_btn_21, pn_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Lime;
                    i.Enabled = true;
                }
            }

        }

        private void pn_lab_MouseLeave(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { pn_btn_16, pn_btn_17, pn_btn_18, pn_btn_19, pn_btn_20, pn_btn_21, pn_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Transparent;
                    i.Enabled = true;
                }
            }
        }

        private void vt_lab_MouseEnter(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { vt_btn_16, vt_btn_17, vt_btn_18, vt_btn_19, vt_btn_20, vt_btn_21, vt_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Lime;
                    i.Enabled = true;
                }

            }
        }

        private void vt_lab_MouseLeave(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { vt_btn_16, vt_btn_17, vt_btn_18, vt_btn_19, vt_btn_20, vt_btn_21, vt_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Transparent;
                    i.Enabled = true;
                }
            }
        }

        private void sr_lab_MouseEnter(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { cr_btn_16, cr_btn_17, cr_btn_18, cr_btn_19, cr_btn_20, cr_btn_21, cr_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Lime;
                    i.Enabled = true;
                }
            }
        }

        private void sr_lab_MouseLeave(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { cr_btn_16, cr_btn_17, cr_btn_18, cr_btn_19, cr_btn_20, cr_btn_21, cr_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Transparent;
                    i.Enabled = true;
                }
            }
        }

        private void cht_lab_MouseEnter(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { cht_btn_16, cht_btn_17, cht_btn_18, cht_btn_19, cht_btn_20, cht_btn_21, cht_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Lime;
                    i.Enabled = true;
                }
            }
        }

        private void cht_lab_MouseLeave(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { cht_btn_16, cht_btn_17, cht_btn_18, cht_btn_19, cht_btn_20, cht_btn_21, cht_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Transparent;
                    i.Enabled = true;
                }
            }
        }

        private void pt_lab_MouseEnter(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { pt_btn_16, pt_btn_17, pt_btn_18, pt_btn_19, pt_btn_20, pt_btn_21, pt_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Lime;
                    i.Enabled = true;
                }
            }
        }

        private void pt_lab_MouseLeave(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { pt_btn_16, pt_btn_17, pt_btn_18, pt_btn_19, pt_btn_20, pt_btn_21, pt_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Transparent;
                    i.Enabled = true;
                }
            }
        }

        private void sb_lab_MouseEnter(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { sb_btn_8, sb_btn_9, sb_btn_10, sb_btn_11, sb_btn_12, sb_btn_13, sb_btn_14, sb_btn_15, sb_btn_16, sb_btn_17, sb_btn_18, sb_btn_19, sb_btn_20, sb_btn_21, sb_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Tomato;
                    i.Enabled = true;
                }
            }
        }

        private void sb_lab_MouseLeave(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { sb_btn_8, sb_btn_9, sb_btn_10, sb_btn_11, sb_btn_12, sb_btn_13, sb_btn_14, sb_btn_15, sb_btn_16, sb_btn_17, sb_btn_18, sb_btn_19, sb_btn_20, sb_btn_21, sb_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Transparent;
                    i.Enabled = true;
                }
            }
        }

        private void vs_lab_MouseEnter(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { vs_btn_8, vs_btn_9, vs_btn_10, vs_btn_11, vs_btn_12, vs_btn_13, vs_btn_14, vs_btn_15, vs_btn_16, vs_btn_17, vs_btn_18, vs_btn_19, vs_btn_20, vs_btn_21, vs_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Tomato;
                    i.Enabled = true;
                }
            }
        }

        private void vs_lab_MouseLeave(object sender, EventArgs e)
        {
            Button[] bt = new Button[] { vs_btn_8, vs_btn_9, vs_btn_10, vs_btn_11, vs_btn_12, vs_btn_13, vs_btn_14, vs_btn_15, vs_btn_16, vs_btn_17, vs_btn_18, vs_btn_19, vs_btn_20, vs_btn_21, vs_btn_22 };
            foreach (Button i in bt)
            {
                if (i.Text == "Занято")
                {
                    i.Enabled = false;
                    i.BackColor = Color.Red;
                }
                else
                {
                    i.BackColor = Color.Transparent;
                    i.Enabled = true;
                }
            }

        }
        //нажатие др форма показ (Суббота)
        application apl = new application();

        private void pn_btn_Click(object sender, EventArgs e)
        {
            if (zareg())
                return;
            Button button = (Button)sender;
            string id = button.Name;
            string time = id.Substring(id.Length - 2) + ":30";
            SetButtonData("Понедельник " + time, "400 рублей", id, (int.Parse(id.Substring(id.Length - 2)) - 16 + 1).ToString());
        }

        private void vt_btn_16_Click(object sender, EventArgs e)
        {
            if (zareg())
                return;
            Button button = (Button)sender;
            string id = button.Name;
            string time = id.Substring(id.Length - 2) + ":30";
            SetButtonData("Вторник " + time, "400 рублей", id, (int.Parse(id.Substring(id.Length - 2)) - 16 + 8).ToString());    
        }
        private void cr_btn_16_Click(object sender, EventArgs e)
        {
            if (zareg())
                return;
            Button button = (Button)sender;
            string id = button.Name;
            string time = id.Substring(id.Length - 2) + ":30";
            SetButtonData("Среда " + time, "400 рублей", id, (int.Parse(id.Substring(id.Length - 2)) - 16 + 15).ToString());
        }
        private void cht_btn_16_Click(object sender, EventArgs e)
        {
            if (zareg())
                return;
            Button button = (Button)sender;
            string id = button.Name;
            string time = id.Substring(id.Length - 2) + ":30";
            SetButtonData("Четверг " + time, "400 рублей", id, (int.Parse(id.Substring(id.Length - 2)) - 16 + 22).ToString());
        }
        private void pt_btn_16_Click(object sender, EventArgs e)
        {
            if (zareg())
                return;
            Button button = (Button)sender;
            string id = button.Name;
            string time = id.Substring(id.Length - 2) + ":30";
            SetButtonData("Пятница " + time, "400 рублей", id, (int.Parse(id.Substring(id.Length - 2)) - 16 + 29).ToString());
        }

        private void SetButtonData(string chislo, string cena, string per_id_bt, string vremy)
        {
            if (zareg()) return;
            this.DialogResult = DialogResult.OK; apl.Show();
            apl.chislo.Text = chislo; apl.cena.Text = cena;
            Program.per_id_bt = per_id_bt;
            data.vremy = vremy;
            this.Hide();
        }

        //-----------------ВЫХОДНЫЕ----------------------WEEKEND--------------------------------------------
        private void SetButtonData(Button button, string time, string price, string programId, string kodvr)
        {
            if (zareg())
                return;
            this.DialogResult = DialogResult.OK;
            apl.Show();
            apl.chislo.Text = time;
            apl.cena.Text = price;
            Program.per_id_bt = programId;
            data.vremy = kodvr;
            this.Hide();
        }

        private void myfunc(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Name)
            {
                case "sb_btn_8":
                    SetButtonData(button, "Суббота 8:30", "300 рублей", "sb_btn_8", "36");
                    break;
                case "sb_btn_9":
                    SetButtonData(button, "Суббота 9:30", "300 рублей", "sb_btn_9","37");
                    break;
                case "sb_btn_10":
                    SetButtonData(button, "Суббота 10:30", "300 рублей", "sb_btn_10", "38");
                    break;
                case "sb_btn_11":
                    SetButtonData(button, "Суббота 11:30", "300 рублей", "sb_btn_11", "39");
                    break;
                case "sb_btn_12":
                    SetButtonData(button, "Суббота 12:30", "300 рублей", "sb_btn_12", "40");
                    break;
                case "sb_btn_13":
                    SetButtonData(button, "Суббота 13:30", "300 рублей", "sb_btn_13", "41");
                    break;
                case "sb_btn_14":
                    SetButtonData(button, "Суббота 14:30", "300 рублей", "sb_btn_14", "42");
                    break;
                case "sb_btn_15":
                    SetButtonData(button, "Суббота 15:30", "400 рублей", "sb_btn_15", "43");
                    break;
                case "sb_btn_16":
                    SetButtonData(button, "Суббота 16:30", "400 рублей", "sb_btn_16", "44");
                    break;
                case "sb_btn_17":
                    SetButtonData(button, "Суббота 17:30", "400 рублей", "sb_btn_17", "45");
                    break;
                case "sb_btn_18":
                    SetButtonData(button, "Суббота 18:30", "400 рублей", "sb_btn_18", "46");
                    break;
                case "sb_btn_19":
                    SetButtonData(button, "Суббота 19:30", "400 рублей", "sb_btn_19", "47");
                    break;
                case "sb_btn_20":
                    SetButtonData(button, "Суббота 20:30", "400 рублей", "sb_btn_20", "48");
                    break;
                case "sb_btn_21":
                    SetButtonData(button, "Суббота 21:30", "400 рублей", "sb_btn_21", "49");
                    break;
                case "sb_btn_22":
                    SetButtonData(button, "Суббота 22:30", "400 рублей", "sb_btn_22", "50");
                    break;
                default:
                    break;
            }
            switch (button.Name)
            {
                case "vs_btn_8": SetButtonData(button, "Воскресенье 8:30", "300 рублей", "vs_btn_8", "51"); break;
                case "vs_btn_9": SetButtonData(button, "Воскресенье 9:30", "300 рублей", "vs_btn_9", "52"); break;
                case "vs_btn_10": SetButtonData(button, "Воскресенье 10:30", "300 рублей", "vs_btn_10", "53"); break;
                case "vs_btn_11": SetButtonData(button, "Воскресенье 11:30", "300 рублей", "vs_btn_11", "54"); break;
                case "vs_btn_12": SetButtonData(button, "Воскресенье 12:30", "300 рублей", "vs_btn_12", "55"); break;
                case "vs_btn_13": SetButtonData(button, "Воскресенье 13:30", "300 рублей", "vs_btn_13", "56"); break;
                case "vs_btn_14": SetButtonData(button, "Воскресенье 14:30", "300 рублей", "vs_btn_14", "57"); break;
                case "vs_btn_15": SetButtonData(button, "Воскресенье 15:30", "400 рублей", "vs_btn_15", "58"); break;
                case "vs_btn_16": SetButtonData(button, "Воскресенье 16:30", "400 рублей", "vs_btn_16", "59"); break;
                case "vs_btn_17": SetButtonData(button, "Воскресенье 17:30", "400 рублей", "vs_btn_17", "60"); break;
                case "vs_btn_18": SetButtonData(button, "Воскресенье 18:30", "400 рублей", "vs_btn_18", "61"); break;
                case "vs_btn_19": SetButtonData(button, "Воскресенье 19:30", "400 рублей", "vs_btn_19", "62"); break;
                case "vs_btn_20": SetButtonData(button, "Воскресенье 20:30", "400 рублей", "vs_btn_20", "63"); break;
                case "vs_btn_21": SetButtonData(button, "Воскресенье 21:30", "400 рублей", "vs_btn_21", "64"); break;
                case "vs_btn_22": SetButtonData(button, "Воскресенье 22:30", "400 рублей", "vs_btn_22", "65"); break;
                default: break;
            }
        }

      /*  private void sb_btn_22_Click(object sender, EventArgs e)
        {
            if (zareg())
                return;
            this.DialogResult = DialogResult.OK;
            apl.Show();
            apl.chislo.Text = "Суббота 22:30";
            apl.cena.Text = "400 рублей";
            Program.per_id_bt = "sb_btn_22";
            data.vremy = "50";
            this.Hide();
        }*/

        public Boolean zareg()
        {
            MainForm main = new MainForm();
            if (main.Labuser.Text == "")
            {
                MessageBox.Show(" Сначала зарегистрируйтесь");
                return true;
            }
            else
                return false;
        }

        private void update_lab_Click(object sender, EventArgs e)
        {
            this.Close();
            raspisanie ras = new raspisanie();
            ras.Show();
        }

       
    }
}
