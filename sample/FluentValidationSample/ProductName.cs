using Bogoware.Monads;
using FluentValidation;

namespace FluentValidationSample;

public class ProductName
{
	public class Validator: AbstractValidator<ProductName>
	{
		public const int NameMaxLength = 10;
		public Validator()
		{
			RuleFor(x => x.Name).NotEmpty().MaximumLength(NameMaxLength);
		}
	}
	
	public string Name { get; }
	private ProductName(string name)
	{
		Name = name;
	}

	public static Result<ProductName> Create(string name)
	{
		var instance = new ProductName(name);
		var validationResult = new Validator().Validate(instance);
		
		if (!validationResult.IsValid) return 
			new ValidationError(typeof(ProductName), validationResult.Errors);

		return instance;
	}
}