using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    //Autofac'in Interception özelliğini kullanır,

    //Attribute özelliklerini ayarla alt satırdan
    //Class ve metotlara uygula, aynı classlara birden fazla kere kullanılabilsin,
    //inherit edilen noktada kullanılabilsin
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        //hangi attribute önce çalışsın priority
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
