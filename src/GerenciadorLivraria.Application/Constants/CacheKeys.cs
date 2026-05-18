namespace GerenciadorLivraria.Application.Constants
{
    public static class CacheKeys
    {
        public static string BooksAll(int page, int size)
            => $"books:all:page:{page}:size:{size}";

        public static string BookById(Guid id)
            => $"books:id:{id}";

        public static string BooksSearch(string term)
            => $"books:search:{term.ToLower().Trim()}";
    }
}