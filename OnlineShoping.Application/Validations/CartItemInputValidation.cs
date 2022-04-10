using FluentValidation;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.ResourcesReader.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.Validations
{
    public class CartItemInputValidation : AbstractValidator<CartItemInputDTO>
    {
        private readonly IMessageResourceReader _messageResourceReader;
        private readonly ICartItemRepository _cartItemRepository;
        //private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartItemInputValidation(IMessageResourceReader messageResourceReader, 
                                       ICartItemRepository cartItemRepository, 
                                       IProductRepository productRepository)
        {
            _messageResourceReader = messageResourceReader;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            IsValid();
        }

        void IsValid()
        {
            //RuleFor(x => x.CartId).Must((total) => { return CheckCart(total); })
            //                  .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.EnglishNameValidation));

            //RuleFor(x => x.UserId).Must((UserId) => { return CheckUserId(UserId); })
            //                  .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ArabicNameValidation));
            //.Must((UserId) => { return CheckCartDublicate(UserId); })
            //.WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ArabicNameValidation));
        }
    }
}
