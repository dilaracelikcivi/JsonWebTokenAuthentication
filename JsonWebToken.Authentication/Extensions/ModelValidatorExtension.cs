using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace JsonWebToken.Authentication.Extensions
{
    public static class ModelValidatorExtension
    {
        /// <summary>
        /// This method provides to validate model for class
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool IsValid(this object model)
        {
            var validationContext = new ValidationContext(model);
            return Validator.TryValidateObject(model, validationContext, null);
        }
        
        /// <summary>
        /// This method provides to get validation result by model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ValidationResult(this object model)
        {
            var validationResult = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            Validator.TryValidateObject(model, validationContext, validationResult);
            
            if (model is IValidatableObject) 
                (model as IValidatableObject).Validate(validationContext);    
                
            return JsonConvert.SerializeObject(validationResult.Select(a => a.ErrorMessage));    
        }
    }
}