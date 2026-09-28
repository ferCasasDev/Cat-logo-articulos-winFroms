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

        public static void ConfigurarBotoPrincipal (Button boton, int x, int y)
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

        public static void ConfigurarBotoSecundario(Button boton, int x, int y)
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

        public static void ConfigurarFormulario(Form formulario)
        {
            formulario.BackColor = Color.FromArgb(245, 246, 248);
        }

    }
}
