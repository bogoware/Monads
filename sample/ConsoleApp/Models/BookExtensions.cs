namespace Sample.Models;

public static class BookExtensions
{
	public static string GetPrintableTitle(this Book book)
		=> book
			.Author
			.Map(author => $"{book.Title} by {author.GetFullName()}")
			.GetValue(() => book.Title);
}