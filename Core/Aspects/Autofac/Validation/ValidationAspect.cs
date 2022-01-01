using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            // Validator gönderilmiş mi kontrol et
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //Validator ün bir Instance ını oluştur
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            ////validatörünün base sınıfına git onun generic argümanını bul <Burası yani>
            //örnek <Product>
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //tipini bulduğun entitinin parametrelerini bul

            //invocation metot demek
            //Mesela Managerde Metotlara bak içine parametre olarak aldığı şey Validatör ün tipi ise
            //işlem yap
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
