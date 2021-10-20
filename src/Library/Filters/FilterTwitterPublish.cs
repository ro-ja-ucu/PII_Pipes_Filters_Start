using TwitterUCU;

namespace CompAndDel.Filters
{
    public class FilterTwitterPublish : IFilter
    {
        private string path;
        private string text;

        public FilterTwitterPublish(string path, string text)
        {
            this.path = path;
            this.text = text;
        }

        public IPicture Filter(IPicture image)
        {
            TwitterImage twitterImage = new TwitterImage();
            twitterImage.PublishToTwitter(text, path);
            return image;
        }
    }
}