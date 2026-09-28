using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPFinalNivel_Casas.UI
{
    public static class EstilosUI
    {

        //Paleta 
        private static readonly Color colorPrimario = Color.FromArgb(52, 73, 94);

        private static readonly Color colorAcento = Color.FromArgb(41, 128, 185);

        private static readonly Color colorTexto = Color.FromArgb(45, 45, 45);

        private static readonly Color colorFondo = Color.FromArgb(245, 247, 250);

        private static readonly Color colorBorde = Color.FromArgb(180, 180, 180);

        private static readonly Color colorFondoControl = Color.FromArgb(250, 250, 250);

        public static void ConfigurarTextBox (TextBox textBox, Panel panel)
        {
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = colorFondoControl;
            textBox.ForeColor = Color.FromArgb(45, 45, 45);
            textBox.Font = new Font("Segoe UI", 12F);

            panel.BackColor = colorBorde;
            panel.Height = textBox.Height + 3;

            textBox.Location = new Point(0, 0);
            textBox.Width = panel.Width;



            textBox.Enter += (Sender, e) =>
            {
                panel.BackColor = colorAcento;
            };

            textBox.Leave += (Sender, e) =>
            {
                panel.BackColor = Color.FromArgb(180, 180, 180);
            };

        }

        //Botón Primario
        public static void ConfigurarBotonPrincipal (Button boton, int x, int y)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Location= new Point(x, y);

            boton.Size = new Size (115, 40);

            boton.BackColor = Color.FromArgb(52, 58, 64);
            boton.ForeColor = Color.White;
            boton.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
                ); // definimos el formato del font

            boton.Cursor = Cursors.Default;// flecha común 
        }

        //Botón Secundario
        public static void ConfigurarBotonSecundario(Button boton, int x, int y)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Location = new Point(x, y);

            boton.Size = new Size(115, 30);

            boton.BackColor = Color.FromArgb(206, 212, 218);
            boton.ForeColor = Color.FromArgb(52, 58, 64);
            boton.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Regular
                ); // definimos el formato del font

            boton.Cursor = Cursors.Default;// flecha común 
        }

        //Formulario
        public static void ConfigurarFormulario(Form formulario)
        {
            formulario.BackColor = Color.FromArgb(245, 246, 248);
        }

    }
}
