using FluentValidation;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.ResourcesReader;
using SharedKernal.Middlewares.ResourcesReader.Message;
using System.Linq;

namespace OnlineShoping.Application.Validations
{
    public class ProductInputValidation : AbstractValidator<ProductInputDTO>
    {
        private readonly IMessageResourceReader _messageResourceReader;
        private readonly IProductRepository _ProductRepository;

        public ProductInputValidation(IMessageResourceReader messageResourceReader, IProductRepository ProductRepository)
        {
            _messageResourceReader = messageResourceReader;
            _ProductRepository = ProductRepository;
            IsValid();
        }
        void IsValid()
        {
            RuleFor(x => x.CategoryId).Must((cateId) => { return CheckProductCategory(cateId); })
                  .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ProductCategoryValidation));

            RuleFor(x => x.AvalaibleQuentity).Must((avalaibleQuentity) => { return CheckAvalaibleQuentity(avalaibleQuentity); })
            .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ProductAvalaibleQuentityValidation));

            RuleFor(x => x.ImageURL).Must((imageURL) => { return CheckProductImage(imageURL); })
            .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ProductImageValidation));

            RuleFor(x => x.NameEn).Must((cateNameEn) => { return CheckProductNameEn(cateNameEn); })
                              .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.EnglishNameValidation));

            RuleFor(x => x.NameAr).Must((cateNameAr) => { return CheckProductNameAr(cateNameAr); })
                              .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ArabicNameValidation));


            RuleFor(x => new { x.NameEn, x.NameAr }).Must((model, name) => { return CheckDuplicateCategoyName(model.Id, name.NameAr, name.NameEn); })
                .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.UserNameAlreadyExist));



        }

        bool CheckProductCategory(int arg) => (arg > default(int));
        bool CheckAvalaibleQuentity(double arg) => (arg > default(double));
        bool CheckProductImage(string arg) => !(string.IsNullOrWhiteSpace(arg));
        bool CheckProductNameEn(string arg) => !(string.IsNullOrWhiteSpace(arg) || arg.Length < 5 || arg.Length > 50);
        bool CheckProductNameAr(string arg) => !(string.IsNullOrWhiteSpace(arg) || arg.Length < 5 || arg.Length > 50);

        bool CheckDuplicateCategoyName(int id, string arg1, string arg2)
        {
            Product ProductObj = _ProductRepository.Get(x => x.Id != id && x.NameEn.Trim().ToLower() == arg1.Trim().ToLower() && x.NameEn.Trim().ToLower() == arg2.Trim().ToLower()).FirstOrDefault();
            return ProductObj is null;
        }
    }
}

