using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace packman
{
    public partial class eattext : Form
    {
        public eattext()
        {
            InitializeComponent();
        }
        bool EatMonster = false;
        int coin = 0;
        //Количество пикселей для перемещения
        int move = 10;

        //Направление движения для пакмена
        int direction = 1;

        //Направление движения для монстра
        int direction_red = 1;
        //Направление движения для монстра
        int direction_red1 = 2;
        //Направление движения для монстра
        int direction_red2 = 3;
        //Направление движения для монстра
        int direction_red3 = 5;

        //Создаем счетчик произвольных чисел
        Random rnd = new Random();

        void up(Control element, int distance)
        {
            int x = element.Top;
            x = x - distance;

            if (x <= 0 - element.Height)
            {
                x = this.Height;
            }

            element.Top = x;
        }

        void down(Control element, int distance)
        {
            int x = element.Top;
            x = x + distance;

            if (x >= this.Height)
            {
                x = 0 - element.Height;
            }

            element.Top = x;
        }

        void left(Control element, int distance)
        {
            int x = element.Left;
            x = x - distance;

            if (x <= 0 - element.Width)
            {
                x = this.Width;
            }

            element.Left = x;
        }

        void right(Control element, int distance)
        {
            int x = element.Left;
            x = x + distance;

            if (x >= this.Width)
            {
                x = 0 - element.Width;
            }

            element.Left = x;
        }

        void move_step(Control element, int distance, int direction)
        {
            if (direction == 1)
                up(element, distance);
            if (direction == 2)
                down(element, distance);
            if (direction == 3)
                left(element, distance);
            if (direction == 4)
                right(element, distance);
        }

        private Control[] Colision(Control target)
        {
            List<Control> data = new List<Control>();
            foreach (Control element in Controls)
            {
                if (element == target) continue;

                if (element.Bounds.IntersectsWith(target.Bounds))
                {
                    data.Add(element);
                }
            }

            return data.ToArray();
        }

        private void move_timer_Tick(object sender, EventArgs e)
        {
            //Сохраняем координаты пакмена
            var coords = btn_shar.Bounds;
            //Даем пакмену пройти
            move_step(btn_shar, move, direction);


            //Получаем все элементы на которые налетел пакмен
            var objects = Colision(btn_shar);
            foreach (var element in objects) //пробегаемся по элементам
            {
                //И если это стена
                if (element.Tag != null)
                {
                    if (coin == 6)
                    {
                        label1.Text = "Game over. You WIN!";
                        Controls.Remove(btn_shar);
                        Controls.Remove(btn_red1);
                        Controls.Remove(btn_red2);
                        Controls.Remove(btn_red);
                    }
                    if (element.Tag.ToString() == "wall")
                        //мы отбрасываем пакмена от стены
                        btn_shar.Bounds = coords;
                    if (element.Tag.ToString() == "coin" ||
                        element.Tag.ToString() == "supcoin")
                    {
                        
                        coin++;
                        label1.Text = "Текущий счёт: " + coin;
                        if (element.Tag.ToString() == "supcoin")
                        {
                            EatTimer.Start();
                            EatMonster = true;
                        }
                        Controls.Remove(element);
                    }
                    
                }
            }

            moveMonster(btn_red, ref direction_red);
            moveMonster(btn_red1, ref direction_red1);
            moveMonster(btn_red2, ref direction_red2);

        }



        void moveMonster(Control monster, ref int direction)
        {
            //Сохраняем координаты монстра
            var coords_red = monster.Bounds;
            //Даем монстру пройти
            move_step(monster, move, direction);

            //Получаем все элементы на которые налетел монст
            var objects = Colision(monster);
            foreach (var element in objects)
            {
                if (element == btn_shar)
                {
                    if (EatMonster)
                    {
                        Controls.Remove(monster);
                        return;
                    } else {
                        Controls.Remove(btn_shar);


                        label1.Text = "Ваш счёт: " + coin + '\n' + "Нажмите для запуска";
                        label1.Text = "Ваш счёт: " + coin + '\n' + "Нажмите для запуска";
                        label1.Text = "Ваш счёт: " + coin + '\n' + "Нажмите для запуска";
                        label1.Text = "Ваш счёт: " + coin + '\n' + "Нажмите для запуска";
                        label1.Text = "Ваш счёт: " + coin + '\n' + "Нажмите для запуска";
                    }

                }

                if (element.Tag == null) continue;
                if (element.Tag.ToString() == "wall")
                {
                    monster.Bounds = coords_red;
                    direction = rnd.Next(1, 5);

                }
            }

            //Произвольная смена направления
            if (rnd.Next(0, 100) > 90)
                direction = rnd.Next(1, 5);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
                direction = 1;
            if (e.KeyChar == 's')
                direction = 2;
            if (e.KeyChar == 'a')
                direction = 3;
            if (e.KeyChar == 'd')
                direction = 4;
            if (e.KeyChar == 'k')
                coin = 6;




        }
        private void OK_button_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.E)
            {
                coin = 6;
                
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_shar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_red2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Controls.Add(btn_shar);
            label1.Text = "Текущий счёт: " + coin;

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EatTimer_Tick(object sender, EventArgs e)
        {
            EatMonster = false;
            EatTimer.Stop();

        }
    }
}
