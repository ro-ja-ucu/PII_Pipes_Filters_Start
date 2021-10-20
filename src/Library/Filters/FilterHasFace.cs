using System.Drawing;
using CognitiveCoreUCU;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y la retorna en escala de grises.
    /// </remarks>
    public class FilterHasFace : IFilterConditional
    {
        private string path;
        private bool faceFound;

        public FilterHasFace(string path)
        {
            this.path = path;
            this.faceFound = false;
        }

        public bool ConditionResult { get => this.faceFound; }

        /// <summary>
        /// Un filtro que retorna la imagen recibida con un filtro de escala de grises aplicado.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida pero en escala de grises.</returns>
        public IPicture Filter(IPicture image)
        {
            this.Recognize();
            return image;
        }

        private void Recognize()
        {
            CognitiveFace cognitiveAPI = new CognitiveFace(true, Color.GreenYellow);
            cognitiveAPI.Recognize(path);
            faceFound = cognitiveAPI.FaceFound;
        }
    }
}
