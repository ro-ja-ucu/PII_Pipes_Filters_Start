namespace CompAndDel.Filters
{
    public class FilterSaveLocal : IFilter
    {
        private string path;
        /// <summary>
        /// Un filtro que retorna una imagen 
        /// despues de guardarla al directorio 
        /// del programa
        /// </summary>
        /// <param name="image">La imagen la cual se va a salvar.</param>
        /// <returns>La misma imagen recibida.</returns>
        public IPicture Filter(IPicture image)
        {
            PictureProvider picProvider = new PictureProvider();
            picProvider.SavePicture(image, path);
            return image;
        }

        public FilterSaveLocal(string path)
        {
            this.path = path;
        }
    }
}