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
                              .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.CategoryEnglishNameValidation));
            {
                RuleFor(x => x.NameAr).Must((cateNameAr) => { return CheckCategoryNameAr(cateNameAr); })
                                  .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.CategoryArabicNameValidation));


                RuleFor(x => new { x.NameEn, x.NameAr }).Must((model, name) => { return CheckDuplicateCategoyName(model.Id, name.NameAr, name.NameEn); })
                    .WithMessage(_messageResourceReader.GetValidationMessage(ValidationMessageKey.UserNameAlreadyExist));



            }

            bool CheckCategoryNameEn(string arg) => !(string.IsNullOrWhiteSpace(arg) || arg.Length < 5 || arg.Length > 50);
            bool CheckCategoryNameAr(string arg) => !(string.IsNullOrWhiteSpace(arg) || arg.Length < 5 || arg.Length > 50);

            bool CheckDuplicateCategoyName(int id, string arg1, string arg2)
            {
                Category categoryObj = _categoryRepository.Get(x => x.Id != id && x.NameEn.Trim().ToLower() == arg1.Trim().ToLower() && x.NameEn.Trim().ToLower() == arg2.Trim().ToLower()).FirstOrDefault();
                return categoryObj is null;
            }
        }
    }
}

