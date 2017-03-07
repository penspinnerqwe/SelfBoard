using System;
using System.Web.Mvc;
using System.Web.Routing;
using SelfBoard.Domain.Abstract;
using SelfBoard.Domain.Concrete;
using Ninject;

namespace SelfBoard.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<ISelfBoardRepository>().To<EFSelfBoardRepository>();
        }
    }
}