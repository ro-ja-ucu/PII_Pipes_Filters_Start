using System;
using System.Drawing;
using CompAndDel;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro de convolución que retorna la imagen recibida con los bordes suavizados. Basado en
    /// https://en.wikipedia.org/wiki/Box_blur utilizando el kernel
    /// https://wikimedia.org/api/rest_v1/media/math/render/svg/91256bfeece3344f8602e288d445e6422c8b8a1c.
    /// </summary>
    public abstract class FilterConvolution : IFilter
    {
        protected int[,] kernel;
        protected int complement, divider;

        /// Procesa la imagen pasada por parametro mediante un kernel, y retorna la imagen resultante.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen con el filtro aplicado.</returns>
        public IPicture Filter(IPicture image)
        {
            IPicture result = image.Clone();
            Color[,] sample;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    sample = CreateSample(image, x, y);
                    result.SetColor(x, y, GetFilteredColor(sample));
                }
            }

            return result;
        }

        private Color GetFilteredColor(Color[,] sample)
        {
            int redFinal = 0;
            int greenFinal = 0;
            int blueFinal = 0;

            for (int x = 0; x < sample.GetLength(0); x++)
            {
                for (int y = 0; y < sample.GetLength(1); y++)
                {
                    redFinal += sample[x, y].R * this.kernel[x, y];
                    greenFinal += sample[x, y].G * this.kernel[x, y];
                    blueFinal += sample[x, y].B * this.kernel[x, y];
                }
            }

            redFinal = Math.Abs((redFinal/this.divider) + this.complement);
            redFinal = Math.Min(255, redFinal);

            greenFinal = Math.Abs((greenFinal / this.divider) + this.complement);
            greenFinal = Math.Min(255, greenFinal);

            blueFinal = Math.Abs((blueFinal / this.divider) + this.complement);
            blueFinal = Math.Min(255, blueFinal);

            return Color.FromArgb(redFinal, greenFinal, blueFinal);
        }

        private Color[,] CreateSample(IPicture image, int x, int y)
        {
            Color[,] sample = new Color[3,3];

            sample[0,0] = image.GetColor(Math.Max(x-1, 0), Math.Max(y-1,0));
            sample[1,0] = image.GetColor(x, Math.Max(y-1,0));
            sample[2,0] = image.GetColor(Math.Min(x+1, image.Width -1), Math.Max(y-1,0));
            sample[0,1] = image.GetColor(Math.Max(x-1, 0), y);
            sample[1,1] = image.GetColor(x, y);
            sample[2,1] = image.GetColor(Math.Min(x+1, image.Width - 1),y);
            sample[0,2] = image.GetColor(Math.Max(x-1, 0), Math.Min(y+1,image.Height - 1));
            sample[1,2] = image.GetColor(x, Math.Min(y+1,image.Height - 1));
            sample[2,2] = image.GetColor(Math.Min(x+1, image.Width - 1), Math.Min(y+1,image.Height - 1));

            return sample;
        }
    }
}