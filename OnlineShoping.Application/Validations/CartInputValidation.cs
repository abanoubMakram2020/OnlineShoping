using FluentValidation;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.ResourcesReader;
using SharedKernal.Middlewares.ResourcesReader.Message;

namespace OnlineShoping.Application.Validations
{
    public class CartInputValidation : AbstractValidator<CartInputDTO>
    {
        private readonly IMessageResourceReader _messageResourceReader;
        private readonly ICartRepository _cartRepository;

        public CartInputValidation(IMessageResourceReader messageResourceReader, ICartRepository cartRepository)
        {
            _messageResourceReader = messageResourceReader;
            _cartRepository = cartRepository;
            IsValid();
        }

        void IsValid()
        {
            //RuleFor(x => x.Total).Must((total) => { return CheckTotal(total); })
            //                  .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.EnglishNameValidation));

            RuleFor(x => x.UserId).Must((UserId) => { return CheckUserId(UserId); })
                              .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ArabicNameValidation));
                              //.Must((UserId) => { return CheckCartDublicate(UserId); })
                              //.WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ArabicNameValidation));


        }

        //bool CheckTotal(string arg) => !(string.IsNullOrWhiteSpace(arg) || arg.Length < 5 || arg.Length > 50);
        bool CheckUserId(int arg) => (arg > default(int));
        //bool CheckCartDublicate(int arg) => (arg > default(int));
    }
}
