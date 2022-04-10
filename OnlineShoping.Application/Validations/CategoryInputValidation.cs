using FluentValidation;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Domain.RepositoryInterfaces;
using SharedKernal.Middlewares.ResourcesReader;
using SharedKernal.Middlewares.ResourcesReader.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoping.Application.Validations
{
    public class CategoryInputValidation : AbstractValidator<CategoryInputDTO>
    {
        private readonly IMessageResourceReader _messageResourceReader;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryInputValidation(IMessageResourceReader messageResourceReader, ICategoryRepository categoryRepository)
        {
            _messageResourceReader = messageResourceReader;
            _categoryRepository = categoryRepository;
            IsValid();
        }
        void IsValid()
        {
            RuleFor(x => x.NameEn).Must((cateNameEn) => { return CheckCategoryNameEn(cateNameEn); })
                              .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.EnglishNameValidation));

            RuleFor(x => x.NameAr).Must((cateNameAr) => { return CheckCategoryNameAr(cateNameAr); })
                              .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ArabicNameValidation));


            RuleFor(x => x).Must((model) => { return CheckDuplicateCategoyName(model); })
                .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.ItemAlreadyExist));

        }



        bool CheckCategoryNameEn(string arg) => !(string.IsNullOrWhiteSpace(arg) || arg.Length < 5 || arg.Length > 50);
        bool CheckCategoryNameAr(string arg) => !(string.IsNullOrWhiteSpace(arg) || arg.Length < 5 || arg.Length > 50);

        bool CheckDuplicateCategoyName(CategoryInputDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.NameAr) || string.IsNullOrWhiteSpace(model.NameEn))
                return true;
            Category categoryObj = _categoryRepository.Get(x => x.Id != model.Id && (x.NameEn.Trim().ToLower() == model.NameEn.Trim().ToLower() || x.NameEn.Trim().ToLower() == model.NameAr.Trim().ToLower())).FirstOrDefault();
            return categoryObj is null;
        }

    }
}

