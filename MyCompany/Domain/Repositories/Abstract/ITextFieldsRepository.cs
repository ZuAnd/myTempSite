using System;
using System.Linq;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    public interface ITextFieldsRepository
    {
        IQueryable<TextField> GetTextFields();
        TextField GetTextFieldById(Guid id);
        TextField GetTextFieldByCodeWord(string codeWord);
        TextField GetTextFieldByMetaDescription(string MetaDescription);
        TextField GetTextFieldByMetaKeywords(string MetaKeywords);
        TextField GetTextFieldByMetaTitle(string MetaTitle);  
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
